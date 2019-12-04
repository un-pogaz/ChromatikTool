using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Class to help in the creation of CASE value
    /// </summary>
    public class SQLiteCaseValue : Dictionary<string, string>
    {
        /// <summary>
        /// Name of the target column
        /// </summary>
        public string ColumnName { get; }

        /// <summary>
        /// Collection of the contitions
        /// </summary>
        new public string[] Keys { get { return base.Keys.ToArray(); } }

        /// <summary>
        /// Collection of the new values
        /// </summary>
        new public string[] Values { get { return base.Values.ToArray(); } }

        /// <summary>
        /// Creat a class to help in the creation of CASE value
        /// </summary>
        /// <param name="column"></param>
        public SQLiteCaseValue(string column) : base(StringComparer.InvariantCulture)
        {
            if (string.IsNullOrWhiteSpace(column))
                throw new ArgumentNullException(nameof(column));

            ColumnName = column.Trim();
        }

        /// <summary>
        /// Add a entrie
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="newValue"></param>
        new public void Add(string condition, string newValue)
        {
            if (string.IsNullOrWhiteSpace(condition))
                throw new ArgumentNullException(nameof(condition));
            if (string.IsNullOrWhiteSpace(newValue))
                throw new ArgumentNullException(nameof(newValue));
            
            base.Add(condition.Trim(), newValue.Trim());
        }

        /// <summary>
        /// Remove the entrie
        /// </summary>
        new public void Remove(string condition)
        {
            base.Remove(condition.Trim()); 
        }

        /// <summary>
        /// Get the CASE section of the request
        /// </summary>
        public string GetCASE()
        {
            string rslt = ColumnName + "= CASE\n";
            foreach (var item in this)
                rslt += "WHEN " + item.Key + " THEN " + item.Value + "\n";
            return rslt += "END";
        }
        /// <summary>
        /// Get the IN section of the request (for WHERE)
        /// </summary>
        public string GetOR()
        {
            return "("+ Keys.Join(") OR (") + ")";
        }

    }
}
