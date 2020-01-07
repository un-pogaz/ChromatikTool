using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffFile
    {
        public XliffSkeleton Skeleton;

        public ICollection<XliffUnit> Units;
        public ICollection<XliffGroup> Groups;
        public ICollection<XliffNote> Notes;

        public string ID;
        public string original;

        protected bool xmlSpacePreserve;

        public XliffFile(string id)
        {
            ID = id;
        }
        public XliffFile(XmlElement element)
        {

        }
    }
}
