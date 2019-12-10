using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Enigma
{
    /// <summary>
    /// Class for operating within the a limit characters range.
    /// </summary>
    public class Alphabet
    {
        /// <summary>
        /// Initialize the alphabet.
        /// </summary>
        /// <param name="alphabet"></param>
        internal Alphabet(IEnumerable<char> alphabet)
        {
            List<char> rslt = alphabet.ToList();
            if (rslt.Count == 0)
                throw new ArgumentException("The alphabet can't be empty.", nameof(alphabet));
            rslt.Sort();
            OperatingAlphabet = rslt.ToArray();
        }

        /// <summary>
        /// The mapping of input wire to output wire.
        /// </summary>
        public char[] OperatingAlphabet { get; }

        /// <summary>
        /// Test if a <see cref="char"/> is contains in the operating alphabet.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AlphabetContains(char? value)
        {
            if (value == null)
                return false;
            return AlphabetContains(value.GetValueOrDefault());
        }
        /// <summary>
        /// Test if a <see cref="char"/> is contains in the operating alphabet.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AlphabetContains(char value)
        {
            return OperatingAlphabet.Contains(value);
        }
        /// <summary></summary>
        /// <param name="obj"></param>
        public override bool Equals(object obj)
        {
            if (obj is Alphabet)
                return Equals((Alphabet)obj);

            return base.Equals(obj);
        }
        /// <summary></summary>
        /// <param name="obj"></param>
        public bool Equals(Alphabet obj)
        {
            if (obj == null)
                return false;
            if (OperatingAlphabet.Length != obj.OperatingAlphabet.Length)
                return false;

            for (int i = 0; i < OperatingAlphabet.Length; i++)
            {
                if (OperatingAlphabet[i] != obj.OperatingAlphabet[i])
                    return false;
            }
            return true;
        }
        /// <summary></summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
