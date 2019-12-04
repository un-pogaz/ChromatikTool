using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chromatik.Cryptography.Enigma
{
    sealed public class EnigmaAlphabet
    {
        public static EnigmaAlphabet Classic { get; } = new EnigmaAlphabet(' ',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
            'J', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z');

        public static EnigmaAlphabet ClassicCase { get; } = new EnigmaAlphabet(' ',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
            'J', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
            'j', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
            'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z');

        public static EnigmaAlphabet ASCII { get; } = new EnigmaAlphabet(' ',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
            'J', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
            'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
            'j', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
            'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',

            '!', '?', ':', ';', ',', '.',
            '#', '$', '%', '&', '\'', '\"',
            '*', '+', '-', '=',  '/', '\\',
            '<', '>', '(', ')', '[', ']', '{', '}',
            '@', '^', '|', '_');

        public static EnigmaAlphabet Byte { get; } = new EnigmaAlphabet(new Func<char[]>(() => 
        {
            char[] rslt = new char[byte.MaxValue+1];
            for (int i = 0; i < rslt.Length; i++)
                rslt[i] = char.ConvertFromUtf32(i)[0];

            return rslt;
        })());

        public char[] Alphabet { get; }

        public EnigmaAlphabet(params char[] alphabet)
        {
            if (alphabet == null)
                throw new ArgumentNullException(nameof(alphabet));
            if (alphabet.Length == 0)
                throw new ArgumentException();
            if (alphabet.LongLength > int.MaxValue)
                throw new Exception();

            List<char> lst = alphabet.ToList();
            lst.Sort();
            Alphabet = lst.Distinct().ToArray();
        }

    }
}
