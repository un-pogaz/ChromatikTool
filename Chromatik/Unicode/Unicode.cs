using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Chromatik.Unicode
{
    /// <summary>
    /// Represente the Unicode standard
    /// </summary>
    static public partial class Unicode
    {
        static private XmlElement XmlUnicode;

        static private string XmlLang;

        /// <summary></summary>
        static public CodePlaneList Planes { get; private set; } = new CodePlaneList();

        /// <summary></summary>
        static public CodeBlockList Blocks { get; private set; } = new CodeBlockList();


        static Unicode()
        {

        }


        /// <summary></summary>
        static public string[] GetPlanesNames()
        {
            return Planes.Keys.ToArray();
        }

        /// <summary></summary>
        static public string[] GetBlocksNames()
        {
            return Blocks.Keys.ToArray();
        }

        /// <summary>
        /// Get the Unicode from a string Xml
        /// </summary>
        static public void LoadFromXml(string xml, string lang)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.LoadXml(xml);

            foreach (XmlElement item in XmlDoc.GetElements())
            {
                if (item.Name == "Unicode")
                {
                    LoadFromXml(item, lang);
                    return;
                }
                else
                {
                    XmlNodeList lst = item.GetElementsByTagName("Unicode");
                    if (lst.Count > 0)
                    {
                        LoadFromXml(lst[0] as XmlElement, lang);
                        return;
                    }
                }
            }

            throw InvalidXmlUnicodeException.DefautMessage;
        }

        /// <summary>
        /// Get the Unicode from a XmlElement
        /// </summary>
        static public void LoadFromXml(XmlElement Element, string lang)
        {
            if (Element.Name != "Unicode")
                throw InvalidXmlUnicodeException.DefautMessage;

            XmlUnicode = Element;
            XmlLang = lang;

            Planes = new CodePlaneList();
            Blocks = new CodeBlockList();

            BasicMultilingual.LoadFromXml(XmlUnicode.OuterXml, XmlLang);
            Planes.Add(BasicMultilingual.Blocks);
            SupplementaryMultilingual.LoadFromXml(XmlUnicode.OuterXml, XmlLang);
            Planes.Add(SupplementaryMultilingual.Blocks);
            SupplementaryIdeographic.LoadFromXml(XmlUnicode.OuterXml, XmlLang);
            Planes.Add(SupplementaryIdeographic.Blocks);

            TertiaryIdeographic.LoadFromXml(XmlUnicode.OuterXml, XmlLang);
            Planes.Add(TertiaryIdeographic.Blocks);
            SupplementarySpecial_purpose.LoadFromXml(XmlUnicode.OuterXml, XmlLang);
            Planes.Add(SupplementarySpecial_purpose.Blocks);

            SupplementaryPrivateUseArea_A.LoadFromXml(XmlUnicode.OuterXml, XmlLang);
            Planes.Add(SupplementaryPrivateUseArea_A.Blocks);
            SupplementaryPrivateUseArea_B.LoadFromXml(XmlUnicode.OuterXml, XmlLang);
            Planes.Add(SupplementaryPrivateUseArea_B.Blocks);


            foreach (CodePlane plane in Planes)
                foreach (CodeBlock block in plane)
                    Blocks.Add(block);

        }
    }
}
