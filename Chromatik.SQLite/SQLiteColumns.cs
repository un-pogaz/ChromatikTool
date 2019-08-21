using System;
using System.Collections.Generic;
using System.Text;

namespace Chromatik.SQLite
{
    /// <summary>
    /// Type of columns in SQLite database
    /// </summary>
    public enum SQLiteColumnsType
    {
        Integer,
        Numerique,
        TimeStamp,
        Real,
        Text,
        BLOB,
    }

    /// <summary>
    /// Collection of columns for a <see cref="SQLiteDataBase"/>
    /// </summary>
    sealed public class SQLiteColumns : List<SQLiteColumns.Column>
    {

        /// <summary>
        /// Collection of columns for a <see cref="SQLiteDataBase"/>
        /// </summary>
        public SQLiteColumns() : base()
        { }

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
            Add(new Column(name, type, null));
        }
        /// <summary>
        /// Add column
        /// </summary>
        new public void Add(Column column)
        {
            for (int i = 0; i < Count; i++)
                if (this[i].Name == column.Name)
                {
                    Remove(this[i]);
                    break;
                }

            base.Add(column);
        }
                
        /// <summary>
        /// Remove column
        /// </summary>
        public void Remove(string name)
        {
            for (int i = 0; i < Count; i++)
                if (this[i].Name == name.Trim())
                {
                    Remove(this[i]);
                    break;
                }
        }

        /// <summary>
        /// Represent a column
        /// </summary>
        sealed public class Column
        {
            public string Name { get; }
            public SQLiteColumnsType Type { get; }
            public object DefaultValue { get; }
            
            public Column(string name, SQLiteColumnsType type) : this(name, type, null)
            { }
            public Column(string name, SQLiteColumnsType type, object defaultValue )
            {
                Name = name.Trim().Regex("(\n|\r)", "");
                Type = type;
                DefaultValue = defaultValue;
            }

            override public string ToString()
            {
                string rslt = Name;
                switch (Type)
                {
                    case SQLiteColumnsType.Integer:
                        rslt += " INTEGER";
                        break;
                    case SQLiteColumnsType.Numerique:
                        rslt += " NUMERIQUE";
                        break;
                    case SQLiteColumnsType.TimeStamp:
                        rslt += " TIMESTAMP";
                        break;
                    case SQLiteColumnsType.Real:
                        rslt += " REAL";
                        break;
                    case SQLiteColumnsType.Text:
                        rslt += " TEXT";
                        break;
                    case SQLiteColumnsType.BLOB:
                        rslt += " BLOB";
                        break;
                    default:
                        rslt += " BLOB";
                        break;
                }

                if (DefaultValue != null)
                {
                    switch (Type)
                    {
                        case SQLiteColumnsType.Integer:
                            rslt += " " + DefaultValue.ToString();
                            break;
                        case SQLiteColumnsType.Numerique:
                            rslt += " " + DefaultValue.ToString();
                            break;
                        case SQLiteColumnsType.TimeStamp:
                            rslt += " " + ((DateTime)DefaultValue).ToString("yyyy-MM-dd hh:mm:ss");
                            break;
                        case SQLiteColumnsType.Real:
                            rslt += " " + DefaultValue.ToString();
                            break;
                        case SQLiteColumnsType.Text:
                            rslt += " " + DefaultValue.ToString();
                            break;
                        case SQLiteColumnsType.BLOB:
                            rslt += " " + DefaultValue;
                            break;
                        default:
                            rslt += " " + DefaultValue;
                            break;
                    }

                }

                return rslt;
            }
        }
    }
}
