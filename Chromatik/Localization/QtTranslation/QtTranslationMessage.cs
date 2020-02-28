using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;

namespace System.Globalization.Localization
{
    public class QtTranslationMessage
    {
        /// <summary>
        /// List of locations use this message.
        /// </summary>
        public QtTranslationLocationList Locations { get; } = new QtTranslationLocationList();
        /// <summary></summary>
        public string Source
        {
            get { return _source; }
            set
            {
                if (value == null)
                    _source = string.Empty;
                else
                    _source = value;
            }
        }
        string _source;
        /// <summary></summary>
        public string Translation
        {
            get { return _translation; }
            set
            {
                if (value == null)
                    _translation = string.Empty;
                else
                    _translation = value;
            }
        }
        string _translation;
        /// <summary></summary>
        public string Comment { get; set; }
        

        public QtTranslationMessage(QtTranslationLocation location, string source, string translation)
            : this(new QtTranslationLocationList() { location }, source, translation)
        { }
        public QtTranslationMessage(QtTranslationLocationList locations, string source, string translation)
        {
            if (locations == null)
                locations = new QtTranslationLocationList();

            Locations = locations;
            Source = source;
            Translation = translation;
        }

        internal QtTranslationMessage(XmlElement message)
        {
            XmlElement location = message.FirstElement("location");
            if (location == null)
                throw QtTranslationException.InvalideNoNodeFound("location");
            if (!location.HasAttribute("filename"))
                throw QtTranslationException.InvalideNoNodeFound("location", "filename");
            if (!location.HasAttribute("line"))
                throw QtTranslationException.InvalideNoNodeFound("location", "line");

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

        /// <summary></summary>
        public override string ToString()
        {
            string rslt = string.Empty;
            int maxChar = 50;
            if (Source.Length > maxChar)
            {
                rslt = "[" + Source.Truncate(maxChar) + "…" + "]";
            }
            else
            {
                rslt = "[" + Source + "]";
            }

            if (Translation.Length > maxChar)
            {
                rslt += " " + Translation.Truncate(maxChar) + "…";
            }
            else
            {
                rslt += " " + Translation;
            }
            return rslt;
        }
    }
}
