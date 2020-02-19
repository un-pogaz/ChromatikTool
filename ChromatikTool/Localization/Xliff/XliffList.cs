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

        internal XliffIdentifiedListe() : this(new T[0])
        { }
        internal XliffIdentifiedListe(ICollection<T> collection) : base(new List<T>())
        {
            collection.ForEach(Add);
        }
        private void IDchangedEvent(object sender, StringChangedEventArgs e)
        {
            if (IDs.Contains(e.ApplyString))
            {

            }
            else
            {

            }

        }

        const string XliffIdentifiedClass = "This action is not supported.\nOnly " + nameof(XliffIdentified) + " or inherited class are allowed.";

        new public T this[int index] { get { return Items[index]; } }

        /// <summary>
        /// Represents a collection of IDs from the <see cref="XliffIdentifiedListe{T}"/>
        /// </summary>
        public class IDCollection : System.Collections.ObjectModel.AutoRefreshReadOnlyCollection<string>
        {
            new protected XliffIdentifiedListe<T> SourceCollection { get; }
            internal IDCollection(XliffIdentifiedListe<T> identifiedListe) : base()
            {
                if (identifiedListe == null)
                    throw new ArgumentNullException(null, "The source " + nameof(XliffIdentifiedListe<T>) + " cannot be null.");

                SourceCollection = identifiedListe;
            }

            /// <summary>
            /// Refresh the content of the <see cref="IDCollection"/>
            /// </summary>
            public override void Refresh()
            {
                Clear();
                foreach (XliffIdentified item in SourceCollection.OfType<XliffIdentified>())
                    Add(item.ID);
            }
        }

        IDCollection _IDs;
        public IDCollection IDs
        {
            get
            {
                if (_IDs == null)
                    _IDs = new IDCollection(this);

                return _IDs;
            }
        }


        public class IdentifiedCollection : Collections.ObjectModel.AutoRefreshReadOnlyCollection<T>
        {
            internal IdentifiedCollection(XliffIdentifiedListe<T> identifiedListe) : base(identifiedListe)
            { }
        }

        IdentifiedCollection _identifieds;
        public IdentifiedCollection Identifieds
        {
            get
            {
                if (_identifieds == null)
                    _identifieds = new IdentifiedCollection(this);

                return _identifieds;
            }
        }

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
