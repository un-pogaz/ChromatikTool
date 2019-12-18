using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    /// <summary>
    /// Static clas to extend <see cref="Stream"/>
    /// </summary>
    static public class StreamExtension
    {
        /// <summary>
        /// Create a clone
        /// </summary>
        static public Stream Clone(this Stream stream)
        {
            long p = stream.Position;
            MemoryStream rslt = new MemoryStream();

            rslt.Position = 0;
            stream.Position = 0;
            stream.CopyTo(rslt);
            rslt.Position = 0;
            stream.Position = p;

            return rslt;
        }

        /// <summary>
        /// Create a stream from a string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public MemoryStream StreamFromString(string input)
        {
            return StreamFromString(input, UTF8SansBomEncoding.UTF8SansBom);
        }
        /// <summary>
        /// Create a stream from a string with the specified encoding.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        static public MemoryStream StreamFromString(string input, Encoding encoding)
        {
            MemoryStream rslt = new MemoryStream();
            using (StreamWriter writer = new StreamWriter(rslt, encoding, 1024, true))
                writer.Write(input);
            rslt.Position = 0;
            return rslt;
        }
    }
}
