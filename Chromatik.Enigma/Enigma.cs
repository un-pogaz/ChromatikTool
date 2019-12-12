using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Machine
{
    /// <summary>
    /// Represent famous WW2 cryptography machine.
    /// </summary>
    sealed public class Enigma : Alphabet, ICloneable
    {
        /// <summary>
        /// Initialize a Enigma machine.
        /// </summary>
        public Enigma(Reflector reflector, params Rotor[] rotors) : this(reflector, new PlugBoard(), rotors)
        { }
        /// <summary>
        /// Initialize a Enigma machine.
        /// </summary>
        public Enigma(Reflector reflector, PlugBoard plugBoard, params Rotor[] rotors) : base(reflector.OperatingAlphabet)
		{
            if (reflector.IsNull())
                throw new ArgumentNullException(nameof(reflector));
            Reflector = reflector;

            if (rotors.IsNull())
                rotors = new Rotor[0];
            Rotors = rotors;

            foreach (Rotor rotor in rotors)
            {
                if (!Reflector.Equals(rotor))
                    throw new ArgumentNullException("A rotor does not use the same Alphabet that the others.");

                rotor.RotateToPosition(rotor.InitialPosition);
            }

            if (plugBoard == null)
                PlugBoard = new PlugBoard();
            else
                PlugBoard = plugBoard;
        }

        /// <summary>
        /// Reflector instaled on this Enigma
        /// </summary>
        public Reflector Reflector { get; }

        /// <summary>
        /// Rotors instaled on this Enigma
        /// </summary>
        public Rotor[] Rotors { get; }

        /// <summary>
        /// Plug Board of this Enigma
        /// </summary>
        public PlugBoard PlugBoard { get; }

        /// <summary>
        /// Process a key press.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>return the processing <see cref="char"/>. If the <see cref="char"/> is not contained in operating alphabet, return the input.</returns>
        public char Process(char input)
		{
            if (AlphabetContains(input))
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

                for (int i = Rotors.Length - 1; i >= 0; i--)
                    working = Rotors[i].ProcessRight(working);

                working = PlugBoard.Process(working);

                return working;
            }
            else
                return input;
        }
        /// <summary>
        /// Process the input <see cref="char"/> table to the output <see cref="char"/> table.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>return the processing <see cref="char"/> table. The <see cref="char"/> not contained in operating alphabet as not modified.</returns>
        public char[] Process(char[] input)
        {
            for (int i = 0; i < input.Length; i++)
                if (OperatingAlphabet.Contains(input[i]))
                    input[i] = Process(input[i]);
            return input;
        }
        /// <summary>
        /// Process the string signal to the string signal.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>return the processing <see cref="char"/> table. The <see cref="char"/> not contained in operating alphabet as not modified.</returns>
        public string Process(string input)
        {
            return Process(input.ToCharArray()).ToOneString();
        }
        /// <summary>
        /// Process the input enumerable <see cref="char"/> to the output enumerable <see cref="char"/>.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>return the processing <see cref="char"/> table. The <see cref="char"/> not contained in operating alphabet as not modified.</returns>
        public IEnumerable<char> Process(IEnumerable<char> input)
        {
            return Process(input.ToArray());
        }
        /// <summary>
        /// Reset the enigma machine (all rotor to the initial position).
        /// </summary>
        public void Reset()
        {
            foreach (Rotor rotor in Rotors)
                rotor.Reset();
        }

        /// <summary></summary>
        public override string ToString()
		{
            return Reflector.ToString() + " / " + Rotors.ToOneString<Rotor>(" / ") + " / Plugs: " + PlugBoard.ToString();
        }

        /// <summary>
        /// Creates a duplicate of this Enigma.
        /// </summary>
        /// <returns></returns>
        public Enigma Clone()
        {
            return CloneEnigma(false);
        }
        /// <summary>
        /// Creates a duplicate of this Enigma and reset then.
        /// </summary>
        /// <returns></returns>
        public Enigma Clone(bool andReset)
        {
            return CloneEnigma(andReset);
        }
        /// <summary>
        /// Creates a duplicate of this Enigma.
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return CloneEnigma(false);
        }
        /// <summary>
        /// Creates a duplicate of this Enigma and reset then.
        /// </summary>
        /// <returns></returns>
        public Enigma CloneEnigma(bool andReset)
        {
            List<Rotor> lst = new List<Rotor>();
            foreach (Rotor rotor in Rotors)
                lst.Add(rotor.Clone(andReset));

            return new Enigma(Reflector.Clone(), PlugBoard.Clone(), lst.ToArray());
        }
    }
}
