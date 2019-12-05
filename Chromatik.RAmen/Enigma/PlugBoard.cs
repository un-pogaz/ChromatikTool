using System;
using System.Collections.Generic;
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

	public class PlugBoard : IReadOnlyDictionary<char, char>, IEnumerable<KeyValuePair<char, char>>, System.Collections.IEnumerable
    {
		public PlugBoard() : this(new Dictionary<char, char>())
		{ }
        public PlugBoard(IDictionary<char, char> dictionary)
        {
            plugs = new Dictionary<char, char>(dictionary);
        }

        private Dictionary<char, char> plugs = new Dictionary<char, char>();


        public char this[char input]
        {
            get { return plugs[input]; }
        }
        public IEnumerable<char> Keys
        {
            get { return plugs.Keys; }
        }
        public IEnumerable<char> Values
        {
            get { return plugs.Values; }
        }
        public bool ContainsKey(char key)
        {
            return plugs.ContainsKey(key);
        }
        public bool ContainsValue(char value)
        {
            return plugs.ContainsValue(value);
        }
        public bool TryGetValue(char key, out char value)
        {
            return plugs.TryGetValue(key, out value);
        }
        public int Count
        {
            get { return plugs.Count; }
        }

        public System.Collections.Generic.IEnumerator<KeyValuePair<char, char>> GetEnumerator()
        {
            return plugs.GetEnumerator();
        }
        System.Collections.IEnumerator GetEnumerator()
        {
            return plugs.GetEnumerator();
        }

        /// <summary>
        /// Process the input character.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public char Process(char input)
		{
			char output;
			if (plugs.TryGetValue(input, out output))
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
			var workingFrom = from;
			if (char.IsLower(workingFrom))
			{
				workingFrom = char.ToUpper(workingFrom);
			}

			var workingTo = to;
			if (char.IsLower(workingTo))
			{
				workingTo = char.ToUpper(workingTo);
			}
			
			if (plugs.ContainsKey(from))
			{
				var existingTo = plugs[from];
				throw new PlugBoardException("Already mapped {0} <=> {1}".Format(from, existingTo));
			}
			if (plugs.ContainsKey(to))
			{
				var existingFrom = plugs[to];
				throw new PlugBoardException("Already mapped {0} <=> {1}".Format(existingFrom, to));
			}

            plugs.Add(workingFrom, workingTo);
            plugs.Add(workingTo, workingFrom);
		}

        /// <summary>
        /// Removes a plug mapping
        /// </summary>
        /// <param name="plug"></param>
        public void RemovePlug(char plug)
		{
			if (plugs.ContainsKey(plug))
            {
                char to = plugs[plug];
                plugs.Remove(plug);
                plugs.Remove(to);
            }
        }

		public override string ToString()
		{
			if(!Plugs.Any())
				return "None";
			else
				return string.Join(", ", this.Plugs.Select(_ => "{0}-{1}".Format(_.Key, _.Value)));
		}
	}
}
