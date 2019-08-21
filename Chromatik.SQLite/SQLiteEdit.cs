﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Chromatik.SQLite
{
    /// <summary>
    /// The base instance for obtains, editing and execute commands in a <see cref="DataBase"/>
    /// </summary>
    public class SQLiteEdit : IDisposable
    {
        private SQLiteDataBase _DB;
        /// <summary>
        /// The <see cref="Chromatik.SQLite.SQLiteDataBase"/> used by this instance
        /// </summary>
        /// <returns></returns>
        public SQLiteDataBase DataBase { get { return _DB; } }

        /// <summary>
        /// State of connection with the database
        /// </summary>
        public bool ConnectionIsOpen { get { return DataBase.ConnectionIsOpen; } }
        /// <summary>
        /// List of all SQL request executed of the database
        /// </summary>
        public SQLlog[] Logs { get { return DataBase.Logs; } }
        /// <summary>
        /// List of all SQL request executed of the database
        /// </summary>
        public string[] LogsSQL { get { return DataBase.LogsSQL; } }

        /// <summary>
        /// Path of the database
        /// </summary>
        public string filePath { get { return DataBase.filePath; } }

        /// <summary>
        /// Full path of the database
        /// </summary>
        public string fullPath { get { return DataBase.fullPath; } }

        /// <summary>
        /// Connection string of the database
        /// </summary>
        public string ConnectionSting { get { return DataBase.ConnectionString; } }

        /// <summary>
        /// Class name for <see cref="IsDisposed()"/>
        /// </summary>
        protected string clsName = "SQLiteEdit";

        /// <summary>
        /// Create a basic instance for work with <see cref="DataBase"/>
        /// </summary>
        /// <param name="dbPath">Path of the target database</param>
        public SQLiteEdit(string dbPath) : this(dbPath, false)
        { }
        /// <summary>
        /// Create a basic instance for work with <see cref="DataBase"/>
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="dbPath">Path of the target database</param>
        /// <param name="OpenConnection">If this constructor must also open the connection</param>
        public SQLiteEdit(string dbPath, bool OpenConnection) : this(SQLiteDataBase.LoadDataBase(dbPath), OpenConnection)
        { }

        /// <summary>
        /// Create a basic instance for work with <see cref="DataBase"/>
        /// </summary>
        /// <param name="db">The target database</param>
        public SQLiteEdit(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a basic instance for work with <see cref="DataBase"/>
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

        protected bool disposed = true;
        public void Dispose()
        {
            if (ConnectionIsOpen)
                CloseConnection();

            disposed = true;
        }
        protected void IsDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(clsName);
        }
        
        ~SQLiteEdit()
        {

        }

        /// <summary>
        /// Open the connection of the <see cref="DataBase"/>
        /// </summary>
        public bool OpenConnection()
        {
            return DataBase.OpenConnection();
        }
        /// <summary>
        /// Close the connection of the <see cref="DataBase"/>
        /// </summary>
        public void CloseConnection()
        {
            DataBase.CloseConnection();
        }
        /// <summary>
        /// Get the Tables of the <see cref="DataBase"/>
        /// </summary>
        /// <remarks>A valide database contain minimum 1 table</remarks>
        public string[] GetTables()
        {
            return DataBase.GetTables();
        }
        /// <summary>
        /// Execute the SQL command and return the number of rows inserted/updated affected by it.
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        public int ExecuteSQLcommand(string SQL, out SQLlog msgErr)
        {
            return DataBase._SQLcommand(SQL, out msgErr);
        }
        /// <summary>
        /// Obtains a DataTable corresponding to the SQL request 
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>The DataTable request</returns>
        public DataTable ExecuteSQLdataTable(string SQL, out SQLlog msgErr)
        {
            return DataBase._SQLdataTable(SQL, out msgErr);
        }
    }
}
