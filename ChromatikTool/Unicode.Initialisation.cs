using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Unicode
{
    static public partial class Unicode
    {
        /// <summary>
        /// Load the <see cref="Unicode"/> class
        /// </summary>
        static public void Load()
        {
            Load("en");
        }
        /// <summary>
        /// Load the <see cref="Unicode"/> class in specifique langue
        /// </summary>
        static public void Load(string lang)
        {
            LoadFromXml(ResourceChromatik.Unicode_XML, lang);
            Charset.LoadFromXml(ResourceChromatik.Charsets_XML, lang);
        }
    }
}
