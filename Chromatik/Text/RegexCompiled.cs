using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Text.RegularExpressions
{
    public interface IRegexCompiledEntry : IEquatable<IRegexCompiledEntry>, IEqualityComparer<IRegexCompiledEntry>, Collections.IEqualityComparer
    {
        /// <summary>
        /// Pattern of the regex
        /// </summary>
        string Pattern { get; }
        /// <summary>
        /// FullNamespace of the regex
        /// </summary>
        string FullNamespace { get; }
        /// <summary>
        /// Name of the regex
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Test if the item has the same FullNamespace
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool TestNamespace(IRegexCompiledEntry item);
    }

    /// <summary>
    /// A compiled regex entry
    /// </summary>
    public class RegexCompiledEntry : IRegexCompiledEntry
    {
        /// <summary>
        /// Pattern of the regex
        /// </summary>
        public string Pattern { get; }
        /// <summary>
        /// FullNamespace of the regex
        /// </summary>
        public string FullNamespace { get; }
        /// <summary>
        /// Name of the regex
        /// </summary>
        public string Name { get; }
        /// <summary></summary>
        public RegexCompiledEntry(string pattern, string fullnamespace, string name)
        {
            if (pattern.IsNullOrWhiteSpace())
                pattern = "";

            if (fullnamespace.IsNullOrWhiteSpace())
                fullnamespace = "";
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));

            fullnamespace = fullnamespace.Trim('.').Replace("..",".");
            name = name.Trim(WhiteCharacter.WhiteCharacters.Concat('(', ')', '.'));

            if (fullnamespace.RegexIsMatch(@"^\d"))
                throw new ArgumentException("A "+nameof(FullNamespace) + " cannot start with a number.", nameof(fullnamespace));
            if (name.RegexIsMatch(@"^\d"))
                throw new ArgumentException("A " + nameof(Name) + " cannot start with a number.", nameof(name));
            
            Pattern = pattern;
            FullNamespace = fullnamespace;
            Name = name;
        }

        internal bool TestNamespace(IRegexCompiledEntry item)
        {
            return string.Equals(ToString(), item.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }
        bool IRegexCompiledEntry.TestNamespace(IRegexCompiledEntry item) { return TestNamespace(item); }

        static internal string ToString(IRegexCompiledEntry item)
        {
            return (item.FullNamespace + "." + item.Name + "()").Trim('.');
        }

        /// <summary></summary>
        public override string ToString()
        {
            return ToString(this);
        }

        #region interface

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
        static bool Equals(IRegexCompiledEntry x, IRegexCompiledEntry y)
        {
            return Equals(x, y, true);
        }
        /// <summary></summary>
        static bool Equals(IRegexCompiledEntry x, IRegexCompiledEntry y, bool caseSensitivePattern)
        {
            if (x == null && y == null)
                return true;
            else if (x != null && y != null)
            {
                if (string.Equals(x.ToString(), y.ToString(), StringComparison.InvariantCultureIgnoreCase))
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
        public override int GetHashCode() { return base.GetHashCode(); }

        /// <summary></summary>
        public override bool Equals(object obj) { return Equals(this, obj); }
        /// <summary></summary>
        public bool Equals(IRegexCompiledEntry obj) { return Equals(this, obj); }

        bool IEqualityComparer<IRegexCompiledEntry>.Equals(IRegexCompiledEntry x, IRegexCompiledEntry y) { return Equals(x, y); }
        bool Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }
        int IEqualityComparer<IRegexCompiledEntry>.GetHashCode(IRegexCompiledEntry obj) { return obj.GetHashCode(); }

        int Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }
        #endregion

        static internal RegexCompiledEntry GetNamespace(string fullNamespace)
        {
            string name = "";
            string fullname = "";
            int i = fullNamespace.LastIndexOf(".");
            if (i > 0)
            {
                name = fullNamespace.Substring(i - 1);
                fullname = fullNamespace.Substring(0, i);
            }
            else
                name = fullNamespace;

            return new RegexCompiledEntry("", fullname, name);
        }
    }

    /// <summary>
    /// A compiled <see cref="Regex"/>
    /// </summary>
    public class CompiledRegex : Regex, IRegexCompiledEntry
    {
        /// <summary>
        /// Pattern of the regex
        /// </summary>
        public string Pattern { get { return _entry.Pattern; } }
        /// <summary>
        /// FullNamespace of the regex
        /// </summary>
        public string FullNamespace { get { return _entry.FullNamespace; } }
        /// <summary>
        /// Name of the regex
        /// </summary>
        public string Name { get { return _entry.Name; } }

        internal RegexCompiledEntry _entry;

        static RegexOptions SetCompiled(RegexOptions options)
        {
            if (!options.HasFlag(RegexOptions.Compiled))
                options |= RegexOptions.Compiled;

            return options;
        }

        internal CompiledRegex(RegexCompiledEntry entry, RegexOptions options, TimeSpan matchTimeout) : base(entry.Pattern, SetCompiled(options), matchTimeout)
        {
            _entry = entry;
        }
        private CompiledRegex(RegexCompiledEntry entry) : base()
        {
            _entry = entry;
        }

        internal bool TestNamespace(IRegexCompiledEntry item)
        {
            return _entry.TestNamespace(item);
        }
        bool IRegexCompiledEntry.TestNamespace(IRegexCompiledEntry item) { return TestNamespace(item); }

        /// <summary></summary>
        public override string ToString()
        {
            return RegexCompiledEntry.ToString(this);
        }

        #region interface

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
        static bool Equals(IRegexCompiledEntry x, IRegexCompiledEntry y)
        {
            return Equals(x, y, true);
        }
        /// <summary></summary>
        static bool Equals(IRegexCompiledEntry x, IRegexCompiledEntry y, bool caseSensitivePattern)
        {
            if (x == null && y == null)
                return true;
            else if (x != null && y != null)
            {
                if (string.Equals(x.ToString(), y.ToString(), StringComparison.InvariantCultureIgnoreCase))
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
        public override int GetHashCode() { return base.GetHashCode(); }

        /// <summary></summary>
        public override bool Equals(object obj) { return Equals(this, obj); }
        /// <summary></summary>
        public bool Equals(IRegexCompiledEntry obj) { return Equals(this, obj); }

        bool IEqualityComparer<IRegexCompiledEntry>.Equals(IRegexCompiledEntry x, IRegexCompiledEntry y) { return Equals(x, y); }
        bool Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }
        int IEqualityComparer<IRegexCompiledEntry>.GetHashCode(IRegexCompiledEntry obj) { return obj.GetHashCode(); }

        int Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }
        #endregion

        static internal CompiledRegex GetNamespace(string fullNamespace)
        {
            return new CompiledRegex(RegexCompiledEntry.GetNamespace(fullNamespace));
        }
    }

    /// <summary></summary>
    public class RegexCompiledList : List<RegexCompiledEntry>
    {
        /// <summary></summary>
        public RegexCompiledList() : base() { }

        internal const string msgException = "The input '" + nameof(RegexCompiledList) + "' must contain at least one entry.";

        /// <summary></summary>
        public RegexCompiledEntry this[string fullNamespace] { get { return Find(GetNamespace(fullNamespace).TestNamespace); } }

        /// <summary></summary>
        protected RegexCompiledEntry GetNamespace(string fullNamespace) { return RegexCompiledEntry.GetNamespace(fullNamespace); }

        /// <summary></summary>
        new public void Add(RegexCompiledEntry item)
        {
            if (Contains(item))
                throw new InvalidOperationException("A entry with the same " + nameof(item.Name)+ " and " + nameof(item.FullNamespace) + " is present in the collection.");
            base.Add(item);
        }
        /// <summary></summary>
        new public void AddRange(IEnumerable<RegexCompiledEntry> collection)
        {
            foreach (var item in collection)
                if (Contains(item))
                    throw new InvalidOperationException("A entry with the same " + nameof(item.Name) + " and " + nameof(item.FullNamespace) + " of one of the range is present in the collection.");
            base.AddRange(collection);
        }

        /// <summary></summary>
        new public void Insert(int index, RegexCompiledEntry item)
        {
            if (Contains(item))
                throw new InvalidOperationException("A entry with the same " + nameof(item.Name) + " and " + nameof(item.FullNamespace) + " is present in the collection.");
            base.Insert(index, item);
        }
        /// <summary></summary>
        new public void InsertRange(int index, IEnumerable<RegexCompiledEntry> collection)
        {
            foreach (var item in collection)
                if (Contains(item))
                    throw new InvalidOperationException("A entry with the same " + nameof(item.Name) + " and " + nameof(item.FullNamespace) + " of one of the range is present in the collection.");
            base.InsertRange(index, collection);
        }

        /// <summary></summary>
        public bool Contains(string fullNamespace)
        {
            return Contains(GetNamespace(fullNamespace));
        }
        /// <summary></summary>
        new public bool Contains(RegexCompiledEntry item)
        {
            return Exists(item.TestNamespace);
        }

        /// <summary></summary>
        public bool Remove(string fullNamespace)
        {
            return Remove(GetNamespace(fullNamespace));
        }
        /// <summary></summary>
        new public bool Remove(RegexCompiledEntry item)
        {
            RegexCompiledEntry f = Find(item.TestNamespace);
            if (f == null)
                return base.Remove(f);
            else
                return false;
        }

        /// <summary></summary>
        public int IndexOf(string fullNamespace)
        {
            return IndexOf(GetNamespace(fullNamespace));
        }
        /// <summary></summary>
        public int IndexOf(string fullNamespace, int index)
        {
            return IndexOf(GetNamespace(fullNamespace), index);
        }
        /// <summary></summary>
        public int IndexOf(string fullNamespace, int index, int count)
        {
            return IndexOf(GetNamespace(fullNamespace), count, index);
        }

        /// <summary></summary>
        new public int IndexOf(RegexCompiledEntry item)
        {
            return FindIndex(item.TestNamespace);
        }
        /// <summary></summary>
        new public int IndexOf(RegexCompiledEntry item, int index)
        {
            return FindIndex(index, item.TestNamespace);
        }
        /// <summary></summary>
        new public int IndexOf(RegexCompiledEntry item, int index, int count)
        {
            return FindIndex(count, index, item.TestNamespace);
        }

        /// <summary></summary>
        new public int LastIndexOf(RegexCompiledEntry item)
        {
            return FindLastIndex(item.TestNamespace);
        }
        /// <summary></summary>
        new public int LastIndexOf(RegexCompiledEntry item, int index)
        {
            return FindLastIndex(index, item.TestNamespace);
        }
        /// <summary></summary>
        new public int LastIndexOf(RegexCompiledEntry item, int index, int count)
        {
            return FindLastIndex(count, index, item.TestNamespace);
        }

        /// <summary></summary>
        public int LastIndexOf(string fullNamespace)
        {
            return LastIndexOf(GetNamespace(fullNamespace));
        }
        /// <summary></summary>
        public int LastIndexOf(string fullNamespace, int index)
        {
            return LastIndexOf(GetNamespace(fullNamespace), index);
        }
        /// <summary></summary>
        public int LastIndexOf(string fullNamespace, int index, int count)
        {
            return LastIndexOf(GetNamespace(fullNamespace), count, index);
        }

    }

    /// <summary>
    /// Collection of <see cref="CompiledRegex"/>
    /// </summary>
    public class RegexCompiled : Collections.ObjectModel.ReadOnlyCollection<CompiledRegex>
    {
        #region static

        /// <summary>
        /// Create a
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static public RegexCompiled Create(RegexCompiledList list)
        {
            return Create(list, RegexHelper.RegexOptions);
        }
        static public RegexCompiled Create(RegexCompiledList list, RegexOptions options)
        {
            return Create(list, options, RegexHelper.Timeout);
        }
        static public RegexCompiled Create(RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout)
        {
            return new RegexCompiled(list, options, matchTimeout);
        }

        static public string CreateAssembly(string name, RegexCompiledList list)
        {
            return CreateAssembly(name, list, RegexHelper.RegexOptions);
        }
        static public string CreateAssembly(string name, RegexCompiledList list, RegexOptions options)
        {
            return CreateAssembly(name, list, options, RegexHelper.Timeout);
        }
        static public string CreateAssembly(string name, RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));

            name = name.Trim();
            if (name.RegexIsMatch(@"[^ -~]"))
                throw new IO.InvalidPathException("Invalide " + nameof(name) + ". Contain a non-ASCII character.");
            if (name.RegexIsMatch(@"[\\/:*?\"" <>|]"))
                throw new IO.InvalidPathException("Invalide " + nameof(name) + ". Contain a invalid file name character.");

            return CreateAssembly(new Reflection.AssemblyName(name + ", Version=1.0.0, Culture=neutral, PublicKeyToken=null"), list, options, matchTimeout);
        }
        static public string CreateAssembly(Reflection.AssemblyName assemblyName, RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                throw new ArgumentException(RegexCompiledList.msgException, nameof(list));

            if (options.HasFlag(RegexOptions.Compiled))
                options ^= RegexOptions.Compiled;

            List<RegexCompilationInfo> lst = new List<RegexCompilationInfo>(list.Count);

            foreach (var item in list)
                lst.Add(new RegexCompilationInfo(item.Pattern, options, item.Name, item.FullNamespace, true, matchTimeout));

            Regex.CompileToAssembly(lst.ToArray(), assemblyName);

            return IO.Path.GetFullPath(assemblyName.Name + ".dll");
        }
        #endregion
        

        /// <summary></summary>
        new protected List<CompiledRegex> Items { get { return (List<CompiledRegex>)base.Items; } }

        private RegexCompiled(RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout) : base(new List<CompiledRegex>())
        {

            foreach (var item in list)
                base.Items.Add(new CompiledRegex(item, options, matchTimeout));
        }
        /// <summary></summary>
        protected CompiledRegex GetNamespace(string fullNamespace) { return CompiledRegex.GetNamespace(fullNamespace); }

        /// <summary></summary>
        public CompiledRegex this[string fullNamespace] { get { return Items.Find(GetNamespace(fullNamespace).TestNamespace); } }


        /// <summary></summary>
        public bool Contains(string fullNamespace)
        {
            return Items.Contains(GetNamespace(fullNamespace));
        }
        /// <summary></summary>
        new public bool Contains(CompiledRegex item)
        {
            return Items.Exists(item.TestNamespace);
        }

        /// <summary></summary>
        public int IndexOf(string fullNamespace)
        {
            return Items.IndexOf(GetNamespace(fullNamespace));
        }
        /// <summary></summary>
        public int IndexOf(string fullNamespace, int index)
        {
            return Items.IndexOf(GetNamespace(fullNamespace), index);
        }
        /// <summary></summary>
        public int IndexOf(string fullNamespace, int index, int count)
        {
            return Items.IndexOf(GetNamespace(fullNamespace), count, index);
        }

        /// <summary></summary>
        new public int IndexOf(CompiledRegex item)
        {
            return Items.FindIndex(item.TestNamespace);
        }
        /// <summary></summary>
        int IndexOf(CompiledRegex item, int index)
        {
            return Items.FindIndex(index, item.TestNamespace);
        }
        /// <summary></summary>
        public int IndexOf(CompiledRegex item, int index, int count)
        {
            return Items.FindIndex(count, index, item.TestNamespace);
        }

        /// <summary></summary>
        public int LastIndexOf(CompiledRegex item)
        {
            return Items.FindLastIndex(item.TestNamespace);
        }
        /// <summary></summary>
        public int LastIndexOf(CompiledRegex item, int index)
        {
            return Items.FindLastIndex(index, item.TestNamespace);
        }
        /// <summary></summary>
        public int LastIndexOf(CompiledRegex item, int index, int count)
        {
            return Items.FindLastIndex(count, index, item.TestNamespace);
        }

        /// <summary></summary>
        public int LastIndexOf(string fullNamespace)
        {
            return Items.LastIndexOf(GetNamespace(fullNamespace));
        }
        /// <summary></summary>
        public int LastIndexOf(string fullNamespace, int index)
        {
            return Items.LastIndexOf(GetNamespace(fullNamespace), index);
        }
        /// <summary></summary>
        public int LastIndexOf(string fullNamespace, int index, int count)
        {
            return Items.LastIndexOf(GetNamespace(fullNamespace), count, index);
        }





    }
}
