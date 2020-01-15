using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;


using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Contracts;

namespace System.Globalization.Localization
{
    public class XliffIdentifiedListe<T> : Collections.ObjectModel.ReadOnlyCollection<T>
    {
        public XliffIdentifiedListe(ICollection<T> collection) : base(new T[0])
        {
            foreach (T item in collection)
                Add(item);
        }
        private void IDchangedEvent(object sender, StringChangedEventArgs args)
        {

        }

        const string XliffIdentifiedClass = "This action is not supported.\nOnly " + nameof(XliffIdentified) + " or inherited class are allowed.";

        new public T this[int index] { get { return Items[index]; } }

        #region ICollection<T>
        internal void Add(T item)
        {
            Insert(Count, item);
        }
        internal void Clear() { Items.Clear(); }
        internal bool Remove(T item) { return Items.Remove(item); }
        #endregion

        #region IList<T>
        internal void Insert(int index, T item)
        {
            if (item is XliffIdentified)
            {
                (item as XliffIdentified).IDchanged += IDchangedEvent;
                Items.Insert(index, item);
            }
            else
                throw new ArgumentException(XliffIdentifiedClass, nameof(item));
        }
        internal void RemoveAt(int index) { Items.RemoveAt(index); }
        #endregion
        
        public class IDCollection : Collections.ObjectModel.ReadOnlyCollection<string>
        {
            internal IDCollection(IdentifiedCollection list) : base(new string[0])
            {
            }
        }
    }
    public class XliffList<T> : Collections.ObjectModel.ReadOnlyCollection<T>
    {
        internal XliffList() : this(new T[0])
        { }
        internal XliffList(IList<T> collection) : base(collection)
        {
        }
    }
}
