using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Data.SQLite.Generic;

namespace Chromatik.SQLite
{
    /// <summary>
    /// The basic instance for represented a SQLite database
    /// </summary>
    public sealed class SQLiteDataBase : IDisposable
    {
        /// <summary>
        /// Path of this database
        /// </summary>
        public string filePath { get; }
        /// <summary>
        /// Full path of this database
        /// </summary>
        public string fullPath { get { return Path.GetFullPath(filePath); } }

        /// <summary>
        /// Connection string of this database
        /// </summary>
        public string ConnectionString { get { return "Data Source="+ fullPath + ";Version=3;"; } }

        /// <summary>
        /// The basic instance for manipuled SQLite database
        /// </summary>
        private SQLiteDataBase(string dbPath)
        {
            disposed = false;
            filePath = dbPath.Trim();
        }

        private bool disposed = true;
        public void Dispose()
        {
            CloseConnection();
            DBconnect.Dispose();
            disposed = true;
        }
        internal void IsDisposed()
        {
            if (disposed)
                throw new ObjectDisposedException("SQLiteDataBase");
        }
        
        private SQLiteConnection DBconnect;
        private SQLiteCommand CreatDBcommand(string SQL)
        {
            IsDisposed();

            SQLiteCommand rslt = new SQLiteCommand(DBconnect);
            rslt.CommandType = CommandType.Text;
            rslt.CommandTimeout = 30; // seconde
            rslt.CommandText = SQL;

            return rslt;
        }

        /// <summary>
        /// State of connection with this database
        /// </summary>
        public bool ConnectionIsOpen {
            get {
                IsDisposed();
                if (DBconnect == null)
                    return false;
                else
                    return (DBconnect.State == ConnectionState.Open);
            }
        }

        /// <summary>
        /// Open the connection with the database
        /// </summary>
        /// <returns></returns>
        public bool OpenConnection()
        {
            IsDisposed();
            if (!ConnectionIsOpen)
            {
                if (DBconnect != null)
                    DBconnect.Dispose();
                DBconnect = new SQLiteConnection(ConnectionString).OpenAndReturn();
                return ConnectionIsOpen;
            }
            else
            {
                if (DBconnect != null)
                    DBconnect.Dispose();
                DBconnect = new SQLiteConnection(ConnectionString);
                return ConnectionIsOpen;
            }
        }
        /// <summary>
        /// Close the connection with the database
        /// </summary>
        public void CloseConnection()
        {
            IsDisposed();
            if (ConnectionIsOpen)
                DBconnect.Close();
        }
        
        /// <summary>
        /// Execute the SQL command and return the number of rows inserted/updated affected by it.
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        internal int _SQLcommand(string SQL, out SQLlog msgErr)
        {
            IsDisposed();

            msgErr = SQLlog.Empty;
            int rslt = -1;
            try
            {
                rslt = CreatDBcommand(SQL).ExecuteNonQuery(CommandBehavior.Default);
                msgErr = new SQLlog(null, SQL);
            }
            catch (Exception ex)
            {
                rslt = - 1;
                msgErr = new SQLlog(ex, SQL);
            }
            
            AddLogEnty(msgErr);
            return rslt;
        }
        /// <summary>
        /// Obtains a DataTable corresponding to the SQL request 
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>The DataTable request</returns>
        internal DataTable _SQLdataTable(string SQL, out SQLlog msgErr)
        {
            IsDisposed();

            msgErr = SQLlog.Empty;
            DataTable rslt = new DataTable();
            try
            {
                rslt = CreatDBcommand(SQL).ExecuteReader(CommandBehavior.Default).GetDataTable();
                msgErr = new SQLlog(null, SQL);
            }
            catch (Exception ex)
            {
                rslt = new DataTable();
                msgErr = new SQLlog(ex, SQL);
            }
            
            AddLogEnty(msgErr);
            return rslt;
        }

        /// <summary>
        /// List of all SQL request executed with this instance
        /// </summary>
        public SQLlog[] Logs { get { return _Logs.ToArray(); } }
        /// <summary>
        /// List of all SQL request executed with this instance
        /// </summary>
        public string[] LogsSQL
        {
            get {
                List<string> lst = new List<string>();
                foreach (SQLlog item in _Logs)
                    lst.Add(item.SQL);
                return lst.ToArray();
            }
        }
        private List<SQLlog> _Logs = new List<SQLlog>();
        private void AddLogEnty(SQLlog SQLrequest)
        {
            _Logs.Insert(0, SQLrequest);

            while (_Logs.Count > ushort.MaxValue)
                _Logs.RemoveAt(_Logs.Count - 1);
        }

        /// <summary>
        /// Get the Tables of the database
        /// </summary>
        /// <remarks>A valide database contain minimum 1 table</remarks>
        /// <returns></returns>
        public string[] GetTables()
        {
            SQLlog msgErr;
            List<string> lst = new List<string>();
            DataTable dt = _SQLdataTable(SQLiteMaster.SQL_master("type='table'", "name", "name"), out msgErr);
            foreach (DataRow r in dt.Rows)
                lst.Add(r["name"].ToString());

            lst.Sort();
            return lst.ToArray();
        }

        ////////////
        // STATIC //
        ////////////

        /// <summary>
        /// Load a valid SQLite 3 database file.
        /// </summary>
        /// <param name="fileDB"></param>
        /// <returns></returns>
        static public SQLiteDataBase LoadDataBase(string fileDB)
        {
            if (!File.Exists(fileDB))
                throw new FileNotFoundException();

            SQLiteDataBase db;
            using (FileStream fileStream = new FileStream(fileDB, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                byte[] sql3 = Encoding.ASCII.GetBytes("SQLite format 3");
                
                for (int i = 0; i < sql3.Length; i++)
                    if (sql3[i] != fileStream.ReadByte())
                        throw new FileLoadException();
                
                using (db = new SQLiteDataBase(fileDB))
                {
                    db.OpenConnection();
                    if (db.GetTables().Length <= 0)
                        throw new FileLoadException();
                }
                db = null;
                db = new SQLiteDataBase(fileDB);
                db.CloseConnection();
            }
            if (db == null)
                throw new FileLoadException();

            return db;
        }
        
        /// <summary>
        /// Create a new database with the Table containing the columns specify
        /// </summary>
        /// <param name="fileDB"></param>
        /// <param name="tableName">Table to create</param>
        /// <param name="columns">Columns of the Table</param>
        /// <returns></returns>
        static public SQLiteDataBase CreateDataBase(string fileDB, string tableName, SQLiteColumns columns)
        {
            try {
                if (File.Exists(fileDB))
                    throw new InvalidPathException();

                SQLiteConnection.CreateFile(fileDB);
                SQLiteDataBase db = new SQLiteDataBase(fileDB);
                SQLlog err = SQLlog.Empty;
                
                db.OpenConnection();
                db._SQLcommand(SQLiteTable.SQL_AddTable(tableName, columns), out err);
                
                if (!string.IsNullOrWhiteSpace(err.msgErr))
                    throw err.e;
                db.CloseConnection();
                return db;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
    
    static internal class SQLiteDataReaderExtension
    {
        static internal DataTable GetDataTable(this SQLiteDataReader dataReader)
        {
            DataTable rslt = new DataTable(dataReader.GetTableName(0));
            foreach (DataRow item in dataReader.GetSchemaTable().Rows)
                rslt.Columns.Add(item[0].ToString(), item[12] as Type);

            while (dataReader.Read())
            {
                object[] tbl = new object[dataReader.FieldCount];
                for (int i = 0; i < dataReader.FieldCount; i++)
                    tbl[i] = dataReader[i];

                rslt.Rows.Add(tbl);
            }

            return rslt;
        }
    }
}
