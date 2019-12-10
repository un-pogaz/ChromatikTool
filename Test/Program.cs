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
using System.Security.Cryptography.Enigma;


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
            Enigma enigma1 = new Enigma(Reflector.B, Rotor.I, Rotor.II, Rotor.III);
            string test = enigma1.Process("HELLO WORLD !");
            Enigma enigma2 = new Enigma(Reflector.B, Rotor.I, Rotor.II, Rotor.III);
            string rslt = enigma2.Process(test);
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
