﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace System.Collections.Generic
{
    /// <summary>
    /// Create a <see cref="IComparer{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Comparator<T> : IComparer<T>, IComparer, IEqualityComparer<T>, IEqualityComparer
    {
        /// <summary></summary>
        static public Comparator<T> Default { get; } = new Comparator<T>();

        /// <summary>
        /// The <see cref="Type"/> of the comparator
        /// </summary>
        public Type TypeCompared;

        List<string> FieldComparator = new List<string>();

        /// <summary>
        /// Create a comparator
        /// </summary>
        public Comparator() : this(string.Empty)
        { }
        /// <summary>
        /// Create a comparator with a fields
        /// </summary>
        /// <param name="fields"></param>
        public Comparator(params string[] fields) : this(fields, true)
        { }
        /// <summary>
        /// Create a comparator with a fields
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="includePrivate"></param>
        public Comparator(string[] fields, bool includePrivate)
        {
            if (fields == null)
                fields = new string[0];
            
            TypeCompared = typeof(T);

            if (fields.Length > 0)
            {
                foreach (var fieldName in fields.TrimAll().Distinct().ToArray())
                {
                    FieldInfo field = TypeCompared.GetField(fieldName);
                    MemberInfo prop = TypeCompared.GetProperty(fieldName);

                    if (field != null)
                        foreach (string testInterface in new string[] { "IComparable`1", "IComparable", "IComparer`1", "IComparer" })
                            if (field.FieldType.GetInterface(testInterface) != null)
                            {
                                FieldComparator.Add(fieldName);
                                break;
                            }

                }
            }
        }

        /// <summary></summary>
        public virtual int Compare(T x, T y)
        {
            if (x == null && y == null)
                return 0;
            else if (y == null)
                return 1;
            else if (x == null)
                return -1;

            IComparable<T> iComparableT = x as IComparable<T>;
            if (iComparableT != null)
                return iComparableT.CompareTo(y);

            IComparable iComparable = x as IComparable;
            if (iComparable != null)
                return iComparable.CompareTo(y);

            IComparer<T> iComparerT = x as IComparer<T>;
            if (iComparerT != null)
                return iComparerT.Compare(x, y);

            IComparer iComparer = x as IComparer;
            if (iComparer != null)
                return iComparer.Compare(x, y);

            foreach (var item in FieldComparator)
            {

            }

            return 0;
        }

        /// <summary></summary>
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
                return 0;

            else if (y == null)
                return 1;
            else if (x == null)
                return -1;
            if (x is T != y is T)
            {
                try
                {
                    return Compare((T)x, (T)y);
                }
                catch
                {
                }
            }
            return 0;
        }

        /// <summary></summary>
        public bool Equals(T x, T y)
        {
            return (Compare(x, y) == 0);
        }
        /// <summary></summary>
        new public bool Equals(object x, object y)
        {
            return (Compare(x, y) == 0);
        }

        /// <summary></summary>
        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
        /// <summary></summary>
        public int GetHashCode(object obj)
        {
            return obj.GetHashCode();
        }
    }

    /// <summary>
    /// Create a <see cref="IComparer{T}"/> for a <see cref="KeyValuePair{TKey, TValue}"/>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class Comparator<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>, IComparer
    {
        #region Default
        static Comparator<TKey, TValue> comp { get; } = new Comparator<TKey, TValue>();

        /// <summary>
        /// The <see cref="IComparer{T}"/> for the Keys
        /// </summary>
        public static IComparer<TKey> DefaultKeyComparator { get; } = comp.KeyComparator;

        /// <summary>
        /// The <see cref="IComparer{T}"/> for the Values
        /// </summary>
        public static IComparer<TValue> DefaultalueComparator { get; } = comp.ValueComparator;

        /// <summary></summary>
        public static IComparer<KeyValuePair<TKey, TValue>> DefaultOnlyKeys { get; } = comp.OnlyKeys;

        /// <summary></summary>
        public static IComparer<KeyValuePair<TKey, TValue>> DefaultOnlyValues { get; } = comp.OnlyValues;

        /// <summary></summary>
        public static IComparer<KeyValuePair<TKey, TValue>> DefaultKeysAndValues { get; } = comp.KeysAndValues;

        /// <summary></summary>
        public static IComparer<KeyValuePair<TKey, TValue>> DefaultValuesAndKeys { get; } = comp.ValuesAndKeys;

        /// <summary>== KeysAndValues</summary>
        public static IComparer<KeyValuePair<TKey, TValue>> Default { get; } = comp.KeysAndValues;

        #endregion

        /// <summary>
        /// The <see cref="IComparer{T}"/> for the Keys
        /// </summary>
        public IComparer<TKey> KeyComparator { get; }

        /// <summary>
        /// The <see cref="Type"/> for the Keys
        /// </summary>
        public Type TypeOfKey;

        /// <summary>
        /// The <see cref="IComparer{T}"/> for the Values
        /// </summary>
        public IComparer<TValue> ValueComparator { get; }

        /// <summary>
        /// The <see cref="Type"/> for the Values
        /// </summary>
        public Type TypeOfValue;

        /// <summary></summary>
        public IComparer<KeyValuePair<TKey, TValue>> OnlyKeys { get; }

        /// <summary></summary>
        public IComparer<KeyValuePair<TKey, TValue>> OnlyValues { get; }

        /// <summary></summary>
        public IComparer<KeyValuePair<TKey, TValue>> KeysAndValues { get; }

        /// <summary></summary>
        public IComparer<KeyValuePair<TKey, TValue>> ValuesAndKeys { get; }

        /// <summary></summary>
        public Comparator() : this(null, null)
        { }
        /// <summary></summary>
        public Comparator(IComparer<TKey> keyCompare, IComparer<TValue> valueCompare)
        {
            if (keyCompare == null)
                KeyComparator = Comparator<TKey>.Default;
            else
                KeyComparator = keyCompare;
            
            if (valueCompare == null)
                ValueComparator = Comparator<TValue>.Default;
            else
                ValueComparator = valueCompare;

            OnlyKeys = new Comparator<TKey, TValue>(KeyComparator, ValueComparator, true, true);
            OnlyValues = new Comparator<TKey, TValue>(KeyComparator, ValueComparator, false, true);

            KeysAndValues = new Comparator<TKey, TValue>(KeyComparator, ValueComparator, true, true);
            ValuesAndKeys = new Comparator<TKey, TValue>(KeyComparator, ValueComparator, false, true);
        }
        private Comparator(IComparer<TKey> keyCompare, IComparer<TValue> valueCompare, bool keyFirst, bool fullPair)
        {
            KeyComparator = keyCompare;
            ValueComparator = valueCompare;
            this.keyFirst = keyFirst;
            this.fullPair = fullPair;
        }

        private bool keyFirst = true;
        private bool fullPair = true;

        /// <summary>
        /// Compare two <see cref="KeyValuePair{TKey, TValue}"/> <see langword="null"/>
        /// </summary>
        public int Compare(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
        {
            if (keyFirst)
            {
                int key = KeyComparator.Compare(x.Key, y.Key);
                if (key == 0 && fullPair)
                    return ValueComparator.Compare(x.Value, y.Value);
                else
                    return key;
            }
            else
            {
                int value = ValueComparator.Compare(x.Value, y.Value);
                if (value == 0 && fullPair)
                    return KeyComparator.Compare(x.Key, y.Key);
                else
                    return value;
            }

        }

        /// <summary></summary>
        public int Compare(object x, object y)
        {
            if (x == null && y == null)
                return 0;
            else if (y == null)
                return 1;
            else if (x == null)
                return -1;

            try
            {
                return Compare((KeyValuePair<TKey, TValue>)x, (KeyValuePair<TKey, TValue>)y);
            }
            catch
            {
                return 0;
            }
        }
    }
    
}
