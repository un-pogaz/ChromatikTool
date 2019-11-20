using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Xml
{
    /// <summary>
    /// Load quickly a <see cref="XmlDocument"/>
    /// </summary>
    static public partial class XmlCreate
    {
        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a file
        /// </summary>
        static public XmlDocument Document(string path)
        {
            return DocumentXML(File.ReadAllText(path));
        }
        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a stream
        /// </summary>
        static public XmlDocument Document(Stream stream)
        {
            return Document(new StreamReader(stream));
        }
        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a TextReader
        /// </summary>
        static public XmlDocument Document(TextReader reader)
        {
            return DocumentXML(reader.ReadToEnd());
        }
        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a string
        /// </summary>
        static public XmlDocument DocumentXML(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.Regex("<!DOCTYPE[^>]*>", "", RegexHelper.DefaultRegexOptions|Text.RegularExpressions.RegexOptions.IgnoreCase));
            return doc;
        }
    }
}
