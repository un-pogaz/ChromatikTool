using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// Base list for <see cref="RegexCompiledList"/> and <see cref="RegexCompiled"/>
    /// </summary>
    public abstract class RegexCompiledList<T> : Collections.ObjectModel.ReadOnlyCollection<RegexCompiledBase> where T : RegexCompiledBase
    {
        new public int Count { get { return Items.Count; } }

        new public T this[int index] { get { return (T)Items[index]; } }

        new public bool Contains(T value) { return Items.Contains(value); }
        new public void CopyTo(T[] array, int index) { Items.CopyTo(array, index); }
        new public int IndexOf(T value) { return Items.IndexOf(value); }

        new public IEnumerator<T> GetEnumerator() { return Items.OfType<T>().GetEnumerator(); }

        /// <summary></summary>
        public RegexCompiledList() : base(new List<RegexCompiledBase>())
        { }

    }

    public class RegexCompiledList : RegexCompiledList<RegexCompiledEntry>
    {
        public void Add(RegexCompiledEntry item)
        {
            Items.Add(item);
        }
    }

    /// <summary>
    /// Collection of <see cref="RegexCompiledClass"/>
    /// </summary>
    public class RegexCompiled : RegexCompiledList<RegexCompiledClass>
    {
        #region static
        
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

        static public RegexCompiled LoadAssembly(string assemblyName)
        {
            return LoadAssembly(Reflection.AssemblyName.GetAssemblyName(assemblyName));
        }
        static public RegexCompiled LoadAssembly(Reflection.AssemblyName assemblyName)
        {
            return LoadAssembly(Reflection.Assembly.Load(assemblyName));
        }
        static public RegexCompiled LoadAssemblyFile(string assemblyPath)
        {
            return LoadAssembly(Reflection.Assembly.LoadFile(assemblyPath));
        }
        static public RegexCompiled LoadAssembly(Reflection.Assembly assembly)
        {
            return new RegexCompiled(assembly);
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
            if (name.RegexIsMatch(RegexHelper.ASCII_forCsharpNameSpace))
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
                throw new ArgumentException("The input list must have at least one entry.", nameof(list));

            if (options.HasFlag(RegexOptions.Compiled))
                options ^= RegexOptions.Compiled;

            List<RegexCompilationInfo> lst = new List<RegexCompilationInfo>(list.Count);

            foreach (var item in list)
                lst.Add(new RegexCompilationInfo(item.Pattern, options, item.Name, item.Namespace, true, matchTimeout));

            Regex.CompileToAssembly(lst.ToArray(), assemblyName);

            return IO.Path.GetFullPath(assemblyName.Name + ".dll");
        }
        #endregion

        
        public Reflection.Assembly Assembly { get; }

        protected RegexCompiled(Reflection.Assembly assembly)
        {
            Assembly = assembly;
        }
        protected RegexCompiled(RegexCompiledList list, RegexOptions options, TimeSpan matchTimeout)
        {
            if (!options.HasFlag(RegexOptions.Compiled))
                options |= RegexOptions.Compiled;

            List<Reflection.Assembly> lst = new List<Reflection.Assembly>();
            foreach (var item in list)
                Items.Add(new RegexCompiledClass(item, options, matchTimeout));
        }

    }
}
