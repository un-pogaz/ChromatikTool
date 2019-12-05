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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;


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

            JArray hh = new JArray();
            hh.Add("");
            hh.Add(true);

            Chromatik.Cryptography.Enigma.PlugBoard pl = new Chromatik.Cryptography.Enigma.PlugBoard();
            pl.AddPlug('U', 'Y');
            pl.AddPlug('A', 'Z');
            pl.RemovePlug('U');
            pl.RemovePlug('Z');


            Chromatik.Cryptography.Enigma.Enigma enigma = new Chromatik.Cryptography.Enigma.Enigma(Chromatik.Cryptography.Enigma.Reflector.A, Chromatik.Cryptography.Enigma.Rotor.I, Chromatik.Cryptography.Enigma.Rotor.II, Chromatik.Cryptography.Enigma.Rotor.III);

            char ccc = enigma.Process('A');

            Chromatik.Cryptography.Enigma.Enigma enigma2 = new Chromatik.Cryptography.Enigma.Enigma(Chromatik.Cryptography.Enigma.Reflector.A, Chromatik.Cryptography.Enigma.Rotor.I, Chromatik.Cryptography.Enigma.Rotor.II, Chromatik.Cryptography.Enigma.Rotor.III);

            char ddd = enigma2.Process(ccc);
            enigma2.ToString();
            XmlDocument rac = XmlCreate.DocumentXML("<der lang=\"fr\" xml:lang=\"fr\"/>");
            string r = new object[] { "000", rac, "000", null, "000" }.ToOneString("|", StringJoinOptions.NullToNull);
            

            Comparator<FileInfo> copmp = new Comparator<FileInfo>("Length");

            XmlNamespace s = XmlNamespace.Calibre;

            Ini ini = new Ini("test.ini");
            ini.WriteString("section1", "value1", "q45243ſ€é");
            
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
