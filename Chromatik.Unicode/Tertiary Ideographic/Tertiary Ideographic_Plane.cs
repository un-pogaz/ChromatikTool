using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    /// <summary>
    /// Represent the Tertiary Ideographic plane
    /// </summary>
    static public partial class TertiaryIdeographic
    {
        /// <summary>
        /// XmlElement of the Plane
        /// </summary>
        static private XmlElement XmlTIP;

        static private string XmlLang;

        static private string NameID = "Tertiary Ideographic Plane";

        /// <summary></summary>
        static public CodePlane Blocks { get; private set; } = new CodePlane(NameID);

        /// <summary></summary>
        static public string Name { get; } = Blocks.Name;

        /// <summary></summary>
        static public string Description { get; } = Blocks.Description;

        /// <summary>
        /// Represents Tertiary Ideographic Plane of the Unicode Standard
        /// </summary>
        static TertiaryIdeographic()
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
                XmlTIP = Element;
                XmlLang = lang;
                Blocks = CodePlane.LoadFromXml(XmlTIP, XmlLang);
                return;
            }

            throw InvalidXmlUnicodeException.DefautMessage;
        }
    }
}
