using System;
using System.Collections.Generic;
using System.Text;

namespace System.IO
{
    /// <summary>
    /// Static class for extend <see cref="FileInfo"/>
    /// </summary>
    static public class FileInfoExtension
    {
        /// <summary>
        /// Get the size of the file in Ko
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static public decimal LengthKo(this FileInfo file)
        {
            return (decimal)file.Length / 1024;
        }
        /// <summary>
        /// Get the size of the file in Mo
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        static public decimal LengthMo(this FileInfo file)
        {
            return file.LengthKo() / 1024;
        }
    }
}
