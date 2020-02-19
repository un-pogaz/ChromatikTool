using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    partial class SupplementaryIdeographic
    {
        /// <summary>
        /// Supplementary Ideographic
        /// </summary>
        public struct SIP
        {
            /// <summary></summary>
            static public CodeBlock CJKUnifiedIdeographsExtension_B
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension B"); }
            }

            /// <summary></summary>
            static public CodeBlock CJKUnifiedIdeographsExtension_C
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension C"); }
            }

            /// <summary></summary>
            static public CodeBlock CJKUnifiedIdeographsExtension_D
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension D"); }
            }

            /// <summary></summary>
            static public CodeBlock CJKUnifiedIdeographsExtension_E
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension E"); }
            }

            /// <summary></summary>
            static public CodeBlock CJKUnifiedIdeographsExtension_F
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension F"); }
            }

            /// <summary></summary>
            static public CodeBlock CJKCompatibilitySupplement
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Compatibility Supplement"); }
            }
            
        }
    }
}
