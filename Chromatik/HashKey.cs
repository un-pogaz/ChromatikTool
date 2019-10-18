using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace System.Security.Cryptography
{
    /// <summary>
    /// Static class to get quick a hash 
    /// </summary>
    static public partial class HashKey
    {
        /// <summary>
        /// Supported algorithm for the hash
        /// </summary>
        public enum Algorithm
        {
            /// <summary>
            /// https://en.wikipedia.org/wiki/MD5
            /// </summary>
            MD5,
            /// <summary>
            /// https://en.wikipedia.org/wiki/SHA-1
            /// </summary>
            SHA1,
            /// <summary>
            /// https://en.wikipedia.org/wiki/SHA-2
            /// </summary>
            SHA256,
            /// <summary>
            /// https://en.wikipedia.org/wiki/SHA-2
            /// </summary>
            SHA384,
            /// <summary>
            /// https://en.wikipedia.org/wiki/SHA-2
            /// </summary>
            SHA512,
            /// <summary>
            /// https://en.wikipedia.org/wiki/HMAC
            /// </summary>
            KeyedHashAlgorithm,
            /// <summary>
            /// https://en.wikipedia.org/wiki/RIPEMD
            /// </summary>
            RIPEMD160,
        }

        static private string HexToString(byte[] arrayByte)
        {
            string rslt = string.Empty;
            for (int i = 0; i < arrayByte.Length; i++)
                rslt += arrayByte[i].ToString("x2");
            return rslt;
        }

        static private byte[] UTF8(string text) { return UTF8SansBomEncoding.Default.GetBytes(text); }

        /// <summary>
        /// Get the <see cref="HashAlgorithm"/> associated to the <see cref="Algorithm"/>
        /// </summary>
        /// <param name="algorithm">Algorithm request</param>
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
        /// Get the hash of a string with the <see cref="Algorithm"/>
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        /// <returns></returns>
        static public string FileHash(Algorithm algorithm, string text)
        {
            return FromAlgorithm(algorithm, UTF8(text));
        }
        /// <summary>
        /// Get the hash of a <see cref="byte"/> array with the <see cref="Algorithm"/>
        /// </summary>
        /// <param name="arrayByte"><see cref="byte"/> array to hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        /// <returns></returns>
        static public string FromAlgorithm(Algorithm algorithm, byte[] arrayByte)
        {
            using (HashAlgorithm algo = GetAlgorithm(algorithm))
                return HexToString(algo.ComputeHash(arrayByte));
        }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with the <see cref="Algorithm"/>
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> to hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        /// <returns></returns>
        static public string FromAlgorithm(Algorithm algorithm, Stream stream)
        {
            using (HashAlgorithm algo = GetAlgorithm(algorithm))
                return HexToString(algo.ComputeHash(stream));
        }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with the <see cref="Algorithm"/>
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        /// <returns></returns>
        static public string FileHashAlgorithm(Algorithm algorithm, string filePath)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return FromAlgorithm(algorithm, file);
        }

        /// <summary>
        /// Get the hash of a string with MD5 algorithm
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <returns></returns>
        static public string FromMD5(string text) { return FromMD5(UTF8(text)); }
        /// <summary>
        /// Get the hash of a <see cref="byte"/> arr with MD5 algorithm
        /// </summary>
        /// <param name="arrayByte"><see cref="byte"/> array to hash</param>
        /// <returns></returns>
        static public string FromMD5(byte[] arrayByte) { return FromAlgorithm(Algorithm.MD5, arrayByte); }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with MD5 algorithm
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to hash</param>
        /// <returns></returns>
        static public string FromMD5(Stream stream) { return FromAlgorithm(Algorithm.MD5, stream); }
        /// <summary>
        /// Get the hash of a file with MD5 algorithm
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <returns></returns>
        static public string FileMD5(string filePath) { return FileHash(Algorithm.MD5, filePath); }

        /// <summary>
        /// Get the hash of a string with SHA1 algorithm
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <returns></returns>
        static public string FromSHA1(string text) { return FromSHA1(UTF8(text)); }
        /// <summary>
        /// Get the hash of a <see cref="byte"/> array with SHA1 algorithm
        /// </summary>
        /// <param name="arrayByte"><see cref="byte"/> array to hash</param>
        /// <returns></returns>
        static public string FromSHA1(byte[] arrayByte) { return FromAlgorithm(Algorithm.SHA1, arrayByte); }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with SHA1 algorithm
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to hash</param>
        /// <returns></returns>
        static public string FromSHA1(Stream stream) { return FromAlgorithm(Algorithm.SHA1, stream); }
        /// <summary>
        /// Get the hash of a file with SHA1 algorithm
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <returns></returns>
        static public string FileSHA1(string filePath) { return FileHash(Algorithm.SHA1, filePath); }

        /// <summary>
        /// Get the hash of a string with SHA256 algorithm
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <returns></returns>
        static public string FromSHA256(string text) { return FromSHA256(UTF8(text)); }
        /// <summary>
        /// Get the hash of a <see cref="byte"/> array with SHA256 algorithm
        /// </summary>
        /// <param name="arrayByte"><see cref="byte"/> array to hash</param>
        /// <returns></returns>
        static public string FromSHA256(byte[] arrayByte) { return FromAlgorithm(Algorithm.SHA256, arrayByte); }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with SHA256 algorithm
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to hash</param>
        /// <returns></returns>
        static public string FromSHA256(Stream stream) { return FromAlgorithm(Algorithm.SHA256, stream); }
        /// <summary>
        /// Get the hash of a file with SHA256 algorithm
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <returns></returns>
        static public string FileSHA256(string filePath) { return FileHash(Algorithm.SHA256, filePath); }

        /// <summary>
        /// Get the hash of a string with SHA384 algorithm
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <returns></returns>
        static public string FromSHA384(string text) { return FromSHA384(UTF8(text)); }
        /// <summary>
        /// Get the hash of a <see cref="byte"/> array with SHA384 algorithm
        /// </summary>
        /// <param name="arrayByte"><see cref="byte"/> array to hash</param>
        /// <returns></returns>
        static public string FromSHA384(byte[] arrayByte) { return FromAlgorithm(Algorithm.SHA384, arrayByte); }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with SHA384 algorithm
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to hash</param>
        /// <returns></returns>
        static public string FromSHA384(Stream stream) { return FromAlgorithm(Algorithm.SHA384, stream); }
        /// <summary>
        /// Get the hash of a file with SHA384 algorithm
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <returns></returns>
        static public string FileSHA384(string filePath) { return FileHash(Algorithm.SHA384, filePath); }

        /// <summary>
        /// Get the hash of a string with SHA512 algorithm
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <returns></returns>
        static public string FromSHA512(string text) { return FromSHA512(UTF8(text)); }
        /// <summary>
        /// Get the hash of a <see cref="byte"/> array with SHA512 algorithm
        /// </summary>
        /// <param name="arrayByte"><see cref="byte"/> array to hash</param>
        /// <returns></returns>
        static public string FromSHA512(byte[] arrayByte) { return FromAlgorithm(Algorithm.SHA512, arrayByte); }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with SHA512 algorithm
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to hash</param>
        /// <returns></returns>
        static public string FromSHA512(Stream stream) { return FromAlgorithm(Algorithm.SHA512, stream); }
        /// <summary>
        /// Get the hash of a file with SHA512 algorithm
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <returns></returns>
        static public string FileSHA512(string filePath) { return FileHash(Algorithm.SHA512, filePath); }

        /// <summary>
        /// Get the hash of a string with RIPEMD160 algorithm
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <returns></returns>
        static public string FromRIPEMD160(string text) { return FromRIPEMD160(UTF8(text)); }
        /// <summary>
        /// Get the hash of a string array with RIPEMD160 algorithm
        /// </summary>
        /// <param name="arrayByte">string array to hash</param>
        /// <returns></returns>
        static public string FromRIPEMD160(byte[] arrayByte) { return FromAlgorithm(Algorithm.RIPEMD160, arrayByte); }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with RIPEMD160 algorithm
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to hash</param>
        /// <returns></returns>
        static public string FromRIPEMD160(Stream stream) { return FromAlgorithm(Algorithm.RIPEMD160, stream); }
        /// <summary>
        /// Get the hash of a file with RIPEMD160 algorithm
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <returns></returns>
        static public string FileRIPEMD160(string filePath) { return FileHash(Algorithm.RIPEMD160, filePath); }

        /// <summary>
        /// Get the hash of a string with KeyedHashAlgorithm algorithm
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <returns></returns>
        static public string FromKeyedHashAlgorithm(string text) { return FromKeyedHashAlgorithm(UTF8(text)); }
        /// <summary>
        /// Get the hash of a <see cref="byte"/> array with KeyedHashAlgorithm algorithm
        /// </summary>
        /// <param name="arrayByte"><see cref="byte"/> array to hash</param>
        /// <returns></returns>
        static public string FromKeyedHashAlgorithm(byte[] arrayByte) { return FromAlgorithm(Algorithm.KeyedHashAlgorithm, arrayByte); }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with KeyedHashAlgorithm algorithm
        /// </summary>
        /// <param name="stream"><see cref="Stream"/> to hash</param>
        /// <returns></returns>
        static public string FromKeyedHashAlgorithm(Stream stream) { return FromAlgorithm(Algorithm.KeyedHashAlgorithm, stream); }
        /// <summary>
        /// Get the hash of a file with KeyedHashAlgorithm algorithm
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <returns></returns>
        static public string FileKeyedHashAlgorithm(string filePath) { return FileHash(Algorithm.KeyedHashAlgorithm, filePath); }
    }
}