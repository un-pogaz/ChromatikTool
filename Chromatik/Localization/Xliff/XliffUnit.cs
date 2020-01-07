using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffUnit
    {
        public ICollection<XliffData> Datas;
        public ICollection<XliffSegment> Segments;
        public ICollection<XliffIgnorable> Ignorables;

        public string ID;
        public string name;

        protected bool xmlSpacePreserve;
    }
}
