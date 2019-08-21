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
        /// Create a specialized instance for work with the tables of a <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <param name="dbPath">Path if the taget database</param>
        public SQLiteTable(string dbPath) : this(dbPath, false)
        { }
        /// <summary>
        /// Create a specialized instance for work with the tables of a <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <param name="dbPath">Path if the taget database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteTable(string dbPath, bool OpenConnection) : this(SQLiteDataBase.LoadDataBase(dbPath), OpenConnection)
        { }

        /// <summary>
        /// Create a specialized instance for work with the tables of a <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <param name="db">Taget database</param>
        public SQLiteTable(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a specialized instance for work with the tables of a <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <param name="db">Taget database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteTable(SQLiteDataBase db, bool OpenConnection) : base(db, OpenConnection)
        {
            clsName = "SQLiteTable";
        }

        /// <summary>
        /// Create new table with the specified columns
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="Columns">Name and typing of columns</param>
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
        /// <param name="Columns">Name and typing of columns</param>
        static public string SQL_AddTable(string tableName, SQLiteColumns columns)
        {
            if (columns.Count == 0)
                throw new ArgumentException("SQLiteColumns cannot be empty", "columns");

            string rslt = string.Empty;
            for (int i = 0; i < columns.Count - 1; i++)
                rslt += columns[i].ToString() + " , ";

            rslt += columns.Last().ToString();

            return "CREATE TABLE '" + tableName.Trim() + "' (" + rslt.Trim() + ")" + ";";
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
            return ExecuteSQLcommand(CreatSQL_DeleteTable(tableName), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for delete the table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        static public string CreatSQL_DeleteTable(string tableName)
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
        /// Modify the columns of a table
        /// </summary>
        /// <param name="tableName">table to affect</param>
        /// <param name="edtiting">Modification to be made</param>
        static public string SQL_EditTable(string tableName, string edtiting)
        {
            return "ALTER TABLE '" + tableName.Trim() + "'" + edtiting.Trim() + ";";
        }
        static public string SQL_EditTable_AddColumns(string tableName, string name, string types)
        {
            return SQL_EditTable(tableName, " ADD '" + name.Trim() + "' " + types.Trim());
        }
        static public string SQL_EditTable_DeleteColumns(string tableName, string name)
        {
            return SQL_EditTable(tableName, " DROP '" + name.Trim() + "'");
        }

    }
}
