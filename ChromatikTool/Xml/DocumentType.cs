using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Xml
{
    /// <summary>
    /// Class to represent a DocumentType 
    /// </summary>
    public class DocumentType : IEquatable<DocumentType>
    {
        static char[] InvalideChar = WhiteCharacter.WhiteCharacters.Concat(ControlCharacter.ControlCharacters, ControlCharacterSupplement.ControlCharactersSupplements);

        /// <summary>
        /// The Name of the DocumentType  
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// The PublicId of the DocumentType  
        /// </summary>
        public string PublicId { get; }
        /// <summary>
        /// The SystemId of the DocumentType  
        /// </summary>
        public string SystemId { get; }
        /// <summary>
        /// The Subset of the DocumentType  
        /// </summary>
        public string Subset { get; }

        /// <summary>
        /// Initialise a <see cref="DocumentType"/> with the spécified name
        /// </summary>
        /// <param name="name"></param>
        public DocumentType(string name) : this(name, null, null)
        { }
        /// <summary>
        /// Initialise a <see cref="DocumentType"/> with the spécified name and ID's
        /// </summary>
        /// <param name="name"></param>
        /// <param name="publicId"></param>
        /// <param name="systemId"></param>
        public DocumentType(string name, string publicId, string systemId) : this(name, publicId, systemId, null)
        { }
        /// <summary>
        /// Initialise a <see cref="DocumentType"/> with the spécified name, ID's and subset
        /// </summary>
        /// <param name="name"></param>
        /// <param name="publicId"></param>
        /// <param name="systemId"></param>
        /// <param name="subset"></param>
        public DocumentType(string name, string publicId,  string systemId, string subset)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            name = name.Trim(InvalideChar);
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentException("The Name can't be empty or WhiteSpace.", nameof(name));

            foreach (char item in InvalideChar)
                if (name.Contains(item))
                    throw new ArgumentException("The Name contains a invalid character.", nameof(name));

            Name = name;

            if (systemId != null)
                systemId = systemId.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
            if (systemId.IsNullOrWhiteSpace())
                SystemId = null;
            else
                SystemId = systemId;

            if (publicId != null)
                publicId = publicId.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
            if (publicId.IsNullOrWhiteSpace())
                PublicId = null;
            else
                PublicId = publicId;

            if (PublicId != null && SystemId == null)
                SystemId = string.Empty;

            if (publicId != null || systemId != null)
            {
                if (subset != null)
                    subset = subset.Trim(InvalideChar).Replace(InvalideChar.ToStringArray(), "");
                if (subset.IsNullOrWhiteSpace())
                    subset = null;
                else
                    Subset = subset;
            }
        }
        /// <summary>
        /// Get the <see cref="DocumentType"/> in a file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        static public DocumentType GetFromFile(string path)
        {
            return GetFromText(IO.File.ReadAllText(path));
        }
        /// <summary>
        /// Get the <see cref="DocumentType"/> in a XML text
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        static public DocumentType GetFromText(string xml)
        {
            Text.RegularExpressions.RegexOptions RegexOptions = RegexHelper.RegexOptions | System.Text.RegularExpressions.RegexOptions.IgnoreCase;
            string match = xml.RegexGetMatch(@"<!\s*DOCTYPE[^>]*>", RegexOptions);
            if (string.IsNullOrWhiteSpace(match))
                return null;

            string name, publicId = null, systemId = null, subset = null;

            name = match.Regex(@"<!\s*DOCTYPE\s+([^\s>]*)[^>]*>", "$1", RegexOptions);

            if (match.RegexIsMatch(@"<!\s*DOCTYPE\s+[^\s]*\s+SYSTEM\s+""([^""]*)""[^>]*>", RegexOptions))
            {
                systemId = match.Regex(@"<!\s*DOCTYPE\s+[^\s]*\s+SYSTEM\s+""([^""]*)""[^>]*>", "$1", RegexOptions);
            }
            else if(match.RegexIsMatch(@"<!\s*DOCTYPE\s+[^\s]*\s+PUBLIC\s+""([^""]*)""[^>]*>", RegexOptions))
            {
                publicId = match.Regex(@"<!\s*DOCTYPE\s+[^\s]*\s+PUBLIC\s+""([^""]*)""[^>]*>", "$1", RegexOptions);

                if (match.RegexIsMatch(@"<!\s*DOCTYPE\s+[^\s]*\s+PUBLIC\s+""[^""]*""\s+""([^""]*)""[^>]*>", RegexOptions))
                    systemId = match.Regex(@"<!\s*DOCTYPE\s+[^\s]*\s+PUBLIC\s+""[^""]*""\s+""([^""]*)""[^>]*>", "$1", RegexOptions);
            }

            if (publicId != null || systemId != null)
            {
                if (match.RegexIsMatch(@"<!\s*DOCTYPE\s+[^\[]*\[([^\[]*)\][^>]*>", RegexOptions))
                    subset = match.Regex(@"<!\s*DOCTYPE\s+[^\[]*\[([^\[]*)\][^>]*>", "$1", RegexOptions);
            }

            return new DocumentType(name, publicId,  systemId, subset);
        }

        /// <summary></summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is DocumentType)
                return Equals((DocumentType)obj);
            else
                return false;
        }
        /// <summary></summary>
        public bool Equals(DocumentType obj)
        {
            if (Text.Equals(obj.Text, StringComparison.InvariantCultureIgnoreCase))
                return true;

            return false;
        }

        /// <summary>
        /// Text of the DOCTYPE.
        /// </summary>
        public string Text
        {
            get
            {
                string rslt = "<!DOCTYPE " + Name;

                if (SystemId != null)
                {
                    if (PublicId == null)
                        rslt += " SYSTEM \"" + SystemId + "\"";
                    else
                        rslt += " PUBLIC \"" + PublicId + "\" \"" + SystemId + "\"";

                    if (Subset != null)
                        rslt += " [" + Subset + "]";
                }

                return rslt + ">";
            }
        }

        /// <summary></summary>
        public override int GetHashCode() { return Text.GetHashCode() + 20; }
        /// <summary></summary>
        /// <returns></returns>
        public override string ToString()
        {
            string rslt = "DocumentType, Name=\"" + Name + "\"";

            if (SystemId != null)
            {
                if (PublicId == null)
                    rslt += ", SYSTEM =\"" + SystemId + "\"";
                else
                    rslt += ", PUBLIC=\"" + PublicId + "\"";

                if (Subset != null)
                    rslt += ", Value=\"" + Subset + "\"";
            }

            return rslt;
        }

        /// <summary></summary>
        static public DocumentType HTML5 { get; } = new DocumentType("html");
        /// <summary></summary>
        static public DocumentType XHTML1_1 { get; } = new DocumentType("html", "-//W3C//DTD XHTML 1.1//EN", "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd");

        /// <summary></summary>
        static public DocumentType NCX { get; } = new DocumentType("ncx", "-//NISO//DTD ncx 2005-1//EN", "http://www.daisy.org/z3986/2005/ncx-2005-1.dtd");

        /// <summary></summary>
        static public DocumentType MathML2 { get; } = new DocumentType("math", "-//W3C//DTD MathML 2.0//EN",  "http://www.w3.org/Math/DTD/mathml2/mathml2.dtd");
        /// <summary></summary>
        static public DocumentType MathML1 { get; } = new DocumentType("math", null, "http://www.w3.org/Math/DTD/mathml1/mathml.dtd");

        /// <summary></summary>
        static public DocumentType XHTML1strict { get; } = new DocumentType("html", "-//W3C//DTD XHTML 1.0 Strict//EN",  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd");
        /// <summary></summary>
        static public DocumentType XHTML1transitional { get; } = new DocumentType("html", "-//W3C//DTD XHTML 1.0 Transitional//EN",  "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd");
        /// <summary></summary>
        static public DocumentType XHTML1frameset { get; } = new DocumentType("html", "-//W3C//DTD XHTML 1.0 Frameset//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd");

        /// <summary></summary>
        static public DocumentType HTML4strict { get; } = new DocumentType("html", "-//W3C//DTD HTML 4.01//EN",  "http://www.w3.org/TR/html4/strict.dtd");
        /// <summary></summary>
        static public DocumentType HTML4transitional { get; } = new DocumentType("html", "-//W3C//DTD HTML 4.01 Transitional//EN",  "http://www.w3.org/TR/html4/loose.dtd");
        /// <summary></summary>
        static public DocumentType HTML4frameset { get; } = new DocumentType("html", "-//W3C//DTD HTML 4.01 Frameset//EN", "http://www.w3.org/TR/html4/frameset.dtd");
        
    }
}
