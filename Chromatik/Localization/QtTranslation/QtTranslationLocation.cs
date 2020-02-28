using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.Globalization.Localization
{
    /// <summary></summary>
    public class QtTranslationLocation : IComparerEquatable<QtTranslationLocation>
    {
        /// <summary></summary>
        public QtTranslationLocation(string filename, int line)
        {
            Filename = filename;
            Line = line;
        }

        /// <summary>
        /// The filename of this location
        /// </summary>
        public string Filename {
            get { return _filename; }
            set
            {
                if (value == null)
                    value = string.Empty;

                _filename = value.Trim(WhiteCharacter.WhiteCharacters.Concat(ControlCharacter.ControlCharacters, ControlCharacterSupplement.ControlCharactersSupplements))
                    .Regex(WhiteCharacter.EndLineCharacters.Concat(ControlCharacter.ControlCharacters, ControlCharacterSupplement.ControlCharactersSupplements).ToOneString('|'), "");
            }
        }
        string _filename;

        /// <summary>
        /// The line in the file of this location
        /// </summary>
        public int Line {
            get { return line; }
            set {
                if (value < 0)
                {
                    value = 0;
                }
                line = value;
            }
        }
        int line;

        /// <summary></summary>
        public override int GetHashCode()
        {
            return Filename.GetHashCode() + Line;
        }
        /// <summary></summary>
        public override bool Equals(object obj)
        {
           return Equals(this, obj);
        }
        /// <summary></summary>
        public bool Equals(QtTranslationLocation obj)
        {
            return Equals(this, obj);
        }
        /// <summary></summary>
        public override string ToString()
        {
            return Filename + ">" + Line;
        }

        #region IComparer 

        /// <summary></summary>
        static public int Compare(QtTranslationLocation x, QtTranslationLocation y)
        {
            int s = string.Compare(x.Filename, y.Filename, StringComparison.InvariantCultureIgnoreCase);
            if (s == 0)
                return x.line.CompareTo(y.line);
            else
                return s;
        }
        int IComparer<QtTranslationLocation>.Compare(QtTranslationLocation x, QtTranslationLocation y)
        {
            return Compare(x, y);
        }
        /// <summary></summary>
        static public int Compare(object x, object y)
        {
            if (x == null && y == null)
                return 0;
            if (x is QtTranslationLocation && y is QtTranslationLocation)
                return Compare((QtTranslationLocation)x, (QtTranslationLocation)y);
            return 0;
        }
        int Collections.IComparer.Compare(object x, object y) { return Compare(x, y); }

        /// <summary></summary>
        public int CompareTo(object obj) { return Compare(this, obj); }
        /// <summary></summary>
        public int CompareTo(QtTranslationLocation value) { return Compare(this, value); }

        #endregion

        /// <summary></summary>
        static public bool Equals(QtTranslationLocation x, QtTranslationLocation y) { return Compare(x, y) == 0; }
        bool IEqualityComparer<QtTranslationLocation>.Equals(QtTranslationLocation x, QtTranslationLocation y) { return Equals(x, y); }

        /// <summary></summary>
        new static public bool Equals(object x, object y)
        {
            if (x == null && y == null)
                return true;
            if (x is QtTranslationLocation && y is QtTranslationLocation)
                return Equals((QtTranslationLocation)x, (QtTranslationLocation)y);
            return x.Equals(y);
        }
        bool Collections.IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }
        int Collections.IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }
        int IEqualityComparer<QtTranslationLocation>.GetHashCode(QtTranslationLocation obj) { return obj.GetHashCode(); }
    }
}
