using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Cryptography.Enigma
{
	sealed public class WireMatrix : Alphabet
    {
		/// <summary>
		/// Initialize the wire matrix with a set of given wires.
		/// </summary>
		/// <param name="wires"></param>
		public WireMatrix(IEnumerable<char> wires) : base(wires)
		{
			Wires = wires.ToList();
		}

		/// <summary>
		/// The mapping of input wire to output wire.
		/// </summary>
		public List<char> Wires { get; private set; }
		
		/// <summary>
		/// Process the input signal to the output signal.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public char Process(char input)
		{
			var asIndex = ProjectCharacter(input);
			return Wires[asIndex];
		}

		public void Rotate()
		{
			var elem = Wires[0];
			Wires.RemoveAt(0);
			Wires.Add(elem);
		}

		public WireMatrix Invert()
		{
			var indexMap = new Dictionary<int, char>();
			for (int index = 0; index < Wires.Count; index++)
				indexMap.Add(ProjectCharacter(Wires[index]), ProjectIndex(index));

			return new WireMatrix(indexMap.OrderBy(_ => _.Key).Select(_ => _.Value));
		}

		/// <summary>
		/// Project a character onto the range use in access wire matrix indexes.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public int ProjectCharacter(char input)
		{
			return (int)input - OperatingAlphabet[0];
        }

        /// <summary>
		/// Project a indexes onto the range use in access wire matrix character.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char ProjectIndex(int index)
		{
			return (char)(index + OperatingAlphabet[0]);
		}
	}
}
