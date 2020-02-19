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
    /// 
    public class ZipEntryCollection : AutoRefreshReadOnlyCollection<ZipEntry>
    {
        new protected ICollection<Ionic.Zip.ZipEntry> SourceCollection { get; }

        internal ZipEntryCollection(ICollection<Ionic.Zip.ZipEntry> collection) : base()
        {
            if (collection == null)
                throw new ArgumentNullException(null, "The source cannot be null.");

            SourceCollection = collection;
        }

        public ZipEntry this[string name] {
            get
            {
                foreach (ZipEntry item in this)
                    if (item.FileName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                        return item;

                return null;
            }
        }

        public override void Refresh()
        {
            Clear();
            foreach (var item in SourceCollection)
                Add(new ZipEntry(item));
        }

        public int IndexOf(string name)
        {
            for (int i = 0; i < Count; i++)
                if (this[i].FileName.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return i;

            return -1;
        }
    }
}
