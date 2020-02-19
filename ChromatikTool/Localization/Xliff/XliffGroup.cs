using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffGroup : XliffIdentified
    {
        public const string NodeName = "group";

        static internal XliffGroup[] GetGroups(XmlElement element)
        {
            if (element == null)
                return new XliffGroup[0];
            else if (element.LocalName == NodeName && element.HasAttribute("id"))
                return new XliffGroup[] { new XliffGroup(element) };
            else
            {
                List<XliffGroup> rslt = new List<XliffGroup>();
                foreach (XmlElement item in element.EnumerableElement(NodeName, "id"))
                    rslt.Add(new XliffGroup(item));

                return rslt.ToArray();
            }
        }

        public XliffIdentifiedListe<XliffUnit> Units { get; protected set; } = new XliffIdentifiedListe<XliffUnit>();
        public XliffIdentifiedListe<XliffGroup> Groups { get; protected set; } = new XliffIdentifiedListe<XliffGroup>();
        public XliffList<XliffNote> Notes { get; protected set; } = new XliffList<XliffNote>();

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

        protected XliffGroup(XmlElement element)
        {
            ID = element.TestIdentified(NodeName);

            Name = element.GetAttribute("name");

            Notes = new XliffList<XliffNote>(XliffNote.GetNotes(element));

            Groups = new XliffIdentifiedListe<XliffGroup>(GetGroups(element));

            Units = new XliffIdentifiedListe<XliffUnit>(XliffUnit.GetUnits(element));

            if (Groups.Count == 0 && Units.Count == 0)
                throw new XliffException("Invalid Xliff file.\nA minimum of one <" + XliffGroup.NodeName + "> or <" + XliffUnit.NodeName + "> with the attribute 'id' is required in a <" + NodeName + ">.");
        }

        public void AddUnit(XliffUnit unit)
        {
        }
        public void AddGroup(XliffGroup group)
        {
        }
        public void AddNote(XliffNote note)
        {
        }
    }
}
