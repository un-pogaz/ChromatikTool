using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace System.Xml
{
    /// <summary>
    /// Represents an HTML entity / XML Entity with the corresponding character.
    /// </summary>
    public class XmlHtmlEntity : IEquatable<XmlHtmlEntity>, IEqualityComparer<XmlHtmlEntity>, System.Collections.IEqualityComparer, IComparable<XmlHtmlEntity>, IComparable, IComparer<XmlHtmlEntity>, System.Collections.IComparer
    {
        static private System.Globalization.CultureInfo InvariantCulture = System.Globalization.CultureInfo.InvariantCulture;

        static XmlHtmlEntity _null { get; } = new XmlHtmlEntity(1);
        static public IEqualityComparer<XmlHtmlEntity> EqualityComparer { get; } = _null;
        static public IComparer<XmlHtmlEntity> Comparator { get; } = _null;

        static XmlHtmlEntity()
        {

        }

        /// <summary>
        /// Parse HTML and XML
        /// </summary>
        /// <param name="html"></param>
        /// <param name="entitys"></param>
        /// <returns></returns>
        static public string ParseToCHAR(string html, IEnumerable<XmlHtmlEntity> entitys)
        {
            foreach (var item in entitys)
                html = item.ParseToCHAR(html);

            return html;
        }
        /// <summary>
        /// Parse XML and CHAR
        /// </summary>
        /// <param name="html"></param>
        /// <param name="entitys"></param>
        /// <returns></returns>
        static public string ParseToHTML(string html, IEnumerable<XmlHtmlEntity> entitys)
        {
            foreach (var item in entitys)
                html = item.ParseToHTML(html);

            return html;
        }
        /// <summary>
        /// Parse HTML and CHAR
        /// </summary>
        /// <param name="html"></param>
        /// <param name="entitys"></param>
        /// <returns></returns>
        static public string ParseToXML(string html, IEnumerable<XmlHtmlEntity> entitys)
        {
            foreach (var item in entitys)
                html = item.ParseToXML(html);

            return html;
        }


        /// <summary>
        /// HTML name of the entity
        /// </summary>
        public string HTML { get; }
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
        /// Initializes an instance with only the XML format.
        /// </summary>
        /// <param name="xml"></param>
        public XmlHtmlEntity(int xml) : this(null, xml, true) { }
        /// <summary>
        /// Initializes an instance with the HTML and the XML format.
        /// </summary>
        /// <param name="html">HTML name of the entity</param>
        /// <param name="xml">XML value of the entity</param>
        public XmlHtmlEntity(string html, int xml) : this(html, xml, true) { }
        /// <summary>
        /// Initializes an instance with the HTML and the XML format.
        /// </summary>
        /// <param name="html">HTML name of the entity</param>
        /// <param name="xml">XML value of the entity</param>
        /// <param name="caseSensitive">Defined if the HTML name of the entity is case sensitive</param>
        public XmlHtmlEntity(string html, int xml, bool caseSensitive)
        {
            IsCaseSensitive = caseSensitive;
            if (!string.IsNullOrWhiteSpace(html))
            {
                html = html.Trim('&', ';');
                if (Regex.IsMatch(html, @"[A-Za-z&;]"))
                    HTML = "&" + html + ";";
                else
                    throw new ArgumentException("The HTML value of the entity contain a no ASCII letter.", nameof(html));
            }
            else
            {
                IsCaseSensitive = false;
                HTML = null;
            }

            if (xml >= 0)
            {
                _XMLvalue = xml;
                XML = "&#" + xml.ToString(InvariantCulture) + ";";
            }
            else
                throw new ArgumentException("The XML value of the entity cannot be negative.", nameof(xml));

            Character = char.ConvertFromUtf32(xml);
        }

        /// <summary>
        /// Parse HTML and CHAR
        /// </summary>
        public string ParseToXML(string input)
        {
            return ParseHTMLtoXML(ParseCHARtoXML(input));
        }
        /// <summary>
        /// Parse XML and CHAR
        /// </summary>
        public string ParseToHTML(string input)
        {
            return ParseCHARtoHTML(ParseXMLtoHTML(input));
        }
        /// <summary>
        /// Parse HTML and XML
        /// </summary>
        public string ParseToCHAR(string input)
        {
            return ParseHTMLtoCHAR(ParseXMLtoCHAR(input));
        }

        /// <summary></summary>
        public string ParseHTMLtoXML(string input)
        {
            if (HTML != null)
                return input.Replace(HTML, XML);
            else
                return input;
        }
        /// <summary></summary>
        public string ParseHTMLtoCHAR(string input)
        {
            if (HTML != null)
                return input.Replace(HTML, Character);
            else
                return input;
        }
        /// <summary></summary>
        public string ParseCHARtoHTML(string input)
        {
            if (HTML != null)
                return input.Replace(Character, HTML);
            else
                return input;
        }
        /// <summary></summary>
        public string ParseCHARtoXML(string input)
        {
            return input.Replace(Character, XML);
        }
        /// <summary></summary>
        public string ParseXMLtoHTML(string input)
        {
            if (HTML != null)
                return input.Replace(XML, HTML);
            else
                return input;
        }
        /// <summary></summary>
        public string ParseXMLtoCHAR(string input)
        {
            return input.Replace(XML, Character);
        }

        /// <summary></summary>
        public override string ToString()
        {
            string rslt = XML + "|" + Character;
            if (HTML != null)
                rslt = HTML + "|" + rslt;
            return rslt;
        }

        /// <summary></summary>
        public override int GetHashCode()  { return _XMLvalue; }
        
        static public XmlHtmlEntity[] HtmlBase { get; } = new XmlHtmlEntity[]
        {
            new XmlHtmlEntity("quot", 34, false),
            new XmlHtmlEntity("amp", 38, false),
            new XmlHtmlEntity("apos", 39, false),
            
            new XmlHtmlEntity("lt", 60, false),
            new XmlHtmlEntity("gt", 62, false),
        };

        /// <summary>
        /// HTML 2 entity
        /// </summary>
        static public XmlHtmlEntity[] Html2 { get; } = new XmlHtmlEntity[]
        {
            new XmlHtmlEntity("Agrave", 192),
            new XmlHtmlEntity("Aacute", 193),
            new XmlHtmlEntity("Acirc", 194),
            new XmlHtmlEntity("Atilde", 195),
            new XmlHtmlEntity("Auml", 196),
            new XmlHtmlEntity("Aring", 197),
            new XmlHtmlEntity("AElig", 198),
            new XmlHtmlEntity("Ccedil", 199),
            new XmlHtmlEntity("Egrave", 200),
            new XmlHtmlEntity("Eacute", 201),
            new XmlHtmlEntity("Ecirc", 202),
            new XmlHtmlEntity("Euml", 203),
            new XmlHtmlEntity("Igrave", 204),
            new XmlHtmlEntity("Iacute", 205),
            new XmlHtmlEntity("Icirc", 206),
            new XmlHtmlEntity("Iuml", 207),
            new XmlHtmlEntity("ETH", 208),
            new XmlHtmlEntity("Ntilde", 209),
            new XmlHtmlEntity("Ograve", 210),
            new XmlHtmlEntity("Oacute", 211),
            new XmlHtmlEntity("Ocirc", 212),
            new XmlHtmlEntity("Otilde", 213),
            new XmlHtmlEntity("Ouml", 214),

            new XmlHtmlEntity("Oslash", 216),
            new XmlHtmlEntity("Ugrave", 217),
            new XmlHtmlEntity("Uacute", 218),
            new XmlHtmlEntity("Ucirc", 219),
            new XmlHtmlEntity("Uuml", 220),
            new XmlHtmlEntity("Yacute", 221),
            new XmlHtmlEntity("THORN", 222),
            new XmlHtmlEntity("szlig", 223),
            new XmlHtmlEntity("agrave", 224),
            new XmlHtmlEntity("aacute", 225),
            new XmlHtmlEntity("acirc", 226),
            new XmlHtmlEntity("atilde", 227),
            new XmlHtmlEntity("auml", 228),
            new XmlHtmlEntity("aring", 229),
            new XmlHtmlEntity("aelig", 230),
            new XmlHtmlEntity("ccedil", 231),
            new XmlHtmlEntity("egrave", 232),
            new XmlHtmlEntity("eacute", 233),
            new XmlHtmlEntity("ecirc", 234),
            new XmlHtmlEntity("euml", 235),
            new XmlHtmlEntity("igrave", 236),
            new XmlHtmlEntity("iacute", 237),
            new XmlHtmlEntity("icirc", 238),
            new XmlHtmlEntity("iuml", 239),
            new XmlHtmlEntity("eth", 240),
            new XmlHtmlEntity("ntilde", 241),
            new XmlHtmlEntity("ograve", 242),
            new XmlHtmlEntity("oacute", 243),
            new XmlHtmlEntity("ocirc", 244),
            new XmlHtmlEntity("otilde", 245),
            new XmlHtmlEntity("ouml", 246),

            new XmlHtmlEntity("oslash", 248),
            new XmlHtmlEntity("ugrave", 249),
            new XmlHtmlEntity("uacute", 250),
            new XmlHtmlEntity("ucirc", 251),
            new XmlHtmlEntity("uuml", 252),
            new XmlHtmlEntity("yacute", 253),
            new XmlHtmlEntity("thorn", 254),
            new XmlHtmlEntity("yuml", 255),
        };

        /// <summary>
        /// HTML 3 entity
        /// </summary>
        static public XmlHtmlEntity[] Html3 { get; } = new XmlHtmlEntity[]
        {
            new XmlHtmlEntity("nbsp", 160),
            new XmlHtmlEntity("iexcl", 161  ),
            new XmlHtmlEntity("cent", 162),
            new XmlHtmlEntity("pound", 163),
            new XmlHtmlEntity("curren", 164),
            new XmlHtmlEntity("yen", 165),
            new XmlHtmlEntity("brvbar", 166),
            new XmlHtmlEntity("sect", 167),
            new XmlHtmlEntity("uml", 168),
            new XmlHtmlEntity("copy", 169, false),
            new XmlHtmlEntity("ordf", 170),
            new XmlHtmlEntity("laquo", 171),
            new XmlHtmlEntity("not", 172),
            new XmlHtmlEntity("shy", 173),
            new XmlHtmlEntity("reg", 174, false),
            new XmlHtmlEntity("macr", 175),
            new XmlHtmlEntity("deg", 176),
            new XmlHtmlEntity("plusmn", 177),
            new XmlHtmlEntity("sup2", 178),
            new XmlHtmlEntity("sup3", 179),
            new XmlHtmlEntity("acute", 180),
            new XmlHtmlEntity("micro", 181),
            new XmlHtmlEntity("para", 182),
            new XmlHtmlEntity("middot", 183),
            new XmlHtmlEntity("cedil", 184),
            new XmlHtmlEntity("sup1", 185),
            new XmlHtmlEntity("ordm", 186),
            new XmlHtmlEntity("raquo", 187),
            new XmlHtmlEntity("frac14", 188),
            new XmlHtmlEntity("frac12", 189),
            new XmlHtmlEntity("frac34", 190),
            new XmlHtmlEntity("iquest", 191),

            new XmlHtmlEntity("times", 215),

            new XmlHtmlEntity("divide", 247),
        };


        /// <summary>
        /// HTML 4 entity
        /// </summary>
        static public XmlHtmlEntity[] Html4 { get; } = new XmlHtmlEntity[]
        {
            new XmlHtmlEntity("OElig", 338),
            new XmlHtmlEntity("oelig", 339),

            new XmlHtmlEntity("Scaron", 352),
            new XmlHtmlEntity("scaron", 353),

            new XmlHtmlEntity("Yuml", 376),

            new XmlHtmlEntity("fnof", 402),

            new XmlHtmlEntity("circ", 710),

            new XmlHtmlEntity("tilde", 732),
            
            new XmlHtmlEntity("Alpha", 913 ),
            new XmlHtmlEntity("Beta", 914 ),
            new XmlHtmlEntity("Gamma", 915 ),
            new XmlHtmlEntity("Delta", 916 ),
            new XmlHtmlEntity("Epsilon", 917 ),
            new XmlHtmlEntity("Zeta", 918 ),
            new XmlHtmlEntity("Eta", 919 ),
            new XmlHtmlEntity("Theta", 920 ),
            new XmlHtmlEntity("Iota", 921 ),
            new XmlHtmlEntity("Kappa", 922 ),
            new XmlHtmlEntity("Lambda", 923 ),
            new XmlHtmlEntity("Mu", 924 ),
            new XmlHtmlEntity("Nu", 925 ),
            new XmlHtmlEntity("Xi", 926 ),
            new XmlHtmlEntity("Omicron", 927 ),
            new XmlHtmlEntity("Pi", 928 ),
            new XmlHtmlEntity("Rho", 929 ),

            new XmlHtmlEntity("Sigma", 931 ),
            new XmlHtmlEntity("Tau", 932 ),
            new XmlHtmlEntity("Upsilon", 933 ),
            new XmlHtmlEntity("Phi", 934 ),
            new XmlHtmlEntity("Chi", 935 ),
            new XmlHtmlEntity("Psi", 936 ),
            new XmlHtmlEntity("Omega", 937 ),
            new XmlHtmlEntity("ohm", 937 ),
            
            new XmlHtmlEntity("alpha", 945 ),
            new XmlHtmlEntity("beta", 946 ),
            new XmlHtmlEntity("gamma", 947 ),
            new XmlHtmlEntity("delta", 948 ),
            new XmlHtmlEntity("epsi", 949 ),
            new XmlHtmlEntity("epsilon", 949 ),
            new XmlHtmlEntity("zeta", 950 ),
            new XmlHtmlEntity("eta", 951 ),
            new XmlHtmlEntity("theta", 952 ),
            new XmlHtmlEntity("iota", 953 ),
            new XmlHtmlEntity("kappa", 954 ),
            new XmlHtmlEntity("lambda", 955 ),
            new XmlHtmlEntity("mu", 956 ),
            new XmlHtmlEntity("nu", 957 ),
            new XmlHtmlEntity("xi", 958 ),
            new XmlHtmlEntity("omicron", 959 ),
            new XmlHtmlEntity("pi", 960 ),
            new XmlHtmlEntity("rho", 961 ),
            new XmlHtmlEntity("sigmav", 962 ),
            new XmlHtmlEntity("sigmaf", 962 ),
            new XmlHtmlEntity("sigma", 963 ),
            new XmlHtmlEntity("tau", 964 ),
            new XmlHtmlEntity("upsi", 965 ),
            new XmlHtmlEntity("phi", 966 ),
            new XmlHtmlEntity("chi", 967 ),
            new XmlHtmlEntity("psi", 968 ),
            new XmlHtmlEntity("omega", 969 ),

            new XmlHtmlEntity("thetav", 977 ),
            new XmlHtmlEntity("upsih", 978 ),

            new XmlHtmlEntity("phiv", 981 ),

            new XmlHtmlEntity("ensp", 8194),
            new XmlHtmlEntity("emsp", 8195),

            new XmlHtmlEntity("thinsp", 8201),

            new XmlHtmlEntity("zwnj", 8204),
            new XmlHtmlEntity("zwj", 8205),
            new XmlHtmlEntity("lrm", 8206),
            new XmlHtmlEntity("rlm", 8207),

            new XmlHtmlEntity("ndash", 8211),
            new XmlHtmlEntity("mdash", 8212),

            new XmlHtmlEntity("lsquo", 8216),
            new XmlHtmlEntity("rsquo", 8217),
            new XmlHtmlEntity("rsquor", 8217),
            new XmlHtmlEntity("sbquo", 8218),
            new XmlHtmlEntity("ldquo", 8220),
            new XmlHtmlEntity("rdquo", 8221 ),
            new XmlHtmlEntity("bdquo", 8222),

            new XmlHtmlEntity("dagger", 8224),
            new XmlHtmlEntity("ddagger", 8225),
            new XmlHtmlEntity("bull", 8226),

            new XmlHtmlEntity("hellip", 8230),

            new XmlHtmlEntity("permil", 8240),

            new XmlHtmlEntity("prime", 8242),
            new XmlHtmlEntity("Prime", 8243),

            new XmlHtmlEntity("lsaquo", 8249),
            new XmlHtmlEntity("rsaquo", 8250),

            new XmlHtmlEntity("oline", 8254),

            new XmlHtmlEntity("euro", 8364),

            new XmlHtmlEntity("image", 8465),

            new XmlHtmlEntity("weierp", 8472),

            new XmlHtmlEntity("real", 8476),

            new XmlHtmlEntity("trade", 8482, false),

            new XmlHtmlEntity("alefsym", 8501),

            new XmlHtmlEntity("rang", 10217),
            new XmlHtmlEntity("loz", 9674),
            new XmlHtmlEntity("spades", 9824),
            new XmlHtmlEntity("clubs", 9827),
            new XmlHtmlEntity("hearts", 9829),
            new XmlHtmlEntity("diams", 9830),
            new XmlHtmlEntity("lang", 10216),
            new XmlHtmlEntity("rang", 10217),
        };
        
        ////  Not relevant with Unicode
        /*

        /// <summary>
        /// HTML 5 entity
        /// </summary>
        static public XmlHtmlEntity[] Html5 { get; } = new XmlHtmlEntity[]
        {
            //HTML 3 retro
            new XmlHtmlEntity("NonBreakingSpace", 160),

            new XmlHtmlEntity("Dot", 168),
            new XmlHtmlEntity("die", 168),
            new XmlHtmlEntity("DoubleDot", 168),

            new XmlHtmlEntity("circledR", 174),
            new XmlHtmlEntity("strns", 175),

            new XmlHtmlEntity("pm", 177),
            new XmlHtmlEntity("PlusMinus", 177),

            new XmlHtmlEntity("DiacriticalAcute", 180),

            new XmlHtmlEntity("centerdot", 183),
            new XmlHtmlEntity("CenterDot", 183),
            new XmlHtmlEntity("Cedilla", 184),

            new XmlHtmlEntity("half", 189),
            
            //HTML 4 retro
            new XmlHtmlEntity("varsigma", 962 ),

            new XmlHtmlEntity("epsiv", 949 ),

            new XmlHtmlEntity("upsilon", 965 ),

            new XmlHtmlEntity("vartheta", 977 ),
            new XmlHtmlEntity("thetasym", 977 ),

            new XmlHtmlEntity("Upsi", 978 ),

            new XmlHtmlEntity("varpi", 982 ),

            new XmlHtmlEntity("ThinSpace", 8201 ),

            new XmlHtmlEntity("OpenCurlyQuote", 8216 ),
            new XmlHtmlEntity("rsquor", 8217 ),
            new XmlHtmlEntity("CloseCurlyQuote", 8218 ),
            new XmlHtmlEntity("lsquor", 8218 ),
            
            new XmlHtmlEntity("Im", 8465),
            new XmlHtmlEntity("imagpart", 8465),
            new XmlHtmlEntity("Ifr", 8465),

            new XmlHtmlEntity("wp", 8472),

            new XmlHtmlEntity("Re", 8476),
            new XmlHtmlEntity("realpart", 8476),
            new XmlHtmlEntity("Rfr", 8476),

            new XmlHtmlEntity("aleph", 8501),

            new XmlHtmlEntity("larr", 8592),
            new XmlHtmlEntity("uarr", 8593),
            new XmlHtmlEntity("rarr", 8594),
            new XmlHtmlEntity("darr", 8595),
            new XmlHtmlEntity("crarr", 8629),
            new XmlHtmlEntity("lArr", 8656),
            new XmlHtmlEntity("uArr", 8657),
            new XmlHtmlEntity("rArr", 8658),
            new XmlHtmlEntity("dArr", 8659),
            new XmlHtmlEntity("hArr", 8660),

            //HTML 5
            new XmlHtmlEntity("Gammad", 988 ),
            new XmlHtmlEntity("gammad", 989 ),
            new XmlHtmlEntity("digamma", 989 ),

            new XmlHtmlEntity("kappav", 1008),
            new XmlHtmlEntity("varkappa", 1008),
            new XmlHtmlEntity("rhov", 1009),
            new XmlHtmlEntity("varrho", 1009),

            new XmlHtmlEntity("epsiv", 1013),
            new XmlHtmlEntity("varepsilon", 1013),
            new XmlHtmlEntity("straightepsilon", 1013),
            new XmlHtmlEntity("bepsi", 1014),
            new XmlHtmlEntity("backepsilon", 1014),

            new XmlHtmlEntity("iiota", 8489),

            new XmlHtmlEntity("leftarrow", 8592),
            new XmlHtmlEntity("LeftArrow", 8592),
            new XmlHtmlEntity("slarr", 8592),
            new XmlHtmlEntity("ShortLeftArrow", 8592),

            new XmlHtmlEntity("uparrow", 8593),
            new XmlHtmlEntity("UpArrow", 8593),
            new XmlHtmlEntity("ShortUpArrow", 8593),

            new XmlHtmlEntity("rightarrow", 8594),
            new XmlHtmlEntity("RightArrow", 8594),
            new XmlHtmlEntity("srarr", 8594),
            new XmlHtmlEntity("ShortRightArrow", 8594),

            new XmlHtmlEntity("downarrow", 8595),
            new XmlHtmlEntity("DownArrow", 8595),
            new XmlHtmlEntity("ShortDownArrow", 8595),

            new XmlHtmlEntity("cularr", 8630),
            new XmlHtmlEntity("curvearrowleft", 8630),
            new XmlHtmlEntity("curarr", 8631),
            new XmlHtmlEntity("curvearrowright", 8631),

            new XmlHtmlEntity("Leftarrow", 8656),
            new XmlHtmlEntity("DoubleLeftArrow", 8656),
            new XmlHtmlEntity("Uparrow", 8657),
            new XmlHtmlEntity("DoubleUpArrow", 8657),
            new XmlHtmlEntity("Rightarrow", 8658),
            new XmlHtmlEntity("DoubleRightArrow", 8658),
            new XmlHtmlEntity("Downarrow", 8659),
            new XmlHtmlEntity("DoubleDownArrow", 8659),
            new XmlHtmlEntity("Leftrightarrow", 8660),

            new XmlHtmlEntity("lozenge", 9674),

            new XmlHtmlEntity("spadesuit", 9824),
            new XmlHtmlEntity("clubsuit", 9827),
            new XmlHtmlEntity("heartsuit", 9829),
            new XmlHtmlEntity("diamondsuit", 9830),



            new XmlHtmlEntity("harr", 8596),
            new XmlHtmlEntity("leftrightarrow", 8596),
            new XmlHtmlEntity("LeftRightArrow", 8596),
            new XmlHtmlEntity("varr", 8597),
            new XmlHtmlEntity("updownarrow", 8597),
            new XmlHtmlEntity("UpDownArrow", 8597),
            new XmlHtmlEntity("nwarr", 8598),
            new XmlHtmlEntity("UpperLeftArrow", 8598),
            new XmlHtmlEntity("nwarrow", 8598),
            new XmlHtmlEntity("nearr", 8599),
            new XmlHtmlEntity("UpperRightArrow", 8599),
            new XmlHtmlEntity("nearrow", 8599),
            new XmlHtmlEntity("searr", 8600),
            new XmlHtmlEntity("searrow", 8600),
            new XmlHtmlEntity("LowerRightArrow", 8600),
            new XmlHtmlEntity("swarr", 8601),
            new XmlHtmlEntity("swarrow", 8601),
            new XmlHtmlEntity("LowerLeftArrow", 8601),
            new XmlHtmlEntity("nlarr", 8602),
            new XmlHtmlEntity("nleftarrow", 8602),
            new XmlHtmlEntity("nrarr", 8603),
            new XmlHtmlEntity("nrightarrow", 8603),

            new XmlHtmlEntity("rarrw", 8605),
            new XmlHtmlEntity("rightsquigarrow", 8605),
            new XmlHtmlEntity("Larr", 8606),
            new XmlHtmlEntity("twoheadleftarrow", 8606),
            new XmlHtmlEntity("Uarr", 8607),
            new XmlHtmlEntity("Rarr", 8608),
            new XmlHtmlEntity("twoheadrightarrow", 8608),
            new XmlHtmlEntity("Darr", 8609),
            new XmlHtmlEntity("larrtl", 8610),
            new XmlHtmlEntity("leftarrowtail", 8610),
            new XmlHtmlEntity("rarrtl", 8611),
            new XmlHtmlEntity("rightarrowtail", 8611),
            new XmlHtmlEntity("mapstoleft", 8612),
            new XmlHtmlEntity("LeftTeeArrow", 8612),
            new XmlHtmlEntity("mapstoup", 8613),
            new XmlHtmlEntity("UpTeeArrow", 8613),
            new XmlHtmlEntity("map", 8614),
            new XmlHtmlEntity("RightTeeArrow", 8614),
            new XmlHtmlEntity("mapsto", 8614),
            new XmlHtmlEntity("DownTeeArrow", 8615),
            new XmlHtmlEntity("mapstodown", 8615),
            new XmlHtmlEntity("larrhk", 8617),
            new XmlHtmlEntity("hookleftarrow", 8617),
            new XmlHtmlEntity("rarrhk", 8618),
            new XmlHtmlEntity("hookrightarrow", 8618),
            new XmlHtmlEntity("larrlp", 8619),
            new XmlHtmlEntity("looparrowleft", 8619),
            new XmlHtmlEntity("rarrlp", 8620),
            new XmlHtmlEntity("looparrowright", 8620),
            new XmlHtmlEntity("harrw", 8621),
            new XmlHtmlEntity("leftrightsquigarrow", 8621),
            new XmlHtmlEntity("nharr", 8622),
            new XmlHtmlEntity("nleftrightarrow", 8622),
            new XmlHtmlEntity("lsh", 8624),
            new XmlHtmlEntity("Lsh", 8624),
            new XmlHtmlEntity("rsh", 8625),
            new XmlHtmlEntity("Rsh", 8625),
            new XmlHtmlEntity("ldsh", 8626),
            new XmlHtmlEntity("rdsh", 8627),

            new XmlHtmlEntity("olarr", 8634),
            new XmlHtmlEntity("circlearrowleft", 8634),
            new XmlHtmlEntity("orarr", 8635),
            new XmlHtmlEntity("circlearrowright", 8635),
            new XmlHtmlEntity("lharu", 8636),
            new XmlHtmlEntity("LeftVector", 8636),
            new XmlHtmlEntity("leftharpoonup", 8636),
            new XmlHtmlEntity("lhard", 8637),
            new XmlHtmlEntity("leftharpoondown", 8637),
            new XmlHtmlEntity("DownLeftVector", 8637),
            new XmlHtmlEntity("uharr", 8638),
            new XmlHtmlEntity("upharpoonright", 8638),
            new XmlHtmlEntity("RightUpVector", 8638),
            new XmlHtmlEntity("uharl", 8639),
            new XmlHtmlEntity("upharpoonleft", 8639),
            new XmlHtmlEntity("LeftUpVector", 8639),
            new XmlHtmlEntity("rharu", 8640),
            new XmlHtmlEntity("RightVector", 8640),
            new XmlHtmlEntity("rightharpoonup", 8640),
            new XmlHtmlEntity("rhard", 8641),
            new XmlHtmlEntity("rightharpoondown", 8641),
            new XmlHtmlEntity("DownRightVector", 8641),
            new XmlHtmlEntity("dharr", 8642),
            new XmlHtmlEntity("RightDownVector", 8642),
            new XmlHtmlEntity("downharpoonright", 8642),
            new XmlHtmlEntity("dharl", 8643),
            new XmlHtmlEntity("LeftDownVector", 8643),
            new XmlHtmlEntity("downharpoonleft", 8643),
            new XmlHtmlEntity("rlarr", 8644),
            new XmlHtmlEntity("rightleftarrows", 8644),
            new XmlHtmlEntity("RightArrowLeftArrow", 8644),
            new XmlHtmlEntity("udarr", 8645),
            new XmlHtmlEntity("UpArrowDownArrow", 8645),
            new XmlHtmlEntity("lrarr", 8646),
            new XmlHtmlEntity("leftrightarrows", 8646),
            new XmlHtmlEntity("LeftArrowRightArrow", 8646),
            new XmlHtmlEntity("llarr", 8647),
            new XmlHtmlEntity("leftleftarrows", 8647),
            new XmlHtmlEntity("uuarr", 8648),
            new XmlHtmlEntity("upuparrows", 8648),
            new XmlHtmlEntity("rrarr", 8649),
            new XmlHtmlEntity("rightrightarrows", 8649),
            new XmlHtmlEntity("ddarr", 8650),
            new XmlHtmlEntity("downdownarrows", 8650),
            new XmlHtmlEntity("lrhar", 8651),
            new XmlHtmlEntity("ReverseEquilibrium", 8651),
            new XmlHtmlEntity("leftrightharpoons", 8651),
            new XmlHtmlEntity("rlhar", 8652),
            new XmlHtmlEntity("rightleftharpoons", 8652),
            new XmlHtmlEntity("Equilibrium", 8652),
            new XmlHtmlEntity("nlArr", 8653),
            new XmlHtmlEntity("nLeftarrow", 8653),
            new XmlHtmlEntity("nhArr", 8654),
            new XmlHtmlEntity("nLeftrightarrow", 8654),
            new XmlHtmlEntity("nrArr", 8655),
            new XmlHtmlEntity("nRightarrow", 8655),

            new XmlHtmlEntity("DoubleLeftRightArrow", 8660),
            new XmlHtmlEntity("iff", 8660),
            new XmlHtmlEntity("vArr", 8661),
            new XmlHtmlEntity("Updownarrow", 8661),
            new XmlHtmlEntity("DoubleUpDownArrow", 8661),
            new XmlHtmlEntity("nwArr", 8662),
            new XmlHtmlEntity("neArr", 8663),
            new XmlHtmlEntity("seArr", 8664),
            new XmlHtmlEntity("swArr", 8665),
            new XmlHtmlEntity("lAarr", 8666),
            new XmlHtmlEntity("Lleftarrow", 8666),
            new XmlHtmlEntity("rAarr", 8667),
            new XmlHtmlEntity("Rrightarrow", 8667),
            new XmlHtmlEntity("zigrarr", 8669),

            new XmlHtmlEntity("larrb", 8676),
            new XmlHtmlEntity("LeftArrowBar", 8676),

            new XmlHtmlEntity("rarrb", 8677),
            new XmlHtmlEntity("RightArrowBar", 8677),

            new XmlHtmlEntity("duarr", 8693),
            new XmlHtmlEntity("DownArrowUpArrow", 8693),

            new XmlHtmlEntity("loarr", 8701),
            new XmlHtmlEntity("roarr", 8702),
            new XmlHtmlEntity("hoarr", 8703),

            new XmlHtmlEntity("langle", 10216),
            new XmlHtmlEntity("LeftAngleBracket", 10216),
            new XmlHtmlEntity("rangle", 10217),
            new XmlHtmlEntity("RightAngleBracket", 10217),

            new XmlHtmlEntity("xlarr", 10229),
            new XmlHtmlEntity("longleftarrow", 10229),
            new XmlHtmlEntity("LongLeftArrow", 10229),
            new XmlHtmlEntity("xrarr", 10230),
            new XmlHtmlEntity("longrightarrow", 10230),
            new XmlHtmlEntity("LongRightArrow", 10230),
            new XmlHtmlEntity("xharr", 10231),
            new XmlHtmlEntity("longleftrightarrow", 10231),
            new XmlHtmlEntity("LongLeftRightArrow", 10231),
            new XmlHtmlEntity("xlArr", 10232),
            new XmlHtmlEntity("Longleftarrow", 10232),
            new XmlHtmlEntity("DoubleLongLeftArrow", 10232),
            new XmlHtmlEntity("xrArr", 10233),
            new XmlHtmlEntity("Longrightarrow", 10233),
            new XmlHtmlEntity("DoubleLongRightArrow", 10233),
            new XmlHtmlEntity("xhArr", 10234),
            new XmlHtmlEntity("Longleftrightarrow", 10234),
            new XmlHtmlEntity("DoubleLongLeftRightArrow", 10234),

            new XmlHtmlEntity("xmap", 10236),
            new XmlHtmlEntity("longmapsto", 10236),

            new XmlHtmlEntity("dzigrarr", 10239),
            new XmlHtmlEntity("nvlArr", 10498),
            new XmlHtmlEntity("nvrArr", 10499),
            new XmlHtmlEntity("nvHarr", 10500),
            new XmlHtmlEntity("Map", 10501),

            new XmlHtmlEntity("lbarr", 10508),
            new XmlHtmlEntity("rbarr", 10509),
            new XmlHtmlEntity("bkarow", 10509),
            new XmlHtmlEntity("lBarr", 10510),
            new XmlHtmlEntity("rBarr", 10511),
            new XmlHtmlEntity("dbkarow", 10511),
            new XmlHtmlEntity("RBarr", 10512),
            new XmlHtmlEntity("drbkarow", 10512),
            new XmlHtmlEntity("DDotrahd", 10513),
            new XmlHtmlEntity("UpArrowBar", 10514),
            new XmlHtmlEntity("DownArrowBar", 10515),

            new XmlHtmlEntity("Rarrtl", 10518),

            new XmlHtmlEntity("latail", 10521),
            new XmlHtmlEntity("ratail", 10522),
            new XmlHtmlEntity("lAtail", 10523),
            new XmlHtmlEntity("rAtail", 10524),
            new XmlHtmlEntity("larrfs", 10525),
            new XmlHtmlEntity("rarrfs", 10526),
            new XmlHtmlEntity("larrbfs", 10527),
            new XmlHtmlEntity("rarrbfs", 10528),

            new XmlHtmlEntity("nwarhk", 10531),
            new XmlHtmlEntity("nearhk", 10532),
            new XmlHtmlEntity("hkswarow", 10533),
            new XmlHtmlEntity("hksearow", 10533),
            new XmlHtmlEntity("swarhk", 10534),
            new XmlHtmlEntity("hkswarow", 10534),

            new XmlHtmlEntity("nwnear", 10535),
            new XmlHtmlEntity("nesear", 10537),
            new XmlHtmlEntity("toea", 10537),
            new XmlHtmlEntity("seswar", 10537),
            new XmlHtmlEntity("tosa", 10537),
            new XmlHtmlEntity("swnwar", 10538),
            new XmlHtmlEntity("rarrc", 10547),
            new XmlHtmlEntity("cudarrr", 10549),
            new XmlHtmlEntity("ldca", 10550),
            new XmlHtmlEntity("rdca", 10551),
            new XmlHtmlEntity("cudarrl", 10552),
            new XmlHtmlEntity("larrpl", 10553),

            new XmlHtmlEntity("curarrm", 10556),
            new XmlHtmlEntity("cularrp", 10557),

            new XmlHtmlEntity("rarrpl", 10565),
            new XmlHtmlEntity("harrcir", 10568),
            new XmlHtmlEntity("Uarrocir", 10569),

            new XmlHtmlEntity("lurdshar", 10570),
            new XmlHtmlEntity("ldrushar", 10571),
            new XmlHtmlEntity("LeftRightVector", 10574),
            new XmlHtmlEntity("RightUpDownVector", 10575),
            new XmlHtmlEntity("DownLeftRightVector", 10576),
            new XmlHtmlEntity("LeftUpDownVector", 10577),
            new XmlHtmlEntity("LeftVectorBar", 10578),
            new XmlHtmlEntity("RightVectorBar", 10579),
            new XmlHtmlEntity("RightUpVectorBar", 10580),
            new XmlHtmlEntity("RightDownVectorBar", 10581),
            new XmlHtmlEntity("DownLeftVectorBar", 10582),
            new XmlHtmlEntity("DownRightVectorBar", 10583),
            new XmlHtmlEntity("LeftUpVectorBar", 10584),
            new XmlHtmlEntity("LeftDownVectorBar", 10585),
            new XmlHtmlEntity("LeftTeeVector", 10586),
            new XmlHtmlEntity("RightTeeVector", 10587),
            new XmlHtmlEntity("RightUpTeeVector", 10588),
            new XmlHtmlEntity("RightDownTeeVector", 10589),
            new XmlHtmlEntity("DownLeftTeeVector", 10590),
            new XmlHtmlEntity("DownRightTeeVector", 10591),
            new XmlHtmlEntity("LeftUpTeeVector", 10592),
            new XmlHtmlEntity("LeftDownTeeVector", 10593),

            new XmlHtmlEntity("lHar", 10594),
            new XmlHtmlEntity("uHar", 10595),
            new XmlHtmlEntity("rHar", 10596),
            new XmlHtmlEntity("dHar", 10597),
            new XmlHtmlEntity("luruhar", 10598),
            new XmlHtmlEntity("ldrdhar", 10599),
            new XmlHtmlEntity("ruluhar", 10600),
            new XmlHtmlEntity("rdldhar", 10601),
            new XmlHtmlEntity("lharul", 10602),
            new XmlHtmlEntity("llhard", 10603),
            new XmlHtmlEntity("rharul", 10604),
            new XmlHtmlEntity("lrhard", 10605),
            new XmlHtmlEntity("udhar", 10606),
            new XmlHtmlEntity("UpEquilibrium", 10606),
            new XmlHtmlEntity("duhar", 10607),
            new XmlHtmlEntity("ReverseUpEquilibrium", 10607),

        };

        */


        /// <summary></summary>
        new static public bool Equals(object x, object y)
        {
            if (x == null && y == null)
                return true;

            if (x != null && y == null || x == null && y != null)
                return false;

            if (x is XmlHtmlEntity && y is XmlHtmlEntity)
                return Equals((XmlHtmlEntity)x, (XmlHtmlEntity)y);

            return false;
        }
        /// <summary></summary>
        static public bool Equals(XmlHtmlEntity x, XmlHtmlEntity y)
        {
            if (x._XMLvalue == y._XMLvalue)
            {
                if (x.HTML != null && y.HTML != null)
                {
                    if (x.IsCaseSensitive || y.IsCaseSensitive)
                        return string.Equals(x.HTML, y.HTML, StringComparison.InvariantCulture);
                    else
                        return string.Equals(x.HTML, y.HTML, StringComparison.InvariantCultureIgnoreCase);
                }
                else
                    return true;
            }
            else
                return false;
        }

        /// <summary></summary>
        public override bool Equals(object obj) { return Equals(this, obj); }
        /// <summary></summary>
        public bool Equals(XmlHtmlEntity obj) { return Equals(this, obj); }


        /// <summary></summary>
        static public int Compare(object x, object y)
        {
            if (x != null && y != null && x is XmlHtmlEntity && y is XmlHtmlEntity)
                return Compare((XmlHtmlEntity)x, (XmlHtmlEntity)y);

            return 0;
        }
        /// <summary></summary>
        static public int Compare(XmlHtmlEntity x, XmlHtmlEntity y)
        {
            int rslt = x._XMLvalue.CompareTo(y._XMLvalue);

            if (rslt == 0)
                if (x.HTML != null && y.HTML != null)
                {
                    if (x.IsCaseSensitive || y.IsCaseSensitive)
                        return string.Compare(x.HTML, y.HTML, StringComparison.InvariantCulture);
                    else
                        return string.Compare(x.HTML, y.HTML, StringComparison.InvariantCultureIgnoreCase);
                }

            return rslt;
        }
        
        #region interface

        bool IEqualityComparer<XmlHtmlEntity>.Equals(XmlHtmlEntity x, XmlHtmlEntity y) { return Equals(x, y); }

        int IEqualityComparer<XmlHtmlEntity>.GetHashCode(XmlHtmlEntity obj) {  return obj.GetHashCode(); }

        bool System.Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }

        /// <summary></summary>
        int System.Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }
        
        /// <summary></summary>
        int IComparer<XmlHtmlEntity>.Compare(XmlHtmlEntity x, XmlHtmlEntity y)
        {
            return Compare(x, y);
        }
        int System.Collections.IComparer.Compare(object x, object y)
        {
            return Compare(x, y);
        }

        /// <summary></summary>
        public int CompareTo(XmlHtmlEntity obj)
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
