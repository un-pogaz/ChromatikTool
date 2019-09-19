using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text
{
    public class UTF8SansBomEncoding : UTF8Encoding
    {
        /// <summary>
        /// <see cref="Encoding"/> pour de l'UTF8 sans BOM
        /// </summary>
        new static public UTF8SansBomEncoding Default { get; } = new UTF8SansBomEncoding();

        /// <summary>
        /// <see cref="Encoding"/> pour de l'UTF8 sans BOM
        /// </summary>
        public UTF8SansBomEncoding() : this(true)
        { }
        /// <summary>
        /// <see cref="Encoding"/> pour de l'UTF8 sans BOM
        /// </summary>
        /// <param name="throwOnInvalidBytes">true pour lever une exception quand un encodage non valide est détecté ; sinon, false.</param>
        public UTF8SansBomEncoding(bool throwOnInvalidBytes) : base(false, throwOnInvalidBytes)
        {
        }
    }
}
