using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Cryptography.Enigma
{
    sealed public partial class Reflector
    {
        public static Reflector A
        {
            get { return new Reflector("A", "EJMZALYXVBWFCRQUONTSPIKHGD"); }
        }

        public static Reflector B
        {
            get { return new Reflector("B", "YRUHQSLDPXNGOKMIEBFZCWVJAT"); }
        }

        public static Reflector C
        {
            get { return new Reflector("C", "FVPJIAOYEDRZXWGCTKUQSBNMHL"); }
        }
    }
}
