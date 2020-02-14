using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace System.Xml
{
    //https://fr.wikipedia.org/wiki/Liste_des_entités_caractère_de_XML_et_HTML#Entités_caractère_de_HTML
    //https://en.wikipedia.org/wiki/List_of_XML_and_HTML_character_entity_references#Character_entity_references_in_HTML

    /// <summary>
    /// Represents an HTML entity / XML Entity with the corresponding character.
    /// </summary>
    public class XmlHtmlEntity // : IComparerEquatable<XmlHtmlEntity>
        : IEquatable<XmlHtmlEntity>, IEqualityComparer<XmlHtmlEntity>, Collections.IEqualityComparer, IComparer<XmlHtmlEntity>, Collections.IComparer, IComparable<XmlHtmlEntity>, IComparable
    {

        static private RegexOptions regOption = RegexOptions.CultureInvariant;
        static private TimeSpan timeout = new TimeSpan(0, 0, 10);
        static private Globalization.CultureInfo InvariantCulture = Globalization.CultureInfo.InvariantCulture;

        static XmlHtmlEntity _null { get; } = new XmlHtmlEntity(0);
        static public IEqualityComparer<XmlHtmlEntity> EqualityComparer { get; } = _null;
        static public IComparer<XmlHtmlEntity> Comparator { get; } = _null;

        static XmlHtmlEntity()
        {
            new XmlHtmlEntity(0);

            Type t = typeof(XmlHtmlEntity);

            IEnumerable<XmlHtmlEntity> enumerable = new XmlHtmlEntity[0];

            foreach (var item in t.GetProperties(BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                if (item.PropertyType.GetInterface("System.Collections.IEnumerable") != null)
                    enumerable = enumerable.Concat(((Collections.IEnumerable)item.GetValue(null)).OfType<XmlHtmlEntity>());

            string space = "CompiledRegex.Xml.XmlHtmlEntity";
            string spaceXml = string.Join(".", space, "Xml");
            string spaceHtml = string.Join(".", space, "Html");
            string spaceChar = string.Join(".", space, "Char");

            List<RegexCompilationInfo> lst = new List<RegexCompilationInfo>(3000);
            List<KeyValuePair<XmlHtmlEntity, RegexCompilationInfo>> pair = new List<KeyValuePair<XmlHtmlEntity, RegexCompilationInfo>>(3000);
            foreach (XmlHtmlEntity item in enumerable.Distinct())
            {
                if (item.HTML != null)
                {
                    string n = item.HTML.Trim('&', ';');

                    lst.Add(new RegexCompilationInfo(item.Character, regOption, "c_" +n + item._XMLvalue.ToString(InvariantCulture), spaceChar, true, timeout));
                    lst.Add(new RegexCompilationInfo(item.XML, regOption, "x_" + n + item._XMLvalue.ToString(InvariantCulture), spaceXml, true, timeout));

                    if (item.IsCaseSensitive)
                        lst.Add(new RegexCompilationInfo(item.HTML, regOption, "h_" + n + item._XMLvalue.ToString(InvariantCulture), spaceHtml, true, timeout));
                    else
                        lst.Add(new RegexCompilationInfo(item.HTML, regOption | RegexOptions.IgnoreCase, "h_" + n + item._XMLvalue.ToString(InvariantCulture), spaceHtml, true, timeout));
                }
                else
                {
                    lst.Add(new RegexCompilationInfo(item.Character, regOption, "c" + item._XMLvalue.ToString(InvariantCulture), spaceChar, true, timeout));
                    lst.Add(new RegexCompilationInfo(item.XML, regOption, "x" + item._XMLvalue.ToString(InvariantCulture), spaceXml, true, timeout));

                }

                pair.Add(new KeyValuePair<XmlHtmlEntity, RegexCompilationInfo>(item, lst.Last()));
            }

            AssemblyName asName = new AssemblyName(space+", Version=1.0.0, Culture=neutral, PublicKeyToken=null");

            List<string> s = new List<string>(lst.Count);
            foreach (var item in lst)
                s.Add(item.Namespace + "." + item.Name);

            string[] du = s.GroupBy(x => x)
                .Where(g => g.Count() > 1)
                .Select(y => y.Key)
                .ToArray();

            IDictionary<string, int> dd = s.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .ToDictionary(x => x.Key, y => y.Count());
            
            ;

            //Regex.CompileToAssembly(lst.ToArray(), asName);

            Assembly assembly = Assembly.Load(asName);
            foreach (KeyValuePair<XmlHtmlEntity, RegexCompilationInfo> item in pair)
            {
                object o = InvokeConstructor(GetType(assembly, item.Value.Name, spaceXml));
                if (o is Regex)
                    item.Key._regexXML = (Regex)o;
            }
            
        }


        static public string Parse(string html, params XmlHtmlEntity[] entitys)
        {
            return null;
        }

        static Type GetType(Assembly assembly, string name, string fullnamespace)
        {
            if (assembly == null)
                return null;

            return assembly.GetType(fullnamespace + "." + name, false, true);
        }
        static object InvokeConstructor(Type type, params object[] parameters)
        {
            if (type == null)
                return null;

            ConstructorInfo[] constructors = type.GetConstructors();
            if (parameters == null)
                parameters = new object[0];

            Type[] parametersType = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                parametersType[i] = parameters[i].GetType();

            foreach (var item in constructors)
            {
                Reflection.ParameterInfo[] param = item.GetParameters();
                if (param.Length == parametersType.Length)
                {
                    bool valide = true;
                    for (int i = 0; i < parametersType.Length; i++)
                        if (param[i].ParameterType != parametersType[i])
                        {
                            valide = false;
                            break;
                        }

                    if (valide)
                        return item.Invoke(parameters);
                }
            }

            return null;
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

        private Regex _regexXML;
        private Regex _regexHTML;
        private Regex _regexCHAR;

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

            _regexXML = new Regex(XML, regOption, timeout);
            _regexCHAR = new Regex(Character, regOption, timeout);
            if (HTML != null)
            {
                if (IsCaseSensitive)
                    _regexHTML = new Regex(HTML, regOption, timeout);
                else
                    _regexHTML = new Regex(HTML, regOption | RegexOptions.IgnoreCase, timeout);
            }
            else
                _regexHTML = null;
        }

        /// <summary>
        /// Initializes an instance with the HTML and the XML format.
        /// </summary>
        /// <param name="html">HTML name of the entity</param>
        /// <param name="xml">XML value of the entity</param>
        public XmlHtmlEntity(string html, int xml) : this(html, xml, true) { }
        /// <summary>
        /// Initializes an instance with only the XML format.
        /// </summary>
        /// <param name="xml"></param>
        public XmlHtmlEntity(int xml) : this(null, xml, true)
        { }


        /// <summary></summary>
        public string ParseHTMLtoXML(string input)
        {
            if (_regexHTML != null && HTML != null)
                return _regexHTML.Replace(input, XML);
            else
                return input;
        }
        /// <summary></summary>
        public string ParseHTMLtoCHAR(string input)
        {
            if (_regexHTML != null && HTML != null)
                return _regexHTML.Replace(input, XML);
            else
                return input;
        }
        /// <summary></summary>
        public string ParseCHARtoHTML(string input)
        {
            if (_regexCHAR != null && HTML != null)
                return _regexCHAR.Replace(input, HTML);
            else
                return input;
        }
        /// <summary></summary>
        public string ParseCHARtoXML(string input)
        {
            if (_regexCHAR != null)
                return _regexCHAR.Replace(input, XML);
            else
                return input;
        }
        /// <summary></summary>
        public string ParseXMLtoHTML(string input)
        {
            if (_regexXML != null && HTML != null)
                return _regexXML.Replace(input, HTML);
            else
                return input;
        }
        /// <summary></summary>
        public string ParseXMLtoCHAR(string input)
        {
            if (_regexXML != null)
                return _regexXML.Replace(input, Character);
            else
                return input;
        }


        /// <summary></summary>
        public override string ToString()
        {
            string rslt = XML + "|" + Character;
            if (HTML != null)
                rslt = HTML + "|" + rslt;
            return rslt;
        }

        public override int GetHashCode()  { return HashCode; }
        
		static int HashCode = Runtime.CompilerServices.RuntimeHelpers.GetHashCode(_null);

        ///rdquo, rdquor
        ///uarr, uparrow
        ///darr, downarrow
        ///sub, subset

        static public XmlHtmlEntity[] HTMLbase { get; } = new XmlHtmlEntity[]
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
        /// HTML 4 entity character
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

            new XmlHtmlEntity("trade", 8482, false),

        };


        /// <summary>
        /// HTML 5 entity character
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

            //HTML 5
        };

        /// <summary>
        /// HTML entity for Greek letter
        /// </summary>
        static public XmlHtmlEntity[] Html_Greek { get; } = new XmlHtmlEntity[]
        {
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
            new XmlHtmlEntity("epsiv", 949 ),
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
            new XmlHtmlEntity("varsigma", 962 ),
            new XmlHtmlEntity("sigmaf", 962 ),
            new XmlHtmlEntity("sigma", 963 ),
            new XmlHtmlEntity("tau", 964 ),
            new XmlHtmlEntity("upsilon", 965 ),
            new XmlHtmlEntity("upsi", 965 ),
            new XmlHtmlEntity("phi", 966 ),
            new XmlHtmlEntity("chi", 967 ),
            new XmlHtmlEntity("psi", 968 ),
            new XmlHtmlEntity("omega", 969 ),

            new XmlHtmlEntity("thetav", 977 ),
            new XmlHtmlEntity("vartheta", 977 ),
            new XmlHtmlEntity("thetasym", 977 ),
            new XmlHtmlEntity("upsih", 978 ),
            new XmlHtmlEntity("Upsi", 978 ),

            new XmlHtmlEntity("straightphi", 981 ),
            new XmlHtmlEntity("phiv", 981 ),
            new XmlHtmlEntity("varphi", 981 ),
            new XmlHtmlEntity("piv", 982 ),
            new XmlHtmlEntity("varpi", 982 ),

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

        };

        /// <summary>
        /// HTML entity for arrows symbols
        /// </summary>
        static public XmlHtmlEntity[] Html_Arrow { get; } = new XmlHtmlEntity[]
        {
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

        /// <summary>
        /// HTML entity for math symbols
        /// </summary>
        static public XmlHtmlEntity[] Html_Math { get; } = new XmlHtmlEntity[]
        {

        };

        /// <summary>
        /// HTML entity for miscellaneous symbols
        /// </summary>
        static public XmlHtmlEntity[] Html5_Misc { get; } = new XmlHtmlEntity[]
        {

        };



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

        bool Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }

        /// <summary></summary>
        int Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }
        
        /// <summary></summary>
        int IComparer<XmlHtmlEntity>.Compare(XmlHtmlEntity x, XmlHtmlEntity y)
        {
            return Compare(x, y);
        }
        int Collections.IComparer.Compare(object x, object y)
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
