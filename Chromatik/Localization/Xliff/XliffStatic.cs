using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization.Localization
{
    internal static class XliffStatic
    {
        static internal bool XmlSpacePreserve(this Xml.XmlElement node)
        {
            return (node.GetAttribute("xml:space") == "preserve");
        }
    }
}
