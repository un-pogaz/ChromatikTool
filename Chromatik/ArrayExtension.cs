using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Linq
{
    /// <summary>
    /// Class to extend System.Linq methods
    /// </summary>
    static public class ArrayExtension
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
        /// Convert <see cref="IEnumerable"/> to a <see cref="object"/> <see cref="IEnumerable{T}"/> collection
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        static public IEnumerable<object> ToObjectEnumerable(this IEnumerable source)
        {
            return source.OfType<object>();
        }
        /// <summary>
        /// Convert <see cref="IEnumerable"/> to a <see cref="object"/> array
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        static public object[] ToObjectArray(this IEnumerable source)
        {
            return source.ToObjectEnumerable().ToArray();
        }
        /// <summary>
        /// Obtains a filter array to the elements of a <see cref="IEnumerable"/> according to the specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        static public T[] OfTypeArray<T>(this IEnumerable source)
        {
            return source.OfType<T>().ToArray();
        }
    }
}