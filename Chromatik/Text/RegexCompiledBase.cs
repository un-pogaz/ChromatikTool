using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// Base for compile regex class or entry
    /// </summary>
    public abstract class RegexCompiledBase : IComparerEquatable<RegexCompiledBase>
    {
        /// <summary>
        /// Pattern of the regex
        /// </summary>
        public string Pattern
        {
            get { return _pattern; }
            protected set { _pattern = value ?? string.Empty; }
        }
        string _pattern;

        static private bool IsValideName(string value)
        {
            if (value.IsNullOrWhiteSpace() || !value.RegexIsMatch(RegexHelper.ASCII_forCsharpNameClass))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Name of the regex
        /// </summary>
        public string Name
        {
            get { return _name; }
            protected set
            {
                if (value.IsNullOrWhiteSpace())
                    throw new ArgumentNullException(nameof(Name));

                value = value.Trim();

                if (!IsValideName(value))
                    throw new ArgumentException("The " + nameof(Name) + " is invalide. Only a ASCII class name is allowed.", nameof(Name));

                _name = value;
            }
        }
        /// <summary></summary>
        protected string _name;


        static private bool IsValideNamespace(string value)
        {
            if (!value.IsNullOrWhiteSpace() && !value.RegexIsMatch(RegexHelper.ASCII_forCsharpNameSpace))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Namespace of the regex
        /// </summary>
        public string Namespace
        {
            get { return _namespace; }
            protected set
            {
                value = value ?? "";

                value = value.Trim();

                if (!IsValideNamespace(value))
                    throw new ArgumentException("The " + nameof(Namespace) + " is invalide. Only a ASCII namespace is allowed.", nameof(Namespace));

                _namespace = value;
            }
        }
        /// <summary></summary>
        protected string _namespace;

        /// <summary>
        /// Full qualified name of the regex
        /// </summary>
        public string FullQualifiedName { get { return (Namespace + "." + Name + "()").Trim('.'); } }

        /// <summary>
        /// Initialise a <see cref="RegexCompiledBase"/> with the specified pattern, name and Namespace
        /// </summary>
        protected RegexCompiledBase(string pattern, string name, string fullnamespace)
        {
            Pattern = pattern;
            Namespace = fullnamespace;
            Name = name;
        }
        private RegexCompiledBase(string pattern, KeyValuePair<string, string> pair) : this(pattern, pair.Key, pair.Value)
        { }
        public RegexCompiledBase(string pattern, string fullqualifiedname) : this(pattern, SplitFullQualifiedName(fullqualifiedname))
        { }

        static internal KeyValuePair<string, string> SplitFullQualifiedName(string fullqualifiedname)
        {
            if (fullqualifiedname.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(fullqualifiedname));

            fullqualifiedname = fullqualifiedname.TrimEnd('(', ')');

            int i = fullqualifiedname.LastIndexOf(".");
            if (i < 0)
                return new KeyValuePair<string, string>(fullqualifiedname, "");
            else
                return new KeyValuePair<string, string>(fullqualifiedname.Substring(i + 1), fullqualifiedname.Substring(0, i));
        }

        /// <summary></summary>
        public override string ToString()
        {
            return Pattern;
        }

        /// <summary></summary>
        new public static bool Equals(object x, object y)
        {
            if (x == null && y == null)
                return true;
            if (x is RegexCompiledEntry && y is RegexCompiledEntry)
                return Equals((RegexCompiledEntry)x, (RegexCompiledEntry)y);
            return false;
        }
        /// <summary></summary>
        static public bool Equals(RegexCompiledBase x, RegexCompiledBase y)
        {
            return Equals(x, y, true);
        }
        /// <summary></summary>
        static public bool Equals(RegexCompiledBase x, RegexCompiledBase y, bool caseSensitivePattern)
        {
            if (x == null && y == null)
                return true;
            else if (x != null && y != null)
            {
                if (string.Equals(x.FullQualifiedName, y.FullQualifiedName, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (caseSensitivePattern)
                        return string.Equals(x.Pattern, y.Pattern, StringComparison.InvariantCulture);
                    else
                        return string.Equals(x.Pattern, y.Pattern, StringComparison.InvariantCultureIgnoreCase);
                }
            }
            return false;
        }
        

        static internal bool EqualsFullQualifiedName(RegexCompiledBase x, string fullqualifiedname)
        {
            KeyValuePair<string, string> pair = SplitFullQualifiedName(fullqualifiedname);
            return EqualsFullQualifiedName(x, pair.Key, pair.Value);
        }
        static internal bool EqualsFullQualifiedName(RegexCompiledBase x, string name, string fullnamespace)
        {
            return string.Equals(x.Namespace, fullnamespace, StringComparison.InvariantCultureIgnoreCase)
                && string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase);
        }



        /// <summary></summary>
        static public int Compare(object x, object y)
        {
            if (x is RegexCompiledBase && y is RegexCompiledBase)
                return Compare((RegexCompiledBase)x, (RegexCompiledBase)y);
            else
                return 0;
        }
        /// <summary></summary>
        static public int Compare(RegexCompiledBase x, RegexCompiledBase y)
        {
            if (x == null && y == null)
                return 0;
            else if (x != null && y == null)
                return 1;
            else if (x == null && y != null)
                return -1;
            else
                return string.Compare(x.FullQualifiedName, y.FullQualifiedName, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary></summary>
        public int CompareTo(object obj) { return Compare(this, obj); }
        /// <summary></summary>
        public int CompareTo(RegexCompiledBase obj) { return Compare(this, obj); }

        #region interface

        /// <summary></summary>
        public override int GetHashCode() { return base.GetHashCode(); }

        /// <summary></summary>
        public override bool Equals(object obj) { return Equals(this, obj); }
        /// <summary></summary>
        public bool Equals(RegexCompiledBase obj) { return Equals(this, obj); }

        bool IEqualityComparer<RegexCompiledBase>.Equals(RegexCompiledBase x, RegexCompiledBase y) { return Equals(x, y); }
        bool Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }
        int IEqualityComparer<RegexCompiledBase>.GetHashCode(RegexCompiledBase obj) { return obj.GetHashCode(); }

        int Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }

        int IComparer<RegexCompiledBase>.Compare(RegexCompiledBase x, RegexCompiledBase y) { return Compare(x, y); }
        int Collections.IComparer.Compare(object x, object y) { return Compare(x, y); }
        #endregion
    }

    /// <summary>
    /// A PreCompiled regex entry
    /// </summary>
    public class RegexCompiledEntry : RegexCompiledBase
    {
        /// <summary>
        /// Pattern of the regex
        /// </summary>
        new public string Pattern
        {
            get { return base.Pattern; }
            set { base.Name = value; }
        }

        /// <summary>
        /// Name of the regex
        /// </summary>
        new public string Name
        {
            get { return base.Name; }
            set { base.Name = value; }
        }

        /// <summary>
        /// Namespace of the regex
        /// </summary>
        new public string Namespace
        {
            get { return base.Namespace; }
            set { base.Namespace = value; }
        }

        /// <summary>
        /// Initialise a <see cref="RegexCompiledEntry"/> with the specified pattern, name and Namespace
        /// </summary>
        public RegexCompiledEntry(string pattern, string name, string fullnamespace) : base(pattern, name, fullnamespace)
        { }

        /// <summary>
        /// Initialise a <see cref="RegexCompiledEntry"/> with the specified pattern and FullQualifiedName
        /// </summary>
        public RegexCompiledEntry(string pattern, string fullqualifiedname) : base(pattern, fullqualifiedname)
        { }
    }
    /// <summary>
    /// A compiled <see cref="Regex"/>
    /// </summary>
    public class RegexCompiledClass : RegexCompiledBase
    {
        internal Regex _regex;

        /// <summary>
        /// Initialise a <see cref="RegexCompiledEntry"/> with the specified pattern, name and Namespace
        /// </summary>
        internal RegexCompiledClass(Regex regex) : base("temp", "temp", "temp")
        {
            _regex = regex;

            Pattern = regex.ToString();
            Type t = regex.GetType();

            _name = t.Name;
            _namespace = t.Namespace;
        }

        /// <summary>
        /// Initialise on base of the specified <see cref="RegexCompiledEntry"/>, <see cref="RegexOptions"/> and <see cref="TimeSpan"/>
        /// </summary>
        internal RegexCompiledClass(RegexCompiledBase regex, RegexOptions options, TimeSpan matchTimeout) : base(regex.Pattern, regex.Name, regex.Namespace)
        {
            if (!options.HasFlag(RegexOptions.Compiled))
                options |= RegexOptions.Compiled;

            _regex = new Regex(regex.Pattern, options, matchTimeout);
        }
    }
}
