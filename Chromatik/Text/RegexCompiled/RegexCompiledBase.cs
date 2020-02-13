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

        /// <summary></summary>
        public override string ToString()
        {
            return Pattern;
        }

        public bool MatchFullQualifiedName(string FullQualifiedName)
        {
            if (FullQualifiedName == null)
                return false;

            FullQualifiedName = FullQualifiedName.TrimEnd('(', ')');

            int i = FullQualifiedName.LastIndexOf(".");
            if (i < 0)
            {
                if (!IsValideName(FullQualifiedName))
                    return false;
                else
                    return MatchFullQualifiedName(new RegexCompiledEntry("", FullQualifiedName, ""));
            }
            else
            {
                string name = FullQualifiedName.Substring(i+1);
                string space = FullQualifiedName.Substring(0,i);
                if (!IsValideName(name) && !IsValideNamespace(space))
                    return false;
                else
                    return MatchFullQualifiedName(new RegexCompiledEntry("", name, space));
            }
        }
        internal bool MatchFullQualifiedName(RegexCompiledBase items) { return string.Equals(FullQualifiedName, items.FullQualifiedName, StringComparison.InvariantCultureIgnoreCase); }


        /// <summary></summary>
        new static bool Equals(object x, object y)
        {
            if (x == null && y == null)
                return true;
            if (x is RegexCompiledEntry && y is RegexCompiledEntry)
                return Equals((RegexCompiledEntry)x, (RegexCompiledEntry)y);
            return false;
        }
        /// <summary></summary>
        static bool Equals(RegexCompiledBase x, RegexCompiledBase y)
        {
            return Equals(x, y, true);
        }
        /// <summary></summary>
        static bool Equals(RegexCompiledBase x, RegexCompiledBase y, bool caseSensitivePattern)
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
}
