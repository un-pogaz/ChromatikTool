using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Globalization
{
    public class TranslationMessage
    {
        public string Location { get; set; }
        public string Source { get; set; }
        public string Translation { get; set; }
        
        public TranslationMessage(string location, int line, string source, string translation)
        {
            if (line < 0)
                line = -1;
            if (location.IsNullOrWhiteSpace())
                location = null;
            if (source == null)
                source = string.Empty;
            if (translation == null)
                translation = string.Empty;

        }
        public TranslationMessage(XmlElement message)
        {

        }
    }
}
