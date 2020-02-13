using System;
using System.Collections.Generic;
using System.Text;

namespace System.Text.RegularExpressions
{
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
    }
}
