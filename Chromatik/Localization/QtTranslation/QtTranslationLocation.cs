using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization.Localization
{
    /// <summary></summary>
    public class QtTranslationLocation
    {
        /// <summary></summary>
        public QtTranslationLocation(string filename, int line)
        {
            Filename = filename;
            Line = line;
        }

        public string Filename { get; set; }

        public int Line { get; set; }
    }
}
