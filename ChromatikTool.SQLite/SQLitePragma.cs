using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Enumeration for the AutoVaccum pragma
    /// </summary>
    public enum SQLiteAutoVaccum
    {
        /// <summary>
        /// Auto-vacuum is disabled (default)
        /// </summary>
        None = 0,
        /// <summary>
        /// Auto-vacuum is enabled and fully automatic.
        /// </summary>
        Full = 1,
        /// <summary>
        /// Auto-vacuum is enabled but must be manually activated.
        /// </summary>
        Incremental = 2,
    }

    /// <summary>
    /// Specialized instance for work with the Pragmas values of a <see cref="SQLiteDataBase"/>
    /// </summary>
    public sealed class SQLitePragmas : SQLiteEdit
    {
        /// <summary>
        /// Create a basic instance for work with <see cref="SQLitePragmas"/>
        /// </summary>
        /// <param name="db">The target database</param>
        public SQLitePragmas(SQLiteDataBase db) : base(db, false)
        {
            clsName = nameof(SQLitePragmas);
        }

        object PragmaValue(string pragma, out SQLlog msg)
        {
            DataTable dt = PragmatTable( pragma, out msg);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0];
            else
                return null;
        }
        DataTable PragmatTable(string pragma, out SQLlog msg)
        {
            bool openStart = ConnectionIsOpen;
            OpenConnection();
            DataTable rslt = ExecuteSQLdataTable("PRAGMA " + pragma.Trim(), out msg);
            if (!openStart)
                CloseConnection();
            return rslt;
        }
        void SetPragmatTable(string pragma, string value, out SQLlog msg)
        {
            bool openStart = ConnectionIsOpen;
            OpenConnection();
            ExecuteSQLcommand("PRAGMA " + pragma.Trim() + " = " + value, out msg);
            if (!openStart)
                CloseConnection();
        }
        
        /// <summary>
        /// ApplicationID value
        ///  </summary>
        public int ApplicationID
        {
            get
            {
                SQLlog log = SQLlog.Empty;
                try
                {
                    return (int)PragmaValue("application_id", out log);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                SQLlog log = SQLlog.Empty;
                SetPragmatTable("application_id", value.ToString(System.Globalization.CultureInfo.InvariantCulture), out log);
            }
        }

        /// <summary>
        /// AutoVaccum value
        ///  </summary>
        public SQLiteAutoVaccum AutoVaccum {
            get {
                SQLlog log = SQLlog.Empty;
                try
                {
                    return (SQLiteAutoVaccum)(int)PragmaValue("auto_vacuum", out log);
                }
                catch
                {
                    return SQLiteAutoVaccum.None;
                }
            }
            set
            {
                SQLlog log = SQLlog.Empty;
                switch (value)
                {
                    case SQLiteAutoVaccum.Full:
                        SetPragmatTable("auto_vacuum", "1", out log);
                        break;
                    case SQLiteAutoVaccum.Incremental:
                        SetPragmatTable("auto_vacuum", "2", out log);
                        break;
                    default: // SQLiteAutoVaccum.None
                        SetPragmatTable("auto_vacuum", "0", out log);
                        break;
                }
            }
        }
        
    }
}
