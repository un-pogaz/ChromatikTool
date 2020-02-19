using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace System.IO
{
    /// <summary>
    /// Class for string compar withe the system file
    /// </summary>
    public class FileSystemComparator : Comparator<string>
    {
        /// <summary></summary>
        static public FileSystemComparator CurrentCulture { get; } = new FileSystemComparator(StringComparer.CurrentCultureIgnoreCase, true);
        /// <summary></summary>
        static public FileSystemComparator CurrentCultureFolderAfter { get; } = new FileSystemComparator(StringComparer.CurrentCultureIgnoreCase, true);

        /// <summary></summary>
        static public FileSystemComparator InvariantCulture { get; } = new FileSystemComparator(StringComparer.InvariantCultureIgnoreCase, true);
        /// <summary></summary>
        static public FileSystemComparator InvariantCultureFolderAfter { get; } = new FileSystemComparator(StringComparer.InvariantCultureIgnoreCase, true);
        
        /// <summary></summary>
        static public FileSystemComparator Ordinal { get; } = new FileSystemComparator(StringComparer.OrdinalIgnoreCase, true);
        /// <summary></summary>
        static public FileSystemComparator OrdinalFolderAfter { get; } = new FileSystemComparator(StringComparer.OrdinalIgnoreCase, true);
        
        /// <summary></summary>
        new static public FileSystemComparator Default { get { return CurrentCulture; } }
        
        /// <summary>
        /// Create a new <see cref="FileSystemComparator"/> with the specified culture.
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="folderAfter"></param>
        /// <returns></returns>
        static public FileSystemComparator Create(Globalization.CultureInfo culture, bool folderAfter) { return new FileSystemComparator(StringComparer.Create(culture, true), folderAfter); }

        private StringComparer _stringComparer;
        private bool _folderAfter;

        /// <summary></summary>
        protected FileSystemComparator(StringComparer stringComparer, bool folderAfter)
        {
            _stringComparer = stringComparer;
            _folderAfter = folderAfter;
        }

        /// <summary></summary>
        public override int Compare(string x, string y)
        {
            if ((x.Contains(Path.DirectorySeparatorChar) || x.Contains(Path.AltDirectorySeparatorChar)) &&
                (!y.Contains(Path.DirectorySeparatorChar) && !y.Contains(Path.AltDirectorySeparatorChar)))
            {
                return -1;
            }
            else
            if ((!x.Contains(Path.DirectorySeparatorChar) && !x.Contains(Path.AltDirectorySeparatorChar)) &&
                (y.Contains(Path.DirectorySeparatorChar) || y.Contains(Path.AltDirectorySeparatorChar)))
            {
                return 1;
            }

            return  _stringComparer.Compare(x, y);
        }
    }
}
