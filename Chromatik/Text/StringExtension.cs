using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    /// <summary>
    /// Static class to extend <see cref="string"/>
    /// </summary>
    static public class StringExtension
    {
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
        static public string ToOneString(this string[] input)
        {
            return input.ToOneString(StringOneLineOptions.NullToEmpty);
        }
        /// <summary>
        /// Combine a <see cref="string"/>[] to one line
        /// </summary>
        /// <param name="input"></param>
        /// <param name="oneLineOptions"></param>
        /// <returns></returns>
        static public string ToOneString(this string[] input, StringOneLineOptions oneLineOptions)
        {
            return input.ToOneString("\n", oneLineOptions);
        }
        /// <summary>
        /// Combine a <see cref="string"/>[] to one line with a specified join
        /// </summary>
        /// <param name="input"></param>
        /// <param name="join"><see cref="string"/> use to join the lines</param>
        /// <returns></returns>
        static public string ToOneString(this string[] input, string join)
        {
            return input.ToOneString(join, StringOneLineOptions.NullToEmpty);
        }
        /// <summary>
        /// Combine a <see cref="string"/>[] to one line with a specified join
        /// </summary>
        /// <param name="input"></param>
        /// <param name="join"><see cref="string"/> use to join the lines</param>
        /// <param name="oneLineOptions">Skip or use empty <see cref="string"/> for <see langword="null"/> value</param>
        /// <returns></returns>
        static public string ToOneString(this string[] input, string join, StringOneLineOptions oneLineOptions)
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
        
        /// <summary>
        /// Truncate the <see cref="string"/> to the specified lenght. If negative, no change. 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        static public string Truncate(this string s, int length)
        {
            if (length < 0)
                return s;
            else
            {
                if (s.Length > length)
                    return s.Substring(0, length);
                else
                    return s;
            }
        }
    }

    /// <summary>
    /// Enum for behaviour with a <see langword="null"/> 
    /// </summary>
    public enum StringOneLineOptions
    {
        /// <summary>
        /// Convert a <see langword="null"/> value to a empty <see cref="string"/>
        /// </summary>
        NullToEmpty,
        /// <summary>
        /// Skip a <see langword="null"/> value
        /// </summary>
        SkipNull
    }
}
