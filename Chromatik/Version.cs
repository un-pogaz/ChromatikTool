using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary></summary>
    public class VersionClass
    {
        /// <summary></summary>
        public int Major { get; } = 1;
        /// <summary></summary>
        public int Minor { get; } = 0;
        /// <summary></summary>
        public int Patch { get; } = 0;
        /// <summary></summary>
        public int Build { get; } = 0;
        
        /// <summary></summary>
        public override string ToString()
        {
            if (Build != 0)
                return Major + "." + Minor + "." + Patch + "." + Build;
            if (Patch != 0)
                return Major + "." + Minor + "." + Patch;
            if (Minor != 0)
                return Major + "." + Minor;

            return Major.ToString();
        }

        static int Value(string[] s, int index)
        {
            if (index < 0)
                index = 0;

            int rslt = 0;
            if (index < s.Length)
                int.TryParse(s[index], out rslt);
            return rslt;
        }

        /// <summary></summary>
        public VersionClass(string version)
        {
            string[] split = version.Regex("(release|pre-release|nightly|beta|alpha|V|R|B|A)", "", RegexHelper.RegexOptions | Text.RegularExpressions.RegexOptions.IgnoreCase).Trim(WhiteCharacter.WhiteCharacters).Split('.', '-', '|', '\\', '/');

            Major = Value(split, 0);
            Minor = Value(split, 1);
            Patch = Value(split, 2);
            Build = Value(split, 3);
        }
        /// <summary></summary>
        public VersionClass(int major) : this(major, 0)
        { }
        /// <summary></summary>
        public VersionClass(int major, int minor) : this(major, minor, 0)
        { }
        /// <summary></summary>
        public VersionClass(int major, int minor, int patch) : this(major, minor, patch, 0)
        { }
        /// <summary></summary>
        public VersionClass(int major, int minor, int patch, int build)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
            Build = build;
        }
    }
}
