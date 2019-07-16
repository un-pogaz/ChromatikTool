using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Chromatik.SQLite
{
    /// <summary>
    /// The base instance for obtains, editing and execute commands in a SQLite database
    /// </summary>
    public class SQLiteEdit : IDisposable
    {
        private SQLiteDataBase _DB;
        /// <summary>
        /// Obtain the database used by this instance
        /// </summary>
        /// <returns></returns>
        public SQLiteDataBase GetDataBase() { IsDisposed(); return _DB; }

        /// <summary>
        /// 
        /// </summary>
        public bool ConnectionIsOpen { get { return GetDataBase().ConnectionIsOpen; } }
        /// <summary>
        /// List of all SQL request executed of the database
        /// </summary>
        public SQLrequest[] Logs { get { return GetDataBase().Logs; } }
        /// <summary>
        /// List of all SQL request executed of the database
        /// </summary>
        public string[] LogsSQL { get { return GetDataBase().LogsSQL; } }
        public string filePath { get { return GetDataBase().filePath; } }
        public string fullPath { get { return GetDataBase().fullPath; } }
        public string ConnectionSting { get { return GetDataBase().ConnectionSting; } }
        
        protected string clsName = "SQLiteEdit";
        protected bool clsConnect = false;

        /// <summary>
        /// Create a basic instance for edit a SQLite database
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="db">The target database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteEdit(SQLiteDataBase db, bool OpenConnection)
        {
            disposed = false;
            clsName = "SQLiteEdit";
            _DB = db;
            if (OpenConnection && !ConnectionIsOpen)
                this.OpenConnection();
        }
        /// <summary>
        /// Create a basic instance for edit a SQLite database
        /// </summary>
        /// <param name="db">The target database</param>
        public SQLiteEdit(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a basic instance for edit a SQLite database
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="dbPath">Path of the target database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteEdit(string dbPath, bool OpenConnection) : this(SQLiteDataBase.LoadDataBase(dbPath), OpenConnection)
        { }
        /// <summary>
        /// Create a basic instance for edit a SQLite database
        /// </summary>
        /// <param name="dbPath">Path of the target database</param>
        public SQLiteEdit(string dbPath) : this(SQLiteDataBase.LoadDataBase(dbPath), false)
        { }

        protected bool disposed = true;
        public void Dispose()
        {
            if (clsConnect)
                CloseConnection();

            disposed = true;
        }
        protected void IsDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(clsName);
        }

        /// <summary>
        /// Destructeur
        /// </summary>
        ~SQLiteEdit()
        {

        }

        /// <summary>
        /// Open the connection of the database
        /// </summary>
        public bool OpenConnection()
        {
            clsConnect = GetDataBase().OpenConnection();
            return clsConnect;
        }
        /// <summary>
        /// Close the connection of the database
        /// </summary>
        public void CloseConnection()
        {
            clsConnect = false;
            GetDataBase().CloseConnection();
        }
        /// <summary>
        /// Get the Tables of the database
        /// </summary>
        /// <remarks>A valide database contain minimum 1 table</remarks>
        public string[] GetTables()
        {
            return GetDataBase().GetTables();
        }
        /// <summary>
        /// Execute the SQL command and return the number of rows inserted/updated affected by it.
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int SQLcommand(string SQL, out SQLerr msgErr)
        {
            return GetDataBase()._SQLcommand(SQL, out msgErr);
        }
        /// <summary>
        /// Obtains a DataTable corresponding to the SQL request 
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>The DataTable request</returns>
        public DataTable SQLdataTable(string SQL, out SQLerr msgErr)
        {
            return GetDataBase()._SQLdataTable(SQL, out msgErr);
        }
    }
}
