using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace System.Xml
{
    /// <summary>
    /// Static class for write a XML file
    /// </summary>
    static public class XmlDocumentWriter
    {
        /// <summary>
        /// Initial setting for <see cref="XmlWriter"/>
        /// </summary>
        static public XmlWriterSettings DefaultSetting
        {
            get {
                return new XmlWriterSettings()
                {
                    Async = false,
                    OmitXmlDeclaration = false,
                    Encoding = UTF8SansBomEncoding.Default,
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = "\n",
                    NewLineOnAttributes = false,
                    CheckCharacters = true,
                    NamespaceHandling = NamespaceHandling.OmitDuplicates,
                    ConformanceLevel = ConformanceLevel.Auto,
                    NewLineHandling = NewLineHandling.Replace,
                    DoNotEscapeUriAttributes = false,
                    WriteEndDocumentOnClose = true,
                    CloseOutput = true,
                };
            }
        }
        /// <summary>
        /// Default setting for <see cref="XmlWriter"/> in <see cref="XmlDocumentWriter"/>
        /// </summary>
        static public XmlWriterSettings Setting { get; set; } = DefaultSetting;
        
        /// <summary>
        /// Write the XML element
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="node"></param>
        static public void Document(string filePath, XmlNode node)
        {
            Document(filePath, node, Setting);
        }
        /// <summary>
        /// Write the XML element with the specified settings
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="node"></param>
        /// <param name="settings"></param>
        static public void Document(string filePath, XmlNode node, XmlWriterSettings settings)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            if (node == null)
                throw new ArgumentNullException("node");

            if (settings == null)
                throw new ArgumentNullException("settings");

            node = node.Clone().RemoveDeclaration();

            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(filePath)));
            using (XmlWriter XMLwriter = XmlWriter.Create(filePath, settings))
            {
                XMLwriter.WriteStartDocument();
                if (node is XmlDocument)
                    ((XmlDocument)node).WriteTo(XMLwriter);
                else if (node is XmlElement)
                    ((XmlElement)node).WriteTo(XMLwriter);
                else
                    node.WriteTo(XMLwriter);
            }
        }

        static public string String(XmlNode node)
        {
            return String(node, Setting);
        }
        static public string String(XmlNode node, XmlWriterSettings settings)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (settings == null)
                throw new ArgumentNullException("settings");

            node = node.Clone().RemoveDeclaration();

            MemoryStream stream = new MemoryStream(node.OuterXml.Length);
            using (XmlWriter writer = XmlWriter.Create(stream, settings))
            {
                writer.WriteStartDocument();
                node.WriteTo(writer);
                writer.WriteEndDocument();
                writer.Flush();

                stream.Position = 0;
                return new StreamReader(stream, settings.Encoding).ReadToEnd();
            }
        }


        static public string String(XmlNode node, DocumentType doctype)
        {
            return String(node, Setting, doctype);
        }
        static public string String(XmlNode node, XmlWriterSettings settings, DocumentType doctype)
        {
            if (doctype == null)
                throw new ArgumentNullException("doctype");

            string rslt = String(node, settings);

            if (settings.OmitXmlDeclaration)
            {
                if (settings.Indent)
                    rslt = doctype.Text + settings.NewLineChars + rslt;
                else
                    rslt = doctype.Text + rslt;
            }
            else
            {
                int index = rslt.IndexOf("?>");
                if (settings.Indent)
                    rslt = rslt.Insert(index + 2, settings.NewLineChars + doctype.Text);
                else
                    rslt = rslt.Insert(index + 2, doctype.Text);
            }

            return rslt;
        }


        static public void Document(string filePath, XmlNode node, DocumentType doctype)
        {
            Document(filePath, node, Setting, doctype);
        }
        static public void Document(string filePath, XmlNode node, XmlWriterSettings settings, DocumentType doctype)
        {
            File.WriteAllText(filePath, String(node, settings, doctype), settings.Encoding);
        }
    }
}
