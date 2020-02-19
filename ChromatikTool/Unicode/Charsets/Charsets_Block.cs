using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    partial class Charset
    {
        /// <summary>
        /// Load a Charset collection from a XML file
        /// </summary>
        /// <param name="Element"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        static public CodeBlock CharsetFromXml(XmlElement Element, string lang)
        {
            if (Element.Name != "Charset" || string.IsNullOrWhiteSpace(Element.GetAttribute("nameID")))
                throw InvalidXmlCharsetException.DefautMessage;

            string name = Element.GetAttribute("nameID");
            string newName = GetTextOf(Element, "Name", lang);
            if (newName != null)
                name = newName;

            CodeBlock result = new CodeBlock(name);

            result.Description = GetTextOf(Element, "Description", lang);

            foreach (XmlElement item in Element.GetElements("Localisable"))
            {
                result.Languages = GetTextOf(item, "Languages", lang);
                result.Countries = GetTextOf(item, "Countries", lang);
            }

            foreach (XmlElement item in Element.GetElements("WebCharTable"))
                foreach (string attrib in item.GetAttribute("url").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    result.WebCharTable = item.InnerText;
            
            XmlElement CodeRangeXml = Element.LastElement("CodeRange");

            if (CodeRangeXml == null)
                throw InvalidXmlCharsetException.DefautMessage;

            string codeRangeTxt = "";
            foreach (XmlElement Range in CodeRangeXml.GetElements("Range"))
                codeRangeTxt += "," + Range.InnerText;

            codeRangeTxt = CodePoint.CleanCoderange(codeRangeTxt);

            if (string.IsNullOrWhiteSpace(codeRangeTxt))
                throw InvalidXmlCharsetException.DefautMessage;

            result.CodePoints = CodePoint.RangeFromString(codeRangeTxt);

            return result;
        }

        static private string GetTextOf(XmlElement Element, string Name, string lang)
        {
            string result = "";

            foreach (XmlElement item in Element.GetElements(Name))
                foreach (string attrib in item.GetAttribute("lang").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    if (attrib.Trim() == "en" && !string.IsNullOrWhiteSpace(item.InnerText))
                        result = item.InnerText;

            foreach (XmlElement item in Element.GetElements(Name))
                foreach (string attrib in item.GetAttribute("lang").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    if (attrib.Trim() == lang && !string.IsNullOrWhiteSpace(item.InnerText))
                        result = item.InnerText;

            return result;
        }

        static internal CodeBlock CharsetFromXml(XmlElement PlaneElement, string lang, string nameID)
        {
            if (PlaneElement != null)
            {
                XmlElement block = PlaneElement.LastElement("Charset", "nameID", nameID);
                if (block != null)
                    return CharsetFromXml(block, lang);

                throw XmlUnicodeException.NotFound;
            }
            else
                throw XmlUnicodeException.NoLoaded;
        }

        /// <summary></summary>
        public struct Sets
        {
            /// <summary></summary>
            static public CodeBlock ASCII
            {
                get { return  CharsetFromXml(XmlCharset, XmlLang, "ASCII"); }
            }

            /// <summary></summary>
            static public CodeBlock Latin_1
            {
                get { return CharsetFromXml(XmlCharset, XmlLang, "Latin-1"); }
            }

            /// <summary></summary>
            static public CodeBlock Latin_9
            {
                get { return CharsetFromXml(XmlCharset, XmlLang, "Latin-9"); }
            }

            /// <summary></summary>
            static public CodeBlock Windows_1252
            {
                get { return CharsetFromXml(XmlCharset, XmlLang, "Windows-1252"); }
            }
        }
    }
}


