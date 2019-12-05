using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Cryptography.Enigma
{
	public class EnigmaException : System.Exception
	{
		public EnigmaException() : base() { }
		public EnigmaException(string message) : base(message) { }
	}

    sealed public class Enigma : Alphabet
    {
        public Enigma(Reflector reflector, params Rotor[] rotors) : this(reflector, new PlugBoard(), rotors)
        { }

        public Enigma(Reflector reflector, PlugBoard plugBoard, params Rotor[] rotors) : base(reflector.OperatingAlphabet)
		{
            if (reflector.IsNull())
                throw new ArgumentNullException(nameof(reflector));
            Reflector = reflector;

            if (rotors.IsNull())
                rotors = new Rotor[0];
            Rotors = rotors;

            foreach (Rotor item in rotors)
                item.RotateToPosition(item.InitialPosition);

            if (plugBoard == null)
                PlugBoard = new PlugBoard();
            else
                PlugBoard = plugBoard;
        }

		/// <summary>
		/// Reflector
		/// </summary>
		public Reflector Reflector { get; }

        /// <summary>
        /// Rotor 1
        /// </summary>
        public Rotor[] Rotors { get; }

		/// <summary>
		/// Plug Board
		/// </summary>
		public PlugBoard PlugBoard { get; }

		/// <summary>
		/// Process a key press.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public char Process(char input)
		{
            char working = input;
            working = PlugBoard.Process(working);

            bool should_rotate_next = true;
            for (int i = 0; i < Rotors.Length; i++)
            {
                if (should_rotate_next)
                    should_rotate_next = Rotors[i].Rotate();
                working = Rotors[i].ProcessLeft(working);
            }

			working = Reflector.Process(working);

            for (int i = Rotors.Length-1; i >= 0; i--)
                working = Rotors[i].ProcessRight(working);

			working = PlugBoard.Process(working);

			return working;
		}
        public char[] Process(char[] input)
        {
            for (int i = 0; i < input.Length; i++)
                if (OperatingAlphabet.Contains(input[i]))
                    input[i] = Process(input[i]);
            return input;
        }
        public string Process(string input)
        {
            return Process(input.ToCharArray()).ToOneString();
        }
        public IEnumerable<char> Process(IEnumerable<char> input)
        {
            return Process(input.ToArray());
        }


        public override string ToString()
		{
            return Reflector.ToString() + " / " + Rotors.ToOneString<Rotor>(" / ") + " / Plugs: {4}" + PlugBoard.ToString();
		}
	}
}
