using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Type of columns in SQLite database
    /// </summary>
    public enum SQLiteColumnsType
    {
        /// <summary></summary>
        Integer,
        /// <summary></summary>
        DateTime,
        /// <summary></summary>
        Real,
        /// <summary></summary>
        Text,
        /// <summary></summary>
        BLOB,
    }

    /// <summary>
    /// Collection of columns for a <see cref="SQLiteDataBase"/>
    /// </summary>
    sealed public class SQLiteColumnsCollection : Dictionary<string, SQLiteColumn>
    {
        /// <summary>
        /// Collection of columns for a <see cref="SQLiteDataBase"/>
        /// </summary>
        public SQLiteColumnsCollection() : base(StringComparer.InvariantCultureIgnoreCase)
        { }

        /// <summary>
        /// Collection of the columns name of this <see cref="SQLiteColumnsCollection"/>
        /// </summary>
        new public string[] Keys { get { return base.Keys.ToArray(); } }

        /// <summary>
        /// Collection of the <see cref="SQLiteColumn"/> of this <see cref="SQLiteColumnsCollection"/>
        /// </summary>
        new public SQLiteColumn[] Values { get { return base.Values.ToArray(); } }

        /// <summary>
        /// Add column
        /// </summary>
        public void Add(string name, SQLiteColumnsType type)
        {
            Add(name, type, null);
        }
        /// <summary>
        /// Add column
        /// </summary>
        public void Add(string name, SQLiteColumnsType type, object defaultValue)
        {
            Add(new SQLiteColumn(name, type, null));
        }
        /// <summary>
        /// Add column
        /// </summary>
        public void Add(SQLiteColumn column)
        {
            base.Add(column.Name, column);
        }
        /// <summary>
        /// Add column
        /// </summary>
        new public void Add(string name, SQLiteColumn column)
        {
            if (name.Equals(column.Name, StringComparison.InvariantCultureIgnoreCase))
                base.Add(column.Name, column);
            else
                throw new ArgumentException("The key name and the column name as not equal.");
        }

        /// <summary></summary>
        override public string ToString()
        {
            if (Count > 0)
            {
                string[] rslt = new string[Count];
                for (int i = 0; i < Count; i++)
                    rslt[i] = Values[i].ToString();
                return rslt.Join(", ");
            }
            else
                return string.Empty;
        }
    }

    /// <summary>
    /// Represent a column
    /// </summary>
    sealed public class SQLiteColumn
    {
        /// <summary>
        /// String format for TimeStamp column (yyyy-MM-dd hh:mm:ss.ffffff)
        /// </summary>
        static public string DateTimeFormat { get; } = "yyyy-MM-dd hh:mm:ss.ffffff";
        /// <summary>
        /// String format for TimeStamp column (yyyy-MM-dd hh:mm:ss)
        /// </summary>
        static public string DateTimeBasicFormat { get; } = "yyyy-MM-dd hh:mm:ss";

        /// <summary>
        /// Name of the column
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Type of the column
        /// </summary>
        public SQLiteColumnsType Type { get; }
        /// <summary>
        /// Default value of the column
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        /// Create a new column with the specified name and type
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public SQLiteColumn(string name, SQLiteColumnsType type) : this(name, type, null)
        { }
        /// <summary>
        /// Create a new column with the specified name, type and default value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="defaultValue"></param>
        public SQLiteColumn(string name, SQLiteColumnsType type, object defaultValue)
        {
            Name = name.Trim().Regex("(\n|\r)", "");
            Type = type;
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// Obtains the string of the type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        static public string GetTypeString(SQLiteColumnsType type)
        {
            switch (type)
            {
                case SQLiteColumnsType.Integer:
                    return "INTEGER";
                case SQLiteColumnsType.DateTime:
                    return "DATETIME";
                case SQLiteColumnsType.Real:
                    return "REAL";
                case SQLiteColumnsType.Text:
                    return "TEXT";
                default: // SQLiteColumnsType.BLOB
                    return "BLOB";
            }
        }

        /// <summary></summary>
        override public string ToString()
        {
            string rslt = Name.ToSQLiteFormat() + " " + GetTypeString(Type);

            if (DefaultValue != null)
            {
                rslt += " DEFAULT ";

                switch (Type)
                {
                    case SQLiteColumnsType.Integer:
                        rslt += DefaultValue.ToString();
                        break;
                    case SQLiteColumnsType.DateTime:
                        {
                            string s = DefaultValue.ToString();
                            if (s.Contains("CURRENT") || s.Contains("current"))
                                rslt += "CURRENT_TIMESTAMP";
                            else
                                rslt += ((DateTime)DefaultValue).ToString(DateTimeFormat);
                            break;
                        }
                    case SQLiteColumnsType.Real:
                        rslt += DefaultValue.ToString();
                        break;
                    case SQLiteColumnsType.Text:
                        rslt += DefaultValue.ToString();
                        break;
                    default: // SQLiteColumnsType.BLOB
                        rslt += DefaultValue;
                        break;
                }
            }

            return rslt;
        }
    }
}
