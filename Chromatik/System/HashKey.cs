using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace System.IO
{

    /// <summary>
    /// Implemented <see cref="HashAlgorithm"/> in <see cref="HashKey"/>.
    /// </summary>
    public enum HashAlgorithmEnum
    {
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
        /// <summary>
        /// https://en.wikipedia.org/wiki/MD5
        /// </summary>
        [Obsolete]
        MD5,
        /// <summary>
        /// https://en.wikipedia.org/wiki/SHA-1
        /// </summary>
        [Obsolete]
        SHA1,
    }

    /// <summary>
    /// Static class to obtain easily and quickly a hash.
    /// </summary>
    static public partial class HashKey
    {
        /// <summary>
        /// Get the <see cref="HashAlgorithm"/> associated to the <see cref="HashAlgorithmEnum"/>
        /// </summary>
        /// <param name="algorithm">Algorithm request</param>
        /// <returns></returns>
        static public HashAlgorithm GetAlgorithm(HashAlgorithmEnum algorithm)
        {
            switch (algorithm)
            {
                case HashAlgorithmEnum.MD5:
                    return MD5.Create();

                case HashAlgorithmEnum.SHA1:
                    return SHA1.Create();

                case HashAlgorithmEnum.SHA256:
                    return SHA256.Create();

                case HashAlgorithmEnum.SHA384:
                    return SHA384.Create();

                case HashAlgorithmEnum.SHA512:
                    return SHA512.Create();

                case HashAlgorithmEnum.RIPEMD160:
                    return RIPEMD160.Create();

                case HashAlgorithmEnum.KeyedHashAlgorithm:
                    return KeyedHashAlgorithm.Create();

                default:
                    return SHA256.Create();
            }
        }

        static private string HexToString(this byte[] arrayByte)
        {
            string rslt = string.Empty;
            for (int i = 0; i < arrayByte.Length; i++)
                rslt += arrayByte[i].ToString("x2");
            return rslt;
        }

        /// <summary>
        /// Get the hash of a <see cref="string"/> with the <see cref="HashAlgorithmEnum"/>
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        static public string Digest(HashAlgorithmEnum algorithm, string text)
        {
            return Digest(algorithm, text, 1);
        }

        /// <summary>
        /// Get the hash of a <see cref="string"/> with the <see cref="HashAlgorithmEnum"/>
        /// </summary>
        /// <param name="text">string to hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        /// <param name="iteration">Number of iteration of the hash</param>
        /// <returns></returns>
        static public string Digest(HashAlgorithmEnum algorithm, string text, int iteration)
        {
            return Digest(algorithm, UTF8SansBomEncoding.Default.GetBytes(text), iteration);
        }
        /// <summary>
        /// Get the hash of a <see cref="byte"/> array with the <see cref="HashAlgorithmEnum"/>
        /// </summary>
        /// <param name="arrayByte"><see cref="byte"/> array to hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        static public string Digest(HashAlgorithmEnum algorithm, byte[] arrayByte)
        {
            return Digest(algorithm, arrayByte, 1);
        }
        /// <summary>
        /// Get the hash of a <see cref="byte"/> array with the <see cref="HashAlgorithmEnum"/>
        /// </summary>
        /// <param name="arrayByte"><see cref="byte"/> array to hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        /// <param name="iteration">Number of iteration of the hash</param>
        /// <returns></returns>
        static public string Digest(HashAlgorithmEnum algorithm, byte[] arrayByte, int iteration)
        {
            if (iteration < 1)
                iteration = 1;
            using (HashAlgorithm algo = GetAlgorithm(algorithm))
            {
                for (int i = 0; i < iteration; i++)
                    arrayByte = algo.ComputeHash(arrayByte);
                return arrayByte.HexToString();
            }
        }

        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with the <see cref="HashAlgorithmEnum"/>
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> to hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        static public string Digest(HashAlgorithmEnum algorithm, Stream stream)
        {
            return Digest(algorithm, stream, 1);
        }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with the <see cref="HashAlgorithmEnum"/>
        /// </summary>
        /// <param name="stream"><see cref="Stream" /> to hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        /// <param name="iteration">Number of iteration of the hash</param>
        /// <returns></returns>
        static public string Digest(HashAlgorithmEnum algorithm, Stream stream, int iteration)
        {
            if (iteration < 1)
                iteration = 1;
            using (HashAlgorithm algo = GetAlgorithm(algorithm))
            {
                byte[] arrayByte = algo.ComputeHash(stream);
                for (int i = 0; i < iteration-1; i++)
                    arrayByte = algo.ComputeHash(arrayByte);
                return arrayByte.HexToString();
            }
        }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with the <see cref="HashAlgorithmEnum"/>
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        /// <returns></returns>
        static public string DigestFile(HashAlgorithmEnum algorithm, string filePath)
        {
            return DigestFile(algorithm, filePath, 1);
        }
        /// <summary>
        /// Get the hash of a <see cref="Stream" /> with the <see cref="HashAlgorithmEnum"/>
        /// </summary>
        /// <param name="filePath">Path of the file to get hash</param>
        /// <param name="algorithm">Hash algorithme</param>
        /// <param name="iteration">Number of iteration of the hash</param>
        /// <returns></returns>
        static public string DigestFile(HashAlgorithmEnum algorithm, string filePath, int iteration)
        {
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                return Digest(algorithm, file, iteration);
        }
    }
}