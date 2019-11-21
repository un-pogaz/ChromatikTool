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
using System.Xml;
using System.Reflection;


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
            IComparer<string> oo = Comparator<string>.Default;
            List<string> lst = new List<string>()
            {
                "sqdf",
                null,
                "111",
                null,
                null,
            };
            lst.Sort(oo);

            Type[] ty = oo.GetType().GetInterfaces();
            Type fa = oo.GetType().GetInterface("IComparer`1");

            DocumentType dt = DocumentType.GetDocumentTypeFromXML("<!DOCTYPE ncx PUBLIC \" -//NISO//DTD ncx 2005-1//EN\" \"http://www.daisy.org/z3986/2005/ncx-2005-1.dtd\"> ");
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static string MakeValidFileName(string name)
        {
            return Path.GetInvalidFileNameChars().Aggregate(name, (current, c) => current.Replace(c, '_'));
        }
    }


    
}
