using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Chromatik.Cryptography
{
    public class FonDue
    {
        static UTF8Encoding UTF8SansBomEncoding = new UTF8Encoding(false);
        
        public string Fromage { get; }
        
        public FonDue(string fromage)
        {
            if (string.IsNullOrEmpty(fromage))
                fromage = "Chromatik";
            Fromage = Fromage;
        }
        
        public byte[] EncodeByte(byte[] arrayByte)
        {
            using (MemoryStream stream = new MemoryStream(arrayByte))
                return EncodeStream(stream).ToArray();
        }
        
        public void WriteFileByte(string filePath, byte[] arrayByte)
        {
            using (MemoryStream stream = new MemoryStream(arrayByte))
                WriteFileStream(filePath, stream);
        }

        public void WriteFileStream(string filePath, Stream stream)
        {
            using (Stream encode = EncodeStream(stream))
            {
                using (FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                    encode.CopyTo(file);
            }
        }

        public byte[] EncodeText(string text)
        {
            return EncodeByte(UTF8SansBomEncoding.GetBytes(text));
        }

        public void WriteText(string filePath, string writeText)
        {
            WriteFileByte(filePath, UTF8SansBomEncoding.GetBytes(writeText));
        }
        
        public byte[] DecodeByte(byte[] arrayByte)
        {
            using (MemoryStream stream = new MemoryStream(arrayByte))
                return DecodeStream(stream).ToArray();
        }
        
        public byte[] ReadFileByte(string filePath)
        {
            return ReadFileStream(filePath).ToArray();
        }
        public MemoryStream ReadFileStream(string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return DecodeStream(file);
        }
        
        public string DecodeText(byte[] arrayByte)
        {
            return UTF8SansBomEncoding.GetString(DecodeByte(arrayByte));
        }
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
                rslt.WriteByte(GetByte((byte)b, i));
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
                rslt.WriteByte(ReadByte((byte)b, (stream.Length - 1) - i));
            }

            rslt.Position = 0;
            stream.Position = initPosition;
            return rslt;
        }


        private SimplexPerlin CreateSimplexPerlin()
        {
            SimplexPerlin tmp = new SimplexPerlin(0, NoiseQuality.Best);
            int s = 0;
            for (int i = 0; i < Fromage.Length; i++)
                s -= (int)tmp.GetValue((int)Fromage[i], -i, NoiseRange.Byte);
            tmp = null;

            return new SimplexPerlin(s, NoiseQuality.Best);
        }

        private byte Get_X(long postition)
        {
            return (byte)CreateSimplexPerlin().GetValue(postition, (postition / 2), -postition * 1.5f, NoiseRange.Byte);
        }
        private byte Get_Y(long postition)
        {
            return (byte)CreateSimplexPerlin().GetValue(Fromage.Length, postition * 2, postition / 3, NoiseRange.Byte);
        }

        private byte GetByte(byte b, long postition)
        {
            byte x = Get_X(postition);
            byte y = Get_Y(postition);

            return (byte)(x + y + b);
        }
        private byte ReadByte(byte b, long postition)
        {
            byte x = Get_X(postition);
            byte y = Get_Y(postition);

            return (byte)(b - y - x);
        }



    }
}

