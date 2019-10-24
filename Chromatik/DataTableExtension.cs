using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace System.Data
{
    /// <summary>
    /// Static clas to extend <see cref="DataTable"/> 
    /// </summary>
    static public class DataTableExtension
    {
        /// <summary>
        /// Get the columns names
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        static public string[] GetColumnsName(this DataTable dataTable)
        {
            return dataTable.Columns.GetNames();
        }
        /// <summary>
        /// Get the columns names
        /// </summary>
        /// <param name="dataColumnCollection"></param>
        /// <returns></returns>
        static public string[] GetNames(this DataColumnCollection dataColumnCollection)
        {
            string[] rslt = new string[dataColumnCollection.Count];
            for (int i = 0; i < dataColumnCollection.Count; i++)
                rslt[i] = dataColumnCollection[i].ColumnName;
            return rslt;
        }

    }
}
