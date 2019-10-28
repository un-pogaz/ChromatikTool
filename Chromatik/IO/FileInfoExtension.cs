using System;
using System.Collections.Generic;
using System.Text;

namespace System.IO
{
    static public class FileInfoExtension
    {
        static public decimal LengthKo(this FileInfo file)
        {

            return (decimal)file.Length / 1024;
        }
        static public decimal LengthMo(this FileInfo file)
        {
            return file.LengthKo() / 1024;
        }
    }
}
