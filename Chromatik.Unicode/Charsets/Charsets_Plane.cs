using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Chromatik.Xml;

namespace Chromatik.Unicode
{
    /// <summary>
    /// Represents the ConScript Unicode Registry
    /// </summary>
    static public partial class Charset
    {
        static private XmlElement XmlCharset;

        static private string XmlLang;

        static private string NameID = "Charsets";

        static public CodePlane Charsets { get; private set; } = new CodePlane(NameID);

        static public string Name { get; } = Charsets.Name;

        static public string Description { get; } = Charsets.Description;

        /// <summary>
        /// Represents a Charsets collection 
        /// </summary>
        static Charset()
        {

        }

        static public string[] GetBlocksNames()
        {
            return Charsets.GetBlocksNames();
        }

        /// <summary>
        /// Get the Basic Multilingual Plane from a string Xml
        /// </summary>
        static public void LoadFromXml(string xml, string lang)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(xml);

            foreach (XmlElement item in XmlDoc.GetElements())
            {
                if (item.Name == "Charsets" || item.GetAttribute("nameID") == NameID)
                {
                    LoadFromXml(item, lang);
                    return;
                }
                else
                {
                    XmlElement[] lst = item.GetElements("Charsets");
                    if (lst.Length > 0)
                    {
                        LoadFromXml(lst[0] as XmlElement, lang);
                        return;
                    }
                }
            }

            throw InvalidXmlUnicodeException.DefautMessage;
        }

        /// <summary>
        /// Get the Basic Multilingual Plane from a XmlElement
        /// </summary>
        static public void LoadFromXml(XmlElement Element, string lang)
        {
            if (Element.Name != "Charsets")
                throw InvalidXmlUnicodeException.DefautMessage;

            XmlCharset = Element;
            XmlLang = lang;
            Charsets = new CodePlane(NameID);

            foreach (XmlElement item in XmlCharset.GetElements("Charset", "nameID"))
                Charsets.Add(CharsetFromXml(item, XmlLang));
        }
    }
}
