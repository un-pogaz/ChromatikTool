using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Xml
{
    public class DocumentType
    {
        public string Name { get; }
        public string Id { get; }
        public Uri Uri { get; }
        public string Subset { get; }

        public bool IsSystemId { get; }

        public DocumentType(string name, string id, bool isSystemId, string uri, string subset)
        {
            char[] InvalideChar = WhiteCharacter.WhiteCharacters.Concat(ControlCharacter.ControlCharacters, ControlCharacterSupplement.ControlCharactersSupplements);

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            name = name.Trim(InvalideChar);
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("The Name can't be empty or WhiteSpace.", nameof(name));

            foreach (char item in InvalideChar)
                if (name.Contains(item))
                    throw new ArgumentException("The Name contains a invalid character.", nameof(name));

            Name = name;

            if (id != null)
                id = id.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
            if (string.IsNullOrWhiteSpace(id))
                Id = null;
            else
                Id = id;

            IsSystemId = isSystemId;

            if (uri != null)
                uri = uri.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
            uri = uri.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
            if (string.IsNullOrWhiteSpace(subset))
            {
                Uri = null;
            }
            else
                Uri = new Uri(uri);

            if (subset != null)
                subset = subset.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
            subset = subset.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
            if (string.IsNullOrWhiteSpace(subset))
                subset = null;
            else
                Subset = subset;
        }
        
        static public DocumentType GetDocumentType(string path)
        {
            return GetDocumentTypeXML(IO.File.ReadAllText(path));
        }
        static public DocumentType GetDocumentTypeXML(string xml)
        {
            Text.RegularExpressions.RegexOptions RegexOptions = RegexHelper.RegexOptions | Text.RegularExpressions.RegexOptions.IgnoreCase;
            string match = xml.RegexGetMatch(@"<!\s*DOCTYPE[^>]*>", RegexOptions);
            if (string.IsNullOrWhiteSpace(match))
                return null;

            string name, system, sub, id, uri;
            bool sys;

            name = match.Regex("<!\\s*DOCTYPE\\s+([^\\s]*)\\s*[^>]*>", "$1", RegexOptions);

            system = match.Regex("<!\\s*DOCTYPE\\s+[^\\s]*\\s+(PUBLIC|SYSTEM)[^>]*>", "$1", RegexOptions);

            sub = match.Regex("<!\\s*DOCTYPE\\s+[^\\s]*\\s+(PUBLIC|SYSTEM)[^>\\[]*([^>\\[]*)[^>]*>", "$2", RegexOptions);

            if (system.Equals("SYSTEM", StringComparison.InvariantCultureIgnoreCase))
                sys = false;
            else
                sys = true;

            return null;
        }
        /// string Subset<summary></summary>
        /// <returns></returns>
        public override string ToString()
        {
            string rslt = "DocumentType, Name=\"" + Name + "\"";

            if (Id != null)
            {
                if (IsSystemId)
                    rslt += ", SYSTEM =\"" + Id + "\"";
                else
                    rslt += ", PUBLIC=\"" + Id + "\"";
            }
            else
            {
                if (IsSystemId)
                    rslt += ", SYSTEM";
            }

            if (Subset != null)
                rslt += ", Value=\"" + Subset + "\"";

            return rslt;
        }

        public string DocumentTypeText
        {
            get
            {
                string rslt = "<!DOCTYPE " + Name;

                if (Id != null)
                {
                    if (IsSystemId)
                        rslt += " SYSTEM \"" + Id + "\"";
                    else
                        rslt += " PUBLIC \"" + Id + "\"";
                }
                else
                {
                    if (IsSystemId)
                        rslt += " SYSTEM";
                }

                if (Uri != null)
                    rslt += " \"" + Uri.AbsoluteUri + "\"";

                if (Subset != null)
                    rslt += " [" + Subset + "]";

                return rslt + ">";
            }
        }

        public DocumentType HTML5 { get; } = new DocumentType("html", null, false, null, null);
        public DocumentType XHTML1_1 { get; } = new DocumentType("html", "-//W3C//DTD XHTML 1.1//EN", false, "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd", null);

        public DocumentType NCX { get; } = new DocumentType("ncx", "-//NISO//DTD ncx 2005-1//EN", false, "http://www.daisy.org/z3986/2005/ncx-2005-1.dtd", null);

        public DocumentType MathML2 { get; } = new DocumentType("math", "-//W3C//DTD MathML 2.0//EN", false, "http://www.w3.org/Math/DTD/mathml2/mathml2.dtd", null);
        public DocumentType MathML1 { get; } = new DocumentType("math", null, true, "http://www.w3.org/Math/DTD/mathml1/mathml.dtd", null);

        public DocumentType XHTML1strict { get; } = new DocumentType("html", "-//W3C//DTD XHTML 1.0 Strict//EN", false, "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", null);
        public DocumentType XHTML1transitional { get; } = new DocumentType("html", "-//W3C//DTD XHTML 1.0 Transitional//EN", false, "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd", null);
        public DocumentType XHTML1frameset { get; } = new DocumentType("html", "-//W3C//DTD XHTML 1.0 Frameset//EN", false, "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd", null);

        public DocumentType HTML4strict { get; } = new DocumentType("html", "-//W3C//DTD HTML 4.01//EN", false, "http://www.w3.org/TR/html4/strict.dtd", null);
        public DocumentType HTML4transitional { get; } = new DocumentType("html", "-//W3C//DTD HTML 4.01 Transitional//EN", false, "http://www.w3.org/TR/html4/loose.dtd", null);
        public DocumentType HTML4frameset { get; } = new DocumentType("html", "-//W3C//DTD HTML 4.01 Frameset//EN", false, "http://www.w3.org/TR/html4/frameset.dtd", null);
        
    }
}
