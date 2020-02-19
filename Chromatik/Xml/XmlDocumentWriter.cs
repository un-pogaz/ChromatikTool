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
        static public XmlWriterSettings Settings
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
        /// Write the XML node
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="node"></param>
        static public void Document(string filePath, XmlNode node)
        {
            Document(filePath, node, Settings);
        }
        /// <summary>
        /// Write the XML node with the specified settings
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
                settings = Settings;

            node = node.CloneNode(true).RemoveDeclaration();

            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(filePath)));
            using (XmlWriter writer = XmlWriter.Create(filePath, settings))
            {
                writer.WriteStartDocument();
                node.WriteTo(writer);
                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Get the outer string of the XML node with
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        static public string String(XmlNode node)
        {
            return String(node, Settings);
        }
        /// <summary>
        /// Get the outer string of the XML node with the specified settings
        /// </summary>
        /// <param name="node"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        static public string String(XmlNode node, XmlWriterSettings settings)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (settings == null)
                settings = Settings;
            
            node = node.CloneNode(true).RemoveDeclaration();

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

        /// <summary>
        /// Get the outer string of the XML node with the DocType 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="doctype"></param>
        /// <returns></returns>
        static public string String(XmlNode node, DocumentType doctype)
        {
            return String(node, Settings, doctype);
        }
        /// <summary>
        /// Get the outer string of the XML node with the specified settings and DocType
        /// </summary>
        /// <param name="node"></param>
        /// <param name="settings"></param>
        /// <param name="doctype"></param>
        /// <returns></returns>
        static public string String(XmlNode node, XmlWriterSettings settings, DocumentType doctype)
        {
            string rslt = String(node, settings);

            if (doctype != null)
            {
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
            }

            return rslt;
        }

        /// <summary>
        /// Write the XML node with the specified DocType
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="node"></param>
        /// <param name="doctype"></param>
        static public void Document(string filePath, XmlNode node, DocumentType doctype)
        {
            Document(filePath, node, Settings, doctype);
        }
        /// <summary>
        /// Write the XML node with the specified settings and DocType
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="node"></param>
        /// <param name="settings"></param>
        /// <param name="doctype"></param>
        static public void Document(string filePath, XmlNode node, XmlWriterSettings settings, DocumentType doctype)
        {
            File.WriteAllText(filePath, String(node, settings, doctype), settings.Encoding);
        }
    }
}
