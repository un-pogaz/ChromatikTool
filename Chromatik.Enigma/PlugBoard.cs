using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security.Cryptography.Machine
{
    /// <summary>
    /// Represent a Plug Board which will switch 2 characters.
    /// </summary>
    [System.Diagnostics.DebuggerDisplay(null)]
    sealed public class PlugBoard : ReadOnlyDictionary<char, char>, ICloneable
    {
        /// <summary>
        /// Initialize a empty Plug Board.
        /// </summary>
        public PlugBoard() : this(new Dictionary<char, char>())
		{ }
        /// <summary>
        /// Initialize a Plug Board with a specified plug.
        /// </summary>
        public PlugBoard(IDictionary<char, char> dictionary) : base(new Dictionary<char, char>())
        {
            foreach (var item in dictionary)
            {
                if (ContainsKey(item.Value) && this[item.Value] == item.Key)
                {
                    // Already mapped
                }
                else
                    AddPlug(item.Key, item.Value);
            }
        }
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
				throw new ArgumentException("Already mapped {0} <=> {1}".Format(from, Dictionary[from]), nameof(from));
			if (ContainsKey(to))
				throw new ArgumentException("Already mapped {0} <=> {1}".Format(Dictionary[to], to), nameof(to));

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

        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        public PlugBoard Clone()
        {
            return ClonePlugBoard();
        }
        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        object ICloneable.Clone()
        {
            return ClonePlugBoard();
        }
        /// <summary>
        /// Creates a duplicate of this rotor
        /// </summary>
        /// <returns></returns>
        public PlugBoard ClonePlugBoard()
        {
            return new PlugBoard(this.Dictionary);
        }
    }
}
