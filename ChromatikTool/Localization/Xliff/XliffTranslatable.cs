using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffTranslatable : XliffIdentified
    {
        public XliffSource Source;
        public XliffTarget Target;
        public ICollection<XliffTarget> Targets;
    }
    public class XliffIgnorable : XliffTranslatable
    { }
    public class XliffSegment : XliffTranslatable
    {
        public XliffStateEnum state;
        public XliffSubState subState;
    }
}
