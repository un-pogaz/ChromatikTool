using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffNote : XliffIdentified
    {
        public const string NodeName = "note";
        public const string ParentNodeName = "notes";

        static internal XliffNote[] GetNotes(XmlElement element)
        {
            if (element == null)
                return new XliffNote[0];
            else if (element.LocalName == NodeName)
                return new XliffNote[] { new XliffNote(element) };
            else if (element.LocalName == ParentNodeName)
            {
                List<XliffNote> rslt = new List<XliffNote>();
                foreach (XmlElement item in element.EnumerableElement(NodeName))
                    rslt.Add(new XliffNote(item));

                return rslt.ToArray();
            }
            else
                return GetNotes(element.FirstElement(ParentNodeName));
        }
        
        public XliffAppliesToEnum AppliesTo
        {
            get { return _appliesTo; }
            set {
                switch (value)
                {
                    case XliffAppliesToEnum.Source:
                        break;
                    case XliffAppliesToEnum.Target:
                        break;
                    default: //XliffAppliesToEnum.NotDef
                        value = XliffAppliesToEnum.NotDef;
                        break;
                }
                _appliesTo = value;
            }
        }
        XliffAppliesToEnum _appliesTo;

        public string Category {
            get { return _category; }
            set
            {
                if (value == null)
                    value = string.Empty;
                _category = value.Trim(WhiteCharacter.WhiteCharacters);
            }
        }
        string _category;

        public int Priority {
            set
            {
                if (value < 0)
                    value = 0;
                if (value > 10)
                    value = 10;

                _priority = value;
            }
            get { return _priority; }
        }
        int _priority = 0;

        public string Content
        {
            get { return _content; }
            set
            {
                if (value == null)
                    value = string.Empty;
                _content = value.Trim(WhiteCharacter.WhiteCharacters);
            }
        }
        string _content;

        protected XliffNote(XmlElement element)
        {
            element.TestName(NodeName);

            ID = element.TestID();

            string appli = element.GetAttribute("appliesTo").Trim();
            if (appli.Equals("source"))
                AppliesTo = XliffAppliesToEnum.Source;
            else if(appli.Equals("target"))
                AppliesTo = XliffAppliesToEnum.Target;
            else
                AppliesTo = XliffAppliesToEnum.NotDef;
            
            Category = element.GetAttribute("category").Trim();

            int.TryParse(element.GetAttribute("id"), out _priority);
            Priority = _priority;

            Content = element.InnerText;
        }
        protected XliffNote(string id, XliffAppliesToEnum appliesTo, string category, int priority)
        {
            ID = id;
            AppliesTo = appliesTo;
            Category = category;
            Priority = priority;
        }
    }

    public enum XliffAppliesToEnum
    {
        NotDef,
        Source,
        Target
    }
}
