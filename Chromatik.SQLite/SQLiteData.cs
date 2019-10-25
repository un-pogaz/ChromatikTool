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
    public class SQLiteData : SQLiteEdit
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
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="values">Values to write in the table; Cannot be null or empty</param>
        /// <param name="msgErr"></param>
        public int Insert(string tableName, string values, out SQLlog msgErr)
        {
            return Insert(tableName, values, null, out msgErr);
        }
        /// <summary>
        /// Add one entries to the table
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="values">Values to write in the table; Cannot be null or empty</param>
        /// <param name="columns">Columns order of the values to write; null or empty for the default columns order</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int Insert(string tableName, string values, string columns, out SQLlog msgErr)
        {
            return Insert(tableName, new string[] { values }, columns, out msgErr);
        }

        /// <summary>
        /// Add multi entries to the table
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="values">Values to write in the table; Cannot be null or empty</param>
        /// <param name="msgErr"></param>
        public int Insert(string tableName, string[] values, out SQLlog msgErr)
        {
            return Insert(tableName, values, null, out msgErr);
        }
        /// <summary>
        /// Add multi entries to the table
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="values">Values to write in the table; Cannot be null or empty</param>
        /// <param name="columns">Columns order of the values to write; null or empty for the default columns order</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int Insert(string tableName, string[] values, string columns, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Insert(tableName, values, columns), out msgErr);
        }

        /// <summary>
        /// Create a SQL request for add one entrie to the table
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="values">Values to write in the table; Cannot be null or empty</param>
        /// <returns></returns>
        static public string SQL_Insert(string tableName, string values)
        {
            return SQL_Insert(tableName, values, null);
        }
        /// <summary>
        /// Create a SQL request for add one entrie to the table
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="values">Values to write in the table; Cannot be null or empty</param>
        /// <param name="columns">Columns order of the values to write; null or empty for the default columns order</param>
        /// <returns></returns>
        static public string SQL_Insert(string tableName, string values, string columns)
        {
            return SQL_Insert(tableName, new string[] { values }, columns);
        }
        
        /// <summary>
        /// Create a SQL request for add multi entries to the table
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="values">Values to write in the table; Cannot be null or empty</param>
        static public string SQL_Insert(string tableName, string[] values)
        {
            return SQL_Insert(tableName, values, null);
        }
        /// <summary>
        /// Create a SQL request for add multi entries to the table
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="values">Values to write in the table; Cannot be null or empty</param>
        /// <param name="columns">Columns order of the values to write; null or empty for the default columns order</param>
        static public string SQL_Insert(string tableName, string[] values, string columns)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            string rslt = "INSERT INTO " + tableName.ToSQLiteFormat() ;

            if (!string.IsNullOrWhiteSpace(columns))
                rslt += " (" + columns.Trim() + ") ";

            string v = values.ToOneString("),\n(", StringOneLineOptions.SkipNullAndWhiteSpace);
            if (string.IsNullOrWhiteSpace(v))
                throw new ArgumentException("The " + nameof(values) + " cannot be empty.", nameof(values));

            rslt += " VALUES (" + v + ")";

            return rslt.Trim() + ";";
        }

        #endregion

        #region Updates

        /// <summary>
        /// Updates the table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="whereValues">Condition to update entrie; Cannot be null or empty</param>
        /// <param name="values">New values to write in the table; Cannot be null or empty</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int Update(string tableName, string whereValues, string values, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Update(tableName, whereValues, values), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for updates the table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="whereValues">Condition to update entrie; Cannot be null or empty</param>
        /// <param name="values">New values to write in the table; Cannot be null or empty</param>
        /// <returns></returns>
        static public string SQL_Update(string tableName, string whereValues, string values)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (string.IsNullOrWhiteSpace(whereValues))
                throw new ArgumentNullException(nameof(whereValues));
            if (string.IsNullOrWhiteSpace(values))
                throw new ArgumentNullException(nameof(values));

            string rslt = "UPDATE " + tableName.ToSQLiteFormat() + " SET " + values.Trim() + " WHERE " + whereValues.Trim();
            
            return rslt.Trim() + ";";
        }
        
        /// <summary>
        /// Updates the table entries corresponding to the <see cref="SQLiteMultiCaseValues"/>
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="multiUpdate"><see cref="SQLiteMultiCaseValues"/> to write in the table; Cannot be null or empty</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int Update(string tableName, SQLiteMultiCaseValues multiUpdate, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Update(tableName, multiUpdate), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for updates the table entries corresponding to the <see cref="SQLiteMultiCaseValues"/>
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="multiUpdate"><see cref="SQLiteMultiCaseValues"/> to write in the table; Cannot be null or empty</param>
        /// <returns></returns>
        static public string SQL_Update(string tableName, SQLiteMultiCaseValues multiUpdate)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (multiUpdate == null)
                throw new ArgumentNullException(nameof(multiUpdate));
            if (multiUpdate.Count == 0)
                throw new ArgumentException("The "+ nameof(multiUpdate) + " {"+nameof(SQLiteMultiCaseValues) +"} cannot be empty.", nameof(multiUpdate));

            string rslt = "UPDATE "+ multiUpdate.ColumnName + " SET " + multiUpdate.GetCASE() +
                "\nWHERE " + multiUpdate.GetOR();

            return rslt.Trim() + ";";
        }

        /// <summary>
        /// Updates the table entries corresponding to the <see cref="SQLiteMultiCaseValues"/>
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="multiUpdate"><see cref="SQLiteMultiCaseValues"/> to write in the table; Cannot be null or empty</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        /// <remarks>Generate a multiple UPDATE CASE for each entrie/column, combined in one request (no others simple way)</remarks>
        public int Update(string tableName, SQLiteMultiCaseValues[] multiUpdate, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Update(tableName, multiUpdate), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for updates the table entries corresponding to a <see cref="SQLiteMultiCaseValues"/>[]
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="multiUpdate"><see cref="SQLiteMultiCaseValues"/> to write in the table; Cannot be null or empty</param>
        /// <returns></returns>
        /// <remarks>Generate a multiple UPDATE CASE for each entrie/column, combined in one string request (no others simple way)</remarks>
        static public string SQL_Update(string tableName, SQLiteMultiCaseValues[] multiUpdate)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (multiUpdate == null)
                throw new ArgumentNullException(nameof(multiUpdate));
            if (multiUpdate.Length == 0)
                throw new ArgumentException("The " + nameof(multiUpdate) + " {" + nameof(SQLiteMultiCaseValues) + "} cannot be empty.", nameof(multiUpdate));

            string[] tbl = new string[multiUpdate.Length];
            for (int i = 0; i < multiUpdate.Length; i++)
                if (multiUpdate[i] != null && multiUpdate[i].Count > 0)
                    tbl[i] = SQL_Update(tableName, multiUpdate[i]);

            string rslt = tbl.ToOneString("\n\n", StringOneLineOptions.SkipNullAndWhiteSpace);

            if (string.IsNullOrWhiteSpace(rslt))
                throw new ArgumentException("The " + nameof(multiUpdate) + "[] {" + nameof(SQLiteMultiCaseValues) + "}[] as invalid (no CASE are defined).", nameof(multiUpdate));

            return rslt.Trim();
        }
        #endregion

        #region Delete

        /// <summary>
        /// Delete the table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="whereValues">Condition for delete row; Cannot be null or empty</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int Delete(string tableName, string whereValues, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_Delete(tableName, whereValues), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for deleted a table entries corresponding to the condition
        /// </summary>
        /// <param name="tableName">Name of the table to updates; Cannot be null or empty</param>
        /// <param name="whereValues">Condition for delete row; Cannot be null or empty</param>
        /// <returns></returns>
        static public string SQL_Delete(string tableName, string whereValues)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (string.IsNullOrWhiteSpace(whereValues))
                throw new ArgumentNullException(nameof(whereValues));

            string rslt = "DELETE FROM " + tableName.ToSQLiteFormat()+ " WHERE " + whereValues.Trim();

            return rslt.Trim() + ";";
        }

        #endregion

        #region GetTable

        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
        /// <param name="msgErr"></param>
        /// <returns>The DataTable request</returns>
        public DataTable GetTable(string tableName, out SQLlog msgErr)
        {
            return GetTable(tableName, null, null, out msgErr);
        }
        /// <summary>
        /// Obtains the table with only the requested columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
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
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
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
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
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
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
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
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
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
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
        static public string SQL_GetTable(string tableName)
        {
            return SQL_GetTable(tableName, null, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns and in the default order.
        /// </summary>
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
        /// <param name="onlyColumns">null for all columns</param>
        static public string SQL_GetTable(string tableName, string onlyColumns)
        {
            return SQL_GetTable(tableName, onlyColumns, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns and in the desired order.
        /// </summary>
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        static public string SQL_GetTable(string tableName, string onlyColumns, string orderColumns)
        {
            return SQL_GetTableWhere(tableName, null, onlyColumns, orderColumns);
        }
        /// <summary>
        /// Create a SQL request for a table with all columns, in the default order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
        /// <param name="whereValues">null to retrieve all values</param>
        static public string SQL_GetTableWhere(string tableName, string whereValues)
        {
            return SQL_GetTableWhere(tableName, whereValues, null, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns, in the default order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="orderColumns">null for all columns</param>
        static public string SQL_GetTableWhere(string tableName, string whereValues, string orderColumns)
        {
            return SQL_GetTableWhere(tableName, whereValues, orderColumns, null);
        }
        /// <summary>
        /// Create a SQL request for a table with only the specified columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="tableName">Name of the table to get; Cannot be null or empty</param>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        static public string SQL_GetTableWhere(string tableName, string whereValues, string onlyColumns, string orderColumns)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                throw new ArgumentNullException(nameof(tableName));

            string rslt = "SELECT";
            if (string.IsNullOrWhiteSpace(onlyColumns))
                rslt += " * ";
            else
                rslt += " " + onlyColumns.Trim() + " ";

            rslt += "FROM " + tableName.ToSQLiteFormat() + " ";

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
