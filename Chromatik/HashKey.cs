using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace System.Security.Cryptography
{
    /// <summary>
    /// Class pour permetre l'obtention rapide des empreintes de hachage 
    /// </summary>
    static public partial class HashKey
    {
        /// <summary>
        /// Énumeration des algorithmes de hachage supporté
        /// </summary>
        public enum Algorithm
        {
            MD5,
            SHA1,
            SHA256,
            SHA384,
            SHA512,
            KeyedHashAlgorithm,
            RIPEMD160,
        }

        static private string HexToString(byte[] arrayByte)
        {
            string rslt = string.Empty;
            for (int i = 0; i < arrayByte.Length; i++)
                rslt += arrayByte[i].ToString("x2");
            return rslt;
        }

        static private byte[] UTF8(string text) { return Encoding.UTF8.GetBytes(text); }

        /// <summary>
        /// Obtient le <see cref="HashAlgorithm"/> correspondant a <see cref="Algorithm"/>
        /// </summary>
        /// <param name="algorithm">Algorithme demandé</param>
        /// <returns></returns>
        static public HashAlgorithm GetAlgorithm(Algorithm algorithm)
        {
            switch (algorithm)
            {
                case Algorithm.MD5:
                    return MD5.Create();

                case Algorithm.SHA1:
                    return SHA1.Create();

                case Algorithm.SHA256:
                    return SHA1.Create();

                case Algorithm.SHA384:
                    return SHA1.Create();

                case Algorithm.SHA512:
                    return SHA1.Create();

                case Algorithm.RIPEMD160:
                    return RIPEMD160.Create();

                case Algorithm.KeyedHashAlgorithm:
                    return KeyedHashAlgorithm.Create();

                default:
                    return SHA1.Create();
            }
        }

        /// <summary>
        /// Obtient l'empreinte d'un <see cref="string"/> en fontion de l'algorithme spécifier
        /// </summary>
        /// <param name="text">Texte a haché</param>
        /// <param name="algorithm">Algorithme de hachage</param>
        /// <returns>Hash du texte</returns>
        static public string FromAlgorithm(Algorithm algorithm, string text)
        {
            return FromAlgorithm(algorithm, UTF8(text));
        }
        /// <summary>
        /// Obtient l'empreinte d'une collection de byte en fontion de l'algorithme spécifier
        /// </summary>
        /// <param name="arrayByte">Collection de byte a haché</param>
        /// <param name="algorithm">Algorithme de hachage</param>
        /// <returns>Hash de la collection de byte</returns>
        static public string FromAlgorithm(Algorithm algorithm, byte[] arrayByte)
        {
            HashAlgorithm a = GetAlgorithm(algorithm);
            byte[] h = a.ComputeHash(arrayByte);
            a.Clear();
            a.Dispose();
            return HexToString(h);
        }
        /// <summary>
        /// Obtient l'empreinte d'un <see cref="Stream" /> en fontion de l'algorithme spécifier
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> a haché</param>
        /// <param name="algorithm">Algorithme de hachage</param>
        /// <returns>Hash du <see cref="Stream" /></returns>
        static public string FromAlgorithm(Algorithm algorithm, Stream stream)
        {
            HashAlgorithm a = GetAlgorithm(algorithm);
            byte[] h = a.ComputeHash(stream);
            a.Clear();
            a.Dispose();
            return HexToString(h);
        }

        /// <summary>
        /// Obtient l'empreinte MD5 d'un <see cref="string"/>
        /// </summary>
        /// <param name="text">Texte a haché</param>
        /// <returns>Hash du texte</returns>
        static public string FromMD5(string text) { return FromMD5(UTF8(text)); }
        /// <summary>
        /// Obtient l'empreinte MD5 d'une collection de byte
        /// </summary>
        /// <param name="arrayByte">Collection de byte a haché</param>
        /// <returns>Hash de la collection de byte</returns>
        static public string FromMD5(byte[] arrayByte) { return FromAlgorithm(Algorithm.MD5, arrayByte); }
        /// <summary>
        /// Obtient l'empreinte MD5 d'un <see cref="Stream" />
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> a haché</param>
        /// <returns>Hash du <see cref="Stream" /></returns>
        static public string FromMD5(Stream stream) { return FromAlgorithm(Algorithm.MD5, stream); }

        /// <summary>
        /// Obtient l'empreinte SHA1 d'un <see cref="string"/>
        /// </summary>
        /// <param name="text">Texte a haché</param>
        /// <returns>Hash du texte</returns>
        static public string FromSHA1(string text) { return FromSHA1(UTF8(text)); }
        /// <summary>
        /// Obtient l'empreinte SHA1 d'une collection de byte
        /// </summary>
        /// <param name="arrayByte">Collection de byte a haché</param>
        /// <returns>Hash de la collection de byte</returns>
        static public string FromSHA1(byte[] arrayByte) { return FromAlgorithm(Algorithm.SHA1, arrayByte); }
        /// <summary>
        /// Obtient l'empreinte SHA1 d'un <see cref="Stream" />
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> a haché</param>
        /// <returns>Hash du <see cref="Stream" /></returns>
        static public string FromSHA1(Stream stream) { return FromAlgorithm(Algorithm.SHA1, stream); }

        /// <summary>
        /// Obtient l'empreinte SHA256 d'un <see cref="string"/>
        /// </summary>
        /// <param name="text">Texte a haché</param>
        /// <returns>Hash du texte</returns>
        static public string FromSHA256(string text) { return FromSHA256(UTF8(text)); }
        /// <summary>
        /// Obtient l'empreinte SHA256 d'une collection de byte
        /// </summary>
        /// <param name="arrayByte">Collection de byte a haché</param>
        /// <returns>Hash de la collection de byte</returns>
        static public string FromSHA256(byte[] arrayByte) { return FromAlgorithm(Algorithm.SHA256, arrayByte); }
        /// <summary>
        /// Obtient l'empreinte SHA256 d'un <see cref="Stream" />
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> a haché</param>
        /// <returns>Hash du <see cref="Stream" /></returns>
        static public string FromSHA256(Stream stream) { return FromAlgorithm(Algorithm.SHA256, stream); }

        /// <summary>
        /// Obtient l'empreinte SHA384 d'un <see cref="string"/>
        /// </summary>
        /// <param name="text">Texte a haché</param>
        /// <returns>Hash du texte</returns>
        static public string FromSHA384(string text) { return FromSHA384(UTF8(text)); }
        /// <summary>
        /// Obtient l'empreinte SHA384 d'une collection de byte
        /// </summary>
        /// <param name="arrayByte">Collection de byte a haché</param>
        /// <returns>Hash de la collection de byte</returns>
        static public string FromSHA384(byte[] arrayByte) { return FromAlgorithm(Algorithm.SHA384, arrayByte); }
        /// <summary>
        /// Obtient l'empreinte SHA384 d'un <see cref="Stream" />
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> a haché</param>
        /// <returns>Hash du <see cref="Stream" /></returns>
        static public string FromSHA384(Stream stream) { return FromAlgorithm(Algorithm.SHA384, stream); }

        /// <summary>
        /// Obtient l'empreinte SHA512 d'un <see cref="string"/>
        /// </summary>
        /// <param name="text">Texte a haché</param>
        /// <returns>Hash du texte</returns>
        static public string FromSHA512(string text) { return FromSHA512(UTF8(text)); }
        /// <summary>
        /// Obtient l'empreinte SHA512 d'une collection de byte
        /// </summary>
        /// <param name="arrayByte">Collection de byte a haché</param>
        /// <returns>Hash de la collection de byte</returns>
        static public string FromSHA512(byte[] arrayByte) { return FromAlgorithm(Algorithm.SHA512, arrayByte); }
        /// <summary>
        /// Obtient l'empreinte SHA512 d'un <see cref="Stream" />
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> a haché</param>
        /// <returns>Hash du <see cref="Stream" /></returns>
        static public string FromSHA512(Stream stream) { return FromAlgorithm(Algorithm.SHA512, stream); }

        /// <summary>
        /// Obtient l'empreinte RIPEMD160 d'un <see cref="string"/>
        /// </summary>
        /// <param name="text">Texte a haché</param>
        /// <returns>Hash du texte</returns>
        static public string FromRIPEMD160(string text) { return FromRIPEMD160(UTF8(text)); }
        /// <summary>
        /// Obtient l'empreinte RIPEMD160 d'une collection de byte
        /// </summary>
        /// <param name="arrayByte">Collection de byte a haché</param>
        /// <returns>Hash de la collection de byte</returns>
        static public string FromRIPEMD160(byte[] arrayByte) { return FromAlgorithm(Algorithm.RIPEMD160, arrayByte); }
        /// <summary>
        /// Obtient l'empreinte RIPEMD160 d'un <see cref="Stream" />
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> a haché</param>
        /// <returns>Hash du <see cref="Stream" /></returns>
        static public string FromRIPEMD160(Stream stream) { return FromAlgorithm(Algorithm.RIPEMD160, stream); }

        /// <summary>
        /// Obtient l'empreinte KeyedHashAlgorithm d'un <see cref="string"/>
        /// </summary>
        /// <param name="text">Texte a haché</param>
        /// <returns>Hash du texte</returns>
        static public string FromKeyedHashAlgorithm(string text) { return FromKeyedHashAlgorithm(UTF8(text)); }
        /// <summary>
        /// Obtient l'empreinte KeyedHashAlgorithm d'une collection de byte
        /// </summary>
        /// <param name="arrayByte">Collection de byte a haché</param>
        /// <returns>Hash de la collection de byte</returns>
        static public string FromKeyedHashAlgorithm(byte[] arrayByte) { return FromAlgorithm(Algorithm.KeyedHashAlgorithm, arrayByte); }
        /// <summary>
        /// Obtient l'empreinte KeyedHashAlgorithm d'un <see cref="Stream" />
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> a haché</param>
        /// <returns>Hash du <see cref="Stream" /></returns>
        static public string FromKeyedHashAlgorithm(Stream stream) { return FromAlgorithm(Algorithm.KeyedHashAlgorithm, stream); }

        
    }
}