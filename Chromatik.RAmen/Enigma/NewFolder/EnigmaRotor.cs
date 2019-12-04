using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chromatik.Cryptography.Enigma
{
    sealed public partial class EnigmaRotor
    {
        public EnigmaAlphabet OperatingAlphabet { get; }
        

        Dictionary<char, char> reader = new Dictionary<char, char>();
        Dictionary<char, char> coder = new Dictionary<char, char>();

        public EnigmaRotor(EnigmaAlphabet alphabet, params char[] substitution)
        {
            if (substitution == null)
                throw new ArgumentNullException(nameof(substitution));
            if (alphabet == null)
                throw new ArgumentNullException(nameof(alphabet));
            OperatingAlphabet = alphabet;

            if (substitution.LongLength > int.MaxValue)
                throw new ArgumentException("The substitution table exceeds the authorized size limit ("+ int.MaxValue + ")", nameof(substitution));

            if (substitution.LongLength != OperatingAlphabet.Alphabet.LongLength)
                throw new ArgumentException("The substitution table is not the same size as the alphabet.", nameof(substitution));

            if (substitution.Length != substitution.Distinct().Count())
                throw new ArgumentException("The substitution table contains duplicate output elements.", nameof(substitution));
            
            List<char> lst = new List<char>(substitution);
            lst.Sort();
            substitution = lst.ToArray();

            for (int i = 0; i < OperatingAlphabet.Alphabet.Length; i++)
                if (!(OperatingAlphabet.Alphabet[i] == substitution[i]))
                    throw new Exception();

            reader.Clear();
            coder.Clear();
            for (int i = 0; i < substitution.Length; i++)
            {
                coder.Add(OperatingAlphabet.Alphabet[i], substitution[i]);
                reader.Add(substitution[i], OperatingAlphabet.Alphabet[i]);
            }
        }

        public char ReadChar(char c)
        {
            return reader[c];
        }
        public char CodeChar(char c)
        {
            return coder[c];
        }
    }
}
