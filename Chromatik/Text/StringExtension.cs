using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
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
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        static public string[] Split(this string input, string separator)
        {
            return input.Split(separator, StringSplitOptions.None);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="separator"></param>
        /// <param name="splitOptions"></param>
        /// <returns></returns>
        static public string[] Split(this string input, string separator, StringSplitOptions splitOptions)
        {
            return input.Split(new string[] { separator }, splitOptions);
        }

        /// <summary>
        /// Get a <see cref="string"/>[] of all line on this text
        /// </summary>
        /// <param name="input"></param>
        /// <param name="splitOptions"></param>
        /// <returns></returns>
        static public string[] SplitLine(this string input, StringSplitOptions splitOptions)
        {
            return input.ToLinux().Split(new string[] { "\n" }, splitOptions);
        }
        
        /// <summary>
        /// Parse the <see cref="string"/> to Linux End of Line char
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string ToLinux(this string input)
        {

            return input.Regex("(\r\n|\r)", "\n");
        }
        /// <summary>
        /// Replace End of Line char by a space " "
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string EndLineToSpace(this string input)
        {
            return input.ToLinux().Replace("\n", " ");
        }
        /// <summary>
        /// Replace End of Line char by a nothing
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string EndLineToNone(this string input)
        {
            return input.ToLinux().Replace("\n", "");
        }

        /// <summary>
        /// Combine a <see cref="string"/>[] to one line
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string ToOneLine(this string[] input)
        {
            return input.ToOneLine(StringOneLineOptions.NullToEmpty);
        }
        /// <summary>
        /// Combine a <see cref="string"/>[] to one line
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string ToOneLine(this string[] input, StringOneLineOptions oneLineOptions)
        {
            return input.ToOneLine("\n", oneLineOptions);
        }
        /// <summary>
        /// Combine a <see cref="string"/>[] to one line with a specified join
        /// </summary>
        /// <param name="input"></param>
        /// <param name="join"><see cref="string"/> use to join the lines</param>
        /// <returns></returns>
        static public string ToOneLine(this string[] input, string join)
        {
            return input.ToOneLine(join, StringOneLineOptions.NullToEmpty);
        }
        /// <summary>
        /// Combine a <see cref="string"/>[] to one line with a specified join
        /// </summary>
        /// <param name="input"></param>
        /// <param name="join"><see cref="string"/> use to join the lines</param>
        /// <param name="oneLineOptions">Skip or use empty <see cref="string"/> for <see langword="null"/> value</param>
        /// <returns></returns>
        static public string ToOneLine(this string[] input, string join, StringOneLineOptions oneLineOptions)
        {
            if (input.Length == 0)
                return string.Empty;

            string rslt = input[0];
            for (long i = 1; i < input.LongLength; i++)
            {
                if (input[i] == null)
                    switch (oneLineOptions)
                    {
                        case StringOneLineOptions.SkipNull:
                            break;
                        default: //StringOneLineOptions.NullToEmpty
                            rslt += join + string.Empty;
                            break;
                    }
                else
                    rslt += join + input[i];
            }

            return rslt;
        }
    }

    public enum StringOneLineOptions
    {
        NullToEmpty,
        SkipNull
    }
}
