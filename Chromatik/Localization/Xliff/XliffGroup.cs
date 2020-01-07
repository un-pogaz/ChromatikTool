using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffGroup
    {
        public ICollection<XliffUnit> Units;
        public ICollection<XliffGroup> Groups;
        public ICollection<XliffNote> Notes;

        public string ID;
        public string name;

        protected bool xmlSpacePreserve;
    }
}
