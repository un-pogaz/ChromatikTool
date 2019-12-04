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

	public class PlugBoard
	{
		public PlugBoard()
		{
			Plugs = new Dictionary<char, char>();
		}

		public Dictionary<char, char> Plugs { get; private set; }

		/// <summary>
		/// Process the input character.
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public char Process(char input)
		{
			char output;
			if (Plugs.TryGetValue(input, out output))
			{
				return output;
			}
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
			
			if (Plugs.ContainsKey(from))
			{
				var existingTo = this.Plugs[from];
				throw new PlugBoardException("Already mapped {0} <=> {1}".Format(from, existingTo));
			}
			if (Plugs.ContainsKey(to))
			{
				var existingFrom = this.Plugs[to];
				throw new PlugBoardException("Already mapped {0} <=> {1}".Format(existingFrom, to));
			}

			Plugs.Add(workingFrom, workingTo);
			Plugs.Add(workingTo, workingFrom);
		}

		/// <summary>
		/// Removes a plug mapping
		/// </summary>
		/// <param name="from"></param>
		public void RemovePlug(char from)
		{
			if (Plugs.ContainsKey(from))
			{
				var to = Plugs[from];
				Plugs.Remove(from);
				Plugs.Remove(to);
			}
		}

		public override string ToString()
		{
			if(!this.Plugs.Any())
			{
				return "None";
			}
			else
			{
				return string.Join(", ", this.Plugs.Select(_ => "{0}-{1}".Format(_.Key, _.Value)));
			}
		}
	}
}
