using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Globalization
{
    [Diagnostics.DebuggerDisplay("{DebbugString()}")]
    public class QtTranslationMessage
    {
        public string Location { get; set; }
        public int Line { get; set; }
        public string Source { get; set; }
        public string Translation { get; set; }

        protected string DebbugString()
        {
            string rslt = string.Empty;
            int maxChar = 100;
            if (Source.Length > maxChar)
            {
                rslt = "[" + Source.Truncate(maxChar) + "…" + "]";
            }
            else
            {
                rslt = "["+ Source+"]";
            }

            if (Translation.Length > maxChar)
            {
                rslt += " " + Translation.Truncate(maxChar) + "…";
            }
            else
            {
                rslt += " "+Translation;
            }
            return rslt;
        }
        
        public QtTranslationMessage(string location, int line, string source, string translation)
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

        internal QtTranslationMessage(XmlElement message)
        {
            XmlElement location = message.FirstElement("location");
            if (location == null)
                throw QtTranslationException.InvalideNoNodeFound("location");
            if (!location.HasAttribute("filename"))
                throw QtTranslationException.InvalideNoAttributeFound("filename", "location");
            if (!location.HasAttribute("line"))
                throw QtTranslationException.InvalideNoAttributeFound("line", "location");

            XmlElement source = message.FirstElement("source");
            if (source == null)
                throw QtTranslationException.InvalideNoNodeFound("source");

            XmlElement translation = message.FirstElement("translation");
            if (translation == null)
                throw QtTranslationException.InvalideNoNodeFound("translation");

            Location = location.GetAttribute("filename");
            Line = int.Parse(location.GetAttribute("line"));
            Source = source.InnerText;
            Translation = translation.InnerText;
        }
    }
}
