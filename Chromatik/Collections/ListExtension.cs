using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Generic
{
    public enum ListKeyValueSort
    {
        OnlyKeys,
        OnlyValues,
        KeysAndValues,
        ValuesAndKeys
    }
    /// <summary>
    /// Static class to extend <see cref="List{T}"/>
    /// </summary>
    static public class ListExtension
    {
        static ListExtension() { }
        
        static public void Sort<TKey, TVlaue>(this List<KeyValuePair<TKey, TVlaue>> list)
        {
            list.Sort(Comparator<TKey, TVlaue>.Default);
        }
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
        static public void Add<TKey, TVlaue>(this List<KeyValuePair<TKey, TVlaue>> list, TKey key, TVlaue value)
        {
            list.Add(new KeyValuePair<TKey, TVlaue>(key, value));
        }
    }

}
