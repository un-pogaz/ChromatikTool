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
    public class XliffList<T> : Collections.ObjectModel.ReadOnlyObservableCollection<T>, IDictionary<string, T>, IReadOnlyDictionary<string, T>, IDictionary
    {
        internal XliffList() : this(new T[0])
        { }
        public XliffList(ICollection<T> collection) : base(new Collections.ObjectModel.ObservableCollection<T>(new T[0]))
        {
            CollectionChanged += CollectionChangedEvent;
            PropertyChanged += PropertyChangedEvent;

            foreach (T item in collection)
                Add(item);
        }
        private void CollectionChangedEvent(object sender, NotifyCollectionChangedEventArgs args)
        {
        }
        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs args)
        {
        }
        private void IDchangedEvent(object sender, StringChangedEventArgs args)
        {
            if (args.ApplyString != null && ContainsID(args.ApplyString))
                throw new XliffException(new ArgumentOutOfRangeException("ID", "The collection already contains a entry with this ID."));
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

        #region ReadOnlyObservableCollection

        new internal event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { base.CollectionChanged += value; }
            remove { base.CollectionChanged -= value; }
        }
        new internal event PropertyChangedEventHandler PropertyChanged
        {
            add { base.PropertyChanged += value; }
            remove { base.PropertyChanged -= value; }
        }

        new internal void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            base.OnCollectionChanged(args);
        }
        new internal void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
        }

        #endregion

        public class IdentifiedDictionary : Collections.ObjectModel.ReadOnlyDictionary<string, T>
        {
            internal IdentifiedDictionary() : base(new Dictionary<string, T>())
            { }
            internal IdentifiedDictionary(IDictionary<string, T> dictionary) : base(dictionary)
            { }

            internal IdentifiedDictionary Refresh(IEnumerable<T> dictionary)
            {
                Dictionary.Clear();
                foreach (var item in dictionary)
                    if (item is XliffIdentified)
                        Dictionary.Add((item as XliffIdentified).ID, item);

                return this;
            }

            internal bool ContainsValue(T value)
            {
                foreach (var item in Dictionary)
                    if (item.Value.Equals(value))
                        return true;

                return false;
            }
            
        }

        #region Dictionary Identified
        [NonSerialized]
        IdentifiedDictionary _identified;
        public IdentifiedDictionary Identified {
            get
            {
                if (_identified == null)
                    _identified = new IdentifiedDictionary();
                
                return _identified.Refresh(Items);
            }
        }
        

        public T this[string id]
        {
            get
            {
                if (ContainsID(id))
                    return Identified[id];
                else
                    return default(T);
            }
        }

        public bool ContainsID(string id) { return Identified.ContainsKey(id); }
        public bool ContainsIdentified(T identified) { return Identified.ContainsValue(identified); }
        public bool TryGetIdentified(string key, out T value) { return Identified.TryGetValue(key, out value); }

        [Serializable]
        public sealed class KeyCollection : Collections.ObjectModel.ReadOnlyCollection<string>
        {
            internal KeyCollection() : base(new string[0])
            { }
            internal KeyCollection(IList<string> collection) : base(collection)
            { }

            internal KeyCollection Refresh(IEnumerable<string> collection)
            {
                Items.Clear();
                foreach (string item in collection)
                    Items.Add(item);

                return this;
            }
        }

        [Serializable]
        public sealed class ValueCollection : Collections.ObjectModel.ReadOnlyCollection<T>
        {
            internal ValueCollection() : base(new T[0])
            { }
            internal ValueCollection(IList<T> collection) : base(collection)
            { }

            internal ValueCollection Refresh(IEnumerable<T> collection)
            {
                Items.Clear();
                foreach (T item in collection)
                    Items.Add(item);

                return this;
            }
        }

        #region IEnumerable<KeyValuePair<string, T>> Members

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator() { return Identified.GetEnumerator(); }

        #endregion

        #region Private

        [NonSerialized]
        private KeyCollection _keys;
        private KeyCollection Keys
        {
            get {
                if (_keys == null)
                    _keys = new KeyCollection();
                return _keys.Refresh(Identified.Keys);
            }
        }

        [NonSerialized]
        private ValueCollection _values;
        private ValueCollection Values
        {
            get {
                if (_values == null)
                    _values = new ValueCollection();
                return _values.Refresh(Identified.Values);
            }
        }

        [NonSerialized]
        private Object _syncRoot;

        #region IDictionary<string, T> Members

        T IDictionary<string, T>.this[string key]
        {
            get { return Identified[key]; }
            set { throw XliffException.NotSupported_ReadOnlyCollection; }
        }

        ICollection<string> IDictionary<string, T>.Keys { get { return Keys; } }
        ICollection<T> IDictionary<string, T>.Values { get { return Values; } }

        void IDictionary<string, T>.Add(string key, T value) { Identified.Add(value); }
        bool IDictionary<string, T>.Remove(string key) { return Identified.Remove(key); }
        bool IDictionary<string, T>.ContainsKey(string key) { return Identified.ContainsKey(key); }
        bool IDictionary<string, T>.TryGetValue(string key, out T value) { return Identified.TryGetValue(key, out value); }

        #endregion

        #region IReadOnlyDictionary<string, T> members

        IEnumerable<string> IReadOnlyDictionary<string, T>.Keys { get { return Keys; } }

        IEnumerable<T> IReadOnlyDictionary<string, T>.Values { get { return Values; } }
        bool IReadOnlyDictionary<string, T>.ContainsKey(string key) { return Identified.ContainsKey(key); }
        bool IReadOnlyDictionary<string, T>.TryGetValue(string key, out T value) { return Identified.TryGetValue(key, out value); }

        #endregion IReadOnlyDictionary members

        #region ICollection<KeyValuePair<string, T>> Members

        bool ICollection<KeyValuePair<string, T>>.Contains(KeyValuePair<string, T> item) { return Identified.Contains(item); }

        void ICollection<KeyValuePair<string, T>>.Add(KeyValuePair<string, T> item) { throw XliffException.NotSupported_ReadOnlyCollection; }
        bool ICollection<KeyValuePair<string, T>>.Remove(KeyValuePair<string, T> item) { throw XliffException.NotSupported_ReadOnlyCollection; }
        void ICollection<KeyValuePair<string, T>>.Clear() { throw XliffException.NotSupported_ReadOnlyCollection; }

        void ICollection<KeyValuePair<string, T>>.CopyTo(KeyValuePair<string, T>[] array, int arrayIndex) { Identified.ToArray().CopyTo(array, arrayIndex); }

        bool ICollection<KeyValuePair<string, T>>.IsReadOnly { get; } = true;

        #endregion
        
        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return ((IEnumerable)Identified).GetEnumerator(); }

        #endregion

        #region IDictionary Members

        private static bool IsCompatibleKey(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return key is string;
        }

        object IDictionary.this[object key]
        {
            get
            {
                if (IsCompatibleKey(key))
                    return this[(string)key];

                return null;
            }
            set { throw XliffException.NotSupported_ReadOnlyCollection; }
        }

        ICollection IDictionary.Keys { get { return Keys; } }
        ICollection IDictionary.Values { get { return Values; } }

        bool IDictionary.Contains(object key) { return IsCompatibleKey(key) && ContainsID((string)key); }

        void IDictionary.Add(object key, object value) { throw XliffException.NotSupported_ReadOnlyCollection; }
        void IDictionary.Remove(object key) { throw XliffException.NotSupported_ReadOnlyCollection; }
        void IDictionary.Clear() { throw XliffException.NotSupported_ReadOnlyCollection; }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            IDictionary d = Identified as IDictionary;
            if (d != null)
                return d.GetEnumerator();
            return new DictionaryEnumerator(Identified);
        }

        void ICollection.CopyTo(Array array, int index) { Identified.ToArray().CopyTo(array, index); }

        bool IDictionary.IsFixedSize { get; } = true;
        bool IDictionary.IsReadOnly { get; } = true;
        bool ICollection.IsSynchronized { get; } = false;

        object ICollection.SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                {
                    ICollection c = Identified as ICollection;
                    if (c != null)
                    {
                        _syncRoot = c.SyncRoot;
                    }
                    else {
                        System.Threading.Interlocked.CompareExchange<Object>(ref _syncRoot, new Object(), null);
                    }
                }
                return _syncRoot;
            }
        }

        [Serializable]
        private struct DictionaryEnumerator : IDictionaryEnumerator
        {
            private readonly IDictionary<string, T> m_dictionary;
            private IEnumerator<KeyValuePair<string, T>> m_enumerator;

            public DictionaryEnumerator(IDictionary<string, T> dictionary)
            {
                m_dictionary = dictionary;
                m_enumerator = m_dictionary.GetEnumerator();
            }

            public DictionaryEntry Entry { get { return new DictionaryEntry(m_enumerator.Current.Key, m_enumerator.Current.Value); } }

            public object Key { get { return m_enumerator.Current.Key; } }

            public object Value { get { return m_enumerator.Current.Value; } }

            public object Current { get { return Entry; } }

            public bool MoveNext() { return m_enumerator.MoveNext(); }

            public void Reset() { m_enumerator.Reset(); }
        }

        #endregion

        #endregion

        #endregion Dictionary

    }
    public class XliffIdentifiedListe<T> : XliffList<T>
    {
        internal XliffIdentifiedListe() : this(new T[0])
        { }
        internal XliffIdentifiedListe(ICollection<T> collection) : base(collection)
        {
            CollectionChanged += CollectionChangedEvent;
            PropertyChanged += PropertyChangedEvent;
        }
        
        private void CollectionChangedEvent(object sender, NotifyCollectionChangedEventArgs args)
        {

        }
        private void PropertyChangedEvent(object sender, PropertyChangedEventArgs args)
        {

        }
    }
}
