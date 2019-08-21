using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace System.Data
{
    static public class DataTableExtension
    {
        static public string[] GetColumnsName(this DataTable dataTable)
        {
            return dataTable.Columns.GetNames();
        }
        static public string[] GetNames(this DataColumnCollection dataColumnCollection)
        {
            string[] rslt = new string[dataColumnCollection.Count];
            for (int i = 0; i < dataColumnCollection.Count; i++)
                rslt[i] = dataColumnCollection[i].ColumnName;
            return rslt;
        }

    }
}
