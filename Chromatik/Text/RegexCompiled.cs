using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// A compiled regex entry
    /// </summary>
    public class RegexCompiledEntry : IEquatable<RegexCompiledEntry>, IEqualityComparer<RegexCompiledEntry>, Collections.IEqualityComparer
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
                throw new ArgumentNullException(nameof(fullnamespace));
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));

            fullnamespace = fullnamespace.Trim(WhiteCharacter.WhiteCharacters);
            name = name.Trim(WhiteCharacter.WhiteCharacters.Concat('(', ')'));

            Pattern = pattern;
            FullNamespace = fullnamespace;
            Name = name;
        }


        Regex _regex;
        internal Regex Regex { get { return _regex; } }
        
        internal Regex Init(RegexOptions options, TimeSpan matchTimeout)
        {
            if (!options.HasFlag(RegexOptions.Compiled))
                options |= RegexOptions.Compiled;

            return _regex = new Regex(Pattern, options, matchTimeout);
        }
        
        internal bool TestNamespace(RegexCompiledEntry item)
        {
            return string.Equals(ToString(), item.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary></summary>
        public override string ToString()
        {
            return FullNamespace + "." + Name + "()";
        }


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
        static bool Equals(RegexCompiledEntry x, RegexCompiledEntry y)
        {
            return Equals(x, y, true);
        }
        /// <summary></summary>
        static bool Equals(RegexCompiledEntry x, RegexCompiledEntry y, bool caseSensitivePattern)
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
        public bool Equals(RegexCompiledEntry obj) { return Equals(this, obj); }

        bool IEqualityComparer<RegexCompiledEntry>.Equals(RegexCompiledEntry x, RegexCompiledEntry y) { return Equals(x, y); }
        bool Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }
        int IEqualityComparer<RegexCompiledEntry>.GetHashCode(RegexCompiledEntry obj) { return obj.GetHashCode(); }

        int Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }
    }

    public class RegexCompiledEntryList : List<RegexCompiledEntry>
    {
        public RegexCompiledEntryList() : base() { }

        internal const string msgException = "The input '" + nameof(RegexCompiledEntryList) + "' must contain at least one entry.";
        
        new public void Add(RegexCompiledEntry item)
        {
            if (Contains(item))
                throw new InvalidOperationException("A entry with the same " + nameof(item.Name)+ " and " + nameof(item.FullNamespace) + " is present in the collection.");
            base.Add(item);
        }

        new public void Insert(int index, RegexCompiledEntry item)
        {
            if (Contains(item))
                throw new InvalidOperationException("A entry with the same " + nameof(item.Name) + " and " + nameof(item.FullNamespace) + " is present in the collection.");
            base.Insert(index, item);
        }

        new public bool Contains(RegexCompiledEntry item)
        {
            return Exists(item.TestNamespace);
        }

        new public bool Remove(RegexCompiledEntry item)
        {
            RegexCompiledEntry f = Find(item.TestNamespace);
            if (f == null)
                return base.Remove(f);
            else
                return false;
        }

        new public int IndexOf(RegexCompiledEntry item)
        {
            return FindIndex(item.TestNamespace);
        }
        new public int IndexOf(RegexCompiledEntry item, int index)
        {
            return FindIndex(index, item.TestNamespace);
        }
        new public int IndexOf(RegexCompiledEntry item, int index, int count)
        {
            return FindIndex(count, index, item.TestNamespace);
        }

        new public int LastIndexOf(RegexCompiledEntry item)
        {
            return FindLastIndex(item.TestNamespace);
        }
        new public int LastIndexOf(RegexCompiledEntry item, int index)
        {
            return FindLastIndex(index, item.TestNamespace);
        }
        new public int LastIndexOf(RegexCompiledEntry item, int index, int count)
        {
            return FindLastIndex(count, index, item.TestNamespace);
        }

    }

    public class RegexCompiled : Collections.ObjectModel.ReadOnlyCollection<RegexCompiledEntry>
    {
        private RegexCompiled(RegexCompiledEntryList list, RegexOptions options, TimeSpan matchTimeout) : base(list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                throw new ArgumentException(RegexCompiledEntryList.msgException, nameof(list));

            if (!options.HasFlag(RegexOptions.Compiled))
                options |= RegexOptions.Compiled;

            foreach (var item in Items)
                item.Init(options, matchTimeout);
        }

        static public RegexCompiled Create(RegexCompiledEntryList list)
        {
            return Create(list, RegexHelper.RegexOptions);
        }
        static public RegexCompiled Create(RegexCompiledEntryList list, RegexOptions options)
        {
            return Create(list, options, RegexHelper.Timeout);
        }
        static public RegexCompiled Create(RegexCompiledEntryList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                throw new ArgumentException(RegexCompiledEntryList.msgException, nameof(list));

            if (!options.HasFlag(RegexOptions.Compiled))
                options |= RegexOptions.Compiled;

            return new RegexCompiled(list, options, matchTimeout);
        }

        static public string CreateAssembly(string name, RegexCompiledEntryList list)
        {
            return CreateAssembly(name, list, RegexHelper.RegexOptions);
        }
        static public string CreateAssembly(string name, RegexCompiledEntryList list, RegexOptions options)
        {
            return CreateAssembly(name, list, options, RegexHelper.Timeout);
        }
        static public string CreateAssembly(string name, RegexCompiledEntryList list, RegexOptions options, TimeSpan matchTimeout)
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
        static public string CreateAssembly(Reflection.AssemblyName assemblyName, RegexCompiledEntryList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                throw new ArgumentException(RegexCompiledEntryList.msgException, nameof(list));

            if (options.HasFlag(RegexOptions.Compiled))
                options ^= RegexOptions.Compiled;

            List<RegexCompilationInfo> lst = new List<RegexCompilationInfo>(list.Count);

            foreach (var item in list)
                lst.Add(new RegexCompilationInfo(item.Pattern, options, item.Name, item.FullNamespace, true, matchTimeout));

            Regex.CompileToAssembly(lst.ToArray(), assemblyName);

            return IO.Path.GetFullPath(assemblyName.Name + ".dll");
        }
    }
}
