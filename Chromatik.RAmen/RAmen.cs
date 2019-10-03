using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Chromatik.RAmen
{
    /// <summary>
    /// Permet de cuisiner des RAmen a partir de fichier ou de text
    /// </summary>
    sealed public class RAmen
    {
        static UTF8Encoding UTF8SansBomEncoding = new UTF8Encoding(false);

        /// <summary>
        /// Saveur du RAmen
        /// </summary>
        public string Taste { get; }

        /// <summary>
        /// Convoque un cuisinier de RAmen spécialisé dans la saveur spécifier
        /// </summary>
        /// <param name="taste">Saveur du RAmen</param>
        public RAmen(string taste)
        {
            if (string.IsNullOrEmpty(taste))
                taste = "Chromatik";
            Taste = taste;
        }


        /// <summary>
        /// Cuisine un RAmen
        /// </summary>
        /// <param name="arrayByte">Byte a écrire</param>
        /// <returns></returns>
        public byte[] EncodeByte(byte[] arrayByte)
        {
            using (MemoryStream stream = new MemoryStream(arrayByte))
                return EncodeStream(stream).ToArray();
        }

        /// <summary>
        /// Cuisine un RAmen
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <param name="arrayByte">Byte a écrire</param>
        public void WriteFileByte(string filePath, byte[] arrayByte)
        {
            using (MemoryStream stream = new MemoryStream(arrayByte))
                WriteFileStream(filePath, stream);
        }
        /// <summary>
        /// Cuisine un RAmen
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="stream"></param>
        public void WriteFileStream(string filePath, Stream stream)
        {
            using (Stream encode = EncodeStream(stream))
            {
                using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    encode.CopyTo(file);
            }
        }
        
        /// <summary>
        /// Cuisine un RAmen textuel
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public byte[] EncodeText(string text)
        {
            return EncodeByte(UTF8SansBomEncoding.GetBytes(text));
        }
        /// <summary>
        /// Cuisine un RAmen textuel
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <param name="writeText">Texte a écrire</param>
        public void WriteText(string filePath, string writeText)
        {
            WriteFileByte(filePath, UTF8SansBomEncoding.GetBytes(writeText));
        }

        /// <summary>
        /// Mange et digére un RAmen
        /// </summary>
        /// <param name="arrayByte">Byte a écrire</param>
        /// <returns></returns>
        public byte[] DecodeByte(byte[] arrayByte)
        {
            using (MemoryStream stream = new MemoryStream(arrayByte))
                return DecodeStream(stream).ToArray();
        }

        /// <summary>
        /// Mange et digére un RAmen
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <returns>Bytes du fichier (null si il n'existe pas)</returns>
        public byte[] ReadFileByte(string filePath)
        {
            return ReadFileStream(filePath).ToArray();
        }
        /// <summary>
        /// Mange et digére un RAmen
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public MemoryStream ReadFileStream(string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return DecodeStream(file);
        }

        /// <summary>
        /// Mange et digére un RAmen textuel
        /// </summary>
        /// <param name="arrayByte"></param>
        /// <returns></returns>
        public string DecodeText(byte[] arrayByte)
        {
            return UTF8SansBomEncoding.GetString(DecodeByte(arrayByte));
        }
        /// <summary>
        /// Mange et digére un RAmen textuel
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <returns>Texte du fichier (null si il n'existe pas)</returns>
        public string ReadText(string filePath)
        {
            return UTF8SansBomEncoding.GetString(ReadFileByte(filePath));
        }


        /// <summary>
        /// Cuisine un RAmen
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public MemoryStream EncodeStream(Stream stream)
        {
            long initPosition = stream.Position;
            MemoryStream rslt = new MemoryStream();
            for (long i = 0; i < stream.Length; i++)
            {
                stream.Position = (stream.Length - 1) - i;
                int b = stream.ReadByte();
                rslt.WriteByte(GetByte(Taste, (byte)b, i));
            }

            rslt.Position = 0;
            stream.Position = initPosition;
            return rslt;
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
            for (long i = 0; i < stream.Length; i++)
            {
                stream.Position = (stream.Length - 1) - i;
                int b = stream.ReadByte();
                rslt.WriteByte(ReadByte(Taste, (byte)b, (stream.Length - 1) - i));
            }

            rslt.Position = 0;
            stream.Position = initPosition;
            return rslt;
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

        private byte Get_X(string taste, long postition)
        {
            return (byte)CreateSimplexPerlin(taste).GetValue(postition, (postition / 2), -postition * 1.5f, NoiseRange.Byte);
        }
        private byte Get_Y(string taste, long postition)
        {
            return (byte)CreateSimplexPerlin(taste).GetValue(taste.Length, postition * 2, postition / 3, NoiseRange.Byte);
        }

        private byte GetByte(string taste, byte b, long postition)
        {
            byte x = Get_X(taste, postition);
            byte y = Get_Y(taste, postition);

            return (byte)(x + y + b);
        }
        private byte ReadByte(string taste, byte b, long postition)
        {
            byte x = Get_X(taste, postition);
            byte y = Get_Y(taste, postition);

            return (byte)(b - y - x);
        }

    }
}
