using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;

namespace System.Globalization.Localization
{
    [Diagnostics.DebuggerDisplay("{DebbugString()}")]
    public class QtTranslationMessage
    {
        public List<QtTranslationLocation> Locations { get; } = new List<QtTranslationLocation>();
        public string Source { get; set; }
        public string Translation { get; set; }
        public string Comment { get; set; }

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

            foreach (XmlElement item in message.EnumerableElement("location"))
                Locations.Add(new QtTranslationLocation(item.GetAttribute("filename"), int.Parse(item.GetAttribute("line"))));

            XmlElement source = message.FirstElement("source");
            if (source == null)
                throw QtTranslationException.InvalideNoNodeFound("source");

            XmlElement translation = message.FirstElement("translation");
            if (translation == null)
                throw QtTranslationException.InvalideNoNodeFound("translation");

            Source = source.InnerText;
            Translation = translation.InnerText;

            XmlElement comment = message.FirstElement("comment");
            if (comment != null && !comment.InnerText.IsNullOrWhiteSpace())
                Comment = comment.InnerText;
        }


    }
}
