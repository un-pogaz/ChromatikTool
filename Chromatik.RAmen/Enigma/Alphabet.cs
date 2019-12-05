using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Cryptography.Enigma
{
	public class Alphabet
    {
        /// <summary>
        /// Initialize the wire matrix with a set of given wires.
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
	}
}
