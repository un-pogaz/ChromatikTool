using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace Chromatik.SQLite
{
    /// <summary>
    /// The base instance for obtains, editing and execute commands in a <see cref="DataBase"/>
    /// </summary>
    public class SQLiteEdit : IDisposable
    {
        /// <summary>
        /// The <see cref="SQLiteDataBase"/> used by this instance
        /// </summary>
        /// <returns></returns>
        public SQLiteDataBase DataBase { get; }

        /// <summary>
        /// State of connection with the database
        /// </summary>
        public bool ConnectionIsOpen { get { return DataBase.ConnectionIsOpen; } }
        /// <summary>
        /// List of all SQL request executed of the database
        /// </summary>
        public StackLogSQL Logs { get { return DataBase.Logs; } }

        /// <summary>
        /// Path of the database
        /// </summary>
        public string FilePath { get { return DataBase.FilePath; } }

        /// <summary>
        /// Full path of the database
        /// </summary>
        public string FullPath { get { return DataBase.FullPath; } }

        /// <summary>
        /// Connection string of the database
        /// </summary>
        public string ConnectionSting { get { return DataBase.ConnectionString; } }

        /// <summary>
        /// Class name for <see cref="IsDisposed()"/>
        /// </summary>
        protected string clsName = nameof(SQLiteEdit);

        /// <summary>
        /// Create a basic instance for work with <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <param name="db">The target database</param>
        public SQLiteEdit(SQLiteDataBase db) : this(db, false)
        { }
        /// <summary>
        /// Create a basic instance for work with <see cref="SQLiteDataBase"/>
        /// </summary>
        /// <remarks>If the connection is opened by this constructor, they will be closed if the instance is dispose.</remarks>
        /// <param name="db">The target database</param>
        /// <param name="openConnection">Open the connection with the data base</param>
        public SQLiteEdit(SQLiteDataBase db, bool openConnection)
        {
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            disposed = false;
            DataBase = db;

            OpenOnStart = ConnectionIsOpen;
            if (openConnection && !ConnectionIsOpen && !this.OpenConnection())
                throw new System.Data.SQLite.SQLiteException(System.Data.SQLite.SQLiteErrorCode.CantOpen, "Database is not open");
        }

        /// <summary> </summary>
        public override string ToString()
        {
            return DataBase.ToString() + " {" + clsName + "}";
        }

        bool OpenOnStart = false;

        /// <summary> </summary>
        protected bool disposed = true;
        /// <summary> </summary>
        public void Dispose()
        {
            if (ConnectionIsOpen && !OpenOnStart)
                CloseConnection();

            disposed = true;
        }
        /// <summary> </summary>
        protected void IsDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException(clsName);
        }

        /// <summary> </summary>
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
        public string[] GetTablesName()
        {
            return DataBase.GetTablesName();
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
            if (string.IsNullOrWhiteSpace(SQL))
                throw new ArgumentNullException(nameof(SQL));

            return DataBase._SQLcommand(SQL, out msgErr);
        }
        /// <summary>
        /// Obtains a <see cref="DataTable"/> corresponding to the SQL request
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>The <see cref="DataTable"/> request</returns>
        public DataTable ExecuteSQLdataTable(string SQL, out SQLlog msgErr)
        {
            if (string.IsNullOrWhiteSpace(SQL))
                throw new ArgumentNullException(nameof(SQL));

            return DataBase._SQLdataTable(SQL, out msgErr);
        }
        /// <summary>
        /// Obtains a value corresponding to the SQL scalar request 
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>The value of the scalar request</returns>
        public object ExecuteSQLscalar(string SQL, out SQLlog msgErr)
        {
            if (string.IsNullOrWhiteSpace(SQL))
                throw new ArgumentNullException(nameof(SQL));

            return DataBase._SQLscalar(SQL, out msgErr);
        }
    }
}
