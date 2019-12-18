using System;
using System.Collections.Generic;
using System.Text;

namespace System.Security.Cryptography.Machine
{
    /// <summary>
    /// Interface represent a cryptography machine
    /// </summary>
    public interface IMachine
    {
        /// <summary>
        /// Process a <see cref="char"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        char Process(char input);
        /// <summary>
        /// Process a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        IEnumerable<char> Process(IEnumerable<char> input);
        /// <summary>
        /// Process a <see cref="string"/>
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string Process(string input);
    }
}
