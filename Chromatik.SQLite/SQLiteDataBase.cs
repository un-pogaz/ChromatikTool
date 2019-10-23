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
    public class SQLiteDataBase : IDisposable
    {
        /// <summary>
        /// Path of this database
        /// </summary>
        public string FilePath { get; }
        /// <summary>
        /// Full path of this database
        /// </summary>
        public string FullPath { get { return Path.GetFullPath(FilePath); } }

        /// <summary>
        /// Connection string of this database
        /// </summary>
        public string ConnectionString { get { return "Data Source="+ FullPath + ";Version=3;"; } }

        /// <summary>
        /// The basic instance for manipuled SQLite database
        /// </summary>
        private SQLiteDataBase(string dbPath)
        {
            disposed = false;
            FilePath = dbPath.Trim();
        }

        public override string ToString()
        {
            return "\""+Path.GetFileName(FilePath)+"\"; Conneting: " + ConnectionIsOpen.ToString().ToLower() + "; Requests: " +Logs.Count+";"; 
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
            }

            return ConnectionIsOpen;
        }
        /// <summary>
        /// Close the connection with the database
        /// </summary>
        public void CloseConnection()
        {
            IsDisposed();
            if (ConnectionIsOpen)
            {
                DBconnect.ReleaseMemory();
                DBconnect.Close();
            }
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

            SQL = SQL.Trim();

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

            _logs.AddEntry(msgErr);
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

            SQL = SQL.Trim();

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

            _logs.AddEntry(msgErr);
            return rslt;
        }


        
        
        /// <summary>
        /// List of all SQL request executed with this instance
        /// </summary>
        public StackLogSQL Logs { get { return _logs; } }
        private StackLogSQL _logs = new StackLogSQL();

        /// <summary>
        /// Get the Tables of the database
        /// </summary>
        /// <remarks>A valide database contain minimum 1 table</remarks>
        /// <returns></returns>
        public string[] GetTablesName()
        {
            string[] rslt = new string[0];
            using (SQLiteMaster master = new SQLiteMaster(this, true))
                rslt = master.GetTablesName();
            return rslt;
        }
        
        public void ExecuteVaccum()
        {
            SQLlog log = SQLlog.Empty;
            _SQLcommand("VACUUM", out log);
        }

        public SQLitePragmas Pragmas { get { return new SQLitePragmas(this); } }

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

                if (new SQLiteDataBase(fileDB).GetTablesName().Length <= 0)
                    throw new FileLoadException();
                
                db = new SQLiteDataBase(fileDB);
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
            return CreateDataBase(fileDB, tableName, columns, false);
        }

        /// <summary>
        /// Create a new database with the Table containing the columns specify
        /// </summary>
        /// <param name="fileDB"></param>
        /// <param name="tableName">Table to create</param>
        /// <param name="columns">Columns of the Table</param>
        /// <param name="truncate">Erase the target DB file</param>
        /// <returns></returns>
        static public SQLiteDataBase CreateDataBase(string fileDB, string tableName, SQLiteColumns columns, bool truncate)
        {
            try
            {
                if (File.Exists(fileDB) && !truncate)
                    throw new InvalidPathException("The target file already exist!");

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

    static public class SQLiteStringFormat
    {
        /// <summary>
        /// Parse a <see cref="string"/> to a valide text for <see cref="SQLiteDataBase"/> (useful if it contains a single quote)
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static public string ToSQLiteFormat(this string s)
        {
            string[] split = s.Split('\'');
            for (long i = 0; i < split.LongLength; i++)
                split[i] = "'" + split[i]+ "'";

            return split.ToOneString("", StringOneLineOptions.SkipNull);
        }
    }
}
