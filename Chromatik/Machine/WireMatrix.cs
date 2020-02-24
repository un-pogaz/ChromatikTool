using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Machine
{
    /// <summary>
    /// Represent a wire matrix with a set of given wires.
    /// </summary>
    sealed public class WireMatrix : Alphabet
    {
		/// <summary>
		/// Initialize the wire matrix with a set of given wires.
		/// </summary>
		/// <param name="wires"></param>
		public WireMatrix(IEnumerable<char> wires) : base(wires)
		{
            _wires = wires.ToList();
        }

        private List<char> _wires;
        /// <summary>
        /// The mapping of input wire to output wire.
        /// </summary>
        public IReadOnlyList<char> Wires { get { return _wires; } }

        /// <summary>
        /// Process the input signal to the output signal.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public char Process(char input)
		{
			return _wires[ProjectCharacter(input)];
		}

        /// <summary>
        /// Rotate the wire matrix.
        /// </summary>
		public void Rotate()
		{
			var elem = _wires[0];
            _wires.RemoveAt(0);
			_wires.Add(elem);
		}

        /// <summary>
        /// Create an inverse of this <see cref="WireMatrix"/>.
        /// </summary>
        /// <returns></returns>
		public WireMatrix Invert()
		{
			var indexMap = new Dictionary<int, char>();
			for (int index = 0; index < _wires.Count; index++)
				indexMap.Add(ProjectCharacter(_wires[index]), ProjectIndex(index));

			return new WireMatrix(indexMap.OrderBy(_ => _.Key).Select(_ => _.Value));
		}

        /// <summary>
        /// Project a character onto the range use in access wire matrix indexes.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int ProjectCharacter(char input)
		{
            return OperatingAlphabet.ToList().IndexOf(input); ;
        }

        /// <summary>
		/// Project a indexes onto the range use in access wire matrix character.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public char ProjectIndex(int index)
		{
			return OperatingAlphabet[index];
		}
	}
}
