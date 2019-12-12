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
using System.Security.Cryptography.Machine;


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
            Rotor Ia = Rotor.I;
            Ia.InitialPosition = Ia.OperatingAlphabet[5];
            Hexa h = new Hexa(byte.MaxValue);

            Enigma enigma1 = new Enigma(Reflector.B, Ia, Rotor.II, Rotor.III);
            string test = enigma1.Process("HELLO WORLD !");
            enigma1.Reset();
            string rslt0 = enigma1.Process(test);
            
            Enigma enigma3 = enigma1.Clone(true);
            string rslt3 = enigma3.Process(test);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
