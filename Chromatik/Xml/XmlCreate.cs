using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;
using Chromatik.Xml;

namespace System.Xml
{
    static public partial class XmlCreate
    {
        static public XmlDocument Document(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }
        static public XmlDocument DocumentXML(string xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            return doc;
        }
    }
}
