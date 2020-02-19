using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffUnit : XliffIdentified
    {
        public const string NodeName = "unit";

        static internal XliffUnit[] GetUnits(XmlElement element)
        {
            if (element == null)
                return new XliffUnit[0];
            else if (element.LocalName == NodeName && element.HasAttribute("id"))
                return new XliffUnit[] { new XliffUnit(element) };
            else
            {
                List<XliffUnit> rslt = new List<XliffUnit>();
                foreach (XmlElement item in element.EnumerableElement(NodeName, "id"))
                    rslt.Add(new XliffUnit(item));

                return rslt.ToArray();
            }
        }

        public XliffList<XliffData> Datas;
        public XliffList<XliffTranslatable> Translatables;
        public XliffList<XliffSegment> Segments;
        public XliffList<XliffIgnorable> Ignorables;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null)
                    value = string.Empty;
                _name = value.Trim(WhiteCharacter.WhiteCharacters);
            }
        }
        string _name;

        protected bool xmlSpacePreserve;

        protected XliffUnit(XmlElement element)
        {
            ID = element.TestID();

            Name = element.GetAttribute("name");
        }
    }
}
