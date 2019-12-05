using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chromatik.Cryptography.Enigma
{
	public class PlugBoardException : EnigmaException
	{
		public PlugBoardException() : base() { }
		public PlugBoardException(string message) : base(message) { }
	}

	public class PlugBoard : ReadOnlyDictionary<char, char>
    {
		public PlugBoard() : this(new Dictionary<char, char>())
		{ }
        public PlugBoard(IDictionary<char, char> dictionary) : base(dictionary)
        { }
        /// <summary>
        /// Process the input character.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public char Process(char input)
		{
			char output;
			if (TryGetValue(input, out output))
				return output;
			return input;
		}

		/// <summary>
		/// Adds a plug mapping
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public void AddPlug(char from, char to)
		{
			if (ContainsKey(from))
				throw new PlugBoardException("Already mapped {0} <=> {1}".Format(from, Dictionary[from]));
			if (ContainsKey(to))
				throw new PlugBoardException("Already mapped {0} <=> {1}".Format(Dictionary[to], to));

            Dictionary.Add(from, to);
            Dictionary.Add(to, from);
		}

        /// <summary>
        /// Removes a plug mapping
        /// </summary>
        /// <param name="plug"></param>
        public void RemovePlug(char plug)
		{
			if (ContainsKey(plug))
            {
                char to = Dictionary[plug];
                Dictionary.Remove(plug);
                Dictionary.Remove(to);
            }
        }
        /// <summary></summary>
		public override string ToString()
		{
            if (!Dictionary.Any())
                return "None";
            else
                return Dictionary.Select(pair => "{0}-{1}".Format(pair.Key, pair.Value)).Join(", ");
		}
	}
}
