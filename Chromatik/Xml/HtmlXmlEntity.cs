using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Xml
{
    //https://fr.wikipedia.org/wiki/Liste_des_entités_caractère_de_XML_et_HTML#Entités_caractère_de_HTML
    //https://en.wikipedia.org/wiki/List_of_XML_and_HTML_character_entity_references#Character_entity_references_in_HTML

    /// <summary>
    /// Represents an HTML entity / XML Entity with the corresponding character.
    /// </summary>
    public class HtmlXmlEntity : IComparerEquatable<HtmlXmlEntity>
    {
        /// <summary>
        /// HTML name of the entity
        /// </summary>
        public string HTML { get; } = null;
        /// <summary>
        /// XML value of the entity
        /// </summary>
        public string XML { get; }

        /// <summary>
        /// integer XML value of the entity
        /// </summary>
        protected int _XMLvalue { get; }
        /// <summary>
        /// Character of the entity 
        /// </summary>
        public string Character { get; }

        /// <summary>
        /// If the HTML name of the entity is case sensitive
        /// </summary>
        public bool IsCaseSensitive { get; }

        /// <summary>
        /// Initializes an instance with the HTML and the XML format.
        /// </summary>
        /// <param name="html">HTML name of the entity</param>
        /// <param name="xml">XML value of the entity</param>
        /// <param name="caseSensitive">Defined if the HTML name of the entity is case sensitive</param>
        public HtmlXmlEntity(string html, int xml, bool caseSensitive)
        {
            IsCaseSensitive = caseSensitive;
            if (!html.IsNullOrWhiteSpace())
            {
                html = html.Trim(WhiteCharacter.WhiteCharacters.Concat('&', ';'));
                if (html.RegexIsMatch(@"[A-Za-z&;]"))
                    HTML = "&" + html + ";";
                else
                    throw new ArgumentException("The HTML value of the entity contain a no ASCII character.", nameof(html));
            }
            else
                IsCaseSensitive = false;

            if (xml > 0)
            {
                _XMLvalue = xml;
                XML = "&#" + xml + ";";
            }
            else
                throw new ArgumentException("The XML value of the entity cannot be negative.", nameof(xml));

            Character = char.ConvertFromUtf32(xml);

        }

        /// <summary>
        /// Initializes an instance with only the XML format.
        /// </summary>
        /// <param name="xml"></param>
        public HtmlXmlEntity(int xml) : this(null, xml, true)
        { }


        static public string Parse(string html, params HtmlXmlEntity[] entitys)
        {
            return null;
        }

        static public HtmlXmlEntity[] HTMLbase { get; } = new HtmlXmlEntity[]
        {
            new HtmlXmlEntity("quot", 34, false),
            new HtmlXmlEntity("amp", 38, false),
            new HtmlXmlEntity("apos", 39, false),
            
            new HtmlXmlEntity("lt", 60, false),
            new HtmlXmlEntity("gt", 62, false),
        };

        /// <summary>
        /// HTML 2.2 entity
        /// </summary>
        static public HtmlXmlEntity[] Html2 { get; } = new HtmlXmlEntity[]
        {
            new HtmlXmlEntity("Agrave", 192, true),
            new HtmlXmlEntity("Aacute", 193, true),
            new HtmlXmlEntity("Acirc", 194, true),
            new HtmlXmlEntity("Atilde", 195, true),
            new HtmlXmlEntity("Auml", 196, true),
            new HtmlXmlEntity("Aring", 197, true),
            new HtmlXmlEntity("AElig", 198, true),
            new HtmlXmlEntity("Ccedil", 199, true),
            new HtmlXmlEntity("Egrave", 200, true),
            new HtmlXmlEntity("Eacute", 201, true),
            new HtmlXmlEntity("Ecirc", 202, true),
            new HtmlXmlEntity("Euml", 203, true),
            new HtmlXmlEntity("Igrave", 204, true),
            new HtmlXmlEntity("Iacute", 205, true),
            new HtmlXmlEntity("Icirc", 206, true),
            new HtmlXmlEntity("Iuml", 207, true),
            new HtmlXmlEntity("ETH", 208, true),
            new HtmlXmlEntity("Ntilde", 209, true),
            new HtmlXmlEntity("Ograve", 210, true),
            new HtmlXmlEntity("Oacute", 211, true),
            new HtmlXmlEntity("Ocirc", 212, true),
            new HtmlXmlEntity("Otilde", 213, true),
            new HtmlXmlEntity("Ouml", 214, true),

            new HtmlXmlEntity("Oslash", 216, true),
            new HtmlXmlEntity("Ugrave", 217, true),
            new HtmlXmlEntity("Uacute", 218, true),
            new HtmlXmlEntity("Ucirc", 219, true),
            new HtmlXmlEntity("Uuml", 220, true),
            new HtmlXmlEntity("Yacute", 221, true),
            new HtmlXmlEntity("THORN", 222, true),
            new HtmlXmlEntity("szlig", 223, true),
            new HtmlXmlEntity("agrave", 224, true),
            new HtmlXmlEntity("aacute", 225, true),
            new HtmlXmlEntity("acirc", 226, true),
            new HtmlXmlEntity("atilde", 227, true),
            new HtmlXmlEntity("auml", 228, true),
            new HtmlXmlEntity("aring", 229, true),
            new HtmlXmlEntity("aelig", 230, true),
            new HtmlXmlEntity("ccedil", 231, true),
            new HtmlXmlEntity("egrave", 232, true),
            new HtmlXmlEntity("eacute", 233, true),
            new HtmlXmlEntity("ecirc", 234, true),
            new HtmlXmlEntity("euml", 235, true),
            new HtmlXmlEntity("igrave", 236, true),
            new HtmlXmlEntity("iacute", 237, true),
            new HtmlXmlEntity("icirc", 238, true),
            new HtmlXmlEntity("iuml", 239, true),
            new HtmlXmlEntity("eth", 240, true),
            new HtmlXmlEntity("ntilde", 241, true),
            new HtmlXmlEntity("ograve", 242, true),
            new HtmlXmlEntity("oacute", 243, true),
            new HtmlXmlEntity("ocirc", 244, true),
            new HtmlXmlEntity("otilde", 245, true),
            new HtmlXmlEntity("ouml", 246, true),

            new HtmlXmlEntity("oslash", 248, true),
            new HtmlXmlEntity("ugrave", 249, true),
            new HtmlXmlEntity("uacute", 250, true),
            new HtmlXmlEntity("ucirc", 251, true),
            new HtmlXmlEntity("uuml", 252, true),
            new HtmlXmlEntity("yacute", 253, true),
            new HtmlXmlEntity("thorn", 254, true),
            new HtmlXmlEntity("yuml", 255, true),
        };

        /// <summary>
        /// HTML 3.2 entity
        /// </summary>
        static public HtmlXmlEntity[] Html3 { get; } = new HtmlXmlEntity[]
        {
            new HtmlXmlEntity("nbsp", 160, true),
            new HtmlXmlEntity("iexcl", 161  , true),
            new HtmlXmlEntity("cent", 162, true),
            new HtmlXmlEntity("pound", 163, true),
            new HtmlXmlEntity("curren", 164, true),
            new HtmlXmlEntity("yen", 165, true),
            new HtmlXmlEntity("brvbar", 166, true),
            new HtmlXmlEntity("sect", 167, true),
            new HtmlXmlEntity("uml", 168, true),
            new HtmlXmlEntity("copy", 169, true),
            new HtmlXmlEntity("ordf", 170, true),
            new HtmlXmlEntity("laquo", 171, true),
            new HtmlXmlEntity("not", 172, true),
            new HtmlXmlEntity("shy", 173, true),
            new HtmlXmlEntity("reg", 174, true),
            new HtmlXmlEntity("macr", 175, true),
            new HtmlXmlEntity("deg", 176, true),
            new HtmlXmlEntity("plusmn", 177, true),
            new HtmlXmlEntity("sup2", 178, true),
            new HtmlXmlEntity("sup3", 179, true),
            new HtmlXmlEntity("acute", 180, true),
            new HtmlXmlEntity("micro", 181, true),
            new HtmlXmlEntity("para", 182, true),
            new HtmlXmlEntity("middot", 183, true),
            new HtmlXmlEntity("cedil", 184, true),
            new HtmlXmlEntity("sup1", 185, true),
            new HtmlXmlEntity("ordm", 186, true),
            new HtmlXmlEntity("raquo", 187, true),
            new HtmlXmlEntity("frac14", 188, true),
            new HtmlXmlEntity("frac12", 189, true),
            new HtmlXmlEntity("frac34", 190, true),
            new HtmlXmlEntity("iquest", 191, true),

            new HtmlXmlEntity("times", 215, true),

            new HtmlXmlEntity("divide", 247, true),
        };


        /// <summary>
        /// HTML 4 entity
        /// </summary>
        static public HtmlXmlEntity[] Html4 { get; } = new HtmlXmlEntity[]
        {
            new HtmlXmlEntity("OElig", 338, true),
            new HtmlXmlEntity("oelig", 339, true),

            new HtmlXmlEntity("Scaron", 352, true),
            new HtmlXmlEntity("scaron", 353, true),

            new HtmlXmlEntity("Yuml", 376, true),

            new HtmlXmlEntity("fnof", 402, true),

            new HtmlXmlEntity("circ", 710, true),

            new HtmlXmlEntity("tilde", 732, true),

            new HtmlXmlEntity("fnof", 402, true),

            new HtmlXmlEntity("fnof", 402, true),

        };

        /// <summary>
        /// HTML 5 entity
        /// </summary>
        static public HtmlXmlEntity[] Html5 { get; } = new HtmlXmlEntity[]
        {

        };

        /// <summary></summary>
        public override string ToString()
        {
            string rslt = XML + "|" + Character;
            if (HTML != null)
                rslt = HTML + "|" + rslt;
            return rslt;
        }


        #region interface

        /// <summary></summary>
        new static public bool Equals(object x, object y)
        {
            if (x == null && y == null)
                return true;

            if (x != null && y == null || x == null && y != null)
                return false;

            if (x is HtmlXmlEntity && y is HtmlXmlEntity)
                return Equals((HtmlXmlEntity)x, (HtmlXmlEntity)y);

            return false;
        }
        /// <summary></summary>
        static public bool Equals(HtmlXmlEntity x, HtmlXmlEntity y)
        {
            if (x._XMLvalue == y._XMLvalue)
            {
                if (x.HTML != null && y.HTML != null)
                    return string.Equals(x.HTML, y.HTML, StringComparison.InvariantCultureIgnoreCase);
                else
                    return true;
            }
            else
                return false;
        }

        bool IEqualityComparer<HtmlXmlEntity>.Equals(HtmlXmlEntity x, HtmlXmlEntity y) { return Equals(x, y); }

        int IEqualityComparer<HtmlXmlEntity>.GetHashCode(HtmlXmlEntity obj) { return obj.GetHashCode(); }

        bool Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }

        /// <summary></summary>
        int Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }

        /// <summary></summary>
        public override bool Equals(object obj) { return Equals(this, obj); }
        /// <summary></summary>
        public bool Equals(HtmlXmlEntity obj) { return Equals(this, obj); }
        
        /// <summary></summary>
        static public int Compare(object x, object y)
        {
            if (x != null && y != null && x is HtmlXmlEntity && y is HtmlXmlEntity)
                return Compare((HtmlXmlEntity)x, (HtmlXmlEntity)y);

            return 0;
        }
        /// <summary></summary>
        static public int Compare(HtmlXmlEntity x, HtmlXmlEntity y)
        {
            if (x._XMLvalue.CompareTo(y._XMLvalue) == 0)
                if (x.HTML != null && y.HTML != null)
                    return string.Compare(x.HTML, y.HTML, StringComparison.InvariantCultureIgnoreCase);

            return 0;
        }

        /// <summary></summary>
        int IComparer<HtmlXmlEntity>.Compare(HtmlXmlEntity x, HtmlXmlEntity y)
        {
            return Compare(x, y);
        }
        int Collections.IComparer.Compare(object x, object y)
        {
            return Compare(x, y);
        }

        /// <summary></summary>
        public int CompareTo(HtmlXmlEntity obj)
        {
            return Compare(this, obj);
        }
        /// <summary></summary>
        public int CompareTo(object obj)
        {
            return Compare(this, obj);
        }
        #endregion
    }
}
