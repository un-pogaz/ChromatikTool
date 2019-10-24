using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chromatik.SQLite;

namespace Test
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SQLiteDataBase db = SQLiteDataBase.CreateDataBase("test", "test", new SQLiteColumnsCollection { new SQLiteColumn("test", SQLiteColumnsType.Text, "".ToSQLiteFormat()) }, true);
            
            string[] f = db.GetTablesName();
            using (SQLiteData data = new SQLiteData(db, true))
            {
                SQLlog l = SQLlog.Empty;
                data.Insert("test", new string[] { "df".ToSQLiteFormat(), "00000".ToSQLiteFormat(), "vvv".ToSQLiteFormat(), "retg".ToSQLiteFormat(), "953".ToSQLiteFormat() }, out l);
                ;

                SQLiteMultiCaseValues sss = new SQLiteMultiCaseValues("test");
                sss.Add("test="+ "df".ToSQLiteFormat(), "FFFF".ToSQLiteFormat());
                sss.Add("test=" + "00000".ToSQLiteFormat(), "O".ToSQLiteFormat());
                
                data.Update("test", sss, out l);

                ;
            }

            ;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
