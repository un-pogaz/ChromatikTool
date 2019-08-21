using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;

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
            string f = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffffff");
            string dd = DateTime.Now.ToString("o");

            string s1 = "hello";
            string s2 = "héllo";

            if (string.Compare(s1, s2, CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace) == 0)
            {
                // both strings are equal
            }

            //"D:\other\Calibre Portable\metadata.db"
            ;
            using (Chromatik.SQLite.SQLiteDataBase db_lite = Chromatik.SQLite.SQLiteDataBase.LoadDataBase(@"D:\other\Calibre Portable\metadata.db"))
            {
                Chromatik.SQLite.SQLlog err;
                using (Chromatik.SQLite.SQLiteData data = new Chromatik.SQLite.SQLiteData(db_lite))
                {
                    DataTable dt1 = data.GetTable("comments", out err);
                    DataTable dt2 = data.GetTable("books", out err);
                    DataTable dt3 = data.GetTable("book", out err);
                    if (err.Succes)
                    {

                    }
                    ;
                }
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
