using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Chromatik.SQLite
{
    public struct SQLiteAssociateTable
    {
        public string Name { get; }
        public string AssociateTable { get; }
        public string SQL { get; }

        internal SQLiteAssociateTable(string name, string associateTable, string sql)
        {
            Name = name;
            AssociateTable = associateTable;
            SQL = sql;
        }
    }

    /// <summary>
    /// Specialized instance for work with the "sqlite_master" table of a <see cref="SQLiteDataBase"/>
    /// </summary>
    public sealed class SQLiteMaster : SQLiteEdit
    {
        /// <summary>
        /// Create a basic instance for work with <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="db">The target database</param>
        public SQLiteMaster(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a specialized instance for work with the "sqlite_master" of a <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="db">The target database</param>
        /// <param name="openConnection">Open the connection with the data base</param>
        public SQLiteMaster(SQLiteDataBase db, bool openConnection) : base(db, openConnection)
        {
            clsName = nameof(SQLiteMaster);
        }

        /// <summary>
        /// Values of "Type" column
        /// </summary>
        public string[] Type { get; } = new string[] { "table", "view", "index", "trigger" };

        /// <summary>
        /// Get the Tables of the database
        /// </summary>
        /// <remarks>A valide database contain minimum 1 table</remarks>
        /// <returns></returns>
        new public string[] GetTablesName()
        {
            List<string> lst = new List<string>();
            foreach (SQLiteAssociateTable table in GetTables_SQL())
                lst.Add(table.Name);
            return lst.ToArray();
        }

        /// <summary>
        /// Obtains a DataTable from 'sqlite_master'
        /// </summary>
        public DataTable GetSQLite_master(string whereValues, string onlyColumns, string orderColumns, out SQLlog msgErr)
        {
            IsDisposed();
            return ExecuteSQLdataTable(SQL_master(whereValues, onlyColumns, orderColumns), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for the "sqlite_master" table with only the specified columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        static public string SQL_master(string whereValues, string onlyColumns, string orderColumns)
        {
            return SQL_master(whereValues, onlyColumns, orderColumns, false);
        }
        /// <summary>
        /// Create a SQL request for the "sqlite_master" table with only the specified columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        /// <param name="temp">temp  "sqlite_master" table</param>
        static public string SQL_master(string whereValues, string onlyColumns, string orderColumns, bool temp)
        {
            if (temp)
                return SQLiteData.SQL_GetTableWhere("sqlite_temp_master", whereValues, onlyColumns, orderColumns);
            else
                return SQLiteData.SQL_GetTableWhere("sqlite_master", whereValues, onlyColumns, orderColumns);
        }

        /// <summary>
        /// Get the Tables of the database and the SQL command associate
        /// </summary>
        /// <returns></returns>
        public SQLiteAssociateTable[] GetTables_SQL()
        {
            SQLlog msgErr = SQLlog.Empty;
            List<SQLiteAssociateTable> lst = new List<SQLiteAssociateTable>();
            DataTable dt = GetSQLite_master("type='table'", "name, sql", "name", out msgErr);
            foreach (DataRow r in dt.Rows)
                lst.Add(new SQLiteAssociateTable(r["name"].ToString(), r["name"].ToString(), r["sql"].ToString()));

            return lst.ToArray();
        }

        /// <summary>
        /// Get the View of the database and the SQL command associate
        /// </summary>
        /// <returns></returns>
        public SQLiteAssociateTable[] GetViews_SQL()
        {
            SQLlog msgErr = SQLlog.Empty;
            List<SQLiteAssociateTable> lst = new List<SQLiteAssociateTable>();
            DataTable dt = GetSQLite_master("type='view'", "name, sql", "name", out msgErr);
            foreach (DataRow r in dt.Rows)
                lst.Add(new SQLiteAssociateTable(r["name"].ToString(), r["name"].ToString(), r["sql"].ToString()));

            return lst.ToArray();
        }

        /// <summary>
        /// Get the Index of the database and the SQL command associate
        /// </summary>
        /// <returns></returns>
        public SQLiteAssociateTable[] GetIndex_SQL()
        {
            SQLlog msgErr = SQLlog.Empty;
            List<SQLiteAssociateTable> lst = new List<SQLiteAssociateTable>();
            DataTable dt = GetSQLite_master("type='index'", "tbl_name, name, sql", "tbl_name, name", out msgErr);
            foreach (DataRow r in dt.Rows)
                lst.Add(new SQLiteAssociateTable(r["name"].ToString(), r["tbl_name"].ToString(), r["sql"].ToString()));

            return lst.ToArray();
        }

        /// <summary>
        /// Get the Triggers of the database and the SQL command associate
        /// </summary>
        /// <returns></returns>
        public SQLiteAssociateTable[] GetTriggers_SQL()
        {
            SQLlog msgErr = SQLlog.Empty;
            List<SQLiteAssociateTable> lst = new List<SQLiteAssociateTable>();
            DataTable dt = GetSQLite_master("type='trigger'", "tbl_name, name, sql", "tbl_name, name", out msgErr);
            foreach (DataRow r in dt.Rows)
                lst.Add(new SQLiteAssociateTable(r["name"].ToString(), r["tbl_name"].ToString(), r["sql"].ToString()));

            return lst.ToArray();
        }
    }
}
