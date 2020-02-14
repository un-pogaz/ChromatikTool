using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    partial class SupplementarySpecial_purpose
    {
        /// <summary></summary>
        public struct SSP
        {
            /// <summary></summary>
            static public CodeBlock Tags
            {
                get { return CodeBlock.LoadFromXml(XmlSSP, XmlLang, "Tags"); }
            }

            /// <summary></summary>
            static public CodeBlock VariationSelectorsSupplement
            {
                get { return CodeBlock.LoadFromXml(XmlSSP, XmlLang, "Variation Selectors Supplement"); }
            }
        }
    }
}
