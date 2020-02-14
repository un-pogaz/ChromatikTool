using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// Base list for <see cref="CompiledRegexList"/> and <see cref="CompiledRegex"/>
    /// </summary>
    public abstract class CompiledRegexList<T> : Collections.ObjectModel.ReadOnlyCollection<CompiledRegexBase> where T : CompiledRegexBase
    {
        /// <summary></summary>
        public CompiledRegexList() : base(new List<CompiledRegexBase>())
        { }

        /// <summary></summary>
        new public int Count { get { return Items.Count; } }

        /// <summary></summary>
        new public T this[int index] { get { return (T)Items[index]; } }

        /// <summary></summary>
        public T this[string fullqualifiedname] { get { return Find(fullqualifiedname); } }


        /// <summary>
        /// Determines whether <see cref="CompiledRegexList{T}"/> contains the exact same value.
        /// </summary>
        public virtual bool Contains(T value)
        {
            foreach (var item in Items)
                if (CompiledRegexBase.Equals(item, value))
                    return true;

            return false; }
        /// <summary>
        /// Determines whether <see cref="CompiledRegexList{T}"/> contains a value with the same <see cref="CompiledRegexBase.FullQualifiedName"/>.
        /// </summary>
        public virtual bool Contains(string fullqualifiedname) { return Find(fullqualifiedname) != null; }
        /// <summary>
        /// Determines whether <see cref="CompiledRegexList{T}"/> contains a value with the same <see cref="CompiledRegexBase.Name"/> and  <see cref="CompiledRegexBase.Namespace"/>.
        /// </summary>
        public virtual bool Contains(string name, string fullnamespace) { return Find(name, fullnamespace) != null; }


        /// <summary>
        /// Find the <see cref="CompiledRegexBase"/> with the same <see cref="CompiledRegexBase.FullQualifiedName"/>.
        /// </summary>
        public T Find(string fullqualifiedname)
        {
            KeyValuePair<string, string> pair = CompiledRegexBase.SplitFullQualifiedName(fullqualifiedname);
            return Find(pair.Key, pair.Value);
        }
        /// <summary>
        /// Find the <see cref="CompiledRegexBase"/> with the same <see cref="CompiledRegexBase.Name"/> and  <see cref="CompiledRegexBase.Namespace"/>.
        /// </summary>
        public T Find(string name, string fullnamespace)
        {
            foreach (var item in Items)
                if (CompiledRegexBase.EqualsFullQualifiedName(item, name, fullnamespace))
                    return (T)item;

            return null;
        }

        /// <summary></summary>
        public void CopyTo(T[] array, int index) { Items.CopyTo(array, index); }

        /// <summary>
        /// Determines the index of a <see cref="CompiledRegexBase"/> with the exact same value.
        /// </summary>
        public virtual int IndexOf(T value)
        {
            for (int i = 0; i < Items.Count; i++)
                if (CompiledRegexBase.Equals(Items[i], value))
                    return i;

            return -1;
        }
        /// <summary>
        /// Determines the index of a <see cref="CompiledRegexBase"/> with the same <see cref="CompiledRegexBase.FullQualifiedName"/>.
        /// </summary>
        public virtual int IndexOf(string fullqualifiedname)
        {
            for (int i = 0; i < Items.Count; i++)
                if (CompiledRegexBase.EqualsFullQualifiedName(Items[i], fullqualifiedname))
                    return i;

            return -1;
        }
        /// <summary>
        /// Determines the index of a <see cref="CompiledRegexBase"/> with the same <see cref="CompiledRegexBase.Name"/> and  <see cref="CompiledRegexBase.Namespace"/>.
        /// </summary>
        public virtual int IndexOf(string name, string fullnamespace)
        {
            for (int i = 0; i < Items.Count; i++)
                if (CompiledRegexBase.EqualsFullQualifiedName(Items[i], name, fullnamespace))
                    return i;

            return -1;
        }

        /// <summary></summary>
        new public IEnumerator<T> GetEnumerator() { return Items.OfType<T>().GetEnumerator(); }
    }

    /// <summary>
    /// Represent a collection of regex to compiled 
    /// </summary>
    public class CompiledRegexList : CompiledRegexList<CompiledRegexEntry>
    {
        /// <summary>
        /// Add a element to the <see cref="CompiledRegexList"/>
        /// </summary>
        /// <param name="item"></param>
        public void Add(CompiledRegexEntry item)
        {
            if (Contains(item.FullQualifiedName))
                throw new ArgumentException("The collection already contains one item with the same '"+nameof(CompiledRegexEntry.FullQualifiedName) + "'.\n\n"+ item.FullQualifiedName, nameof(item));

            Items.Add(item);
        }

        /// <summary>
        /// Remove the specified element of the <see cref="CompiledRegexList"/>
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(CompiledRegexEntry item)
        {
            for (int i = 0; i < Items.Count; i++)
                if (CompiledRegexBase.Equals(item, Items[i]))
                    return Items.Remove(Items[i]);

            return false;
        }
        /// <summary>
        /// Remove the element with the specified index number.
        /// </summary>
        /// <returns></returns>
        public bool RemoveAt(int index)
        {
            if (index >= 0 && index < Count)
            {
                Items.RemoveAt(index);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Remove the element with the same FullQualifiedName (IgnoreCase).
        /// </summary>
        /// <returns></returns>
        public bool RemoveQualifiedName(string fullqualifiedname)
        {
            CompiledRegexEntry t = Find(fullqualifiedname);
            if (t == null)
                return Items.Remove(t);
            else
                return false;
        }
        /// <summary>
        /// Remove the element with the same Name and FullNamespace (IgnoreCase).
        /// </summary>
        /// <returns></returns>
        public bool RemoveQualifiedName(string name, string fullnamespace)
        {
            CompiledRegexEntry t = Find(name, fullnamespace);
            if (t == null)
                return Items.Remove(t);
            else
                return false;
        }
    }

    /// <summary> 
    /// Represent a collection of compiled regex in a Assembly
    /// </summary>
    public class CompiledRegex : CompiledRegexList<CompiledRegexClass>
    {
        #region static

        /// <summary>
        /// Compile a <see cref="CompiledRegexList"/> into a <see cref="RuntimeAssembly"/>
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static public CompiledRegex CompileToRuntime(CompiledRegexList list) { return CompileToRuntime(list, RegexHelper.RegexOptions); }
        /// <summary>
        /// Compile a <see cref="CompiledRegexList"/> into a <see cref="RuntimeAssembly"/>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        static public CompiledRegex CompileToRuntime(CompiledRegexList list, RegexOptions options) { return CompileToRuntime(list, options, RegexHelper.Timeout); }
        /// <summary>
        /// Compile a <see cref="CompiledRegexList"/> into a <see cref="RuntimeAssembly"/>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        /// <returns></returns>
        static public CompiledRegex CompileToRuntime(CompiledRegexList list, RegexOptions options, TimeSpan matchTimeout)
        {
            return new CompiledRegex(list, options, matchTimeout);
        }

        /// <summary>
        /// Compile a <see cref="CompiledRegexList"/> into a Assembly file (.dll)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        static public CompiledRegex CompileToAssembly(string name, CompiledRegexList list) { return CompileToAssembly(name, list, RegexHelper.RegexOptions); }
        /// <summary>
        /// Compile a <see cref="CompiledRegexList"/> into a Assembly file (.dll)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="list"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        static public CompiledRegex CompileToAssembly(string name, CompiledRegexList list, RegexOptions options) { return CompileToAssembly(name, list, options, RegexHelper.Timeout); }
        /// <summary>
        /// Compile a <see cref="CompiledRegexList"/> into a Assembly file (.dll)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="list"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        /// <returns></returns>
        static public CompiledRegex CompileToAssembly(string name, CompiledRegexList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                throw new ArgumentException("The input list must have at least one entry.", nameof(list));

            if (options.HasFlag(RegexOptions.Compiled))
                options ^= RegexOptions.Compiled;

            return CompileToAssembly(name, Convert(list, options, matchTimeout));
        }
        /// <summary>
        /// Compile a <see cref="CompiledRegexList"/> into a Assembly file (.dll)
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="list"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        /// <returns></returns>
        static public CompiledRegex CompileToAssembly(AssemblyName assemblyName, CompiledRegexList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                throw new ArgumentException("The input list must have at least one entry.", nameof(list));

            if (options.HasFlag(RegexOptions.Compiled))
                options ^= RegexOptions.Compiled;

            return CompileToAssembly(assemblyName, Convert(list, options, matchTimeout));
        }

        static private RegexCompilationInfo[] Convert(IEnumerable<CompiledRegexBase> enumerable, RegexOptions options, TimeSpan matchTimeout)
        {
            List<RegexCompilationInfo> rslt = new List<RegexCompilationInfo>(enumerable.Count());
            RegexOptions opt;
            TimeSpan timeout;

            foreach (var item in enumerable)
            {
                if (item.MatchTimeout.HasValue)
                    timeout = item.MatchTimeout.Value;
                else
                    timeout = matchTimeout;

                if (item.Options.HasValue)
                    opt = item.Options.Value;
                else
                    opt = options;

                rslt.Add(new RegexCompilationInfo(item.Pattern, opt, item.Name, item.Namespace, true, timeout));
            }

            return rslt.ToArray();
        }

        /// <summary>
        /// Compile a enumeration into a Assembly file (.dll)
        /// </summary>
        /// <param name="name"></param>
        /// <param name="compilationInfos"></param>
        static public CompiledRegex CompileToAssembly(string name, IEnumerable<RegexCompilationInfo> compilationInfos)
        {
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));

            name = name.Trim();
            if (!name.RegexIsMatch(RegexHelper.ASCII_forCsharpNameSpace))
                throw new IO.InvalidPathException("Invalide " + nameof(name) + ". Contain a non-ASCII character.");
            if (name.RegexIsMatch(@"[\\/:*?\"" <>|]"))
                throw new IO.InvalidPathException("Invalide " + nameof(name) + ". Contain a invalid file name character.");

            return CompileToAssembly(new AssemblyName(name + ", Version=1.0.0, Culture=neutral, PublicKeyToken=null"), compilationInfos);
        }
        /// <summary>
        /// Compile a enumeration into a Assembly file (.dll)
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <param name="compilationInfos"></param>
        static public CompiledRegex CompileToAssembly(AssemblyName assemblyName, IEnumerable<RegexCompilationInfo> compilationInfos)
        {
            Regex.CompileToAssembly(compilationInfos.ToArray(), assemblyName);
            return LoadFromAssembly(Assembly.Load(assemblyName));
        }

        /// <summary>
        /// Generate a C# file corresponding to the collection
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="enumerable"></param>
        static public void GenerateFile(string filePath, IEnumerable<CompiledRegexBase> enumerable)
        {
            GenerateFile(filePath, enumerable, RegexHelper.RegexOptions);
        }
        /// <summary>
        /// Generate a C# file corresponding to the collection
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="enumerable"></param>
        /// <param name="options"></param>
        static public void GenerateFile(string filePath, IEnumerable<CompiledRegexBase> enumerable, RegexOptions options)
        {
            GenerateFile(filePath, enumerable, options, RegexHelper.Timeout);
        }
        /// <summary>
        /// Generate a C# file corresponding to the collection
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="enumerable"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        static public void GenerateFile(string filePath, IEnumerable<CompiledRegexBase> enumerable, RegexOptions options, TimeSpan matchTimeout)
        {
            GenerateFile(filePath, Convert(enumerable, options, matchTimeout));
        }
        /// <summary>
        /// Generate a C# file corresponding to the collection
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="compilationInfos"></param>
        static public void GenerateFile(string filePath, IEnumerable<RegexCompilationInfo> compilationInfos)
        {
            SortedDictionary<string, List<RegexCompilationInfo>> dic = new SortedDictionary<string, List<RegexCompilationInfo>>(StringComparer.InvariantCulture);
            
            foreach (var item in compilationInfos)
            {
                if (dic.ContainsKey(item.Namespace))
                    dic[item.Namespace].Add(item);
                else
                {
                    dic.Add(item.Namespace, new List<RegexCompilationInfo>());
                    dic[item.Namespace].Add(item);
                }
            }

            StringBuilder rslt = new StringBuilder(2000);
            rslt.AppendLine("using System;");
            rslt.AppendLine("using System.Text.RegularExpressions;");
            rslt.AppendLine();

            foreach (var item in dic)
            {
                rslt.AppendLine("namespace " + item.Key);
                rslt.AppendLine("{");
                foreach (var reg in item.Value)
                {
                    string[] split = RegexHelper.RegexOptions.ToString().Split(",");
                    for (int i = 0; i < split.Length; i++)
                        split[i] = "RegexOptions." + split[i].Trim();
                    string option = split.Join("|", StringJoinOptions.SkipNullAndWhiteSpace);
                    
                    rslt.AppendLine("public static class "+ reg.Name + " {");
                    rslt.AppendLine("static Regex _regex = new Regex(@\""+ reg.Pattern.Replace("\"", "\"\"") +"\", ("+ option + "), new TimeSpan("+ reg.MatchTimeout.Ticks.ToString(Globalization.CultureInfo.InvariantCulture) + "));");
                    rslt.AppendLine(Chromatik.Resources.RegexStatic);
                }
                rslt.AppendLine("}");

                string d = rslt.ToString();
            }

            IO.File.WriteAllText(filePath, rslt.ToString(), UTF8SansBomEncoding.UTF8SansBom);
        }



        /// <summary>
        /// Load all compiled regex in the Assembly
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        static public CompiledRegex LoadToAssembly(string assemblyName)
        {
            return LoadFromAssembly(AssemblyName.GetAssemblyName(assemblyName));
        }
        /// <summary>
        /// Load all compiled regex in the Assembly
        /// </summary>
        /// <param name="assemblyName"></param>
        /// <returns></returns>
        static public CompiledRegex LoadFromAssembly(AssemblyName assemblyName)
        {
            return LoadFromAssembly(Assembly.Load(assemblyName));
        }
        /// <summary>
        /// Load all compiled regex in a Assembly file (.dll)
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <returns></returns>
        static public CompiledRegex LoadFromAssemblyFile(string assemblyPath)
        {
            return LoadFromAssembly(Assembly.LoadFile(assemblyPath));
        }
        /// <summary>
        /// Load all compiled regex in the Assembly
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns></returns>
        static public CompiledRegex LoadFromAssembly(Assembly assembly)
        {
            return new CompiledRegex(assembly);
        }
        #endregion


        static private Type RegexType = new global::System.Text.RegularExpressions.Regex("\0").GetType();

        /// <summary>
        /// Source <see cref="SourceAssembly"/> of this collection
        /// </summary>
        public Assembly SourceAssembly { get; }

        /// <summary></summary>
        protected CompiledRegex(Assembly assembly)
        {
            SourceAssembly = assembly;
            foreach (Type item in SourceAssembly.ExportedTypes)
                if (item.IsInheritanceOf(RegexType))
                {
                    Regex regex = (global::System.Text.RegularExpressions.Regex)item.InvokeConstructor();
                    if (regex != null)
                        Items.Add(new CompiledRegexClass(regex));
                }
        }
        /// <summary></summary>
        protected CompiledRegex(CompiledRegexList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (!options.HasFlag(RegexOptions.Compiled))
                options |= RegexOptions.Compiled;

            List<Assembly> lst = new List<Assembly>();
            foreach (var item in list)
                Items.Add(new CompiledRegexClass(item, options, matchTimeout));

            SourceAssembly = Items.Last().GetType().Assembly;
        }

    }
}
