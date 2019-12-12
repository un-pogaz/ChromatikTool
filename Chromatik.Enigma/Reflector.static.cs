using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Machine
{
    /// <summary>
    /// Represent a reflector for a <see cref="Enigma"/>.
    /// </summary>
    sealed public partial class Reflector
    {
        /// <summary>
        /// Standard reflector A
        /// </summary>
        public static Reflector A
        {
            get { return new Reflector("A", "EJMZALYXVBWFCRQUONTSPIKHGD"); }
        }
        /// <summary>
        /// Standard reflector B
        /// </summary>
        public static Reflector B
        {
            get { return new Reflector("B", "YRUHQSLDPXNGOKMIEBFZCWVJAT"); }
        }
        /// <summary>
        /// Standard reflector C
        /// </summary>
        public static Reflector C
        {
            get { return new Reflector("C", "FVPJIAOYEDRZXWGCTKUQSBNMHL"); }
        }

        static public class Byte
        {
            /// <summary>
            /// Byte reflector C
            /// </summary>
            public static Reflector C
            {
                get { return new Reflector("C", "FVPJIAOYEDRZXWGCTKUQSBNMHL"); }
            }
        }
    }
}
