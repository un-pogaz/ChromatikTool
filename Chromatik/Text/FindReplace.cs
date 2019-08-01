using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Text
{
    /// <summary>
    /// Extension class for advanced Search and Replace in a string.
    /// </summary>
    static public class FindReplace
    {
        /// <summary>
        /// Execute a simple Search/Replace loop until the pattern disappears.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string ReplaceLoop(this string input, string pattern, string replacement)
        {
            DateTime dt = DateTime.Now + Timeout;
            string rslt = input;
            do
            {
                if (dt < DateTime.Now)
                    throw new TimeoutException();

                rslt = rslt.Replace(pattern, replacement);
            } while (rslt.Contains(pattern));

            return rslt;
        }

        /// <summary>
        /// Execution time for Regex Search/Replace. 
        /// </summary>
        static public TimeSpan Timeout { get; set; } = new TimeSpan(0, 1, 0);

        /// <summary>
        /// <see cref="RegularExpressions.RegexOptions"/> for regex operations.
        /// </summary>
        static public RegexOptions RegexOptions { get; set; } = (RegexOptions.Singleline | RegexOptions.CultureInvariant);

        /// <summary>
        /// Execute a single regex Search/Replace.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string Regex(this string input, string pattern, string replacement)
        {
            return RegularExpressions.Regex.Replace(input, pattern, replacement, RegexOptions);
        }
        /// <summary>
        /// Execute a single regex Search/Replace.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        static public string Regex(this string input, string pattern, string replacement, RegexOptions options)
        {
            return RegularExpressions.Regex.Replace(input, pattern, replacement, options, Timeout);
        }
        /// <summary>
        /// Execute a single regex Search/Replace.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        /// <returns></returns>
        static public string Regex(this string input, string pattern, string replacement, RegexOptions options, TimeSpan matchTimeout)
        {
            return RegularExpressions.Regex.Replace(input, pattern, replacement, options, matchTimeout);
        }

        /// <summary>
        /// Execute a regex Search/Replace loop until the pattern disappears.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string RegexLoop(this string input, string pattern, string replacement)
        {
            return input.RegexLoop(pattern, replacement, RegexOptions);
        }
        /// <summary>
        /// Execute a regex Search/Replace loop until the pattern disappears.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        static public string RegexLoop(this string input, string pattern, string replacement, RegexOptions options)
        {
            return input.RegexLoop(pattern, replacement, options, Timeout);
        }
        /// <summary>
        /// Execute a regex Search/Replace loop until the pattern disappears.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        /// <returns></returns>
        static public string RegexLoop(this string input, string pattern, string replacement, RegexOptions options, TimeSpan matchTimeout)
        {
            DateTime dt = DateTime.Now + matchTimeout;
            string rslt = input;
            do
            {
                if (dt < DateTime.Now)
                    throw new TimeoutException();

                rslt = rslt.Regex(pattern, replacement, options, matchTimeout);
            } while (rslt.RegexIsMatch(pattern, options, matchTimeout));

            return rslt;
        }

        /// <summary>
        /// Split a string wiht a regex the pattern.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        static public string[] RegexSplit(this string input, string pattern)
        {
            return input.RegexSplit(pattern, RegexOptions);
        }
        /// <summary>
        /// Split a string wiht a regex the pattern.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        static public string[] RegexSplit(this string input, string pattern, RegexOptions options)
        {
            return input.RegexSplit(pattern, options, Timeout);
        }
        /// <summary>
        /// Split a string wiht a regex the pattern.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        /// <returns></returns>
        static public string[] RegexSplit(this string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            return RegularExpressions.Regex.Split(input, pattern, options, matchTimeout);
        }

        /// <summary> 
        /// Test if the Regex pattern exist.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        static public bool RegexIsMatch(this string input, string pattern)
        {
            return RegularExpressions.Regex.IsMatch(input, pattern, RegexOptions);
        }
        /// <summary>
        /// Test if the Regex pattern exist.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        static public bool RegexIsMatch(this string input, string pattern, RegexOptions options)
        {
            return RegularExpressions.Regex.IsMatch(input, pattern, options, Timeout);
        }
        /// <summary>
        /// Test if the Regex pattern exist.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        /// <returns></returns>
        static public bool RegexIsMatch(this string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            return RegularExpressions.Regex.IsMatch(input, pattern, options, matchTimeout);
        }

    }
}