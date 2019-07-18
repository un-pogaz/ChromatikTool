using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    /// <summary>
    /// Represent the Basic Multilingual plane
    /// </summary>
    static public partial class BasicMultilingual
    {
        /// <summary>
        /// XmlElement of the Plane
        /// </summary>
        static private XmlElement XmlBMP;

        static private string XmlLang;

        static private string NameID = "Basic Multilingual Plane";

        /// <summary>
        /// All <see cref="CodeBlock"/> of the Basic Multilingual plane
        /// </summary>
        static public CodePlane Blocks { get; private set; } = new CodePlane(NameID);

        /// <summary>
        /// Local name of the Basic Multilingual plane
        /// </summary>
        static public string Name { get; } = Blocks.Name;

        /// <summary>
        /// Local description of the Basic Multilingual plane
        /// </summary>
        static public string Description { get; } = Blocks.Description;

        /// <summary>
        /// Represents Basic Multilingual Plane of the Unicode Standard
        /// </summary>
        static BasicMultilingual()
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
                XmlBMP = Element;
                XmlLang = lang;
                Blocks = CodePlane.LoadFromXml(XmlBMP, XmlLang);
                return;
            }

            throw InvalidXmlUnicodeException.DefautMessage;
        }
    }
}
