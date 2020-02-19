using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// Enume for sort a <see cref="List{T}"/> using sa <see cref="KeyValuePair{TKey, TVlaue}"/>
    /// </summary>
    public enum ListKeyValueSort
    {
        /// <summary></summary>
        OnlyKeys,
        /// <summary></summary>
        OnlyValues,
        /// <summary></summary>
        KeysAndValues,
        /// <summary></summary>
        ValuesAndKeys
    }
    /// <summary>
    /// Static class to extend <see cref="List{T}"/>
    /// </summary>
    static public class ListExtension
    {
        static ListExtension() { }

        /// <summary></summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVlaue"></typeparam>
        /// <param name="list"></param>
        static public void Sort<TKey, TVlaue>(this List<KeyValuePair<TKey, TVlaue>> list)
        {
            list.Sort(Comparator<TKey, TVlaue>.Default);
        }
        /// <summary></summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVlaue"></typeparam>
        /// <param name="list"></param>
        /// <param name="KeyValueSort"></param>
        static public void Sort<TKey, TVlaue>(this List<KeyValuePair<TKey, TVlaue>> list, ListKeyValueSort KeyValueSort)
        {
            switch (KeyValueSort)
            {
                case ListKeyValueSort.OnlyKeys:
                    list.Sort(Comparator<TKey, TVlaue>.DefaultOnlyKeys);
                    break;
                case ListKeyValueSort.OnlyValues:
                    list.Sort(Comparator<TKey, TVlaue>.DefaultOnlyValues);
                    break;
                case ListKeyValueSort.KeysAndValues:
                    list.Sort(Comparator<TKey, TVlaue>.DefaultKeysAndValues);
                    break;
                case ListKeyValueSort.ValuesAndKeys:
                    list.Sort(Comparator<TKey, TVlaue>.DefaultValuesAndKeys);
                    break;
                default:
                    list.Sort(Comparator<TKey, TVlaue>.Default);
                    break;
            }
        }
        /// <summary></summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TVlaue"></typeparam>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        static public void Add<TKey, TVlaue>(this List<KeyValuePair<TKey, TVlaue>> list, TKey key, TVlaue value)
        {
            list.Add(new KeyValuePair<TKey, TVlaue>(key, value));
        }
    }

}
