using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace Chromatik.SQLite
{
    static internal class SQLiteDataReaderExtension
    {
        static public DataTable GetDataTable(this SQLiteDataReader dataReader)
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
