using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Machine
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
            get { return new Rotor("I", "EKMFLGDQVZNTOWYHXUSPAIBRCJ", initialPosition: null, rotateAt: 'Q'); }
        }
        /// <summary>
        /// Standard rotor II
        /// </summary>
        public static Rotor II
        {
            get { return new Rotor("II", "AJDKSIRUXBLHWTMCQGZNPYFVOE", initialPosition: null, rotateAt: 'E'); }
        }
        /// <summary>
        /// Standard rotor III
        /// </summary>
        public static Rotor III
        {
            get { return new Rotor("III", "BDFHJLCPRTXVZNYEIWGAKMUSQO", initialPosition: null, rotateAt: 'V'); }
        }
        /// <summary>
        /// Standard rotor IV
        /// </summary>
        public static Rotor IV
        {
            get { return new Rotor("IV", "ESOVPZJAYQUIRHXLNFTGKDCMWB", initialPosition: null, rotateAt: 'J'); }
        }
        /// <summary>
        /// Standard rotor V
        /// </summary>
        public static Rotor V
        {
            get { return new Rotor("V", "VZBRGITYUPSDNHLXAWMJQOFECK", initialPosition: null, rotateAt: 'Z'); }
        }
        /// <summary>
        /// Standard rotor VI
        /// </summary>
        public static Rotor VI
        {
            get { return new Rotor("VI", "JPGVOUMFYQBENHZRDKASXLICTW", initialPosition: null, rotateAt: 'Z', rotateAtSecondary: 'M'); }
        }
        /// <summary>
        /// Standard rotor VII
        /// </summary>
        public static Rotor VII
        {
            get { return new Rotor("VII", "NZJHGRCXMYSWBOUFAIVLPEKQDT", initialPosition: null, rotateAt: 'Z', rotateAtSecondary: 'M'); }
        }
        /// <summary>
        /// Standard rotor VIII
        /// </summary>
        public static Rotor VIII
        {
            get { return new Rotor("VIII", "FKQHTLXOCBJSPDZRAMEWNIUYGV", initialPosition: null, rotateAt: 'Z', rotateAtSecondary: 'M'); }
        }

        static public class Byte
        {

        }
    }
}
