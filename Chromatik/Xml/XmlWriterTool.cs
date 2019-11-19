using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace System.Xml
{
    /// <summary>
    /// Static
    /// </summary>
    static public class XmlWriterTool
    {
        /// <summary>
        /// Default setting for XmlWriter
        /// </summary>
        static public XmlWriterSettings DefaultSetting { get; } = new XmlWriterSettings()
        {
            OmitXmlDeclaration = false,
            Encoding = UTF8SansBomEncoding.Default,
            Indent = true,
            IndentChars = "  ",
            NewLineChars = "\n",
            NewLineOnAttributes = false,
            CheckCharacters = true,
            NamespaceHandling = NamespaceHandling.OmitDuplicates,
            ConformanceLevel = ConformanceLevel.Auto,
            NewLineHandling = NewLineHandling.None,
            DoNotEscapeUriAttributes = false,
            WriteEndDocumentOnClose = true,
            CloseOutput = true
        };

        /// <summary>
        /// Write the XML document
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="document"></param>
        static public void Document(string filePath, XmlDocument document)
        {
            Document(filePath, document, DefaultSetting);
        }
        /// <summary>
        /// Write the XML document with the specified settings
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="document"></param>
        /// <param name="settings"></param>
        static public void Document(string filePath, XmlDocument document, XmlWriterSettings settings)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            if (document == null)
                throw new ArgumentNullException("document");

            if (settings == null)
                throw new ArgumentNullException("settings");

            document.RemoveDeclaration();

            writeXML(filePath, document, settings);
        }

        /// <summary>
        /// Write the XML element
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="element"></param>
        static public void Document(string filePath, XmlElement element)
        {
            Document(filePath, element, DefaultSetting);
        }
        /// <summary>
        /// Write the XML element with the specified settings
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="element"></param>
        /// <param name="settings"></param>
        static public void Document(string filePath, XmlElement element, XmlWriterSettings settings)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            if (element == null)
                throw new ArgumentNullException("element");

            if (settings == null)
                throw new ArgumentNullException("settings");

            writeXML(filePath, element, settings);
        }

        static private void writeXML(string filePath, XmlNode node, XmlWriterSettings settings)
        {
            if (filePath == null)
                throw new ArgumentNullException("filePath");

            if (node == null)
                throw new ArgumentNullException("node");

            if (settings == null)
                throw new ArgumentNullException("settings");

            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            XmlWriter XMLwriter = XmlWriter.Create(filePath, settings);

            XMLwriter.WriteStartDocument();

            if (node is XmlDocument)
                ((XmlDocument)node).WriteTo(XMLwriter);
            else if(node is XmlElement)
                ((XmlElement)node).WriteTo(XMLwriter);
            else
                node.WriteTo(XMLwriter);

            XMLwriter.Close();
            XMLwriter.Flush();
        }
    }
}
