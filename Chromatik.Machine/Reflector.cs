using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Machine
{
    sealed public partial class Reflector : Alphabet, ICloneable
    {
        /// <summary>
        /// Initialize a reflector with a set of given wires.
        /// </summary>
        public Reflector(string id, IEnumerable<char> wires) : base(wires)
		{
			Id = id;
			Wires = new WireMatrix(wires);
		}
        /// <summary>
        /// Identifiant of the reflector.
        /// </summary>
		public string Id { get; }
        /// <summary>
        /// Wire matrix of this reflector
        /// </summary>
		public WireMatrix Wires { get; }

        /// <summary>
        /// Process the input signal to the output signal.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public char Process(char input)
		{
			return Wires.Process(input);
		}

        /// <summary></summary>
		public override string ToString()
		{
			return string.Format("Reflector {0}", Id);
        }

        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        public Reflector Clone()
        {
            return CloneReflector();
        }
        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return CloneReflector();
        }
        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        public Reflector CloneReflector()
        {
            return new Reflector(Id, source_alphabet);
        }
    }
}
