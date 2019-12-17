using System;
using System.Collections.Generic;
using System.Text;

namespace System.Security.Cryptography.Machine
{
    public interface IMachine
    {
        char Process(char input);
        IEnumerable<char> Process(IEnumerable<char> input);
        string Process(string input);
    }
}
