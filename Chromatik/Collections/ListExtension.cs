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
                    list.Sort(Comparator<TKey, TVlaue>.OnlyKeys);
                    break;
                case ListKeyValueSort.OnlyValues:
                    list.Sort(Comparator<TKey, TVlaue>.OnlyValues);
                    break;
                case ListKeyValueSort.KeysAndValues:
                    list.Sort(Comparator<TKey, TVlaue>.KeysAndValues);
                    break;
                case ListKeyValueSort.ValuesAndKeys:
                    list.Sort(Comparator<TKey, TVlaue>.ValuesAndKeys);
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
