using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    static public class StreamExtension
    {
        static public Stream CreateClone(this Stream stream)
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
