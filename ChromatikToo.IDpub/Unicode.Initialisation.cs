using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Unicode
{
    static public partial class Unicode
    {
        static public void Load()
        {
            Load("en");
        }
        static public void Load(string lang)
        {
            LoadFromXml(ResourceChromatik.Unicode_XML, lang);
            Charset.LoadFromXml(ResourceChromatik.Charsets_XML, lang);
        }
    }
}
