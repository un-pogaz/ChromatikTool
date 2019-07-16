using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Chromatik.Unicode
{
    partial class SupplementaryPrivateUseArea_B
    {

        public struct SPUA_B
        {
            static public CodeBlock SupplementaryPrivateUseArea_B
            {
                get { return CodeBlock.LoadFromXml(XmlSPUA_B, XmlLang, "Supplementary Private Use Area-B"); }
            }
        }
    }
}
