using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Specialized instance for editing the tables of a <see cref="SQLiteDataBase"/>
    /// </summary>
    public sealed class SQLiteTable : SQLiteEdit
    {
        /// <summary>
        /// Create a basic instance for work with <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="db">The target database</param>
        public SQLiteTable(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a specialized instance for work with the tables of a <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="db">The target database</param>
        /// <param name="openConnection">Open the connection with the data base</param>
        public SQLiteTable(SQLiteDataBase db, bool openConnection) : base(db, openConnection)
        {
            clsName = nameof(SQLiteTable);
        }

        /// <summary>
        /// Create new table with the specified columns
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="columns">Name and typing of columns</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int AddTable(string tableName, SQLiteColumns columns, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_AddTable(tableName, columns), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for a new table with the specified columns
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="columns">Name and typing of columns</param>
        static public string SQL_AddTable(string tableName, SQLiteColumns columns)
        {
            if (columns.Count == 0)
                throw new ArgumentException("SQLiteColumns cannot be empty", "columns");
            
            return "CREATE TABLE '" + tableName.Trim() + "' (" + columns.GetFullString() + ")" + ";";
        }

        /// <summary>
        /// Clean all data from the table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int TruncateTable(string tableName, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_TruncateTable(tableName), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for clean all data from the table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        static public string SQL_TruncateTable(string tableName)
        {
            return "TRUNCATE TABLE '" + tableName.Trim() + "'" + ";";
        }

        /// <summary>
        /// Delete the table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int DeleteTable(string tableName, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_DeleteTable(tableName), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for delete the table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        static public string SQL_DeleteTable(string tableName)
        {
            return "DROP TABLE '" + tableName.Trim() + "'" + ";";
        }

        /// <summary>
        /// Modify the columns of a table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="edtiting">Modification to be made</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int EditTable(string tableName, string edtiting, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_EditTable(tableName, edtiting), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for modify the columns of a table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="edtiting">Modification to be made</param>
        static public string SQL_EditTable(string tableName, string edtiting)
        {
            return "ALTER TABLE '" + tableName.Trim() + "' " + edtiting.Trim() + ";";
        }

        /// <summary>
        /// Add columns to the table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="columns">columns to add</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int AddColumns(string tableName, SQLiteColumns columns, out SQLlog msgErr)
        {
            msgErr = SQLlog.Empty;
            for (int i = 0; i < columns.Count; i++)
            {
                AddColumn(tableName, columns[i], out msgErr);
                if (!msgErr.Succes)
                    return i+1;
            }
            return columns.Count;
        }
        /// <summary>
        /// Add column to the table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="column">column to add</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int AddColumn(string tableName, SQLiteColumns.Column column, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_EditTable_AddColumn(tableName, column), out msgErr); ;
        }
        /// <summary>
        /// Create a SQL request for add column in a table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="column">column to add</param>
        /// <returns></returns>
        static public string SQL_EditTable_AddColumn(string tableName, SQLiteColumns.Column column)
        {
            return SQL_EditTable(tableName, " ADD '" + column.Name.Trim() + "' " + SQLiteColumns.GetTypeString(column.Type));
        }

        /// <summary>
        /// Delete a column to the table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="columnName">name of column to delete</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int DeleteColumn(string tableName, string columnName, out SQLlog msgErr)
        {
            return ExecuteSQLcommand(SQL_EditTable_DeleteColumn(tableName, columnName), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for delete columns in a table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="columnName">name of column to delete</param>
        /// <returns></returns>
        static public string SQL_EditTable_DeleteColumn(string tableName, string columnName)
        {
            return SQL_EditTable(tableName, " DROP '" + columnName.Trim() + "'");
        }

    }
}
