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
        static public string RAmen_STR { get; } = "RAmen";
        static public string RAmen_EXT { get; } = "." + RAmen_STR;
        static public byte[] RAmen_BYT { get; } = Encoding.UTF8.GetBytes(RAmen_STR);

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
            WriteFile(filePath, arrayByte, false);
        }
        /// <summary>
        /// Cuisine un RAmen
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <param name="arrayByte">Byte a écrire</param>
        /// <param name="append">Ajouter les bytes a la fin du fichier (false remplace le fichier)</param>
        public void WriteFile(string filePath, byte[] arrayByte, bool append)
        {
            byte[] src = new byte[0];
            if (append && File.Exists(filePath))
                src = File.ReadAllBytes(filePath);

            File.WriteAllBytes(filePath, GenerateByte(src, arrayByte));
        }

        /// <summary>
        /// Prépare un RAmen textuel
        /// </summary>
        /// <param name="writeText">Texte a écrire</param>
        /// <returns></returns>
        public byte[] GenerateByte(string writeText)
        {
            return GenerateByte(new byte[0], Encoding.UTF8.GetBytes(writeText));
        }
        /// <summary>
        /// Prépare un RAmen
        /// </summary>
        /// <param name="arrayByte">Byte a écrire</param>
        /// <returns></returns>
        public byte[] GenerateByte(byte[] arrayByte)
        {
            return GenerateByte(new byte[0], arrayByte);
        }
        /// <summary>
        /// Prépare un RAmen
        /// </summary>
        /// <param name="source">Byte source</param>
        /// <param name="arrayByte">Byte a écrire a la fin de la source</param>
        /// <returns></returns>
        public byte[] GenerateByte(byte[] source, byte[] arrayByte)
        {
            byte[] send = new byte[arrayByte.LongLength];
            List<byte> map = CreateByteMap(Taste);
            for (long i = 0; i < send.LongLength; i++)
                send[i] = map[(byte)(arrayByte[i] + i + source.LongLength)];

            return source.Concat(send);
        }

        /// <summary>
        /// Cuisine un RAmen textuel
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <param name="writeText">Texte a écrire</param>
        public void WriteText(string filePath, string writeText)
        {
            WriteText(filePath, writeText, false);
        }
        /// <summary>
        /// Cuisine un RAmen textuel
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <param name="writeText">Texte a écrire</param>
        /// <param name="append">Ajouter le texte a la fin du fichier (false remplace le fichier)</param>
        public void WriteText(string filePath, string writeText, bool append)
        {
            WriteFile(filePath, Encoding.UTF8.GetBytes(writeText), append);
        }

        /// <summary>
        /// Mange et digére un RAmen
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <returns>Bytes du fichier (null si il n'existe pas)</returns>
        public byte[] ReadFile(string filePath)
        {
            if (File.Exists(filePath))
                return ReadByte(File.ReadAllBytes(filePath));
            else
                return null;

        }
        /// <summary>
        /// Mange et digére un RAmen
        /// </summary>
        /// <param name="arrayByte">Byte a écrire</param>
        /// <returns></returns>
        public byte[] ReadByte(byte[] arrayByte)
        {
            byte[] rslt = new byte[0];

            rslt = new byte[arrayByte.LongLength];
            List<byte> map = CreateByteMap(Taste);

            for (long i = 0; i < rslt.LongLength; i++)
                rslt[i] = (byte)(map.IndexOf(arrayByte[i]) - i);

            return rslt;
        }

        /// <summary>
        /// Mange et digére un RAmen textuel
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <returns>Texte du fichier (null si il n'existe pas)</returns>
        public string ReadText(string filePath)
        {
            if (File.Exists(filePath))
                return ReadText(ReadFile(filePath));
            else
                return null;
        }
        /// <summary>
        /// Mange et digére un RAmen textuel
        /// </summary>
        /// <param name="filePath">Fichier cible</param>
        /// <returns>Texte du fichier (null si il n'existe pas)</returns>
        public string ReadText(byte[] arrayByte)
        {
            return Encoding.UTF8.GetString(arrayByte);
        }

        private List<byte> CreateByteMap(string key)
        {
            string md5_1 = CreateBase64sting(HashKey.FromMD5(key + HashKey.FromMD5(HashKey.FromMD5(key) + key)));
            string md5_2 = CreateBase64sting(HashKey.FromMD5(md5_1 + HashKey.FromMD5(HashKey.FromMD5(md5_1) + md5_1)));

            string md5_3 = CreateBase64sting(HashKey.FromMD5(key + HashKey.FromMD5(CreateBase64sting(md5_1) + md5_1)));
            string md5_4 = CreateBase64sting(HashKey.FromMD5(md5_2 + CreateBase64sting(HashKey.FromMD5(md5_2) + md5_3)));

            byte[] b64_1 = ExtraTable(CreateBase64Array(md5_1));
            byte[] b64_2 = ExtraTable(CreateBase64Array(md5_2));

            byte[] b64_3 = ExtraTable(CreateBase64Array(md5_3));
            byte[] b64_4 = ExtraTable(CreateBase64Array(md5_4));

            byte[] b64_5 = ExtraTable(CreateBase64Array(md5_3 + md5_4 + md5_1 + md5_2 + md5_4));
            byte[] b64_6 = ExtraTable(CreateBase64Array(CreateBase64sting(HashKey.FromMD5(md5_4 + md5_1 + md5_3 + md5_2))));

            byte[] b64_7 = ExtraTable(CreateBase64Array(key));
            byte[] b64_8 = ExtraTable(CreateBase64Array(md5_1 + md5_2 + md5_3 + md5_4));

            byte[] b64_9 = b64_6;
            for (int i = 0; i < b64_9.Length; i++)
                b64_9[i] = (byte)(b64_6[i] / 2);
            
            byte[] b64_10 = b64_2;
            for (int i = 0; i < b64_9.Length; i++)
                b64_10[i] = (byte)(b64_2[i] / 2 + byte.MaxValue / 2);
            
            byte[] b64_0 = new byte[byte.MaxValue + 1];
            for (int i = 0; i < b64_0.Length; i++)
                b64_0[i] = (byte)i;

            byte[][] b_tbl = new byte[][] { b64_0, b64_1, b64_2, b64_3, b64_4, b64_5, b64_6, b64_7, b64_8, b64_9, b64_10 };

            List<byte> rslt = new List<byte>();
            
            byte b_t = 0;
            for (int l = 0; rslt.Count <= byte.MaxValue; l++)
                for (int i = 0; i <= byte.MaxValue; i++)
                {
                    if (l == 0)
                        b_t = CreatByte(i, b_tbl[1], b_tbl[2], b_tbl[7], b_tbl[5]);

                    else if (l == 1)
                        b_t = CreatByte(i, b_tbl[8], b_tbl[8], b_tbl[3], b_tbl[4]);

                    else if (l == 2)
                        b_t = CreatByte(i, b_tbl[2], b_tbl[2], b_tbl[2], b_tbl[2]);

                    else if (l == 3)
                        b_t = CreatByte(i, b_tbl[7], b_tbl[1], b_tbl[2], b_tbl[1]);

                    else if (l == 4)
                        b_t = CreatByte(i, b_tbl[8], b_tbl[7], b_tbl[6], b_tbl[2]);

                    else if (l == 5)
                        b_t = CreatByte(i, b_tbl[5], b_tbl[5], b_tbl[1], b_tbl[3]);

                    else if (l == 6)
                        b_t = CreatByte(i, b_tbl[4], b_tbl[7], b_tbl[2], b_tbl[1]);

                    else if (l == 7)
                        b_t = CreatByte(i, b_tbl[1], b_tbl[6], b_tbl[4], b_tbl[3]);

                    else if (l == 8)
                        b_t = CreatByte(i, b_tbl[8], b_tbl[7], b_tbl[6], b_tbl[5]);

                    else if (l == 9)
                        b_t = CreatByte(i, b_tbl[4], b_tbl[5], b_tbl[2], b_tbl[3]);

                    else if (l == 10)
                        b_t = CreatByte(i, b_tbl[7], b_tbl[5], b_tbl[3], b_tbl[3]);
                    
                    else if (l == 11)
                        b_t = CreatByte(i, b_tbl[7], b_tbl[1], b_tbl[3], b_tbl[4]);

                    else if (l == 12)
                        b_t = CreatByte(i, b_tbl[4], b_tbl[5], b_tbl[3], b_tbl[4]);

                    else if (l == 13)
                        b_t = CreatByte(i, b_tbl[4], b_tbl[1], b_tbl[2], b_tbl[8]);

                    else if (l == 14)
                        b_t = CreatByte(i, b_tbl[5], b_tbl[4], b_tbl[1], b_tbl[8]);

                    else if (l == 15)
                        b_t = CreatByte(i, b_tbl[7], b_tbl[2], b_tbl[4], b_tbl[8]);

                    else if (l == 16)
                        b_t = CreatByte(i, b_tbl[6], b_tbl[3], b_tbl[2], b_tbl[4]);


                    else if (l == 17)
                        b_t = b64_9[i];

                    else if (l == 18)
                        b_t = b64_10[i];


                    else
                        b_t = (byte)(byte.MaxValue - i);

                    if (!rslt.Contains(b_t))
                        if (l <= 18)
                            rslt.Add(b_t);
                        else
                            rslt.Insert(i, b_t);

                    if (rslt.Count > byte.MaxValue)
                        break;
                }
            
            return rslt;
        }

        static private byte CreatByte(int i, byte[] tbl1, byte[] tbl2, byte[] tbl3, byte[] tbl4)
        {
            return (byte)(tbl1[i] + tbl2[i] + tbl3[i] + tbl4[i]);
        }

        static private string CreateBase64sting(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text)).Trim('=');
        }

        static private byte[] CreateBase64Array(string text)
        {
            return Encoding.UTF8.GetBytes(CreateBase64sting(text));
        }

        static private byte[] ExtraTable(byte[] tbl)
        {
            while (tbl.Length < byte.MaxValue)
                tbl = tbl.Concat(Encoding.UTF8.GetBytes(Convert.ToBase64String(tbl).Trim('=')));

            return tbl;
        }
        
    }
}
