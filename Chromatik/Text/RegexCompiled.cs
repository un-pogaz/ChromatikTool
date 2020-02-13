using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// Base list for <see cref="RegexCompiledList"/> and <see cref="RegexCompiled"/>
    /// </summary>
    public abstract class RegexCompiledList<T> : Collections.ObjectModel.ReadOnlyCollection<RegexCompiledBase> where T : RegexCompiledBase
    {
        /// <summary></summary>
        public RegexCompiledList() : base(new List<RegexCompiledBase>())
        {
        }

        protected List<RegexCompiledBase> Items { get { return (List<RegexCompiledBase >)base.Items; } }

        new public int Count { get { return Items.Count; } }

        new public T this[int index] { get { return (T)Items[index]; } }

        public T this[string fullqualifiedname] { get { return Find(fullqualifiedname); } }


        new public bool Contains(T value) { return Items.Contains(value); }
        new public bool Contains(string fullqualifiedname) { return Find(fullqualifiedname) == null; }
        new public bool Contains(string name, string fullnamespace) { return Find(name, fullnamespace) == null; }

        T Find(string fullqualifiedname)
        {
            KeyValuePair<string, string> pair = RegexCompiledBase.SplitFullQualifiedName(fullqualifiedname);
            return Find(pair.Key, pair.Value);
        }
        T Find(string name, string fullnamespace)
        {
            foreach (var item in Items)
                if (RegexCompiledBase.EqualsFullQualifiedName(item, name, fullnamespace))
                    return (T)item;

            return null;
        }

        new public void CopyTo(T[] array, int index) { Items.CopyTo(array, index); }
        new public int IndexOf(T value) { return Items.IndexOf(value); }

        new public IEnumerator<T> GetEnumerator() { return Items.OfType<T>().GetEnumerator(); }

    }

    public class RegexCompiledList : RegexCompiledList<RegexCompiledEntry>
    {
        public void Add(RegexCompiledEntry item)
        {
            Items.Add(item);
        }
        public void Remove(RegexCompiledEntry item)
        {

        }
    }

    /// <summary>
    /// Collection of <see cref="RegexCompiledClass"/>
    /// </summary>
    public class RegexCompiled : RegexCompiledList<RegexCompiledClass>
    {
        #region static
        
        static public RegexCompiled CompileToRuntime(RegexCompiledList list)
        {
            return CompileToRuntime(list, RegexHelper.RegexOptions);
        }
        static public RegexCompiled CompileToRuntime(RegexCompiledList list, RegexOptions options)
        {
            return CompileToRuntime(list, options, RegexHelper.Timeout);
        }
        static public RegexCompiled CompileToRuntime(RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout)
        {
            return new RegexCompiled(list, options, matchTimeout);
        }

        static public RegexCompiled LoadToAssembly(string assemblyName)
        {
            return LoadToAssembly(Reflection.AssemblyName.GetAssemblyName(assemblyName));
        }
        static public RegexCompiled LoadToAssembly(Reflection.AssemblyName assemblyName)
        {
            return LoadAssembly(Assembly.Load(assemblyName));
        }
        static public RegexCompiled LoadAssemblyFile(string assemblyPath)
        {
            return LoadAssembly(Reflection.Assembly.LoadFile(assemblyPath));
        }
        static public RegexCompiled LoadAssembly(Reflection.Assembly assembly)
        {
            return new RegexCompiled(assembly);
        }



        static public RegexCompiled CreateAssembly(string name, RegexCompiledList list)
        {
            return CreateAssembly(name, list, RegexHelper.RegexOptions);
        }
        static public RegexCompiled CreateAssembly(string name, RegexCompiledList list, RegexOptions options)
        {
            return CreateAssembly(name, list, options, RegexHelper.Timeout);
        }
        static public RegexCompiled CreateAssembly(string name, RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));

            name = name.Trim();
            if (!name.RegexIsMatch(RegexHelper.ASCII_forCsharpNameSpace))
                throw new IO.InvalidPathException("Invalide " + nameof(name) + ". Contain a non-ASCII character.");
            if (name.RegexIsMatch(@"[\\/:*?\"" <>|]"))
                throw new IO.InvalidPathException("Invalide " + nameof(name) + ". Contain a invalid file name character.");

            return CreateAssembly(new Reflection.AssemblyName(name + ", Version=1.0.0, Culture=neutral, PublicKeyToken=null"), list, options, matchTimeout);
        }
        static public RegexCompiled CreateAssembly(Reflection.AssemblyName assemblyName, RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (list.Count == 0)
                throw new ArgumentException("The input list must have at least one entry.", nameof(list));

            if (options.HasFlag(RegexOptions.Compiled))
                options ^= RegexOptions.Compiled;

            List<RegexCompilationInfo> lst = new List<RegexCompilationInfo>(list.Count);

            foreach (var item in list)
                lst.Add(new RegexCompilationInfo(item.Pattern, options, item.Name, item.Namespace, true, matchTimeout));

            Regex.CompileToAssembly(lst.ToArray(), assemblyName);

            return LoadAssembly(Reflection.Assembly.Load(assemblyName));
        }
        #endregion

        
        public Reflection.Assembly Assembly { get; }

        static private Type RegexType = new System.Text.RegularExpressions.Regex("\0").GetType();

        /// <summary></summary>
        protected RegexCompiled(Reflection.Assembly assembly)
        {
            Assembly = assembly;
            foreach (Type item in Assembly.ExportedTypes)
                if (item.BaseType == RegexType)
                    Items.Add(new RegexCompiledClass((System.Text.RegularExpressions.Regex)item.InvokeConstructor()));
        }
        protected RegexCompiled(RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (!options.HasFlag(RegexOptions.Compiled))
                options |= RegexOptions.Compiled;

            List<Reflection.Assembly> lst = new List<Reflection.Assembly>();
            foreach (var item in list)
                Items.Add(new RegexCompiledClass(item, options, matchTimeout));

            Assembly = Items.Last().GetType().Assembly;
        }

    }
}
