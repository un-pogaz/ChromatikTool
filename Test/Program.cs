using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using LibNoise;


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
            string dd = Chromatik.Unicode.CodePoint.CleanCoderange("U+123");

            LibNoise.Primitive.SimplexPerlin perlin = new LibNoise.Primitive.SimplexPerlin(0, LibNoise.NoiseQuality.Best);

            float ff = LibNoiseExtension.ToRange(1, NoiseRange.Byte);
            ;

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
