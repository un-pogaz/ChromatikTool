using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffFile : XliffIdentified
    {
        public const string NodeName = "file";
        
        static internal XliffFile[] GetFiles(XmlElement element)
        {
            if (element == null)
                return new XliffFile[0];
            else if (element.LocalName == NodeName && element.HasAttribute("id"))
                return new XliffFile[] { new XliffFile(element) };
            else
            {
                List<XliffFile> rslt = new List<XliffFile>();
                foreach (XmlElement item in element.EnumerableElement(NodeName, "id"))
                    rslt.Add(new XliffFile(item));

                return rslt.ToArray();
            }
        }

        public XliffSkeleton Skeleton;

        public XliffList<XliffNote> Notes { get; protected set; } = new XliffList<XliffNote>();
        public XliffIdentifiedListe<XliffGroup> Groups { get; protected set; } = new XliffIdentifiedListe<XliffGroup>();
        public XliffIdentifiedListe<XliffUnit> Units { get; protected set; } = new XliffIdentifiedListe<XliffUnit>();
        
        public string Original
        {
            get { return _original; }
            set
            {
                if (value == null)
                    value = string.Empty;
                _original = value.Trim(WhiteCharacter.WhiteCharacters);
            }
        }
        string _original;
        
        internal XliffFile(XmlElement element)
        {            
            ID = element.TestIdentified(NodeName);

            Original = element.GetAttribute("original");

            Notes = new XliffList<XliffNote>(XliffNote.GetNotes(element));

            Groups = new XliffIdentifiedListe<XliffGroup>(XliffGroup.GetGroups(element));

            Units = new XliffIdentifiedListe<XliffUnit>(XliffUnit.GetUnits(element));

            if (Groups.Count == 0 && Units.Count == 0)
                throw new XliffException("Invalid Xliff file.\nA minimum of one <" + XliffGroup.NodeName + "> or <" + XliffUnit.NodeName + "> with the attribute 'id' is required in a <"+NodeName+">.");
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
