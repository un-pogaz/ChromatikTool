using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text
{
    /// <summary>
    /// Clone of <see cref="UTF8Encoding"/> with no BOM
    /// </summary>
    public class UTF8SansBomEncoding : UTF8Encoding
    {
        /// <summary>
        /// UTF-8 <see cref="Encoding"/> without BOM
        /// </summary>
        static public UTF8SansBomEncoding UTF8SansBom { get; } = new UTF8SansBomEncoding();

        /// <summary>
        /// UTF-8 <see cref="Encoding"/> without BOM
        /// </summary>
        new static public UTF8SansBomEncoding Default { get; } = UTF8SansBom;

        /// <summary>
        /// Create UTF-8 <see cref="Encoding"/> without BOM
        /// </summary>
        public UTF8SansBomEncoding() : this(true)
        { }
        /// <summary>
        /// Create UTF-8 <see cref="Encoding"/> without BOM
        /// </summary>
        /// <param name="throwOnInvalidBytes">true to raise an exception when an invalid encoding is detected; else, false.</param>
        public UTF8SansBomEncoding(bool throwOnInvalidBytes) : base(false, throwOnInvalidBytes)
        { }
    }
}
