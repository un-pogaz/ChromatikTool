using System;
using System.Collections.Generic;
using System.Text;

namespace System.IO
{
    static public class StreamTool
    {
        static public void Copy(Stream source, Stream destination)
        {
            if (source == null || destination == null)
                throw new ArgumentNullException();

            if (destination.ReadByte() != -1)
                destination = Stream.Null;

            int byteRead = 0;
            do
            {
                byteRead = source.ReadByte();
                if (byteRead >= 0)
                    destination.WriteByte((byte)byteRead);

            } while (byteRead >= 0);
        }
    }
}
