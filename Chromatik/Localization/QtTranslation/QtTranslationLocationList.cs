using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization.Localization
{
    /// <summary></summary>
    public class QtTranslationLocationList : Collections.ObjectModel.ReadOnlyCollection<QtTranslationLocation>
    {
        /// <summary></summary>
        public QtTranslationLocationList() : base(new List<QtTranslationLocation>())
        { }

        /// <summary></summary>
        public void Add(QtTranslationLocation location)
        {
            if (!Contains(location))
            {
                Items.Add(location);
                ((List<QtTranslationLocation>)Items).Sort();
            }
        }
        /// <summary></summary>
        public bool Remove(QtTranslationLocation location)
        {
            int i = IndexOf(location);
            if (i >= 0)
            {
                Items.RemoveAt(i);
                return true;
            }
            return false;
        }
        /// <summary></summary>
        new public bool Contains(QtTranslationLocation location)
        {
            return (IndexOf(location) >= 0);
        }
        /// <summary></summary>
        new public int IndexOf(QtTranslationLocation location)
        {
            for (int i = 0; i < Count; i++)
                if (Items[i].Equals(location))
                    return i;

            return -1;
        }
    }
}
