using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Chromatik.Cryptography.Enigma
{
    sealed public partial class EnigmaRotor
    {
        static public class Classic
        {
            static public EnigmaRotor ReflectorC { get; } = new EnigmaRotor(EnigmaAlphabet.Classic,
                ' ',
                'A',
                'B',
                'C',
                'D',
                'E',
                'F',
                'G',
                'H',
                'I',
                'J',
                'K',
                'L',
                'M',
                'N',
                'O',
                'P',
                'Q',
                'R',
                'S',
                'T',
                'U',
                'V',
                'W',
                'X',
                'Y',
                'Z');
        }
        
    }
}
