using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class XliffNote
    {
        public string ID;
        public XliffAppliesToEnum appliesTo;
        public string category;
        public int priority;
    }

    public enum XliffAppliesToEnum
    {
        NotDef,
        Source,
        Target
    }
}
