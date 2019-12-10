using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Enigma
{
    /// <summary>
    /// Represent a rotor for a <see cref="Enigma"/>.
    /// </summary>
	sealed public partial class Rotor
    {
        /// <summary>
        /// Standard rotor I
        /// </summary>
        public static Rotor I
        {
            get { return new Rotor("I", "EKMFLGDQVZNTOWYHXUSPAIBRCJ", rotateAt: 'Q'); }
        }
        /// <summary>
        /// Standard rotor II
        /// </summary>
        public static Rotor II
        {
            get { return new Rotor("II", "AJDKSIRUXBLHWTMCQGZNPYFVOE", rotateAt: 'E'); }
        }
        /// <summary>
        /// Standard rotor III
        /// </summary>
        public static Rotor III
        {
            get { return new Rotor("III", "BDFHJLCPRTXVZNYEIWGAKMUSQO", rotateAt: 'V'); }
        }
        /// <summary>
        /// Standard rotor IV
        /// </summary>
        public static Rotor IV
        {
            get { return new Rotor("IV", "ESOVPZJAYQUIRHXLNFTGKDCMWB", rotateAt: 'J'); }
        }
        /// <summary>
        /// Standard rotor V
        /// </summary>
        public static Rotor V
        {
            get { return new Rotor("V", "VZBRGITYUPSDNHLXAWMJQOFECK", rotateAt: 'Z'); }
        }
        /// <summary>
        /// Standard rotor VI
        /// </summary>
        public static Rotor VI
        {
            get { return new Rotor("VI", "JPGVOUMFYQBENHZRDKASXLICTW", rotateAt: 'Z', rotateAtSecondary: 'M'); }
        }
        /// <summary>
        /// Standard rotor VII
        /// </summary>
        public static Rotor VII
        {
            get { return new Rotor("VII", "NZJHGRCXMYSWBOUFAIVLPEKQDT", rotateAt: 'Z', rotateAtSecondary: 'M'); }
        }
        /// <summary>
        /// Standard rotor VIII
        /// </summary>
        public static Rotor VIII
        {
            get { return new Rotor("VIII", "FKQHTLXOCBJSPDZRAMEWNIUYGV", rotateAt: 'Z', rotateAtSecondary: 'M'); }
        }

    }
}
