using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Text
{
    /// <summary>
    /// Static class to extend <see cref="string"/>
    /// </summary>
    static public class StringExtension
    {

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
            return input.Split(WhiteCharacter.EndLineString, splitOptions);
        }
        
        /// <summary>
        /// Parse the <see cref="string"/> to Linux End of Line char
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string ToLinux(this string input)
        {
            return input.Regex("("+ WhiteCharacter.EndLineString.ToOneString("|")+")", "\n");
        }

        /// <summary>
        /// Combine a <see cref="string"/>[] to one line with a specified join
        /// </summary>
        /// <param name="input"></param>
        /// <param name="join"><see cref="string"/> use to join the lines</param>
        /// <returns></returns>
        /// <remarks>Use <see cref="StringOneLineOptions.NullToEmpty"/></remarks>
        static public string ToOneString(this string[] input, string join)
        {
            return input.ToOneString(join, StringOneLineOptions.NullToEmpty);
        }
        /// <summary>
        /// Combine a <see cref="string"/>[] to one line with a specified join
        /// </summary>
        /// <param name="input"></param>
        /// <param name="join"><see cref="string"/> use to join the lines</param>
        /// <param name="oneLineOptions">Behaviour to be use for the null, empty and WhiteSpace entries</param>
        /// <returns></returns>
        static public string ToOneString(this string[] input, string join, StringOneLineOptions oneLineOptions)
        {
            if (input == null || input.Length == 0)
                return string.Empty;

            if (input.Length == 1)
            {
                if (input != null)
                    return input[0];
                else
                    return string.Empty;
            }

            if (join == null)
                join = string.Empty;
            
            bool firstEntry = true;
            
            if (input.Length < 100)
            {
                #region Normal String for small array
                string rslt = string.Empty;

                for (long i = 0; i < input.LongLength; i++)
                {
                    switch (oneLineOptions)
                    {
                        case StringOneLineOptions.SkipNull:
                            {
                                if (input[i] != null)
                                    rslt = Join(rslt, ref firstEntry, join, input[i]);
                                break;
                            }
                        case StringOneLineOptions.SkipNullAndEmpty:
                            {
                                if (!string.IsNullOrEmpty(input[i]))
                                    rslt = Join(rslt, ref firstEntry, join, input[i]);
                                break;
                            }
                        case StringOneLineOptions.SkipNullAndWhiteSpace:
                            {
                                if (!string.IsNullOrWhiteSpace(input[i]))
                                    rslt = Join(rslt, ref firstEntry, join, input[i]);
                                break;
                            }

                        default: // StringOneLineOptions.NullToEmpty
                            {
                                if (input[i] == null)
                                    rslt = Join(rslt, ref firstEntry, join, string.Empty);
                                else
                                    rslt = Join(rslt, ref firstEntry, join, input[i]);
                                break;
                            }
                    }
                }
                return rslt;
                #endregion
            }
            else
            {
                #region StringBuilder for big array
                StringBuilder rslt = new StringBuilder(short.MaxValue);

                for (long i = 0; i < input.LongLength; i++)
                {
                    switch (oneLineOptions)
                    {
                        case StringOneLineOptions.SkipNull:
                            {
                                if (input[i] != null)
                                    rslt.Join(ref firstEntry, join, input[i]);
                                break;
                            }
                        case StringOneLineOptions.SkipNullAndEmpty:
                            {
                                if (!string.IsNullOrEmpty(input[i]))
                                    rslt.Join(ref firstEntry, join, input[i]);
                                break;
                            }
                        case StringOneLineOptions.SkipNullAndWhiteSpace:
                            {
                                if (!string.IsNullOrWhiteSpace(input[i]))
                                    rslt.Join(ref firstEntry, join, input[i]);
                                break;
                            }

                        default: // StringOneLineOptions.NullToEmpty
                            {
                                if (input[i] == null)
                                    rslt.Join(ref firstEntry, join, string.Empty);
                                else
                                    rslt.Join(ref firstEntry, join, input[i]);
                                break;
                            }
                    }
                }
                return rslt.ToString();
                #endregion
            }
        }

        static private void Join(this StringBuilder input, ref bool firstEntry, string join, string addition)
        {
            if (firstEntry)
            {
                input.Clear();
                input.Append(addition);
                firstEntry = false;
            }
            else
                input.Append(join + addition);
        }
        static private string Join(string input, ref bool firstEntry, string join, string addition)
        {
            if (firstEntry)
            {
                input = addition;
                firstEntry = false;
            }
            else
                input += join + addition;

            return input;
        }

        /// <summary>
        /// Truncate the <see cref="string"/> to the specified lenght. If negative, no change. 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="length">Lenght to truncate the string. If negative, no change</param>
        /// <returns></returns>
        static public string Truncate(this string input, int length)
        {
            if (length < 0)
                return input;
            else
            {
                if (input.Length > length)
                    return input.Substring(0, length);
                else
                    return input;
            }
        }

        /// <summary>
        /// Convert a <see cref="char"/>[] to a <see cref="string"/>[]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string[] ToStringArray(this char[] input)
        {
            string[] rslt = new string[input.Length];
            for (int i = 0; i < input.Length; i++)
                rslt[i] = input[i].ToString();
            return rslt;
        }

        /// <summary>
        /// Combine a <see cref="char"/>[] to a simple <see cref="string"/>
        /// </summary>
        static public string ToOneString(this char[] input)
        {
            return input.ToStringArray().ToOneString("", StringOneLineOptions.SkipNull);
        }
    }

    /// <summary>
    /// Enum for behaviour with a <see langword="null"/>, empty and WhiteSpace
    /// </summary>
    public enum StringOneLineOptions
    {
        /// <summary>
        /// Convert a <see langword="null"/> value to a empty
        /// </summary>
        NullToEmpty,
        /// <summary>
        /// Skip a <see langword="null"/> value
        /// </summary>
        SkipNull,
        /// <summary>
        /// Skip a <see langword="null"/> and empty value
        /// </summary>
        SkipNullAndEmpty,
        /// <summary>
        /// Skip a <see langword="null"/> and WhiteSpace value
        /// </summary>
        SkipNullAndWhiteSpace,
    }
}
