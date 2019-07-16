using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    partial class SupplementarySpecial_purpose
    {

        public struct SSP
        {
            static public CodeBlock Tags
            {
                get { return CodeBlock.LoadFromXml(XmlSSP, XmlLang, "Tags"); }
            }

            static public CodeBlock VariationSelectorsSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlSSP, XmlLang, "Variation Selectors Supplement"); }
            }
        }
    }
}
