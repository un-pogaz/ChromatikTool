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
        /// Concatenate two array
        /// </summary>
        static public T[] Concat<T>(this T[] tbl, params T[] addition)
        {
            if (tbl == null)
                tbl = new T[0];
            
            if (addition == null)
                addition = new T[0];
            
            return Enumerable.Concat(tbl, addition).ToArray();
        }
        /// <summary>
        /// Compacte a jagged array whit a concatenate of these entrys
        /// </summary>
        static public T[] Concat<T>(this T[] tbl, params T[][] array)
        {
            if (tbl == null)
                tbl = new T[0];

            if (array == null)
                array = new T[0][];

            IEnumerable<T> rslt = tbl;
            foreach (T[] item in array)
                if (item != null)
                    rslt = rslt.Concat(item);
            
            return rslt.ToArray();
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
        /// Concatenate two array
        /// </summary>
        static public T[] Distinct<T>(this T[] tbl)
        {
            if (tbl == null)
                tbl = new T[0];

            return Enumerable.Distinct(tbl).ToArray();
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
    }
}