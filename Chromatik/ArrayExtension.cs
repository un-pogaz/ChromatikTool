using System;
using System.Globalization;

namespace System.Linq
{
    /// <summary>
    /// Allows to Get a merged Array
    /// </summary>
    static public class ArrayExtension
    {
        /// <summary>
        /// Get a fusion with this array
        /// </summary>
        static public T[] Concat<T>(this T[] tbl, T[] addition)
        {
            if (tbl == null)
                tbl = new T[0];
            
            if (addition == null)
                addition = new T[0];

            return Enumerable.Concat(tbl, addition).ToArray();
        }
        /// <summary>
        /// Get a fusion with all arrays
        /// </summary>
        static public T[] Concat<T>(this T[] tbl, T[][] array)
        {
            if (tbl == null)
                tbl = new T[0];

            if (array == null)
                array = new T[0][];

            foreach (T[] item in array)
                tbl = tbl.Concat(item);
            
            return tbl;
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
                throw new ArgumentOutOfRangeException("startIndex", "Negative index");

            if (startIndex > tbl.LongLength)
                throw new ArgumentOutOfRangeException("startIndex", "Start Index larger than Length");

            if (length < 0)
                throw new ArgumentOutOfRangeException("length", "Negative Length");

            if (startIndex + length > tbl.LongLength)
                throw new ArgumentOutOfRangeException("length", "Length selected out of limit");

            for (long i = 0; i < rslt.LongLength; i++)
                rslt[i] = tbl[startIndex + i];
            
            return rslt;
        }
    }
}