using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Linq
{
    /// <summary>
    /// Class to extend System.Linq methods
    /// </summary>
    static public class EnumerableExtension
    {
        /// <summary>
        /// Concatenate a jagged array whit a concatenate of these entrys
        /// </summary>
        static public IEnumerable<T> Concat<T>(this IEnumerable<T> first, params IEnumerable<T>[] additions)
        {
            IEnumerable<T> rslt = first;
            foreach (T[] item in additions)
                if (item != null)
                    rslt = rslt.Concat(item);

            return rslt;
        }
        /// <summary>
        /// Concatenate two array
        /// </summary>
        static public T[] Concat<T>(this T[] first, params T[] additions)
        {
            if (first == null)
                first = new T[0];
            
            if (additions == null)
                additions = new T[0];
            
            return Enumerable.Concat(first, additions).ToArray();
        }
        /// <summary>
        /// Concatenate a jagged array whit a concatenate of these entrys
        /// </summary>
        static public T[] Concat<T>(this T[] first, params T[][] additions)
        {
            if (first == null)
                first = new T[0];

            if (additions == null)
                additions = new T[0][];
            
            return first.Concat(additions as IEnumerable<T>[]).ToArray();
        }

        /// <summary>
        /// Get a sub array
        /// </summary>
        static public T[] SubArray<T>(this T[] tbl, long startIndex)
        {
            return tbl.SubArray(startIndex, tbl.LongLength - startIndex);
        }
        /// <summary>
        /// Get a sub array
        /// </summary>
        static public T[] SubArray<T>(this T[] tbl, long startIndex, long length)
        {
            if (tbl == null)
                tbl = new T[0];
            T[] rslt = new T[length];

            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Negative index");

            if (startIndex > tbl.LongLength)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Start Index larger than Length");

            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Negative Length");

            if (startIndex + length > tbl.LongLength)
                throw new ArgumentOutOfRangeException(nameof(length), "Length selected out of limit");

            for (long i = 0; i < rslt.LongLength; i++)
                rslt[i] = tbl[startIndex + i];
            
            return rslt;
        }

        /// <summary>
        /// Returns separate elements of a sequence and uses the default equality comparator to compare values.
        /// </summary>
        static public T[] Distinct<T>(this T[] tbl)
        {
            if (tbl == null)
                tbl = new T[0];

            return Enumerable.Distinct(tbl).ToArray();
        }
        /// <summary>
        /// Returns separate elements of a sequence and uses the specified <see cref="IEqualityComparer{T}"/> to compare values.
        /// </summary>
        static public T[] Distinct<T>(this T[] tbl, IEqualityComparer<T> comparer)
        {
            if (tbl == null)
                tbl = new T[0];

            return Enumerable.Distinct(tbl, comparer).ToArray();
        }
        
        /// <summary>
        /// Reverses the order of the elements in a sequence
        /// </summary>
        static public T[] Reverse<T>(this T[] tbl)
        {
            if (tbl == null)
                tbl = new T[0];

            return Enumerable.Reverse(tbl).ToArray();
        }
        /// <summary>
        /// Obtains a filter array to the elements of a <see cref="IEnumerable"/> according to the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        static public T[] OfType<T>(this Array source)
        {
            return Enumerable.OfType<T>(source).ToArray();
        }

        /// <summary>
        /// Set all entry of a array to the specified value.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T[] SetAllValue<T>(this T[] input, T value)
        {
            if (input != null)
                for (int i = 0; i < input.Length; i++)
                    input[0] = value;

            return input;
        }
        /// <summary>
        /// Test if the enumeration is empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }
        /// <summary>
        /// Test if the enumeration contains a single entry
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsSingle<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() == 1;
        }

        /// <summary>
        /// Executes an <see cref="Action"/> for each element of an instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action(item);

            return enumerable;
        }

        /// <summary>
        /// Get the duplicate entry in a enumeration
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        static public T[] GetDuplicate<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.GetDuplicateCounted().Keys.ToArray();
        }

        /// <summary>
        /// Get the duplicate entry in a enumeration with the count
        /// </summary>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        static public IDictionary<T, int> GetDuplicateCounted<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .ToDictionary(x => x.Key, y => y.Count());
        }
    }
}