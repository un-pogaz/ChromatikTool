using System;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Represente a SQL request run in a <see cref="SQLiteDataBase"/>
    /// </summary>
    public class SQLlog
    {
        internal SQLlog(Exception e, string SQL)
        {
            this.e = e;
            this.SQL = SQL;
            if (e != null)
                msgErr = e.Message;
            else
                msgErr = string.Empty;
        }

        /// <summary>
        /// State of the SQL request of the log
        /// </summary>
        public bool Succes { get { return e == null; } }

        /// <summary>
        /// The SQL request
        /// </summary>
        public string SQL { get; } = string.Empty;

        /// <summary>
        /// Exception at the origin of the error message
        /// </summary>
        public Exception e { get; } = null;
        /// <summary>
        /// The message error
        /// </summary>
        public string msgErr { get; } = string.Empty;

        /// <summary> </summary>
        override public string ToString()
        {
            return msgErr;
        }

        /// <summary>
        /// Represents a empty <see cref="SQLlog"/> (readonly)
        /// </summary>
        static readonly public SQLlog Empty = new SQLlog(null, string.Empty);
    }
}
