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
    /// Specialized instance for work with the "sqlite_master" table of a SQLite database
    /// </summary>
    public sealed class SQLiteMaster : SQLiteEdit
    {
        /// <summary>
        /// Create a specialized instance for work with the "sqlite_master" of a SQLite database
        /// </summary>
        /// <param name="db">Taget database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteMaster(SQLiteDataBase db, bool OpenConnection) : base(db, OpenConnection)
        {
            clsName = "SQLiteMaster";
        }
        /// <summary>
        /// Create a specialized instance for work with the "sqlite_master" of a SQLite database
        /// </summary>
        /// <param name="db">Taget database</param>
        public SQLiteMaster(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a specialized instance for work with the "sqlite_master" of a SQLite database
        /// </summary>
        /// <param name="dbPath">Path if the taget database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteMaster(string dbPath, bool OpenConnection) : this(SQLiteDataBase.LoadDataBase(dbPath), OpenConnection)
        { }
        /// <summary>
        /// Create a specialized instance for work with the "sqlite_master" of a SQLite database
        /// </summary>
        /// <param name="dbPath">Path if the taget database</param>
        public SQLiteMaster(string dbPath) : this(SQLiteDataBase.LoadDataBase(dbPath), false)
        { }

        public string[] Type { get { return _Type; } }
        private string[] _Type = new string[] { "table", "view", "index", "trigger" };


        /// <summary>
        /// Obtains a DataTable from 'sqlite_master'
        /// </summary>
        public DataTable GetSQLite_master(string whereValues, string onlyColumns, string orderColumns, out SQLerr msgErr)
        {
            IsDisposed();
            return SQLdataTable(CreatSQL_master(whereValues, onlyColumns, orderColumns), out msgErr);
        }
        /// <summary>
        /// Create a SQL request for the "sqlite_master" table with only the specified columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        static public string CreatSQL_master(string whereValues, string onlyColumns, string orderColumns)
        {
            return CreatSQL_master(whereValues, onlyColumns, orderColumns, false);
        }
        /// <summary>
        /// Create a SQL request for the "sqlite_master" table with only the specified columns, in the desired order and with the values corresponding to the condition.
        /// </summary>
        /// <param name="whereValues">null to retrieve all values</param>
        /// <param name="onlyColumns">null for all columns</param>
        /// <param name="orderColumns">null for the default order</param>
        /// <param name="temp">temp  "sqlite_master" table</param>
        static public string CreatSQL_master(string whereValues, string onlyColumns, string orderColumns, bool temp)
        {
            if (temp)
                return SQLiteData.CreatSQL_GetTableWhere("sqlite_temp_master", whereValues, onlyColumns, orderColumns);
            else
                return SQLiteData.CreatSQL_GetTableWhere("sqlite_master", whereValues, onlyColumns, orderColumns);
        }

        /// <summary>
        /// Get the Tables of the database and the SQL command associate
        /// </summary>
        /// <returns></returns>
        public SQLiteAssociateTable[] GetTables_SQL()
        {
            SQLerr msgErr;
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
            SQLerr msgErr;
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
            SQLerr msgErr;
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
            SQLerr msgErr;
            List<SQLiteAssociateTable> lst = new List<SQLiteAssociateTable>();
            DataTable dt = GetSQLite_master("type='trigger'", "tbl_name, name, sql", "tbl_name, name", out msgErr);
            foreach (DataRow r in dt.Rows)
                lst.Add(new SQLiteAssociateTable(r["name"].ToString(), r["tbl_name"].ToString(), r["sql"].ToString()));

            return lst.ToArray();
        }




    }
}
