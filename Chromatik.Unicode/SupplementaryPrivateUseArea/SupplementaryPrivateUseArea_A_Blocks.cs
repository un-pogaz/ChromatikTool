using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    partial class SupplementaryPrivateUseArea_A
    {

        public struct SPUA_A
        {
            static public CodeBlock SupplementaryPrivateUseArea_A
            {
                get { return CodeBlock.LoadFromXml(XmlSPUA_A, XmlLang, "Supplementary Private Use Area-A"); }
            }
        }
    }
}
