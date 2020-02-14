using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// Base for compile regex class or entry
    /// </summary>
    public abstract class CompiledRegexBase : IComparerEquatable<CompiledRegexBase>
    {
        static CompiledRegexBase _null { get; } = new CompiledRegexEntry("temp", "temp");
        /// <summary></summary>
        static public IEqualityComparer<CompiledRegexBase> EqualityComparer { get; } = _null;
        /// <summary></summary>
        static public IComparer<CompiledRegexBase> Comparator { get; } = _null;
        /// <summary></summary>
        public override int GetHashCode() { return HashCode; }

        static int HashCode = Runtime.CompilerServices.RuntimeHelpers.GetHashCode(_null);

        /// <summary>
        /// Pattern of the regex
        /// </summary>
        public virtual string Pattern
        {
            get { return _pattern; }
            set { _pattern = value ?? string.Empty; }
        }
        /// <summary></summary>
        protected string _pattern;

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
        public virtual string Name
        {
            get { return _name; }
            set
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
        public virtual string Namespace
        {
            get { return _namespace; }
            set
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
        /// Default options for this regex
        /// </summary>
        public RegexOptions? Options
        {
            get { return _options; }
            set { _options = value; }
        }
        /// <summary></summary>
        protected RegexOptions? _options;


        /// <summary>
        /// Default MatchTimeout for this regex
        /// </summary>
        public TimeSpan? MatchTimeout
        {
            get { return _matchTimeout; }
            set { _matchTimeout = value; }
        }
        /// <summary></summary>
        protected TimeSpan? _matchTimeout;

        /// <summary>
        /// Full qualified name of the regex
        /// </summary>
        public string FullQualifiedName { get { return GetQualifiedName(Namespace, Name); } }

        /// <summary>
        /// Get the FullQualifiedName text
        /// </summary>
        /// <returns></returns>
        protected static string GetQualifiedName(string name, string fullnamespace) { return (fullnamespace + "." + name + "()").Trim('.'); }

        /// <summary>
        /// Initialise a <see cref="CompiledRegexBase"/> with the specified pattern, name and Namespace
        /// </summary>
        protected CompiledRegexBase(string pattern, string name, string fullnamespace)
        {
            Pattern = pattern;
            Namespace = fullnamespace;
            Name = name;
        }
        private CompiledRegexBase(string pattern, KeyValuePair<string, string> pair) : this(pattern, pair.Key, pair.Value)
        { }
        /// <summary>
        /// Initialise a <see cref="CompiledRegexBase"/> with the specified pattern FullQualifiedName
        /// </summary>
        public CompiledRegexBase(string pattern, string fullqualifiedname) : this(pattern, SplitFullQualifiedName(fullqualifiedname))
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
            if (x is CompiledRegexEntry && y is CompiledRegexEntry)
                return Equals((CompiledRegexEntry)x, (CompiledRegexEntry)y);
            return false;
        }
        /// <summary></summary>
        static public bool Equals(CompiledRegexBase x, CompiledRegexBase y)
        {
            return Equals(x, y, false);
        }
        /// <summary></summary>
        static public bool Equals(CompiledRegexBase x, CompiledRegexBase y, bool ignoreCase)
        {
            if (x == null && y == null)
                return true;
            else if (x != null && y != null)
                if (string.Equals(x.FullQualifiedName, y.FullQualifiedName, StringComparison.InvariantCultureIgnoreCase))
                    if (x.MatchTimeout.HasValue && y.MatchTimeout.HasValue && x.MatchTimeout.Value == y.MatchTimeout.Value)
                        if (x.Options.HasValue && y.Options.HasValue && x.Options.Value == y.Options.Value)
                        {
                            if (ignoreCase || x.Options.HasValue && x.Options.Value.HasFlag(RegexOptions.IgnoreCase) &&
                                y.Options.HasValue && y.Options.Value.HasFlag(RegexOptions.IgnoreCase))
                            {
                                return string.Equals(x.Pattern, y.Pattern, StringComparison.InvariantCultureIgnoreCase);
                            }
                            else
                                return string.Equals(x.Pattern, y.Pattern, StringComparison.InvariantCulture);
                        }

            return false;
        }
        
        

        /// <summary>
        /// Test if the is the same FullQualifiedName (IgnoreCase)
        /// </summary>
        static public bool EqualsFullQualifiedName(CompiledRegexBase x, string fullqualifiedname)
        {
            KeyValuePair<string, string> pair = SplitFullQualifiedName(fullqualifiedname);
            return EqualsFullQualifiedName(x, pair.Key, pair.Value);
        }
        /// <summary>
        /// Test if the is the same Name and Fullnamespace (IgnoreCase)
        /// </summary>
        static public bool EqualsFullQualifiedName(CompiledRegexBase x, string name, string fullnamespace)
        {
            return string.Equals(x.Namespace, fullnamespace, StringComparison.InvariantCultureIgnoreCase)
                && string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase);
        }
        /// <summary>
        /// Test if the is the same FullQualifiedName (IgnoreCase)
        /// </summary>
        static public bool EqualsFullQualifiedName(CompiledRegexBase x, CompiledRegexBase y)
        {
            return string.Equals(x.FullQualifiedName, y.FullQualifiedName, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Test if the is the same FullQualifiedName (IgnoreCase)
        /// </summary>
        public bool EqualsFullQualifiedName(string fullqualifiedname) { return EqualsFullQualifiedName(this, fullqualifiedname); }
        /// <summary>
        /// Test if the is the same Name and Fullnamespace (IgnoreCase)
        /// </summary>
        public bool EqualsFullQualifiedName(string name, string fullnamespace) { return EqualsFullQualifiedName(this, name, fullnamespace); }
        /// <summary>
        /// Test if the is the same FullQualifiedName (IgnoreCase)
        /// </summary>
        public bool EqualsFullQualifiedName(CompiledRegexBase y) { return EqualsFullQualifiedName(this, y); }


        /// <summary></summary>
        static public int Compare(object x, object y)
        {
            if (x is CompiledRegexBase && y is CompiledRegexBase)
                return Compare((CompiledRegexBase)x, (CompiledRegexBase)y);
            else
                return 0;
        }
        /// <summary></summary>
        static public int Compare(CompiledRegexBase x, CompiledRegexBase y)
        {
            if (x == null && y == null)
                return 0;
            else if (x != null && y == null)
                return 1;
            else if (x == null && y != null)
                return -1;
            else
            {
                int rslt = string.Compare(x.FullQualifiedName, y.FullQualifiedName, StringComparison.InvariantCultureIgnoreCase);
                if (rslt == 0)
                {
                    if (x.Options.HasValue && x.Options.Value.HasFlag(RegexOptions.IgnoreCase) &&
                        y.Options.HasValue && y.Options.Value.HasFlag(RegexOptions.IgnoreCase))
                    {
                        return string.Compare(x.Pattern, y.Pattern, StringComparison.InvariantCultureIgnoreCase);
                    }
                    else
                        return string.Compare(x.Pattern, y.Pattern, StringComparison.InvariantCulture);
                }
                else
                    return rslt;
            }
        }

        /// <summary></summary>
        public int CompareTo(object obj) { return Compare(this, obj); }
        /// <summary></summary>
        public int CompareTo(CompiledRegexBase obj) { return Compare(this, obj); }


        /// <summary>
        /// Compare the FullQualifiedName (IgnoreCase)
        /// </summary>
        static public int CompareFullQualifiedName(CompiledRegexBase x, string fullqualifiedname)
        {
            KeyValuePair<string, string> pair = SplitFullQualifiedName(fullqualifiedname);
            return CompareFullQualifiedName(x, pair.Key, pair.Value);
        }
        /// <summary>
        /// Compare the Name and Fullnamespace (IgnoreCase)
        /// </summary>
        static public int CompareFullQualifiedName(CompiledRegexBase x, string name, string fullnamespace)
        {
            int rslt = string.Compare(x.Namespace, fullnamespace, StringComparison.InvariantCultureIgnoreCase);
            if (rslt == 0)
                return string.Compare(x.Name, name, StringComparison.InvariantCultureIgnoreCase);
            else
                return rslt;
        }
        /// <summary>
        /// Compare the FullQualifiedName (IgnoreCase)
        /// </summary>
        static public int CompareFullQualifiedName(CompiledRegexBase x, CompiledRegexBase y)
        {
            return string.Compare(x.FullQualifiedName, y.FullQualifiedName, StringComparison.InvariantCultureIgnoreCase);
        }


        /// <summary>
        /// Test Compare the FullQualifiedName (IgnoreCase)
        /// </summary>
        public int CompareFullQualifiedName(string fullqualifiedname) { return CompareFullQualifiedName(this, fullqualifiedname); }
        /// <summary>
        /// Compare the Name and Fullnamespace (IgnoreCase)
        /// </summary>
        public int CompareFullQualifiedName(string name, string fullnamespace) { return CompareFullQualifiedName(this, name, fullnamespace); }
        /// <summary>
        /// Compare the FullQualifiedName (IgnoreCase)
        /// </summary>
        public int CompareFullQualifiedName(CompiledRegexBase y) { return CompareFullQualifiedName(this, y); }


        #region interface


        /// <summary></summary>
        public override bool Equals(object obj) { return Equals(this, obj); }
        /// <summary></summary>
        public bool Equals(CompiledRegexBase obj) { return Equals(this, obj); }

        bool IEqualityComparer<CompiledRegexBase>.Equals(CompiledRegexBase x, CompiledRegexBase y) { return Equals(x, y); }
        bool Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }
        int IEqualityComparer<CompiledRegexBase>.GetHashCode(CompiledRegexBase obj) { return obj.GetHashCode(); }

        int Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }

        int IComparer<CompiledRegexBase>.Compare(CompiledRegexBase x, CompiledRegexBase y) { return Compare(x, y); }
        int Collections.IComparer.Compare(object x, object y) { return Compare(x, y); }
        #endregion
    }

    /// <summary>
    /// A PreCompiled regex entry
    /// </summary>
    public class CompiledRegexEntry : CompiledRegexBase
    {

        /// <summary>
        /// Initialise a <see cref="CompiledRegexEntry"/> with the specified pattern, name and Namespace
        /// </summary>
        public CompiledRegexEntry(string pattern, string name, string fullnamespace) : base(pattern, name, fullnamespace)
        { }

        /// <summary>
        /// Initialise a <see cref="CompiledRegexEntry"/> with the specified pattern and FullQualifiedName
        /// </summary>
        public CompiledRegexEntry(string pattern, string fullqualifiedname) : base(pattern, fullqualifiedname)
        { }
    }
    /// <summary>
    /// A compiled <see cref="Regex"/>
    /// </summary>
    public class CompiledRegexClass : CompiledRegexBase
    {
        internal Regex _regex;

        /// <summary>
        /// Pattern of the regex
        /// </summary>
        new public string Pattern { get { return _pattern; } }
        /// <summary>
        /// Name of the regex
        /// </summary>
        new public string Name { get { return _name; } }
        /// <summary>
        /// Namespace of the regex
        /// </summary>
        new public string Namespace { get { return _namespace; } }

        /// <summary>
        /// Full qualified name of the regex
        /// </summary>
        new public string FullQualifiedName { get { return GetQualifiedName(Namespace, Name); } }

        /// <summary>
        /// Default options for this regex
        /// </summary>
        new public RegexOptions Options { get { return _regex.Options; } }
        /// <summary>
        /// Default MatchTimeout for this regex
        /// </summary>
        new public TimeSpan MatchTimeout { get { return _regex.MatchTimeout; } }
        /// <summary>
        /// Gets a value that indicates whether the regular expression searches from right to left.</summary>
        public bool RightToLeft { get { return _regex.RightToLeft; } }

        /// <summary>
        /// Initialise a <see cref="CompiledRegexEntry"/> with the specified pattern, name and Namespace
        /// </summary>
        internal CompiledRegexClass(Regex regex) : base("temp", "temp")
        {
            _regex = regex;

            _pattern = _regex.ToString();

            _matchTimeout = _regex.MatchTimeout;
            _options = _regex.Options;

            Type t = regex.GetType();
            _name = t.Name;
            _namespace = t.Namespace;
        }

        /// <summary>
        /// Initialise on base of the specified <see cref="CompiledRegexEntry"/>, <see cref="RegexOptions"/> and <see cref="TimeSpan"/>
        /// </summary>
        internal CompiledRegexClass(CompiledRegexBase regex, RegexOptions options, TimeSpan matchTimeout) : base(regex.Pattern, regex.Name, regex.Namespace)
        {
            if (!options.HasFlag(RegexOptions.Compiled))
                options |= RegexOptions.Compiled;

            if (regex.MatchTimeout.HasValue)
                matchTimeout = regex.MatchTimeout.Value;

            if (regex.Options.HasValue)
                options = regex.Options.Value;

            _regex = new Regex(regex.Pattern, options, matchTimeout);
        }

        /// <summary></summary>
        public override string ToString()
        {
            return Pattern;
        }

        /// <summary>
        /// Returns an array of catch group names for the regular expression.
        /// </summary>
        /// <returns>String table of group names.</returns>
        public string[] GetGroupNames() { return _regex.GetGroupNames(); }
        /// <summary>
        /// Returns a table of catch group numbers that correspond to the group names in a table.
        /// </summary>
        /// <returns>Table of integers of group numbers.</returns>
        public int[] GetGroupNumbers() { return _regex.GetGroupNumbers(); }
        /// <summary>
        /// Gets the group name that corresponds to the specified group number.
        /// </summary>
        /// <param name="i">Group number to be converted to the corresponding group name.</param>
        /// <returns>String that contains the group name associated with the specified group number. If no group name matches <paramref name="i"/>, the method returns <see cref="string.Empty"/>.</returns>
        public string GroupNameFromNumber(int i) { return _regex.GroupNameFromNumber(i); }
        /// <summary>
        /// Returns the group number that corresponds to the specified group name.
        /// </summary>
        /// <param name="name">Group name to be converted to the corresponding group number.</param>
        /// <returns>Group number corresponding to the specified group <paramref name="name"/>, or -1 if <paramref name="name"/> is not a valid group <paramref name="name"/>.</returns>
        /// <exception cref="ArgumentNullException">name has the value null.</exception>
        public int GroupNumberFromName(string name) { return _regex.GroupNumberFromName(name); }

        /// <summary>
        /// Specifies whether the regular expression specified in the <see cref="Regex"/> constructor looks for a match in a specific <paramref name="input"/> string.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <returns>true if the regular expression finds a match; else, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public bool IsMatch(string input) { return _regex.IsMatch(input); }
        /// <summary>
        /// Specifies whether the regular expression specified in the <see cref="Regex"/> constructor looks for a match in the specified <paramref name="input"/> string, starting at the defined starting position in the string.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="startat">Position of the character where the search should start.</param>
        /// <returns>true if the regular expression finds a match; else, false.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startat"/> is less than zero or greater than the <paramref name="input"/> lenght.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public bool IsMatch(string input, int startat) { return _regex.IsMatch(input, startat); }

        /// <summary>
        /// Searches the input string for the first occurrence of a regular expression, starting at the specified starting position in the string.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <returns>Object that contains correspondence information.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public Match Match(string input) { return _regex.Match(input); }
        /// <summary>
        /// Searches the input string for the first occurrence of a regular expression, starting at the specified starting position and searching for only the specified number of characters.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="startat">Position of the character where the search should start.</param>
        /// <returns>Object that contains correspondence information.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startat"/> is less than zero or greater than the <paramref name="input"/> lenght.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public Match Match(string input, int startat) { return _regex.Match(input, startat); }
        /// <summary>
        /// Searches the input string for the first occurrence of a regular expression, starting at the specified starting position and searching for only the specified number of characters.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="startat">Position of the character where the search should start.</param>
        /// <param name="length">Number of characters in the substring to be included in the search.</param>
        /// <returns>Object that contains correspondence information.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startat"/> is less than zero or greater than the <paramref name="input"/> lenght. Or <paramref name="length"/> is less than zero or greater than the <paramref name="input"/> lenght. Ou <paramref name="startat"/>+<paramref name="length"/>-1 identifies a position that is outside the range of <paramref name="input"/>.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public Match Match(string input, int startat, int length) { return _regex.Match(input, startat, length); }

        /// <summary>
        ///  Searches the specified input string for all occurrences of a regular expression.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <returns>Collection of <see cref="RegularExpressions.Match"/> objects found by the search. If no match is found, the method returns an empty collection object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        public MatchCollection Matches(string input) { return _regex.Matches(input); }
        /// <summary>
        /// Searches the specified input string for all occurrences of a regular expression, starting at the specified start position in the string.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="startat">Position of the character where the search should start.</param>
        /// <returns>Collection of <see cref="RegularExpressions.Match"/> objects found by the search. If no match is found, the method returns an empty collection object.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startat"/> is less than zero or greater than the <paramref name="input"/> lenght.</exception>
        public MatchCollection Matches(string input, int startat) { return _regex.Matches(input, startat); }

        /// <summary>
        /// Splits an input string into an array of substrings, at positions defined by a regular expression template specified in the <see cref="Regex"/> constructor.
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <returns>Table of chains.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public string[] Split(string input) { return _regex.Split(input); }
        /// <summary>
        /// Splits a specified input string a specified maximum number of times into an array of substrings, at positions defined by a regular expression specified in the <see cref="Regex"/> constructor.
        /// </summary>
        /// <param name="input">Splitting chain.</param>
        /// <param name="count">Maximum number of times the splitting can take place.</param>
        /// <returns>Table of chains.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public string[] Split(string input, int count) { return _regex.Split(input, count); }
        /// <summary>
        /// Splits a specified input string a specified maximum number of times into an array of substrings, at positions defined by a regular expression specified in the <see cref="Regex"/> constructor. The search for the regular expression pattern starts at a specified character position in the input chain.
        /// </summary>
        /// <param name="input">Splitting chain.</param>
        /// <param name="count">Maximum number of times the splitting can take place.</param>
        /// <param name="startat">Position of the character where the search should start.</param>
        /// <returns>Table of chains.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> has the value null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startat"/> is less than zero or greater than the <paramref name="input"/> lenght.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public string[] Split(string input, int count, int startat) { return _regex.Split(input, count, startat); }

        /// <summary>
        /// In the specified input substring, replaces the specified maximum number of strings that match a regular expression pattern with a specific replacement string.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="replacement">Replacement chain.</param>
        /// <returns>New string identical to the <paramref name="input"/> string, except that the <paramref name="replacement"/> string replaces each matching string. If the regular expression pattern has no match in the current instance, the method returns the current instance unchanged.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="replacement"/> has the value null.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public string Replace(string input, string replacement) { return _regex.Replace(input, replacement); }
        /// <summary>
        /// In the specified input substring, replaces the specified maximum number of strings that match a regular expression pattern with a specific replacement string.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="replacement">Replacement chain.</param>
        /// <param name="count">Maximum number of times the splitting can take place.</param>
        /// <returns>New string identical to the <paramref name="input"/> string, except that the <paramref name="replacement"/> string replaces each matching string. If the regular expression pattern has no match in the current instance, the method returns the current instance unchanged.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="replacement"/> has the value null.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public string Replace(string input, string replacement, int count) { return _regex.Replace(input, replacement, count); }
        /// <summary>
        /// In the specified input substring, replaces the specified maximum number of strings that match a regular expression pattern with a specific replacement string.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="replacement">Replacement chain.</param>
        /// <param name="count">Maximum number of times the splitting can take place.</param>
        /// <param name="startat">Position of the character where the search should start.</param>
        /// <returns>New string identical to the <paramref name="input"/> string, except that the <paramref name="replacement"/> string replaces each matching string. If the regular expression pattern has no match in the current instance, the method returns the current instance unchanged.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="replacement"/> has the value null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startat"/> is less than zero or greater than the <paramref name="input"/> lenght.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public string Replace(string input, string replacement, int count, int startat) { return _regex.Replace(input, replacement, count, startat); }

        /// <summary>
        /// In the specified input string, replaces all strings that match a specified regular expression with a string returned by a System.Text.RegularExpressions.MatchEvaluator delegate.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="evaluator">A custom method that examines each match and returns the original match string or a replacement string.</param>
        /// <returns>New string identical to the <paramref name="input"/> string, except that a replacement string replaces each matching string. If the regular expression pattern has no match in the current instance, the method returns the current instance unchanged.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="evaluator"/> has the value null.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public string Replace(string input, MatchEvaluator evaluator) { return _regex.Replace(input, evaluator); }
        /// <summary>
        /// In the specified input string, replaces all strings that match a specified regular expression with a string returned by a System.Text.RegularExpressions.MatchEvaluator delegate.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="evaluator">A custom method that examines each match and returns the original match string or a replacement string.</param>
        /// <param name="count">Maximum number of times the splitting can take place.</param>
        /// <returns>New string identical to the <paramref name="input"/> string, except that a replacement string replaces each matching string. If the regular expression pattern has no match in the current instance, the method returns the current instance unchanged.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="evaluator"/> has the value null.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public string Replace(string input, MatchEvaluator evaluator, int count) { return _regex.Replace(input, evaluator, count); }
        /// <summary>
        /// In the specified input string, replaces all strings that match a specified regular expression with a string returned by a System.Text.RegularExpressions.MatchEvaluator delegate.
        /// </summary>
        /// <param name="input">String in which a match is to be searched.</param>
        /// <param name="evaluator">A custom method that examines each match and returns the original match string or a replacement string.</param>
        /// <param name="count">Maximum number of times the splitting can take place.</param>
        /// <param name="startat">Position of the character where the search should start.</param>
        /// <returns>New string identical to the <paramref name="input"/> string, except that a replacement string replaces each matching string. If the regular expression pattern has no match in the current instance, the method returns the current instance unchanged.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="input"/> or <paramref name="evaluator"/> has the value null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startat"/> is less than zero or greater than the <paramref name="input"/> lenght.</exception>
        /// <exception cref="RegexMatchTimeoutException">The timeout period has expired.For more information on timeouts, see Notes.</exception>
        public string Replace(string input, MatchEvaluator evaluator, int count, int startat) { return _regex.Replace(input, evaluator, count, startat); }
    }
}
