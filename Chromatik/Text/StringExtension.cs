using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    static public class StringExtension
    {
        /// <summary>
        /// Default char array for <see cref="string.Trim()"/>
        /// </summary>
        static public char[] TrimChar { get; } = new char[] {          
            //Latin
            ' ',
            '\n',
            '\r',
            '\t',
            '\v',
            '\x000a', //LINE FEED
            '\x000c', //FORM FEED
            '\x00a0', //NO-BREAK SPACE
            
            //Unicode SpaceSeparator
            '\x1680', // Ogham Space Mark
            '\x2000', // En Quad
            '\x2001', // Em Quad
            '\x2002', // En Space
            '\x2003', // Em Space
            '\x2004', // Three-Per-Em Space
            '\x2005', // Four-Per-Em Space
            '\x2006', // Six-Per-Em Space
            '\x2007', // Figure Space
            '\x2008', // Punctuation Space
            '\x2009', // Thin Space
            '\x200A', // Hair Space
            '\x202F', // Narrow No-Break Space
            '\x205F', // Medium Mathematical Space
            '\x3000', // Ideographic Space
            
            //Unicode LineSeparator
            '\x2028',
            //Unicode ParagraphSeparator
            '\x2029',

            //Perso
            '\x200B', // Zero Width Space
            '\x200C', // Zero Width Non-Joiner
            
        };

        /// <summary>
        /// End of line char
        /// </summary>
        static public char[] EndLineChar { get; } = new char[]
        {
            '\n',
            '\r',

            '\x000a', //LINE FEED
            '\x000c', //FORM FEED

            //Unicode LineSeparator
            '\x2028',
            //Unicode ParagraphSeparator
            '\x2029',
        };

        /// <summary>
        /// Get a <see cref="string"/>[] of all line on this text
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string[] SplitLine(this string input)
        {
            return input.SplitLine(StringSplitOptions.None);
        }

        /// <summary>
        /// Get a <see cref="string"/>[] of all line on this text
        /// </summary>
        /// <param name="input"></param>
        /// <param name="splitOptions"></param>
        /// <returns></returns>
        static public string[] SplitLine(this string input, StringSplitOptions splitOptions)
        {
            return input.Split(EndLineChar, splitOptions);
        }

    }
}
