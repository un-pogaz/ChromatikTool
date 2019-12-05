using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Cryptography.Enigma
{
	sealed public partial class Rotor
    {
        public static Rotor I
        {
            get { return new Rotor("I", "EKMFLGDQVZNTOWYHXUSPAIBRCJ", rotateAt: 'Q'); }
        }

        public static Rotor II
        {
            get { return new Rotor("II", "AJDKSIRUXBLHWTMCQGZNPYFVOE", rotateAt: 'E'); }
        }

        public static Rotor III
        {
            get { return new Rotor("III", "BDFHJLCPRTXVZNYEIWGAKMUSQO", rotateAt: 'V'); }
        }

        public static Rotor IV
        {
            get { return new Rotor("IV", "ESOVPZJAYQUIRHXLNFTGKDCMWB", rotateAt: 'J'); }
        }

        public static Rotor V
        {
            get { return new Rotor("V", "VZBRGITYUPSDNHLXAWMJQOFECK", rotateAt: 'Z'); }
        }

        public static Rotor VI
        {
            get { return new Rotor("VI", "JPGVOUMFYQBENHZRDKASXLICTW", rotateAt: 'Z', rotateAtSecondary: 'M'); }
        }

        public static Rotor VII
        {
            get { return new Rotor("VII", "NZJHGRCXMYSWBOUFAIVLPEKQDT", rotateAt: 'Z', rotateAtSecondary: 'M'); }
        }

        public static Rotor VIII
        {
            get { return new Rotor("VIII", "FKQHTLXOCBJSPDZRAMEWNIUYGV", rotateAt: 'Z', rotateAtSecondary: 'M'); }
        }

    }
}
