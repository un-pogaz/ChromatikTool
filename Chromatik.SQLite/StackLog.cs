using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Represents a collection of <see cref="SQLlog"/> of the last in, first out (LIFO) type for a <see cref="SQLiteDataBase"/>.
    /// </summary>
    public class StackLogSQL : IReadOnlyList<SQLlog>, IReadOnlyCollection<SQLlog>, IEnumerable<SQLlog>, System.Collections.IEnumerable
    {

        List<SQLlog> items = new List<SQLlog>(10);

        internal StackLogSQL()
        { }
        internal StackLogSQL(IEnumerable<SQLlog> collection)
        {
            items = new List<SQLlog>(collection);
        }

        /// <summary></summary>
        public SQLlog this[int index]
        {
            get { return items[index]; }
        }
        
        internal void AddEntry(SQLlog entry)
        {
            items.Insert(0, entry);

            while (items.Count > short.MaxValue)
                items.RemoveAt(items.Count - 1);
        }

        /// <summary></summary>
        public bool ContainFailedRequest { get { return (Failed.Count > 0); } }

        /// <summary>
        /// Collection of failed request only
        /// </summary>
        public IReadOnlyCollection<SQLlog> Failed
        {
            get
            {
                List<SQLlog> rslt = new List<SQLlog>(items.Count);
                foreach (var item in items)
                    if (!item.Succes)
                        rslt.Add(item);
                
                rslt.Capacity = rslt.Count;
                return rslt;
            }
        }

        /// <summary>
        /// List of all SQL request executed with this instance
        /// </summary>
        public string[] SQLrequest
        {
            get
            {
                List<string> lst = new List<string>();
                foreach (SQLlog item in items)
                    lst.Add(item.SQL);
                return lst.ToArray();
            }
        }
        /// <summary>
        /// List of all SQL request executed with this instance
        /// </summary>
        public string[] SQLfailed
        {
            get
            {
                List<string> lst = new List<string>();
                foreach (SQLlog item in Failed)
                    lst.Add(item.SQL);
                return lst.ToArray();
            }
        }

        /// <summary></summary>
        public IEnumerator<SQLlog> GetEnumerator()
        {
            return items.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        /// <summary></summary>
        public int Count { get { return items.Count; } }
    }
}
