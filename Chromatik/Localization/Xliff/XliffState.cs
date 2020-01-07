using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public enum XliffStateEnum
    {
        NotDef,
        Initial,
        Translated,
        Reviewed,
        Final,
    }

    public class XliffSubState
    {
        public string prefix;
        public string value;
    }
}
