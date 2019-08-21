using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Specialized instance for work with the table content of a <see cref="SQLiteDataBase"/>
    /// </summary>
    public sealed class SQLiteData : SQLiteEdit
    {
        /// <summary>
        /// Create a basic instance for work with <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="db">The target database</param>
        public SQLiteData(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a specialized instance for work with the table content of a <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="db">The target database</param>
        /// <param name="openConnection">Open the connection with the data base</param>
        public SQLiteData(SQLiteDataBase db, bool openConnection) : base(db, openConnection)
        {
            clsName = "SQLiteData";
        }

        #region Add/Insert

        /// <summary>
        /// Add entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="values">Entries to add</param>
        /// <param name="msgErr"></param>
        public int Add(string tableName, string values, out SQLlog msgErr)
        {
            return Add(tableName, values, null, out msgErr);
        }
        /// <summary>
        /// Add entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="columns">null for the default column order</param>
        /// <param name="valeurs">Entries to add</param>
        /// <param name="msgErr"></param>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int Add(string tableName, string valeurs, string columns, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Add(tableName, valeurs, columns), out msgErr);
        }

        /// <summary>
        /// Create a SQL request for add entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to add</param>
        /// <returns></returns>
        static public string SQL_Add(string tableName, string valeurs)
        {
            return SQL_Add(tableName, valeurs, null);
        }
        /// <summary>
        /// Create a SQL request for add entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to add</param>
        /// <param name="columns">null for the default column order</param>
        /// <returns></returns>
        static public string SQL_Add(string tableName, string valeurs, string columns)
        {
            string SQL = "INSERT INTO ";
            if (string.IsNullOrWhiteSpace(tableName))
                SQL += " unknow_table ";
            else
                SQL += " '" + tableName.Trim() + "' ";

            if (!string.IsNullOrWhiteSpace(columns))
                SQL += " (" + columns.Trim() + ") ";

            SQL += " VALUES (" + valeurs.Trim() + ")";

            return SQL.Trim() + ";";
        }

        #endregion

        #region Updates

        /// <summary>
        /// Updates the table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">Can be null or empty</param>
        /// <param name="values">Values to updates</param>
        /// <param name="msgErr"></param>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int Update(string tableName, string whereValues, string values, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Update(tableName, whereValues, values), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for updates a table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">Can be null or empty</param>
        /// <param name="values">Values to updates</param>
        /// <returns></returns>
        static public string SQL_Update(string tableName, string whereValues, string values)
        {
            string SQL = "UPDATE ";
            if (string.IsNullOrWhiteSpace(tableName))
                SQL += " unknow_table ";
            else
                SQL += " '" + tableName.Trim() + "' ";

            if (!string.IsNullOrWhiteSpace(tableName))
                SQL += " SET " + values.Trim() + " ";

            if (!string.IsNullOrWhiteSpace(whereValues))
                SQL += " WHERE " + whereValues.Trim();

            return SQL.Trim() + ";";
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete the table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">Can be null or empty</param>
        /// <param name="msgErr"></param>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int Delete(string tableName, string whereValues, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Delete(tableName, whereValues), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for deleted a table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">Can be null or empty</param>
        /// <returns></returns>
        static public string SQL_Delete(string tableName, string whereValues)
        {
            string SQL = "DELETE FROM ";
            if (string.IsNullOrWhiteSpace(tableName))
                SQL += " unknow_table ";
            else
                SQL += " '" + tableName.Trim() + "' ";

            SQL += " WHERE " + whereValues.Trim();

            return SQL.Trim() + ";";
        }

        #endregion

        #region GetTable

        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="msgErr"></param>
        /// <returns>The DataTable request</returns>
        public DataTable GetTable(string tableName, out SQLlog msgErr)
        {
            return GetTable(tableName, null, null, out msgErr);
        }
        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="msgErr"></param>
        /// <returns>The DataTable request</returns>
        public DataTable GetTable(string tableName, string onlyColumns, out SQLlog msgErr)
        {
            return GetTable(tableName, onlyColumns, null, out msgErr);
        }
        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        /// <param name="msgErr"></param>
        /// <returns>The DataTable request</returns>
        public DataTable GetTable(string tableName, string onlyColumns, string orderColumns, out SQLlog msgErr)
        {
            return GetTableWhere(tableName, null, onlyColumns, orderColumns, out msgErr);
        }

        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="msgErr"></param>
        /// <returns>The DataTable request</returns>
        public DataTable GetTableWhere(string tableName, string whereValues, out SQLlog msgErr)
        {
            return GetTableWhere(tableName, whereValues, null, null, out msgErr);
        }
        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="msgErr"></param>
        /// <returns>The DataTable request</returns>
        public DataTable GetTableWhere(string tableName, string whereValues, string onlyColumns, out SQLlog msgErr)
        {
            return GetTableWhere(tableName, whereValues, onlyColumns, null, out msgErr);
        }
        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        /// <param name="msgErr"></param>
        /// <returns>The DataTable request</returns>
       public DataTable GetTableWhere(string tableName, string whereValues, string onlyColumns, string orderColumns, out SQLlog msgErr)
        {
            return ExecuteSQLdataTable(SQL_GetTableWhere(tableName, whereValues, onlyColumns, orderColumns), out msgErr);
        }


        #region Create SQL

        /// <summary>
        /// Create a SQL request for a table with all columns and in the default order.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        static public string SQL_GetTable(string tableName)
        {
            return SQL_GetTable(tableName, null, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns and in the default order.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="onlyColumns">null for all columns</param>
        static public string SQL_GetTable(string tableName, string onlyColumns)
        {
            return SQL_GetTable(tableName, onlyColumns, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns and in the desired order.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        static public string SQL_GetTable(string tableName, string onlyColumns, string orderColumns)
        {
            return SQL_GetTableWhere(tableName, null, onlyColumns, orderColumns);
        }
        /// <summary>
        /// Create a SQL request for a table with all columns, in the default order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">null to retrieve all values</param>
        static public string SQL_GetTableWhere(string tableName, string whereValues)
        {
            return SQL_GetTableWhere(tableName, whereValues, null, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns, in the default order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="orderColumns">null for all columns</param>
        static public string SQL_GetTableWhere(string tableName, string whereValues, string orderColumns)
        {
            return SQL_GetTableWhere(tableName, whereValues, orderColumns, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        static public string SQL_GetTableWhere(string tableName, string whereValues, string onlyColumns, string orderColumns)
        {
            string SQL = "SELECT";
            if (string.IsNullOrWhiteSpace(onlyColumns))
                SQL += " * ";
            else
                SQL += " " + onlyColumns.Trim() + " ";

            SQL += "FROM";
            if (string.IsNullOrWhiteSpace(tableName))
                SQL += " 'unknow_table' ";
            else
                SQL += " '" + tableName.Trim() + "' ";

            if (!string.IsNullOrWhiteSpace(whereValues))
                SQL += " WHERE " + whereValues.Trim() + " ";

            if (!string.IsNullOrWhiteSpace(orderColumns))
                SQL += " ORDER BY " + orderColumns.Trim() + " ";

            return SQL.Trim() + ";";
        }

        #endregion

        #endregion
    }
}
