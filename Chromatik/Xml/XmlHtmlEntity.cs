using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace System.Xml
{
    //https://fr.wikipedia.org/wiki/Liste_des_entités_caractère_de_XML_et_HTML#Entités_caractère_de_HTML
    //https://en.wikipedia.org/wiki/List_of_XML_and_HTML_character_entity_references#Character_entity_references_in_HTML

    /// <summary>
    /// Represents an HTML entity / XML Entity with the corresponding character.
    /// </summary>
    public class XmlHtmlEntity : IComparerEquatable<XmlHtmlEntity>
    {
        static XmlHtmlEntity()
        {
            System.Text.RegularExpressions.RegexCompilationInfo[] info = new System.Text.RegularExpressions.RegexCompilationInfo[]
            {
                new System.Text.RegularExpressions.RegexCompilationInfo("><", System.Text.RegularExpressions.RegexOptions.Compiled, "name", "fullname", true),
                new System.Text.RegularExpressions.RegexCompilationInfo("<>", System.Text.RegularExpressions.RegexOptions.Compiled, "name2", "fullname", true),
            };
            System.Reflection.AssemblyName na = new System.Reflection.AssemblyName("Regelib, Version=1.0.0, Culture=neutral, PublicKeyToken=null");
            System.Text.RegularExpressions.Regex.CompileToAssembly(info, na);

            System.Reflection.Assembly ass = System.Reflection.Assembly.Load(na);

            object o = InvokeConstructor(GetType(ass, info[0].Name, info[0].Namespace));
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
        public XmlHtmlEntity(string html, int xml, bool caseSensitive)
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
        public XmlHtmlEntity(int xml) : this(null, xml, true)
        { }


        static public string Parse(string html, params XmlHtmlEntity[] entitys)
        {
            return null;
        }

        static public XmlHtmlEntity[] HTMLbase { get; } = new XmlHtmlEntity[]
        {
            new XmlHtmlEntity("quot", 34, false),
            new XmlHtmlEntity("amp", 38, false),
            new XmlHtmlEntity("apos", 39, false),
            
            new XmlHtmlEntity("lt", 60, false),
            new XmlHtmlEntity("gt", 62, false),
        };

        /// <summary>
        /// HTML 2.2 entity
        /// </summary>
        static public XmlHtmlEntity[] Html2 { get; } = new XmlHtmlEntity[]
        {
            new XmlHtmlEntity("Agrave", 192, true),
            new XmlHtmlEntity("Aacute", 193, true),
            new XmlHtmlEntity("Acirc", 194, true),
            new XmlHtmlEntity("Atilde", 195, true),
            new XmlHtmlEntity("Auml", 196, true),
            new XmlHtmlEntity("Aring", 197, true),
            new XmlHtmlEntity("AElig", 198, true),
            new XmlHtmlEntity("Ccedil", 199, true),
            new XmlHtmlEntity("Egrave", 200, true),
            new XmlHtmlEntity("Eacute", 201, true),
            new XmlHtmlEntity("Ecirc", 202, true),
            new XmlHtmlEntity("Euml", 203, true),
            new XmlHtmlEntity("Igrave", 204, true),
            new XmlHtmlEntity("Iacute", 205, true),
            new XmlHtmlEntity("Icirc", 206, true),
            new XmlHtmlEntity("Iuml", 207, true),
            new XmlHtmlEntity("ETH", 208, true),
            new XmlHtmlEntity("Ntilde", 209, true),
            new XmlHtmlEntity("Ograve", 210, true),
            new XmlHtmlEntity("Oacute", 211, true),
            new XmlHtmlEntity("Ocirc", 212, true),
            new XmlHtmlEntity("Otilde", 213, true),
            new XmlHtmlEntity("Ouml", 214, true),

            new XmlHtmlEntity("Oslash", 216, true),
            new XmlHtmlEntity("Ugrave", 217, true),
            new XmlHtmlEntity("Uacute", 218, true),
            new XmlHtmlEntity("Ucirc", 219, true),
            new XmlHtmlEntity("Uuml", 220, true),
            new XmlHtmlEntity("Yacute", 221, true),
            new XmlHtmlEntity("THORN", 222, true),
            new XmlHtmlEntity("szlig", 223, true),
            new XmlHtmlEntity("agrave", 224, true),
            new XmlHtmlEntity("aacute", 225, true),
            new XmlHtmlEntity("acirc", 226, true),
            new XmlHtmlEntity("atilde", 227, true),
            new XmlHtmlEntity("auml", 228, true),
            new XmlHtmlEntity("aring", 229, true),
            new XmlHtmlEntity("aelig", 230, true),
            new XmlHtmlEntity("ccedil", 231, true),
            new XmlHtmlEntity("egrave", 232, true),
            new XmlHtmlEntity("eacute", 233, true),
            new XmlHtmlEntity("ecirc", 234, true),
            new XmlHtmlEntity("euml", 235, true),
            new XmlHtmlEntity("igrave", 236, true),
            new XmlHtmlEntity("iacute", 237, true),
            new XmlHtmlEntity("icirc", 238, true),
            new XmlHtmlEntity("iuml", 239, true),
            new XmlHtmlEntity("eth", 240, true),
            new XmlHtmlEntity("ntilde", 241, true),
            new XmlHtmlEntity("ograve", 242, true),
            new XmlHtmlEntity("oacute", 243, true),
            new XmlHtmlEntity("ocirc", 244, true),
            new XmlHtmlEntity("otilde", 245, true),
            new XmlHtmlEntity("ouml", 246, true),

            new XmlHtmlEntity("oslash", 248, true),
            new XmlHtmlEntity("ugrave", 249, true),
            new XmlHtmlEntity("uacute", 250, true),
            new XmlHtmlEntity("ucirc", 251, true),
            new XmlHtmlEntity("uuml", 252, true),
            new XmlHtmlEntity("yacute", 253, true),
            new XmlHtmlEntity("thorn", 254, true),
            new XmlHtmlEntity("yuml", 255, true),
        };

        /// <summary>
        /// HTML 3.2 entity
        /// </summary>
        static public XmlHtmlEntity[] Html3 { get; } = new XmlHtmlEntity[]
        {
            new XmlHtmlEntity("nbsp", 160, true),
            new XmlHtmlEntity("iexcl", 161  , true),
            new XmlHtmlEntity("cent", 162, true),
            new XmlHtmlEntity("pound", 163, true),
            new XmlHtmlEntity("curren", 164, true),
            new XmlHtmlEntity("yen", 165, true),
            new XmlHtmlEntity("brvbar", 166, true),
            new XmlHtmlEntity("sect", 167, true),
            new XmlHtmlEntity("uml", 168, true),
            new XmlHtmlEntity("copy", 169, true),
            new XmlHtmlEntity("ordf", 170, true),
            new XmlHtmlEntity("laquo", 171, true),
            new XmlHtmlEntity("not", 172, true),
            new XmlHtmlEntity("shy", 173, true),
            new XmlHtmlEntity("reg", 174, true),
            new XmlHtmlEntity("macr", 175, true),
            new XmlHtmlEntity("deg", 176, true),
            new XmlHtmlEntity("plusmn", 177, true),
            new XmlHtmlEntity("sup2", 178, true),
            new XmlHtmlEntity("sup3", 179, true),
            new XmlHtmlEntity("acute", 180, true),
            new XmlHtmlEntity("micro", 181, true),
            new XmlHtmlEntity("para", 182, true),
            new XmlHtmlEntity("middot", 183, true),
            new XmlHtmlEntity("cedil", 184, true),
            new XmlHtmlEntity("sup1", 185, true),
            new XmlHtmlEntity("ordm", 186, true),
            new XmlHtmlEntity("raquo", 187, true),
            new XmlHtmlEntity("frac14", 188, true),
            new XmlHtmlEntity("frac12", 189, true),
            new XmlHtmlEntity("frac34", 190, true),
            new XmlHtmlEntity("iquest", 191, true),

            new XmlHtmlEntity("times", 215, true),

            new XmlHtmlEntity("divide", 247, true),
        };


        /// <summary>
        /// HTML 4 entity
        /// </summary>
        static public XmlHtmlEntity[] Html4 { get; } = new XmlHtmlEntity[]
        {
            new XmlHtmlEntity("OElig", 338, true),
            new XmlHtmlEntity("oelig", 339, true),

            new XmlHtmlEntity("Scaron", 352, true),
            new XmlHtmlEntity("scaron", 353, true),

            new XmlHtmlEntity("Yuml", 376, true),

            new XmlHtmlEntity("fnof", 402, true),

            new XmlHtmlEntity("circ", 710, true),

            new XmlHtmlEntity("tilde", 732, true),

            new XmlHtmlEntity("fnof", 402, true),

            new XmlHtmlEntity("fnof", 402, true),

        };

        /// <summary>
        /// HTML 5 entity
        /// </summary>
        static public XmlHtmlEntity[] Html5 { get; } = new XmlHtmlEntity[]
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

        public override int GetHashCode() { return base.GetHashCode(); }

        #region interface

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
                    return string.Equals(x.HTML, y.HTML, StringComparison.InvariantCultureIgnoreCase);
                else
                    return true;
            }
            else
                return false;
        }

        bool IEqualityComparer<XmlHtmlEntity>.Equals(XmlHtmlEntity x, XmlHtmlEntity y) { return Equals(x, y); }

        int IEqualityComparer<XmlHtmlEntity>.GetHashCode(XmlHtmlEntity obj) { return obj.GetHashCode(); }

        bool Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }

        /// <summary></summary>
        int Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }

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
            if (x._XMLvalue.CompareTo(y._XMLvalue) == 0)
                if (x.HTML != null && y.HTML != null)
                    return string.Compare(x.HTML, y.HTML, StringComparison.InvariantCultureIgnoreCase);

            return 0;
        }

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
