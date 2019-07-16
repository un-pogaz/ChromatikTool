using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Specialized instance for work with the table content of a SQLite database
    /// </summary>
    public sealed class SQLiteData : SQLiteEdit
    {
        /// <summary>
        /// Create a specialized instance for work with the table content of a SQLite database
        /// </summary>
        /// <param name="db">Taget database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteData(SQLiteDataBase db, bool OpenConnection) : base(db, OpenConnection)
        {
            clsName = "SQLiteData";
        }
        /// <summary>
        /// Create a specialized instance for work with the table content of a SQLite database
        /// </summary>
        /// <param name="db">Taget database</param>
        public SQLiteData(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a specialized instance for work with the table content of a SQLite database
        /// </summary>
        /// <param name="dbPath">Path if the taget database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteData(string dbPath, bool OpenConnection) : this(SQLiteDataBase.LoadDataBase(dbPath), OpenConnection)
        { }
        /// <summary>
        /// Create a specialized instance for work with the table content of a SQLite database
        /// </summary>
        /// <param name="dbPath">Path if the taget database</param>
        public SQLiteData(string dbPath) : this(SQLiteDataBase.LoadDataBase(dbPath), false)
        { }

        #region Add/Insert

        /// <summary>
        /// Add entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to add</param>
        /// <param name="msgErr"></param>
        public int Add(string tableName, string valeurs, out SQLerr msgErr)
        {
            return Add(tableName, valeurs, null, out msgErr);
        }
        /// <summary>
        /// Add entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="columns">null for the default column order</param>
        /// <param name="valeurs">Entries to add</param>
        /// <param name="msgErr"></param>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int Add(string tableName, string valeurs, string columns, out SQLerr msgErr)
        {
            return SQLcommand(CreatSQL_Add(tableName, valeurs, columns), out msgErr);
        }

        /// <summary>
        /// Create a SQL request for add entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to add</param>
        /// <returns></returns>
        static public string CreatSQL_Add(string tableName, string valeurs)
        {
            return CreatSQL_Add(tableName, valeurs, null);
        }
        /// <summary>
        /// Create a SQL request for add entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to add</param>
        /// <param name="columns">null for the default column order</param>
        /// <returns></returns>
        static public string CreatSQL_Add(string tableName, string valeurs, string columns)
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
        /// Create a SQL request for updates a table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to updates</param>
        /// <param name="condition">Can be null or empty</param>
        /// <returns></returns>
        static public string CreatSQL_Update(string tableName, string valeurs, string condition)
        {
            string SQL = "UPDATE ";
            if (string.IsNullOrWhiteSpace(tableName))
                SQL += " unknow_table ";
            else
                SQL += " '" + tableName.Trim() + "' ";

            if (!string.IsNullOrWhiteSpace(tableName))
                SQL += " SET " + valeurs.Trim() + " ";

            SQL += " WHERE " + condition.Trim();

            return SQL.Trim() + ";";
        }
        /// <summary>
        /// Updates the table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to updates</param>
        /// <param name="condition">Can be null or empty</param>
        /// <param name="msgErr"></param>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int Update(string tableName, string valeurs, string condition, out SQLerr msgErr)
        {
            return SQLcommand(CreatSQL_Update(tableName, valeurs, condition), out msgErr);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Create a SQL request for deleted a table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="condition">Can be null or empty</param>
        /// <returns></returns>
        static public string CreatSQL_Delete(string tableName, string condition)
        {
            string SQL = "DELETE FROM ";
            if (string.IsNullOrWhiteSpace(tableName))
                SQL += " unknow_table ";
            else
                SQL += " '" + tableName.Trim() + "' ";

            SQL += " WHERE " + condition.Trim();

            return SQL.Trim() + ";";
        }
        /// <summary>
        /// Delete the table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="condition">Can be null or empty</param>
        /// <param name="msgErr"></param>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int Delete(string tableName, string condition, out SQLerr msgErr)
        {
            return SQLcommand(CreatSQL_Delete(tableName, condition), out msgErr);
        }

        #endregion

        #region GetTable

        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        /// <param name="msgErr"></param>
        /// <returns>The DataTable request</returns>
        public DataTable GetTable(string tableName, out SQLerr msgErr)
        {
            return GetTable(tableName, null, null, out msgErr);
        }
        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        /// <param name="msgErr"></param>
        /// <returns>The DataTable request</returns>
        public DataTable GetTable(string tableName, string onlyColumns, out SQLerr msgErr)
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
        public DataTable GetTable(string tableName, string onlyColumns, string orderColumns, out SQLerr msgErr)
        {
            return GetTableWhere(tableName, null, onlyColumns, orderColumns, out msgErr);
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
        public DataTable GetTableWhere(string tableName, string whereValues, out SQLerr msgErr)
        {
            return GetTableWhere(tableName, whereValues, null, null, out msgErr);
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
        public DataTable GetTableWhere(string tableName, string whereValues, string onlyColumns, out SQLerr msgErr)
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
       public DataTable GetTableWhere(string tableName, string whereValues, string onlyColumns, string orderColumns, out SQLerr msgErr)
        {
            return SQLdataTable(CreatSQL_GetTableWhere(tableName, whereValues, onlyColumns, orderColumns), out msgErr);
        }

        #endregion

        #region CreatSQL

        /// <summary>
        /// Create a SQL request for a table with all columns and in the default order.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        static public string CreatSQL_GetTable(string tableName)
        {
            return CreatSQL_GetTable(tableName, null, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns and in the default order.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="onlyColumns">null for all columns</param>
        static public string CreatSQL_GetTable(string tableName, string onlyColumns)
        {
            return CreatSQL_GetTable(tableName, onlyColumns, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns and in the desired order.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        static public string CreatSQL_GetTable(string tableName, string onlyColumns, string orderColumns)
        {
            return CreatSQL_GetTableWhere(tableName, null, onlyColumns, orderColumns);
        }
        /// <summary>
        /// Create a SQL request for a table with all columns, in the default order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">null to retrieve all values</param>
        static public string CreatSQL_GetTableWhere(string tableName, string whereValues)
        {
            return CreatSQL_GetTableWhere(tableName, whereValues, null, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns, in the default order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="OnlyColumns">null for all columns</param>
        static public string CreatSQL_GetTableWhere(string tableName, string whereValues, string orderColumns)
        {
            return CreatSQL_GetTableWhere(tableName, whereValues, orderColumns, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        static public string CreatSQL_GetTableWhere(string tableName, string whereValues, string onlyColumns, string orderColumns)
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

    }
}
