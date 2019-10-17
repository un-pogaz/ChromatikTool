﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Linq
{
    /// <summary>
    /// Allows to Get a merged Array
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
        /// Concatenate a jagged array
        /// </summary>
        static public T[] Concat<T>(this T[] tbl, params T[][] array)
        {
            if (tbl == null)
                tbl = new T[0];

            if (array == null)
                array = new T[0][];

            IEnumerable<T> rslt = tbl;
            foreach (T[] item in array)
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