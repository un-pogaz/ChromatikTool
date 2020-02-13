using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text.RegularExpressions
{
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
            Pattern = regex.ToString();
            Type t = _regex.GetType();

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
