using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    partial class SupplementaryIdeographic
    {

        public struct SIP
        {
            static public CodeBlock CJKUnifiedIdeographsExtension_B
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension B"); }
            }

            static public CodeBlock CJKUnifiedIdeographsExtension_C
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension C"); }
            }

            static public CodeBlock CJKUnifiedIdeographsExtension_D
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension D"); }
            }

            static public CodeBlock CJKUnifiedIdeographsExtension_E
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension E"); }
            }

            static public CodeBlock CJKUnifiedIdeographsExtension_F
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Unified Ideographs Extension F"); }
            }

            static public CodeBlock CJKCompatibilitySupplement
            {
                get { return CodeBlock.LoadFromXml(XmlSIP, XmlLang, "CJK Compatibility Supplement"); }
            }
            
        }
    }
}
