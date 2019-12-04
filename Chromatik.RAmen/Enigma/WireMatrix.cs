using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
	public class WireMatrix
	{
		public const int ASCII_UPPER_A = 65;
		public static readonly List<char> ALPHABET = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

		/// <summary>
		/// Initialize the wire matrix with a random set of wires.
		/// </summary>
		public WireMatrix()
		{
			this.Wires = RandomWires();
		}

		/// <summary>
		/// Initialize the wire matrix with a set of given wires.
		/// </summary>
		/// <param name="wires"></param>
		public WireMatrix(IEnumerable<char> wires)
		{
			if (wires.Count() != 26)
			{
				throw new EnigmaException("Invalid input wire matrix, count is not 26");
			}
			this.Wires = wires.ToList();
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
			var asIndex = Projectcharacter(input);
			return Wires[asIndex];
		}

		public void Rotate()
		{
			var elem = this.Wires[0];
			this.Wires.RemoveAt(0);
			this.Wires.Add(elem);
		}

		public WireMatrix Invert()
		{
			var indexMap = new Dictionary<Int32, char>();
			for (int index = 0; index < this.Wires.Count; index++)
			{
				indexMap.Add(WireMatrix.Projectcharacter(this.Wires[index]), WireMatrix.ProjectIndex(index));
			}

			return new WireMatrix(indexMap.OrderBy(_ => _.Key).Select(_ => _.Value));
		}

		/// <summary>
		/// Return a random set of wires.
		/// </summary>
		/// <returns></returns>
		public static List<char> RandomWires()
		{
			return ALPHABET.OrderBy(_ => System.Guid.NewGuid()).ToList();
		}

		/// <summary>
		/// Project a character onto the range 0,25 for use in access wire matrix indexes.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static int Projectcharacter(char input)
		{
			if (!char.IsLetter(input))
			{
				throw new EnigmaException("Invalid input; {0} is not a letter".Format(input));
			}

			char working = input;
			if (char.IsLower(working))
			{
				working = char.ToUpper(input);
			}

			return (int)working - ASCII_UPPER_A;
        }

		public static char ProjectIndex(Int32 index)
		{
			return (char)(index + ASCII_UPPER_A);
		}
	}
}
