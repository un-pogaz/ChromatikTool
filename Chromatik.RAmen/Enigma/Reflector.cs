using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Cryptography.Enigma
{
	public class Reflector
	{
		public Reflector(string id)
		{
			this.Id = id;
			this.Wires = new WireMatrix();
		}

		public Reflector(string id, IEnumerable<char> wires)
		{
			this.Id = id;
			this.Wires = new WireMatrix(wires);
		}

		public string Id { get; private set; }
		public WireMatrix Wires { get; private set; }

		public char Process(char input)
		{
			var output = this.Wires.Process(input);
			return output;
		}

		public override string ToString()
		{
			return string.Format("Reflector {0}", this.Id);
		}
	}
}
