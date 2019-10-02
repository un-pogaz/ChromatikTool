using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Chromatik.RAmen
{
    /// <summary>
    /// Une instance pour obfusqué un fichier ou un texte
    /// </summary>
    sealed public class RAmen
    {
        static public string RAmen { get; } = "RAmen";

        /// <summary>
        /// Saveur du RAmen (Clée d'obfusquation)
        /// </summary>
        public string Taste { get; }

        /// <summary>
        /// Instance pour crée des RAmen a la saveur spécifier
        /// </summary>
        /// <param name="taste">Saveur du RAmen (Clée d'obfusquation)</param>
        public RAmen(string taste)
        {
            if (string.IsNullOrEmpty(taste))
                taste = "Chromatik";
            Taste = taste;
        }

        /// <summary>
        /// Cuisine un RAmen
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <param name="arrayByte">Byte a écrire</param>
        public void WriteFile(string filePath, byte[] arrayByte)
        {
            using (MemoryStream stream = new MemoryStream(arrayByte))
                WriteFile(filePath, stream);
        }
        /// <summary>
        /// Cuisine un RAmen
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="stream"></param>
        public void WriteFile(string filePath, Stream stream)
        {
            long initPosition = stream.Position;
            stream.Position = 0;
            using (Stream encode = EncodeStream(stream))
            {
                using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    encode.CopyTo(file);
            }

            stream.Position = initPosition;
        }

        /// <summary>
        /// Prépare un RAmen textuel
        /// </summary>
        /// <param name="writeText"></param>
        /// <returns></returns>
        public byte[] GenerateByte(string writeText)
        {
            return GenerateByte(UTF8SansBomEncoding.Default.GetBytes(writeText));
        }
        /// <summary>
        /// Prépare un RAmen
        /// </summary>
        /// <param name="arrayByte">Byte a écrire</param>
        /// <returns></returns>
        public byte[] GenerateByte(byte[] arrayByte)
        {
            using (MemoryStream stream = new MemoryStream(arrayByte))
                return EncodeStream(stream).ToArray();
        }

        /// <summary>
        /// Prépare un RAmen
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public MemoryStream EncodeStream(Stream stream)
        {
            long initPosition = stream.Position;
            stream.Position = 0;
            MemoryStream rslt = new MemoryStream();
            SimplexPerlin perlin = CreateSimplexPerlin(Taste);
            int b = stream.ReadByte();
            while (b > 0)
            {
                rslt.WriteByte(GetByte(perlin, Taste, (byte)b, stream.Position - 1));
                b = stream.ReadByte();
            }
            perlin = null;

            rslt.Position = 0;
            stream.Position = initPosition;
            return rslt;
        }
        
        /// <summary>
        /// Cuisine un RAmen textuel
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <param name="writeText">Texte a écrire</param>
        public void WriteText(string filePath, string writeText)
        {
            WriteFile(filePath, UTF8SansBomEncoding.Default.GetBytes(writeText));
        }

        /// <summary>
        /// Mange et digére un RAmen
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <returns>Bytes du fichier (null si il n'existe pas)</returns>
        public byte[] DecodeFile(string filePath)
        {
            return DecodeFileStream(filePath).ToArray();
        }
        /// <summary>
        /// Mange et digére un RAmen
        /// </summary>
        /// <param name="arrayByte">Byte a écrire</param>
        /// <returns></returns>
        public byte[] DecodeByte(byte[] arrayByte)
        {
            using (MemoryStream stream = new MemoryStream(arrayByte))
                return ((MemoryStream)DecodeStream(stream)).ToArray();
        }
        /// <summary>
        /// Mange et digére un RAmen
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public MemoryStream DecodeFileStream(string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return DecodeStream(file);
        }
        /// <summary>
        /// Mange et digére un RAmen
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public MemoryStream DecodeStream(Stream stream)
        {
            long initPosition = stream.Position;
            stream.Position = 0;
            MemoryStream rslt = new MemoryStream();
            SimplexPerlin perlin = CreateSimplexPerlin(Taste);
            int b = stream.ReadByte();
            while (b > 0)
            {
                rslt.WriteByte(ReadByte(perlin, Taste, (byte)b, stream.Position - 1));
                b = stream.ReadByte();
            }
            perlin = null;

            rslt.Position = 0;
            stream.Position = initPosition;
            return rslt;
        }

        /// <summary>
        /// Mange et digére un RAmen textuel
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <returns>Texte du fichier (null si il n'existe pas)</returns>
        public string ReadText(string filePath)
        {
            return ReadText(DecodeFile(filePath));
        }
        /// <summary>
        /// Mange et digére un RAmen textuel
        /// </summary>
        /// <param name="arrayByte"></param>
        /// <returns></returns>
        public string ReadText(byte[] arrayByte)
        {
            return UTF8SansBomEncoding.Default.GetString(arrayByte);
        }


        private SimplexPerlin CreateSimplexPerlin(string taste)
        {
            SimplexPerlin tmp = new SimplexPerlin(0, NoiseQuality.Best);
            int s = 0;
            for (int i = 0; i < taste.Length; i++)
                s += (int)tmp.GetValue((int)taste[i], -i, NoiseRange.Byte);
            tmp = null; 

            return new SimplexPerlin(s, NoiseQuality.Best);
        }

        private byte Get_X(SimplexPerlin simplexPerlin, string taste, long lenght)
        {
            return (byte)simplexPerlin.GetValue(lenght, (lenght / 2), -lenght * 1.5f, NoiseRange.Byte);
        }
        private byte Get_Y(SimplexPerlin simplexPerlin, string taste, long lenght)
        {
            return (byte)simplexPerlin.GetValue(taste.Length, lenght * 2, lenght / 3, NoiseRange.Byte);
        }

        private byte GetByte(SimplexPerlin simplexPerlin, string taste, byte b, long lenght)
        {
            byte x = Get_X(simplexPerlin, taste, lenght);
            byte y = Get_Y(simplexPerlin, taste, lenght);

            return (byte)(x + y + b);
        }
        private byte ReadByte(SimplexPerlin simplexPerlin, string taste, byte b, long lenght)
        {
            byte x = Get_X(simplexPerlin, taste, lenght);
            byte y = Get_Y(simplexPerlin, taste, lenght);

            return (byte)(b - y - x);
        }

    }
}
