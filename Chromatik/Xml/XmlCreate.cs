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
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }
        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a string
        /// </summary>
        static public XmlDocument DocumentXML(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }
    }
}
