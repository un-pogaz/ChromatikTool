using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SQLite;
using System.Data.SQLite.Linq;
using System.Data.SQLite.Generic;

namespace Chromatik.SQLite
{
    /// <summary>
    /// The basic instance for represented a SQLite database
    /// </summary>
    public sealed class SQLiteDataBase : IDisposable
    {
        public string filePath { get; }
        public string fullPath { get { return Path.GetFullPath(filePath); } }

        public string ConnectionSting { get { IsDisposed(); return "Data Source="+ fullPath + ";Version=3;"; } }

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

            SQLiteCommand rslt;
            if (DBconnect == null || !ConnectionIsOpen)
            {
                if (DBconnect != null)
                    DBconnect.Dispose();

                DBconnect = new SQLiteConnection(ConnectionSting).OpenAndReturn();
            }
            else
            {
                if (DBconnect == null)
                    OpenConnection();
            }

            rslt =  new SQLiteCommand(DBconnect);
            rslt.CommandType = CommandType.Text;
            rslt.CommandTimeout = 30; // seconde
            rslt.CommandText = SQL.Trim();

            return rslt;
        }
        
        public bool ConnectionIsOpen { get { IsDisposed(); return (DBconnect.State == ConnectionState.Open); } }

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
                DBconnect = new SQLiteConnection(ConnectionSting).OpenAndReturn();
                return ConnectionIsOpen;
            }
            return false;
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

        private CommandBehavior CloseBehavior()
        {
            if (ConnectionIsOpen)
                return CommandBehavior.CloseConnection;

            return CommandBehavior.Default;
        }

        /// <summary>
        /// Execute the SQL command and return the number of rows inserted/updated affected by it.
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>Number of rows inserted/updated affected by it</returns>
        internal int _SQLcommand(string SQL, out SQLerr msgErr)
        {
            IsDisposed();

            SQLrequest request;
            int rslt = -1;
            try
            {
                rslt = CreatDBcommand(SQL).ExecuteNonQuery(CloseBehavior());
                request = new SQLrequestSucces(null, SQL);
            }
            catch (Exception e)
            {
                rslt = - 1;
                request = new SQLrequestFailed(e, SQL);
            }

            msgErr = request.SQLerr;
            AddLogEnty(request);
            return rslt;
        }
        /// <summary>
        /// Obtains a DataTable corresponding to the SQL request 
        /// </summary>
        /// <param name="SQL">The SQL request to be executed</param>
        /// <param name="msgErr">Advanced error message</param>
        /// <remarks>Will automatically open and close a new connection if it is not opened</remarks>
        /// <returns>The DataTable request</returns>
        internal DataTable _SQLdataTable(string SQL, out SQLerr msgErr)
        {
            IsDisposed();

            SQLrequest request;
            DataTable rslt = new DataTable();
            try
            {
                SQLiteDataReader data = CreatDBcommand(SQL).ExecuteReader(CloseBehavior());

                DataTable dt = new DataTable(data.GetTableName(0));
                foreach (DataRow item in data.GetSchemaTable().Rows)
                    dt.Columns.Add(item[0].ToString(), item[12] as Type);

                while (data.Read())
                {
                    object[] tbl = new object[data.FieldCount];
                    for (int i = 0; i < data.FieldCount; i++)
                        tbl[i] = data[i];

                    dt.Rows.Add(tbl);
                }
                
                rslt = dt;
                request = new SQLrequestSucces(null, SQL);
            }
            catch (Exception e)
            {
                rslt = new DataTable();
                request = new SQLrequestFailed(e, SQL);
            }

            msgErr = request.SQLerr;
            AddLogEnty(request);
            return rslt;
        }
        
        /// <summary>
        /// List of all SQL request executed with this instance
        /// </summary>
        public SQLrequest[] Logs { get { IsDisposed(); return _Logs.ToArray(); } }
        /// <summary>
        /// List of all SQL request executed with this instance
        /// </summary>
        public string[] LogsSQL
        {
            get {
                IsDisposed();
                List<string> lst = new List<string>();
                foreach (SQLrequest item in _Logs)
                    lst.Add(item.SQL);
                return lst.ToArray();
            }
        }
        private List<SQLrequest> _Logs = new List<SQLrequest>();
        private void AddLogEnty(SQLrequest SQLrequest)
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
            SQLerr msgErr;
            List<string> lst = new List<string>();
            DataTable dt = _SQLdataTable(SQLiteMaster.CreatSQL_master("type='table'", "name", "name"), out msgErr);
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

                List<byte> test = new List<byte>();
                for (int i = 0; i < sql3.Length; i++)
                    if (sql3[i] != fileStream.ReadByte())
                        throw new FileLoadException();

                db = new SQLiteDataBase(fileDB);
                if (db.GetTables().Length <= 0)
                    throw new FileLoadException();
            }
            if (db == null)
                throw new FileLoadException();

            return db;
        }


        /// <summary>
        /// Create a empty file
        /// </summary>
        /// <returns>Returns the path</returns>
        static public void CreateFile(string fileDB)
        {
            SQLiteConnection.CreateFile(fileDB);
        }

        /// <summary>
        /// Create a new database with the Table containing the columns specify
        /// </summary>
        /// <param name="fileDB"></param>
        /// <param name="tableName">Table to create</param>
        /// <param name="columns">Columns of the Table</param>
        /// <param name="msgErr"></param>
        /// <returns></returns>
        static public SQLiteDataBase CreatDataBase(string fileDB, string tableName, string columns)
        {
            try {
                if (File.Exists(fileDB))
                    File.Delete(fileDB);
                
                CreateFile(fileDB);
                SQLiteDataBase db = new SQLiteDataBase(fileDB);
                SQLerr err = SQLerr.Empty;
                db._SQLcommand(SQLiteTable.CreatSQL_AddTable(tableName, columns), out err);
                
                if (!string.IsNullOrWhiteSpace(err.msgErr))
                    throw err.e;

                return db;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}
