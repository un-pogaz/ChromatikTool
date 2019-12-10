using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Enigma
{
	sealed public partial class Rotor : Alphabet
    {
        /// <summary>
        /// Initialize a rotor with a set of given wires.
        /// </summary>
        public Rotor(string id, IEnumerable<char> wires) : this(id, wires, null)
		{ }
        /// <summary>
        /// Initialize a rotor with a set of given wires and the specified rotate.
        /// </summary>
        public Rotor(string id, IEnumerable<char> wires, char? rotateAt) : this(id, wires, rotateAt, null)
        { }
        /// <summary>
        /// Initialize a rotor with a set of given wires and the specifieds rotates.
        /// </summary>
		public Rotor(string id, IEnumerable<char> wires, char? rotateAt, char? rotateAtSecondary) : base(wires)
        {
			Id = id;
            
            WiresLeft = new WireMatrix(wires);
            WiresRight = WiresLeft.Invert();
            if (rotateAt == null)
                RotateAt = OperatingAlphabet[0];
            else
                RotateAt = rotateAt;

            RotateAtSecondary = rotateAtSecondary;

            if (!AlphabetContains(RotateAt))
                throw new ArgumentException("The \""+ nameof(RotateAt) + "\" character is not contained in the operating alphabet.", nameof(RotateAt));

            if (RotateAtSecondary != null && !AlphabetContains(RotateAtSecondary))
                throw new ArgumentException("The \"" + nameof(RotateAtSecondary) + "\" character is not contained in the operating alphabet.", nameof(RotateAtSecondary));

            InitialPosition = OperatingAlphabet[0];
            OffsetPosition = OperatingAlphabet[0];
        }
        /// <summary>
        /// Identifiant of the rotor.
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Left wire matrix of this reflector
        /// </summary>
        public WireMatrix WiresLeft { get; private set; }
        /// <summary>
        /// Right wire matrix of this reflector
        /// </summary>
        public WireMatrix WiresRight { get; private set; }

        /// <summary>
        /// Initial position of the rotor
        /// </summary>
        public char InitialPosition { get; set; }
        /// <summary>
        /// Offset position of the rotor
        /// </summary>
		public char OffsetPosition { get; set; }

        /// <summary></summary>
		public char? RotateAt { get; }
        /// <summary></summary>
		public char? RotateAtSecondary { get; }

        /// <summary>
        /// Rotate the rotor to the specified position.
        /// </summary>
        /// <returns></returns>
        public void RotateToPosition(char position)
		{
			if(!char.IsLetter(position))
				throw new ArgumentException("Invalid rotor position: {0}".Format(position), nameof(position));

            int positionIndex = WiresLeft.ProjectCharacter(position);
			int offsetIndex = WiresLeft.ProjectCharacter(OffsetPosition);
            int delta = positionIndex - offsetIndex;
			if (delta < 0)
				delta = OperatingAlphabet.Length + delta;

			for(int currentIndex = 0; currentIndex < delta; currentIndex++)
			{
				WiresLeft.Rotate();
				WiresRight = WiresLeft.Invert();

				offsetIndex = offsetIndex + 1;
				if (offsetIndex >= OperatingAlphabet.Length)
					offsetIndex = 0;

				OffsetPosition = WiresLeft.ProjectIndex(offsetIndex);
			}
		}

        /// <summary>
        /// Rotate the rotor to the next position.
        /// </summary>
        /// <returns></returns>
        public bool Rotate()
		{
			bool shouldAdvance = OffsetPosition.Equals(RotateAt);
			if (RotateAtSecondary != null)
				shouldAdvance = shouldAdvance || OffsetPosition.Equals(RotateAtSecondary.Value);

			int offsetIndex = WiresLeft.ProjectCharacter(OffsetPosition);
			offsetIndex = offsetIndex + 1;
			if (offsetIndex >= OperatingAlphabet.Length)
				offsetIndex = 0;
			OffsetPosition = WiresLeft.ProjectIndex(offsetIndex);

			WiresLeft.Rotate();
			WiresRight = WiresLeft.Invert();

			return shouldAdvance;
        }

        /// <summary>
        /// Process the input signal to left output signal.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public char ProcessLeft(char input)
		{
			return WiresLeft.Process(input);
        }
        /// <summary>
        /// Process the input signal to right output signal.
        /// </summary>  
        /// <param name="input"></param>
        /// <returns></returns>
		public char ProcessRight(char input)
		{	
			return WiresRight.Process(input);
		}

        /// <summary></summary>
		public override string ToString()
		{
			return string.Format("Rotor Id: {0} Initial: {1} Offset: {2}", Id, InitialPosition, OffsetPosition);
		}
	}
}
