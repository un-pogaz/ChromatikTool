using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Chromatik.Unicode
{
    public enum LanguageType
    {
        Udefined,
        Systeme,
        Alphabet,
        AlphabetSupplement,
        NotUsed
    }

    public class CodeBlock
    {
        /// <summary>
        /// Name of Block
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Create a Block with a name
        /// </summary>
        public CodeBlock(string Name)
        {
            this.Name = Name;
        }

        /// <summary>
        /// Liste of Code Point characters
        /// </summary>
        public string[] CodePoints
        {
            set {
                if (value == null)
                    value = new string[0];

                 _CodePoints = CodePoint.RangeFilter(value);

                List<string> lst = new List<string>();
                foreach (string item in _CodePoints)
                    if (!string.IsNullOrWhiteSpace(item))
                        lst.Add(CodePoint.CharFromHex(item));

                Characters = lst.ToArray();
            }
            get { return _CodePoints; }
        }
        private string[] _CodePoints;

        public string this[int index]
        {
            get { return _CodePoints[index]; }
            set { _CodePoints[index] = value; }
        }
        public IEnumerator<string> GetEnumerator()
        {
            foreach (string item in CodePoints)
                yield return item;
        }

        /// <summary>
        /// Charaters of the Block
        /// </summary>
        public string[] Characters { get; private set; }

        /// <summary>
        /// CodeRange of the Block
        /// </summary>
        public string CodeRange
        {
            get { return CodePoint.StringFromRange(_CodePoints); }
            set { CodePoints = CodePoint.RangeFromString(value); }
        }

        /// <summary>
        /// Description for the block
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// First CodePoint of the block
        /// </summary>
        public string CodeStart { get; set; }
        
        /// <summary>
        /// Ending CodePoint of the block
        /// </summary>
        public string CodeEnd { get; set; }

        /// <summary>
        /// Language used
        /// </summary>
        public string Languages { get; set; }

        /// <summary>
        /// Countries used
        /// </summary>
        public string Countries { get; set; }

        /// <summary>
        /// Countries used
        /// </summary>
        public string WebCharTable { get; set; }

        public LanguageType Type { get; set; }

        public bool RigthToLeft { get; set; }

        /// <summary>
        /// Get the code Block from a XmlElement
        /// </summary>
        static public CodeBlock LoadFromXml(XmlElement Element, string lang)
        {
            if (Element.Name != "Block" || string.IsNullOrWhiteSpace(Element.GetAttribute("nameID")))
                throw InvalidXmlBlockException.DefautMessage;

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
                result.WebCharTable = item.GetAttribute("url");

            XmlElement CodeRangeXml = Element.LastElement("CodeRange");

            if (CodeRangeXml == null)
                throw InvalidXmlCharsetException.DefautMessage;

            if (string.IsNullOrWhiteSpace(CodeRangeXml.GetAttribute("start")) || string.IsNullOrWhiteSpace(CodeRangeXml.GetAttribute("end")))
                throw InvalidXmlBlockException.DefautMessage;

            result.CodeStart = CodeRangeXml.GetAttribute("start");
            result.CodeEnd = CodeRangeXml.GetAttribute("end");

            string codeRangeTxt = "";
            foreach (XmlElement Range in CodeRangeXml.GetElements("Range"))
                codeRangeTxt += "," + Range.InnerText;

            codeRangeTxt = CodePoint.CleanCoderange(codeRangeTxt);

            if (string.IsNullOrWhiteSpace(codeRangeTxt))
                throw InvalidXmlBlockException.DefautMessage;

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

        static internal CodeBlock LoadFromXml(XmlElement PlaneElement, string lang, string nameID)
        {
            if (PlaneElement != null)
            {
                XmlElement block = PlaneElement.LastElement("Block", "nameID", nameID);
                if (block != null)
                    return LoadFromXml(block, lang);

                throw XmlUnicodeException.NotFound;
            }
            else
                throw XmlUnicodeException.NoLoaded;
        }
    }
}
