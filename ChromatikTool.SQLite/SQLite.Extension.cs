using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Data.SQLite;

namespace System.Data.SQLite
{
    /// <summary>
    /// Static class to extend System.Data.SQLite
    /// </summary>
    static public class SQLiteExtension
    {
        /// <summary>
        /// Get the <see cref="DataTable"/> from a <see cref="SQLiteDataReader"/>
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        static public DataTable GetDataTable(this SQLiteDataReader dataReader)
        {
            DataTable rslt = new DataTable(dataReader.GetTableName(0));
            foreach (DataRow item in dataReader.GetSchemaTable().Rows)
                rslt.Columns.Add(item[0].ToString(), item[12] as Type);

            while (dataReader.Read())
            {
                object[] tbl = new object[dataReader.FieldCount];
                for (int i = 0; i < dataReader.FieldCount; i++)
                    tbl[i] = dataReader[i];

                rslt.Rows.Add(tbl);
            }

            return rslt;
        }

        /// <summary>
        /// Parse a <see cref="string"/> to a valide text for SQLite request (useful if it contains a single quote)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string ToSQLiteFormat(this string input)
        {
            if (input == null || input.Equals("null", StringComparison.InvariantCultureIgnoreCase))
                return "NULL";
            else
                return "'" + input.Trim().Split('\'').Join("''") + "'";
        }
    }
}
