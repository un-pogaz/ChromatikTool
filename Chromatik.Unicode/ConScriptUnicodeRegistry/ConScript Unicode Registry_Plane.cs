using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    /// <summary>
    /// Represents the ConScript Unicode Registry
    /// </summary>
    static public partial class ConScript
    {
        static private XmlElement XmlCSUR;

        static private string XmlLang;

        static private string NameID = "ConScript Unicode Registry";

        static public CodePlane Blocks { get; private set; } = new CodePlane(NameID);

        static public string Name { get; } = Blocks.Name;

        static public string Description { get; } = Blocks.Description;

        /// <summary>
        /// Represents ConScript Unicode Registry
        /// </summary>
        static ConScript()
        {

        }

        static public string[] GetBlocksNames()
        {
            return Blocks.GetBlocksNames();
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
                    if (item.Name == "ConScript")
                    {
                        LoadFromXml(item.LastElement("Plane", "nameID", NameID), lang);
                        return;
                    }
                    else
                    {
                        XmlElement[] lst = item.GetElements("ConScript");
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
            if (Element.Name != "Plane" || Element.GetAttribute("nameID") != NameID)
                throw InvalidXmlUnicodeException.DefautMessage;

            XmlCSUR = Element;
            XmlLang = lang;
            Blocks = CodePlane.LoadFromXml(XmlCSUR, XmlLang);
        }
    }
}