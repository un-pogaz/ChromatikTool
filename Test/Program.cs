using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            System.IO.FileInfo f = new System.IO.FileInfo(@"E:\Calibre\Perry Rhodan\metadata - Copie.db");
            decimal o = f.Length;
            decimal ko = f.LengthKo();
            decimal mo = f.LengthMo();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
