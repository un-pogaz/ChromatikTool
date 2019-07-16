using System;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SQLite
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLrequest
    {
        protected SQLrequest(Exception e, string SQL)
        {
            SQLerr = new SQLerr(e, SQL);
        }
        public bool Succes { get { return SQLerr.e == null; } }

        /// <summary>
        /// Represente a SQL error class
        /// </summary>
        public SQLerr SQLerr { get; }

        /// <summary>
        /// The SQL request
        /// </summary>
        public string SQL { get { return SQLerr.SQL; } }

        /// <summary>
        /// Exception at the origin of the error message
        /// </summary>
        public Exception e { get { return SQLerr.e; } }
        /// <summary>
        /// The message error
        /// </summary>
        public string msgErr { get { return SQLerr.msgErr; } }
        
    }

    /// <summary>
    /// Represente a succeful SQL request
    /// </summary>
    public sealed class SQLrequestSucces : SQLrequest
    {
        internal SQLrequestSucces(Exception e, string SQL) : base(e, SQL)
        {
            if (!Succes)
                throw new ArgumentException();
        }
    }
    /// <summary>
    /// Represente a failed SQL request
    /// </summary>
    public sealed class SQLrequestFailed : SQLrequest
    {
        internal SQLrequestFailed(Exception e, string SQL) : base(e, SQL)
        {
            if (Succes)
                throw new ArgumentException();
        }
    }
    
    /// <summary>
    /// Class for return a avanced error message
    /// </summary>
    public class SQLerr
    {
        /// <summary>
        /// Exception at the origin of the error message
        /// </summary>
        public Exception e { get; }
        /// <summary>
        /// The message error
        /// </summary>
        public string msgErr { get; }
        /// <summary>
        /// The SQL request at the origin of the error
        /// </summary>
        public string SQL { get; }

        /// <summary>
        /// Class for return a avanced error message
        /// </summary>
        internal SQLerr(Exception e, string SQL)
        {
            this.e = e;
            this.SQL = SQL;
            if (e != null)
                msgErr = e.Message;
            else
                msgErr = string.Empty;
        }

        override public string ToString()
        {
            return msgErr;
        }

        /// <summary>
        /// Represents the empty SQLerr
        /// </summary>
        static public SQLerr Empty { get; } = new SQLerr(null, string.Empty);
    }
}
