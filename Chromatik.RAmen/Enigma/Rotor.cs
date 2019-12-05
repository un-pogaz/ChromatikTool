using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Cryptography.Enigma
{
	sealed public partial class Rotor : Alphabet
    {
		public Rotor(string id, IEnumerable<char> wires) : this(id, wires, 'A')
		{ }

        public Rotor(string id, IEnumerable<char> wires, char rotateAt) : this(id, wires, rotateAt, null)
        { }

		public Rotor(string id, IEnumerable<char> wires, char rotateAt, char? rotateAtSecondary) : base(wires)
        {
			Id = id;
            WiresLeft = new WireMatrix(wires);
            WiresRight = WiresLeft.Invert();
            RotateAt = rotateAt;
			RotateAtSecondary = rotateAtSecondary;
			InitialPosition = 'A';
			OffsetPosition = 'A';
		}
        
		public string Id { get; }

		public WireMatrix WiresLeft { get; private set; }
        public WireMatrix WiresRight { get; private set; }

        public char InitialPosition { get; set; }

		public char OffsetPosition { get; set; }

		public char RotateAt { get; }
		public char? RotateAtSecondary { get; }


		public void RotateToPosition(char position)
		{
			if(!char.IsLetter(position))
			{
				throw new EnigmaException("Invalid rotor position: {0}".Format(position));
			}

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

		public char ProcessLeft(char input)
		{
			var output = this.WiresLeft.Process(input);
			return output;
		}

		public char ProcessRight(char input)
		{
			var output = this.WiresRight.Process(input);			
			return output;
		}

		public override string ToString()
		{
			return string.Format("Rotor Id: {0} Initial: {1} Offset: {2}", Id, InitialPosition, OffsetPosition);
		}
	}
}
