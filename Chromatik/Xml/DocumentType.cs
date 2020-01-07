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
        public string Uri { get; }
        public string Subset { get; }

        public bool IsSystem { get; }

        public DocumentType(string name, bool isSystem, string id, string uri) : this(name, isSystem, id, uri, null)
        { }
        public DocumentType(string name, bool isSystem, string id,  string uri, string subset)
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

            IsSystem = isSystem;

            if (uri != null)
                uri = uri.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
            if (string.IsNullOrWhiteSpace(uri))
                Uri = null;
            else
                Uri = uri;

            if (subset != null)
                subset = subset.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
            if (string.IsNullOrWhiteSpace(subset))
                subset = null;
            else
                Subset = subset;
        }
        
        static public DocumentType GetDocumentTypeOfFile(string path)
        {
            return GetDocumentTypeFromXML(IO.File.ReadAllText(path));
        }
        static public DocumentType GetDocumentTypeFromXML(string xml)
        {
            Text.RegularExpressions.RegexOptions RegexOptions = RegexHelper.RegexOptions | System.Text.RegularExpressions.RegexOptions.IgnoreCase;
            string match = xml.RegexGetMatch(@"<!\s*DOCTYPE[^>]*>", RegexOptions);
            if (string.IsNullOrWhiteSpace(match))
                return null;

            string name, sys = null, sub = null, id = null, uri = null;
            bool system;

            name = match.Regex("<!\\s*DOCTYPE\\s+([^\\s]*)\\s*[^>]*>", "$1", RegexOptions);

            if (match.RegexIsMatch("<!\\s*DOCTYPE\\s+[^\\s]*\\s+([^\\s>]*)[^>]*>", RegexOptions))
                sys = match.Regex("<!\\s*DOCTYPE\\s+[^\\s]*\\s+([^\\s>]*)[^>]*>", "$1", RegexOptions);

            if (match.RegexIsMatch("<!\\s*DOCTYPE\\s+[^\\s]*\\s+[^\\s>]*\\s+\"([^\"]*)\"[^>]*>", RegexOptions))
                id = match.Regex("<!\\s*DOCTYPE\\s+[^\\s]*\\s+[^\\s>]*\\s+\"([^\"]*)\"[^>]*>", "$1", RegexOptions);

            if (match.RegexIsMatch("<!\\s*DOCTYPE\\s+[^\\s]*\\s+[^\\s>]*\\s+\"[^\"]*\"\\s+\"([^\"]*)\"[^>]*>", RegexOptions))
                uri = match.Regex("<!\\s*DOCTYPE\\s+[^\\s]*\\s+[^\\s>]*\\s+\"[^\"]*\"\\s+\"([^\"]*)\"[^>]*>", "$1", RegexOptions);

            if (match.RegexIsMatch("<!\\s*DOCTYPE\\s+[^\\s]*\\s+[^>\\[]*\\[([^>\\[]*)\\][^>]*>", RegexOptions))
                sub = match.Regex("<!\\s*DOCTYPE\\s+[^\\s]*\\s+[^>\\[]*\\[([^>\\[]*)\\][^>]*>", "$1", RegexOptions);

            if (sys.Equals("SYSTEM", StringComparison.InvariantCultureIgnoreCase))
                system = true;
            else
                system = false;

            return new DocumentType(name, system, id,  uri, sub);
        }

        /// <summary></summary>
        /// <returns></returns>
        public override string ToString()
        {
            string rslt = "DocumentType, Name=\"" + Name + "\"";

            if (Id != null)
            {
                if (IsSystem)
                    rslt += ", SYSTEM =\"" + Id + "\"";
                else
                    rslt += ", PUBLIC=\"" + Id + "\"";
            }
            else
            {
                if (IsSystem)
                    rslt += ", SYSTEM";
            }

            if (Subset != null)
                rslt += ", Value=\"" + Subset + "\"";

            return rslt;
        }

        /// <summary>
        /// Text of the DOCTYPE.
        /// </summary>
        public string Text
        {
            get
            {
                string rslt = "<!DOCTYPE " + Name;

                if (Id != null)
                {
                    if (IsSystem)
                        rslt += " SYSTEM \"" + Id + "\"";
                    else
                        rslt += " PUBLIC \"" + Id + "\"";
                }
                else
                {
                    if (IsSystem)
                        rslt += " SYSTEM";
                }

                if (Uri != null)
                    rslt += " \"" + Uri + "\"";

                if (Subset != null)
                    rslt += " [" + Subset + "]";

                return rslt + ">";
            }
        }

        /// <summary></summary>
        static public DocumentType HTML5 { get; } = new DocumentType("html", false, null, null);
        /// <summary></summary>
        static public DocumentType XHTML1_1 { get; } = new DocumentType("html", false, "-//W3C//DTD XHTML 1.1//EN", "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd");

        /// <summary></summary>
        static public DocumentType NCX { get; } = new DocumentType("ncx", false, "-//NISO//DTD ncx 2005-1//EN", "http://www.daisy.org/z3986/2005/ncx-2005-1.dtd");

        /// <summary></summary>
        static public DocumentType MathML2 { get; } = new DocumentType("math", false, "-//W3C//DTD MathML 2.0//EN",  "http://www.w3.org/Math/DTD/mathml2/mathml2.dtd");
        /// <summary></summary>
        static public DocumentType MathML1 { get; } = new DocumentType("math", true, null, "http://www.w3.org/Math/DTD/mathml1/mathml.dtd");

        /// <summary></summary>
        static public DocumentType XHTML1strict { get; } = new DocumentType("html", false, "-//W3C//DTD XHTML 1.0 Strict//EN",  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd");
        /// <summary></summary>
        static public DocumentType XHTML1transitional { get; } = new DocumentType("html", false, "-//W3C//DTD XHTML 1.0 Transitional//EN",  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd");
        /// <summary></summary>
        static public DocumentType XHTML1frameset { get; } = new DocumentType("html", false, "-//W3C//DTD XHTML 1.0 Frameset//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd");

        /// <summary></summary>
        static public DocumentType HTML4strict { get; } = new DocumentType("html", false, "-//W3C//DTD HTML 4.01//EN",  "http://www.w3.org/TR/html4/strict.dtd");
        /// <summary></summary>
        static public DocumentType HTML4transitional { get; } = new DocumentType("html", false, "-//W3C//DTD HTML 4.01 Transitional//EN",  "http://www.w3.org/TR/html4/loose.dtd");
        /// <summary></summary>
        static public DocumentType HTML4frameset { get; } = new DocumentType("html", false, "-//W3C//DTD HTML 4.01 Frameset//EN", "http://www.w3.org/TR/html4/frameset.dtd");
        
    }
}
