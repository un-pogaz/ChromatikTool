using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

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
            LoadFromXml(typeof(Unicode).GetManifestResourceString("Chromatik", "Ressources", "unicode.unicode.xml"), lang);
            Charset.LoadFromXml(typeof(Unicode).GetManifestResourceString("Chromatik", "Ressources", "unicode.charsets.xml"), lang);
            ConScript.LoadFromXml(typeof(Unicode).GetManifestResourceString("Chromatik", "Ressources", "unicode.conscript.xml"), lang);
        }
    }
}