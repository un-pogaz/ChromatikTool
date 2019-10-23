using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SQLite
{
    public enum SQLiteAutoVaccum
    {
        None = 0,
        Full = 1,
        Incremental = 2,
    }

    public sealed class SQLitePragmas : SQLiteEdit
    {
        public SQLitePragmas(SQLiteDataBase db) : base(db, false)
        {
            clsName = nameof(SQLitePragmas);
        }
        
        protected object PragmaValue(string pragma, out SQLlog msg)
        {
            DataTable dt = PragmatTable( pragma, out msg);
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0];
            else
                return null;
        }
        protected DataTable PragmatTable(string pragma, out SQLlog msg)
        {
            return ExecuteSQLdataTable("PRAGMA " + pragma.Trim(), out msg);
        }
        protected void SetPragmatTable(string pragma, string value, out SQLlog msg)
        {
            ExecuteSQLcommand("PRAGMA " + pragma.Trim() + " = " + value, out msg);
        }


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
