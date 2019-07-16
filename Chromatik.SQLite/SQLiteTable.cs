using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Specialized instance for editing the tables of a SQLite database
    /// </summary>
    public sealed class SQLiteTable : SQLiteEdit
    {
        /// <summary>
        /// Create a specialized instance for work with the tables of a SQLite database
        /// </summary>
        /// <param name="db">Taget database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteTable(SQLiteDataBase db, bool OpenConnection) : base(db, OpenConnection)
        {
            clsName = "SQLiteTable";
        }
        /// <summary>
        /// Create a specialized instance for work with the tables of a SQLite database
        /// </summary>
        /// <param name="db">Taget database</param>
        public SQLiteTable(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a specialized instance for work with the tables of a SQLite database
        /// </summary>
        /// <param name="dbPath">Path if the taget database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteTable(string dbPath, bool OpenConnection) : this(SQLiteDataBase.LoadDataBase(dbPath), OpenConnection)
        { }
        /// <summary>
        /// Create a specialized instance for work with the tables of a SQLite database
        /// </summary>
        /// <param name="dbPath">Path if the taget database</param>
        public SQLiteTable(string dbPath) : this(SQLiteDataBase.LoadDataBase(dbPath), false)
        { }

        /// <summary>
        /// Create new table with the specified columns
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="Columns">Name and typing of columns</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int AddTable(string tableName, string Columns, out SQLerr msgErr)
        {
            return SQLcommand(CreatSQL_AddTable(tableName, Columns), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for a new table with the specified columns
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="Columns">Name and typing of columns</param>
        static public string CreatSQL_AddTable(string tableName, string Columns)
        {
            return "CREATE TABLE '" + tableName.Trim() + "' (" + Columns.Trim() + ")" + ";";
        }

        /// <summary>
        /// Clean all data from the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int TruncateTable(string tableName, out SQLerr msgErr)
        {
            return SQLcommand(CreatSQL_TruncateTable(tableName), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for clean all data from the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        static public string CreatSQL_TruncateTable(string tableName)
        {
            return "TRUNCATE TABLE '" + tableName.Trim() + "'" + ";";
        }

        /// <summary>
        /// Delete the table
        /// </summary>
        /// <param name="db">Target database</param>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int DeleteTable(string tableName, out SQLerr msgErr)
        {
            return SQLcommand(CreatSQL_DeleteTable(tableName), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for delete the table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        static public string CreatSQL_DeleteTable(string tableName)
        {
            return "DROP TABLE '" + tableName.Trim() + "'" + ";";
        }

        /// <summary>
        /// Modify the columns of a table
        /// </summary>
        /// <param name="db">Database to be edited</param>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="edtiting">Modification to be made</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        public int EditTable(string tableName, string edtiting, out SQLerr msgErr)
        {
            return SQLcommand(CreatSQL_EditTable(tableName, edtiting), out msgErr);
        }
        
        /// <summary>
        /// Modify the columns of a table
        /// </summary>
        /// <param name="tableName">null for 'unknow_table'</param>
        /// <param name="edtiting">Modification to be made</param>
        /// <param name="msgErr"></param>
        static public string CreatSQL_EditTable(string tableName, string edtiting)
        {
            return "ALTER TABLE '" + tableName.Trim() + "'" + edtiting.Trim() + ";";
        }
        static public string CreatSQL_EditTable_AddColumns(string tableName, string name, string types)
        {
            return CreatSQL_EditTable(tableName, " ADD '" + name.Trim() + "' " + types.Trim());
        }
        static public string CreatSQL_EditTable_DeleteColumns(string tableName, string name)
        {
            return CreatSQL_EditTable(tableName, " DROP '" + name.Trim() + "'");
        }

        


    }
}
