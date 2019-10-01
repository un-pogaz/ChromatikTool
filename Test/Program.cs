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
            LibNoise.Primitive.SimplexPerlin noise = new LibNoise.Primitive.SimplexPerlin();

            float sdd = noise.GetValue(2, NoiseRange.Byte);

            float[,,] rslt = noise.GetCube(byte.MaxValue, byte.MaxValue, byte.MaxValue, 0.01f, NoiseRange.Byte);
            long ae = rslt.LongLength;

            Bitmap map = new Bitmap(3000, 3000);

            float scale = 0.0009f; // 3000
            //float scale = 0.0025f; // 1000
            //float scale = 0.01f; // 255

            float[,] r = noise.GetPlane(map.Width, map.Height, scale, NoiseRange.Byte, map.Width*0, map.Height*0);
            float[,] g = noise.GetPlane(map.Width, map.Height, scale, NoiseRange.Byte, map.Width*1, map.Height*0);
            float[,] b = noise.GetPlane(map.Width, map.Height, scale, NoiseRange.Byte, map.Width*0, map.Height*1);


            for (int w = 0; w < map.Width; w++)
                for (int h = 0; h < map.Height; h++)
                    map.SetPixel(w, h, Color.FromArgb((byte)r[w, h], (byte)r[w, h], (byte)r[w, h]));

            map.SavePNG("mapR.png");
            ;

            /*
            for (int w = 0; w < map.Width; w++)
                for (int h = 0; h < map.Height; h++)
                    map.SetPixel(w, h, Color.FromArgb((byte)g[w, h], (byte)g[w, h], (byte)g[w, h]));

            map.SavePNG("mapG.png");


            for (int w = 0; w < map.Width; w++)
                for (int h = 0; h < map.Height; h++)
                    map.SetPixel(w, h, Color.FromArgb((byte)b[w, h], (byte)b[w, h], (byte)b[w, h]));

            map.SavePNG("mapB.png");
            */

            for (int w = 0; w < map.Width; w++)
                for (int h = 0; h < map.Height; h++)
                    map.SetPixel(w, h, Color.FromArgb((byte)r[w, h], (byte)g[w, h], (byte)b[w, h]));

            map.SavePNG("map.png");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
