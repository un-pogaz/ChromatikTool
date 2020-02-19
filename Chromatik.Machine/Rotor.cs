using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Machine
{
    /// <summary>
    /// Base class for create a rotor
    /// </summary>
	public partial class Rotor : Alphabet, ICloneable
    {
        internal static ArgumentException ExceptionInvalidChar(char c, string name)
        {
            return new ArgumentException("The \'" + c + "\' character is not contained in the operating alphabet.", name);
        }
        internal static ArgumentOutOfRangeException ExceptionNegative(string name)
        {
            return new ArgumentOutOfRangeException(name, "The index of rotate contain a negative value.", name);
        }

        /// <summary>
        /// Initialize a rotor with a set of given wires.
        /// </summary>
        public Rotor(string id, IEnumerable<char> wires) : this(id, wires, null)
        { }
        /// <summary>
        /// Initialize a rotor with a set of given wires.
        /// </summary>
        public Rotor(string id, IEnumerable<char> wires, char? initialPosition) : this(id, wires, initialPosition, null)
		{ }
        /// <summary>
        /// Initialize a rotor with a set of given wires and the specifieds rotates.
        /// </summary>
		public Rotor(string id, IEnumerable<char> wires, char? initialPosition, IEnumerable<char> rotateAt) : base(wires)
        {
            if (id.IsNullOrWhiteSpace())
                throw new ArgumentNullException("The "+ nameof(id) + " can be null, Empty or WhiteSpace.", nameof(id));
			Id = id;
            
            WiresLeft = new WireMatrix(wires);
            WiresRight = WiresLeft.Invert();
            
            if (rotateAt == null)
                RotateAt = new char[0];
            else
            {
                rotateAt = rotateAt.Distinct();

                foreach (char item in rotateAt)
                    if (!AlphabetContains(item))
                        throw ExceptionInvalidChar(item, nameof(rotateAt));

                RotateAt = rotateAt.ToArray();
            }


            if (initialPosition == null)
                InitialPosition = OperatingAlphabet[0];
            else
                InitialPosition = initialPosition.GetValueOrDefault();
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
        public char InitialPosition
        {
            set
            {
                _initialPosition = value;
                RotateToPosition(value);
            }
            get { return _initialPosition; }
        }
        char _initialPosition;

        /// <summary>
        /// Offset position of the rotor
        /// </summary>
		public char OffsetPosition { get; protected set; }

        /// <summary>
        /// Character cause the rotation of the adjacent rotor
        /// </summary>
		virtual public char[] RotateAt
        {
            set
            {
                if (value == null)
                    value = new char[0];
                else
                {
                    value = value.Distinct();

                    foreach (char item in value)
                        if (!AlphabetContains(item))
                            throw ExceptionInvalidChar(item, nameof(value));

                    _rotateAt = value;
                }
            }
            get { return _rotateAt; }
        }
        char[] _rotateAt;
        
        /// <summary>
        /// Rotate the rotor to the specified position.
        /// </summary>
        /// <returns></returns>
        public void RotateToPosition(char position)
		{
			if(!AlphabetContains(position))
				throw new ArgumentException("Invalid rotor position: {0}".Format(position), nameof(position));

            int positionIndex = WiresLeft.ProjectCharacter(position);
			int offsetIndex = WiresLeft.ProjectCharacter(OffsetPosition);
            int delta = positionIndex - offsetIndex;
			if (delta < 0)
				delta = OperatingAlphabet.Count + delta;

			for(int currentIndex = 0; currentIndex < delta; currentIndex++)
			{
				WiresLeft.Rotate();
				WiresRight = WiresLeft.Invert();

				offsetIndex = offsetIndex + 1;
				if (offsetIndex >= OperatingAlphabet.Count)
					offsetIndex = 0;

				OffsetPosition = WiresLeft.ProjectIndex(offsetIndex);
			}
		}
        /// <summary>
        /// Rotate the rotor to the next position.
        /// </summary>
        /// <returns>true if the next should be rotate</returns>
        public bool Rotate()
		{
			bool shouldAdvance = false;
            foreach (char item in RotateAt)
                if (OffsetPosition.Equals(item))
                {
                    shouldAdvance = true;
                    break;
                }

			int offsetIndex = WiresLeft.ProjectCharacter(OffsetPosition);
			offsetIndex = offsetIndex + 1;
			if (offsetIndex >= OperatingAlphabet.Count)
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
        /// <summary>
        /// Reset the rotor to the initial position.
        /// </summary>
        public void Reset()
        {
            RotateToPosition(InitialPosition);
        }

        /// <summary></summary>
		public override string ToString()
		{
			return string.Format("Rotor Id: {0} Initial: {1} Offset: {2}", Id, InitialPosition, OffsetPosition);
		}

        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        public Rotor Clone()
        {
            return CloneRotor(false);
        }
        /// <summary>
        /// Creates a duplicate of this rotor and reset then.
        /// </summary>
        /// <returns></returns>
        public Rotor Clone(bool andReset)
        {
            return CloneRotor(andReset);
        }
        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return CloneRotor(false);
        }
        /// <summary>
        /// Creates a duplicate of this rotor and reset then.
        /// </summary>
        /// <returns></returns>
        public Rotor CloneRotor(bool andReset)
        {
            Rotor rslt = new Rotor(Id, source_alphabet, InitialPosition, RotateAt);
            if (!andReset)
                rslt.RotateToPosition(OffsetPosition);
            return rslt;
        }
    }
}