using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Machine
{
    /// <summary>
    /// Represent MarkMark cryptography machine.
    /// </summary>
    sealed public class Kraken : ICloneable, IMachine
    {
        /// <summary>
        /// Initialize a Kraken machine.
        /// </summary>
        public Kraken(params Module[] modules)
		{
            if (modules == null)
                throw new ArgumentNullException(nameof(modules));
            if (modules.Length == 0)
                throw new ArgumentException("It is necessary to have a minimum of 1 module.", nameof(modules));
            
            Modules = modules;
        }
        
        /// <summary>
        /// Rotors instaled on this MarkMark
        /// </summary>
        public Module[] Modules { get; }

        /// <summary>
        /// Process a key press.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>return the processing <see cref="char"/>. If the <see cref="char"/> is not contained in operating alphabet, return the input.</returns>
        public char Process(char input)
        {
            char working = input;

            bool should_rotate_next = true;
            for (int i = 0; i < Modules.Length; i++)
            {
                if (should_rotate_next)
                    should_rotate_next = Modules[i].Rotate();
                working = Modules[i].ProcessLeft(working);
            }

            return working;
        }
        /// <summary>
        /// Process the input <see cref="char"/> table to the output <see cref="char"/> table.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>return the processing <see cref="char"/> table. The <see cref="char"/> not contained in operating alphabet as not modified.</returns>
        public char[] Process(char[] input)
        {
            for (int i = 0; i < input.Length; i++)
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
                rslt[i] = Process(rslt[i]);

            return rslt;
        }

        /// <summary>
        /// Reset the MarkMark machine (all rotor to the initial position).
        /// </summary>
        public void Reset()
        {
            foreach (Module module in Modules)
                module.Rotor.Reset();
        }

        /// <summary></summary>
        public override string ToString()
		{
            return Modules.ToOneString(" / ");
        }

        /// <summary>
        /// Creates a duplicate of this Enigma.
        /// </summary>
        /// <returns></returns>
        public Kraken Clone()
        {
            return CloneKraken(false);
        }
        /// <summary>
        /// Creates a duplicate of this Enigma and reset then.
        /// </summary>
        /// <returns></returns>
        public Kraken Clone(bool andReset)
        {
            return CloneKraken(andReset);
        }
        /// <summary>
        /// Creates a duplicate of this Enigma.
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return CloneKraken(false);
        }
        /// <summary>
        /// Creates a duplicate of this Enigma and reset then.
        /// </summary>
        /// <returns></returns>
        public Kraken CloneKraken(bool andReset)
        {
            List<Module> lst = new List<Module>();
            foreach (Module module in Modules)
                lst.Add(module.Clone(andReset));

            return new Kraken(lst.ToArray());
        }
    }
}
