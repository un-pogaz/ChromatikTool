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
    }
}
