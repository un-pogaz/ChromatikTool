using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Machine
{
    /// <summary>
    /// Represent "evolution" of the WW2 Enigma cryptography machine.
    /// </summary>
    sealed public class EnigmaII : Alphabet, ICloneable, IMachine
    {
        /// <summary>
        /// Initialize a EnigmaII machine.
        /// </summary>
        public EnigmaII(Reflector reflector, params ModuleEnigmaII[] modules) : this(reflector, new PlugBoard(), modules)
        { }
        /// <summary>
        /// Initialize a EnigmaII machine.
        /// </summary>
        public EnigmaII(Reflector reflector, PlugBoard frontPlugBoard, params ModuleEnigmaII[] modules) : base(reflector.OperatingAlphabet)
		{
            if (reflector == null)
                throw new ArgumentNullException(nameof(reflector));
            Reflector = reflector;

            if (modules == null)
                modules = new ModuleEnigmaII[0];
            Modules = modules;

            foreach (ModuleEnigmaII module in modules)
            {
                if (module.Rotor != null && !Reflector.Equals(module.Rotor))
                    throw new ArgumentNullException("A rotor does not use the same Alphabet that the others.");
            }

            if (frontPlugBoard == null)
                frontPlugBoard = new PlugBoard();
            else
                FrontPlugBoard = frontPlugBoard;
        }

        /// <summary>
        /// Reflector instaled on this EnigmaII
        /// </summary>
        public Reflector Reflector { get; }

        /// <summary>
        /// Rotors instaled on this EnigmaII
        /// </summary>
        public ModuleEnigmaII[] Modules { get; }

        /// <summary>
        /// Plug Board of this EnigmaII
        /// </summary>
        public PlugBoard FrontPlugBoard { get; }

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
                working = FrontPlugBoard.Process(working);

                bool should_rotate_next = true;
                for (int i = 0; i < Modules.Length; i++)
                {
                    if (should_rotate_next)
                        should_rotate_next = Modules[i].Rotate();
                    working = Modules[i].ProcessLeft(working);
                }

                working = Reflector.Process(working);

                for (int i = Modules.Length - 1; i >= 0; i--)
                    working = Modules[i].ProcessRight(working);

                working = FrontPlugBoard.Process(working);

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
            return Process(input.ToCharArray()).ToOneString("");
        }
        /// <summary>
        /// Process the input enumerable <see cref="char"/> to the output enumerable <see cref="char"/>.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>return the processing <see cref="char"/> table. The <see cref="char"/> not contained in operating alphabet as not modified.</returns>
        public IEnumerable<char> Process(IEnumerable<char> input)
        {
            char[] rslt = input.ToArray();

            for (int i = 0; i < rslt.Length; i++)
                if (OperatingAlphabet.Contains(rslt[i]))
                    rslt[i] = Process(rslt[i]);

            return rslt;
        }

        /// <summary>
        /// Reset the EnigmaII machine (all rotor to the initial position).
        /// </summary>
        public void Reset()
        {
            foreach (ModuleEnigmaII module in Modules)
                module.Rotor.Reset();
        }

        /// <summary></summary>
        public override string ToString()
		{
            string module = " / [" + Modules.ToOneString<ModuleEnigmaII>("] [") + "]";
            if (Modules.Length == 0)
                module = string.Empty;

            return Reflector.ToString() + module + " / FrontPlugs: " + FrontPlugBoard.ToString();
        }

        /// <summary>
        /// Creates a duplicate of this Enigma.
        /// </summary>
        /// <returns></returns>
        public EnigmaII Clone()
        {
            return CloneEnigmaII(false);
        }
        /// <summary>
        /// Creates a duplicate of this Enigma and reset then.
        /// </summary>
        /// <returns></returns>
        public EnigmaII Clone(bool andReset)
        {
            return CloneEnigmaII(andReset);
        }
        /// <summary>
        /// Creates a duplicate of this Enigma.
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return CloneEnigmaII(false);
        }
        /// <summary>
        /// Creates a duplicate of this Enigma and reset then.
        /// </summary>
        /// <returns></returns>
        public EnigmaII CloneEnigmaII(bool andReset)
        {
            List<ModuleEnigmaII> lst = new List<ModuleEnigmaII>();
            foreach (ModuleEnigmaII module in Modules)
                lst.Add(module.Clone(andReset));

            return new EnigmaII(Reflector.Clone(), FrontPlugBoard.Clone(), lst.ToArray());
        }
    }
}
