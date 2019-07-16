using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace Chromatik.Xml
{
    static public class XmlWriterTool
    {
        /// <summary>
        /// Setting pour XmlWriter
        /// </summary>
        static public XmlWriterSettings WriterSetting { get; } = new XmlWriterSettings()
        {
            OmitXmlDeclaration = false,
            Encoding = new UTF8Encoding(false),
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

        static XmlWriter XMLwriter;

        /// <summary>
        /// Écrie le document XML a l'enplacement spécifier
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="Document"></param>
        static public void Document(string filePath, XmlDocument Document)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            XMLwriter = XmlWriter.Create(filePath, WriterSetting);
            
            foreach (XmlNode item in Document.ChildNodes)
                if (item is XmlDeclaration)
                {
                    Document.RemoveChild(item);
                    break;
                }
            
            XMLwriter.WriteStartDocument();

            Document.WriteTo(XMLwriter);

            XMLwriter.Close();
            XMLwriter.Flush();
        }

        /// <summary>
        /// Écrie le nœud dans un XML a l'enplacement spécifier
        /// </summary>
        /// <param name="Path">Enplacement du fichier</param>
        /// <param name="Node">Nœud a écrire</param>
        static public void Document(string filePath, XmlElement Element)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            XMLwriter = XmlWriter.Create(filePath, WriterSetting);

            XMLwriter.WriteStartDocument();

            Element.WriteTo(XMLwriter);

            XMLwriter.Close();
            XMLwriter.Flush();
        }

        /// <summary>
        /// Écrie le nœud dans un XML a l'enplacement spécifier
        /// </summary>
        /// <param name="filePath">Enplacement du fichier</param>
        /// <param name="Node">Nœud a écrire</param>
        /// <param name="Parent">Parent du document XML</param>
        static public void Document(string filePath, XmlElement Element, string NamespaceURI)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            XMLwriter = XmlWriter.Create(filePath, WriterSetting);           

            XMLwriter.WriteStartDocument();

            if (Element.LocalName != "XML")
                XMLwriter.WriteStartElement("XML", NamespaceURI);


            Element.WriteTo(XMLwriter);

            XMLwriter.Close();
            XMLwriter.Flush();
        }
    }
}
