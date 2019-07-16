using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    static public partial class SupplementaryPrivateUseArea_A
    {
        /// <summary>
        /// XmlElement of the Plane
        /// </summary>
        static private XmlElement XmlSPUA_A;

        static private string XmlLang;

        static private string NameID = "Supplementary Private Use Area-A";

        static public CodePlane Blocks { get; private set; } = new CodePlane(NameID);

        static public string Name { get; } = Blocks.Name;

        static public string Description { get; } = Blocks.Description;

        /// <summary>
        /// Represents Supplementary Special-purpose Plane of the Unicode Standard
        /// </summary>
        static SupplementaryPrivateUseArea_A()
        {

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
                if (item.Name == "Plane" || item.GetAttribute("nameID") == NameID)
                {
                    LoadFromXml(item, lang);
                    return;
                }
                else
                {
                    if (item.Name == "Unicode")
                    {
                        LoadFromXml(item.LastElement("Plane", "nameID", NameID), lang);
                        return;
                    }
                    else
                    {
                        XmlElement[] lst = item.GetElements("Unicode");
                        if (lst.Length > 0)
                        {
                            LoadFromXml(lst[0].LastElement("Plane", "nameID", NameID), lang);
                            return;
                        }
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
            if (Element.Name == "Plane" && Element.GetAttribute("nameID") == NameID)
            {
                XmlSPUA_A = Element;
                XmlLang = lang;
                Blocks = CodePlane.LoadFromXml(XmlSPUA_A, XmlLang);
                return;
            }

            throw InvalidXmlUnicodeException.DefautMessage;
        }
    }
}
