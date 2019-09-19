using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;

namespace Chromatik.Zip
{
    /// <summary>
    /// Represents a collection of ZipEntry contained in the ZipFile.
    /// </summary>
    public class ZipEntryCollection : IReadOnlyList<ZipEntry>, IReadOnlyCollection<ZipEntry>, IEnumerable<ZipEntry>, IEnumerable
    {
        /// <summary>
        /// ! protected | <see cref="IList"/> containing the items in the collection.
        /// </summary>
        protected IList<ZipEntry> Items;

        /// <summary>
        /// ! internal ! Creat a new instance with the list.
        /// </summary>
        /// <param name="lst"></param>
        internal ZipEntryCollection(IList<ZipEntry> lst)
        {
            Items = lst;
        }

        /// <summary>
        /// Gets the ZipEntry located at the specified index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual ZipEntry this[int index]
        {
            get { return Items[index]; }
        }
        /// <summary>
        /// Gets the ZipEntry located at the specified FileName.
        /// </summary>
        /// <param name="entryName"></param>
        /// <returns>null if not found</returns>
        public virtual ZipEntry this[string entryName]
        {
            get
            {
                int index = IndexOf(entryName);

                if (index < 0)
                    return null;
                else
                    return Items[index];
            }
        }

        /// <summary>
        /// Obtains the number of elements contained in the collection.
        /// </summary>
        public int Count { get { return Items.Count; } }

        /// <summary>
        /// Gets the index of the specified ZipEntry.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public int IndexOf(ZipEntry entry)
        {
            return IndexOf(entry.FileName);
        }
        /// <summary>
        /// Gets the index of the specified FileName.
        /// </summary>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public int IndexOf(string entryName)
        {
            if (string.IsNullOrWhiteSpace(entryName))
                return -1;

            for (int i = 0; i < Items.Count; i++)
                if (entryName.Equals(Items[i].FileName, StringComparison.InvariantCultureIgnoreCase))
                    return i;

            return -1;
        }
        /// <summary>
        /// Determines if an element existe in the collection.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public bool Contains(ZipEntry entry)
        {
            return (IndexOf(entry) >= 0);
        }
        /// <summary>
        /// Determines if an FileName existe in the collection.
        /// </summary>
        /// <param name="entryName"></param>
        /// <returns></returns>
        public bool Contains(string entryName)
        {
            return (IndexOf(entryName) >= 0);
        }

        /// <summary>
        /// Get the Enumerator.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ZipEntry> GetEnumerator()
        {
            return Items.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
