using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Cryptography.Enigma
{
    sealed public partial class Reflector : Alphabet
    {
		public Reflector(string id, IEnumerable<char> wires) : base(wires)
		{
			Id = id;
			Wires = new WireMatrix(wires);
		}

		public string Id { get; }
		public WireMatrix Wires { get; }

		public char Process(char input)
		{
			var output = Wires.Process(input);
			return output;
		}

		public override string ToString()
		{
			return string.Format("Reflector {0}", Id);
		}
	}
}
