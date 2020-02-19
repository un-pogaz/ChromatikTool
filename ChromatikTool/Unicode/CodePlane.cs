using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;

namespace Chromatik.Unicode
{
    /// <summary>
    /// Represents Plane
    /// </summary>
    public class CodePlane : CodeBlockList
    {
        /// <summary>
        /// Create a Plane with a name
        /// </summary>
        public CodePlane(string Name)
        {
            this.Name = Name;
        }

        /// <summary></summary>
        public string Name { get; set; }

        /// <summary></summary>
        public string Description { get; set; }

        /// <summary></summary>
        public CodeBlockList Blocks
        {
            get { return this; }
        }

        /// <summary></summary>
        public string[] GetBlocksNames()
        {
            return this.Keys.ToArray();
        }

        /// <summary>
        /// Get the code Plane from a XmlElement
        /// </summary>
        static public CodePlane LoadFromXml(XmlElement Element, string lang)
        {
            if (Element.Name != "Plane" || string.IsNullOrWhiteSpace(Element.GetAttribute("nameID")))
                throw InvalidXmlPlaneException.DefautMessage;

            string name = Element.GetAttribute("nameID");
            string newName = GetTextOf(Element, "Name", lang);
            if (newName != null)
                name = newName;
            
            CodePlane result = new CodePlane(name);

            result.Description = GetTextOf(Element, "Description", lang);

            foreach (XmlElement Range in Element.GetElements("Block"))
                result.Add(CodeBlock.LoadFromXml(Range, lang));

            return result;
        }

        static private string GetTextOf(XmlElement Element, string Name, string lang)
        {
            string result = null;

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
    }

}
