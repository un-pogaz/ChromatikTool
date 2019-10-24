using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            clsName = nameof(SQLiteData);
        }

        #region Insert

        /// <summary>
        /// Add one entrie to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="values">Entries to add</param>
        /// <param name="msgErr"></param>
        public int Insert(string tableName, string values, out SQLlog msgErr)
        {
            return Insert(tableName, values, null, out msgErr);
        }
        /// <summary>
        /// Add multi entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="values">Entries to add</param>
        /// <param name="msgErr"></param>
        public int Insert(string tableName, string[] values, out SQLlog msgErr)
        {
            return Insert(tableName, values, null, out msgErr);
        }
        /// <summary>
        /// Add one entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="columns">null for the default column order</param>
        /// <param name="valeurs">Entries to add</param>
        /// <param name="msgErr"></param>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int Insert(string tableName, string valeurs, string columns, out SQLlog msgErr)
        {
            return Insert(tableName, new string[] { valeurs }, columns, out msgErr);
        }
        /// <summary>
        /// Add multi entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="columns">null for the default column order</param>
        /// <param name="valeurs">Entries to add</param>
        /// <param name="msgErr"></param>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int Insert(string tableName, string[] valeurs, string columns, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Insert(tableName, valeurs, columns), out msgErr);
        }

        /// <summary>
        /// Create a SQL request for add one entrie to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to add</param>
        /// <returns></returns>
        static public string SQL_Insert(string tableName, string valeurs)
        {
            return SQL_Insert(tableName, valeurs, null);
        }

        /// <summary>
        /// Create a SQL request for add multi entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to add</param>
        static public string SQL_Insert(string tableName, string[] valeurs)
        {
            return SQL_Insert(tableName, valeurs, null);
        }
        /// <summary>
        /// Create a SQL request for add one entrie to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to add</param>
        /// <param name="columns">null for the default column order</param>
        /// <returns></returns>
        static public string SQL_Insert(string tableName, string valeurs, string columns)
        {
            return SQL_Insert(tableName, new string[] { valeurs }, columns);
        }
        /// <summary>
        /// Create a SQL request for add multi entries to the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="valeurs">Entries to add</param>
        /// <param name="columns">null for the default column order</param>
        static public string SQL_Insert(string tableName, string[] valeurs, string columns)
        {
            string rslt = "INSERT INTO ";
            if (string.IsNullOrWhiteSpace(tableName))
                rslt += " unknow_table ";
            else
                rslt += " '" + tableName.Trim() + "' ";

            if (!string.IsNullOrWhiteSpace(columns))
                rslt += " (" + columns.Trim() + ") ";
            
            rslt += " VALUES (" + valeurs.ToOneString("),\n\t(", StringOneLineOptions.SkipNull) + ")";

            return rslt.Trim() + ";";
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
        /// Create a SQL request for updates the table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="whereValues">Can be null or empty</param>
        /// <param name="values">Values to updates</param>
        /// <returns></returns>
        static public string SQL_Update(string tableName, string whereValues, string values)
        {
            string rslt = "UPDATE ";
            if (string.IsNullOrWhiteSpace(tableName))
                rslt += " unknow_table ";
            else
                rslt += " '" + tableName.Trim() + "' ";

            if (!string.IsNullOrWhiteSpace(tableName))
                rslt += " SET " + values.Trim() + " ";

            if (!string.IsNullOrWhiteSpace(whereValues))
                rslt += " WHERE " + whereValues.Trim();

            return rslt.Trim() + ";";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="multiUpdate"></param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int Update(string tableName, SQLiteMultiCaseValues multiUpdate, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Update(tableName, multiUpdate), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for updates the table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="multiUpdate">Values to updates</param>
        /// <returns></returns>
        static public string SQL_Update(string tableName, SQLiteMultiCaseValues multiUpdate)
        {
            string rslt = "UPDATE "+ multiUpdate.ColumnName + " SET " + multiUpdate.GetCASE() +
                "\nWHERE " + multiUpdate.GetOR();

            return rslt.Trim() + ";";
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
            string rslt = "DELETE FROM ";
            if (string.IsNullOrWhiteSpace(tableName))
                rslt += " unknow_table ";
            else
                rslt += " '" + tableName.Trim() + "' ";

            rslt += " WHERE " + whereValues.Trim();

            return rslt.Trim() + ";";
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


        #region GetTable SQL

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
            string rslt = "SELECT";
            if (string.IsNullOrWhiteSpace(onlyColumns))
                rslt += " * ";
            else
                rslt += " " + onlyColumns.Trim() + " ";

            rslt += "FROM";
            if (string.IsNullOrWhiteSpace(tableName))
                rslt += " 'unknow_table' ";
            else
                rslt += " '" + tableName.Trim() + "' ";

            if (!string.IsNullOrWhiteSpace(whereValues))
                rslt += " WHERE " + whereValues.Trim() + " ";

            if (!string.IsNullOrWhiteSpace(orderColumns))
                rslt += " ORDER BY " + orderColumns.Trim() + " ";

            return rslt.Trim() + ";";
        }

        #endregion

        #endregion
    }
}
