using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Text.RegularExpressions
{
    public interface IRegexCompiledEntry : IComparerEquatable<IRegexCompiledEntry>
    {
        /// <summary>
        /// Pattern of the regex
        /// </summary>
        string Pattern { get; }
        /// <summary>
        /// Name of the regex
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Namespace of the regex
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// FullNamespace of the regex
        /// </summary>
        string FullNamespace { get; }
    }

    /// <summary>
    /// A compiled regex entry
    /// </summary>
    public class RegexPreCompiledEntry : IRegexCompiledEntry
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
        public RegexPreCompiledEntry(string pattern, string fullnamespace, string name)
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
            if (x is RegexPreCompiledEntry && y is RegexPreCompiledEntry)
                return Equals((RegexPreCompiledEntry)x, (RegexPreCompiledEntry)y);
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
    }

    /// <summary>
    /// A compiled <see cref="Regex"/>
    /// </summary>
    public class RegexCompiledEntry : IRegexCompiledEntry
    {

        Regex _regex;

        internal RegexCompiledEntry(Regex regex)
        {

        }
        internal RegexCompiledEntry(RegexPreCompiledEntry regex)
        {

        }
    }


    /// <summary></summary>
    public abstract class RegexCompiledList<T> : Collections.ObjectModel.ReadOnlyCollection<IRegexCompiledEntry> where T : IRegexCompiledEntry
    {
        protected List<T> Items { get { return (List<T>)base.Items; } }

        /// <summary></summary>
        public RegexCompiledList() : base(new List<IRegexCompiledEntry>())
        { }

    }

    public class RegexPreCompileList : RegexCompiledList<RegexPreCompiledEntry>
    {

    }
    public class RegexCompileList : RegexCompiledList<RegexCompiledEntry>
    {

    }

    /// <summary>
    /// Collection of <see cref="RegexCompiledEntry"/>
    /// </summary>
    public class RegexCompiled : RegexCompiledList<RegexCompiledEntry>
    {
        #region static

        /// <summary>
        /// Create a
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static public RegexCompiled Create(RegexPreCompileList list)
        {
            return Create(list, RegexHelper.RegexOptions);
        }
        static public RegexCompiled Create(RegexPreCompileList list, RegexOptions options)
        {
            return Create(list, options, RegexHelper.Timeout);
        }
        static public RegexCompiled Create(RegexPreCompileList list, RegexOptions options, TimeSpan matchTimeout)
        {
            return new RegexCompiled(list, options, matchTimeout);
        }

        static public RegexCompiled LoadAssembly(string assemblyName)
        {
            return LoadAssembly(Reflection.AssemblyName.GetAssemblyName(assemblyName));
        }
        static public RegexCompiled LoadAssembly(Reflection.AssemblyName assemblyName)
        {
            Reflection.Assembly ass = Reflection.Assembly.Load(assemblyName);
        }
        static public RegexCompiled LoadAssemblyFile(string assemblyPath)
        {

        }

        static public string CreateAssembly(string name, RegexPreCompileList list)
        {
            return CreateAssembly(name, list, RegexHelper.RegexOptions);
        }
        static public string CreateAssembly(string name, RegexPreCompileList list, RegexOptions options)
        {
            return CreateAssembly(name, list, options, RegexHelper.Timeout);
        }
        static public string CreateAssembly(string name, RegexPreCompileList list, RegexOptions options, TimeSpan matchTimeout)
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
        static public string CreateAssembly(Reflection.AssemblyName assemblyName, RegexPreCompileList list, RegexOptions options, TimeSpan matchTimeout)
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
        new protected List<RegexCompiledEntry> Items { get { return (List<RegexCompiledEntry>)base.Items; } }

        private RegexCompiled(RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout) : base(new List<RegexCompiledEntry>())
        {

            foreach (var item in list)
                base.Items.Add(new RegexCompiledEntry(item, options, matchTimeout));
        }
        /// <summary></summary>
        protected RegexCompiledEntry GetNamespace(string fullNamespace) { return RegexCompiledEntry.GetNamespace(fullNamespace); }

        /// <summary></summary>
        public RegexCompiledEntry this[string fullNamespace] { get { return Items.Find(GetNamespace(fullNamespace).TestNamespace); } }


        /// <summary></summary>
        public bool Contains(string fullNamespace)
        {
            return Items.Contains(GetNamespace(fullNamespace));
        }
        /// <summary></summary>
        new public bool Contains(RegexCompiledEntry item)
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
        new public int IndexOf(RegexCompiledEntry item)
        {
            return Items.FindIndex(item.TestNamespace);
        }
        /// <summary></summary>
        int IndexOf(RegexCompiledEntry item, int index)
        {
            return Items.FindIndex(index, item.TestNamespace);
        }
        /// <summary></summary>
        public int IndexOf(RegexCompiledEntry item, int index, int count)
        {
            return Items.FindIndex(count, index, item.TestNamespace);
        }

        /// <summary></summary>
        public int LastIndexOf(RegexCompiledEntry item)
        {
            return Items.FindLastIndex(item.TestNamespace);
        }
        /// <summary></summary>
        public int LastIndexOf(RegexCompiledEntry item, int index)
        {
            return Items.FindLastIndex(index, item.TestNamespace);
        }
        /// <summary></summary>
        public int LastIndexOf(RegexCompiledEntry item, int index, int count)
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
