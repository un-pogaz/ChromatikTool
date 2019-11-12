using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Chromatik.Zip;


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
            List<KeyValuePair<int, string>> lst = new List<KeyValuePair<int, string>>();
            lst.Add(8, "z");
            lst.Add(9, "r");
            lst.Add(0, "t");
            lst.Add(1, "a");
            ;
            lst.Sort(ListKeyValueSort.KeysAndValues);
            ;
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
    
}
