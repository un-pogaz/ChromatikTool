using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffIgnorable
    {
        public XliffSource Source;
        public XliffTarget Target;
        public ICollection<XliffTarget> Targets;

        public string ID;
    }
}
