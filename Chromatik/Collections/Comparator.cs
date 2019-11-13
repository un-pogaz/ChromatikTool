using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// Create a <see cref="IComparer{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Comparator<T> : IComparer<T>
    {
        /// <summary> </summary>
        public static readonly Comparator<T> Default = new Comparator<T>();

        private Comparator() { }

        /// <summary> </summary>
        public int Compare(T x, T y)
        {
            IComparable<T> iComparableKey = x as IComparable<T>;
            if (iComparableKey != null)
                return iComparableKey.CompareTo(y);

            IComparable iComparable = x as IComparable;
            if (iComparable != null)
                return iComparable.CompareTo(y);

            IComparer<T> iComparer = x as IComparer<T>;
            if (iComparer != null)
                return iComparer.Compare(x, y);

            return 0;
        }
    }

    /// <summary>
    /// Create a <see cref="IComparer{T}"/> for a <see cref="KeyValuePair{TKey, TValue}"/>
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class Comparator<TKey, TValue> : IComparer<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// The <see cref="IComparer{T}"/> for the Keys
        /// </summary>
        public static IComparer<TKey> KeyComparator { get; } = Comparator<TKey>.Default;

        /// <summary>
        /// The <see cref="IComparer{T}"/> for the Values
        /// </summary>
        public static IComparer<TValue> ValueComparator { get; } = Comparator<TValue>.Default;


        /// <summary> </summary>
        public static Comparator<TKey, TValue> OnlyKeys { get; } = new Comparator<TKey, TValue>(true, false);

        /// <summary> </summary>
        public static Comparator<TKey, TValue> OnlyValues { get; } = new Comparator<TKey, TValue>(false, false);

        /// <summary> </summary>
        public static Comparator<TKey, TValue> KeysAndValues { get; } = new Comparator<TKey, TValue>(true, true);

        /// <summary> </summary>
        public static Comparator<TKey, TValue> ValuesAndKeys { get; } = new Comparator<TKey, TValue>(false, true);

        /// <summary>== KeysAndValues</summary>
        public static Comparator<TKey, TValue> Default { get; } = KeysAndValues;

        private Comparator(bool keyFirst, bool fullPair)
        {
            this.keyFirst = keyFirst;
            this.fullPair = fullPair;
        }

        private bool keyFirst = true;
        private bool fullPair = true;

        /// <summary>
        /// Compare two <see cref="KeyValuePair{TKey, TValue}"/>
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
    }

}
