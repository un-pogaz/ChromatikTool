using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Text
{
    /// <summary>
    /// Extension class for advanced Search and Replace in a string
    /// </summary>
    static public class FindReplace
    {
        /// <summary>
        /// Execute a simple Search/Replace.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string ReplaceOnce(this string input, string pattern, string replacement)
        {
            return input.Replace(pattern, replacement);
        }

        /// <summary>
        /// Execute a simple Search/Replace loop until the pattern disappears.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string ReplaceBoucle(this string input, string pattern, string replacement)
        {
            DateTime dt = DateTime.Now + Timeout;
            string rslt = input;
            do
            {
                if (dt < DateTime.Now)
                    throw new TimeoutException();
                
                rslt = ReplaceOnce(rslt, pattern, replacement);
            } while (rslt.Contains(pattern));

            return rslt;
        }

        /// <summary>
        /// Execution time for Regex Search/Replace. 
        /// </summary>
        static public TimeSpan Timeout { get; set; } = new TimeSpan(0, 1, 0);

        /// <summary>
        /// <see cref="RegexOptions"/> for regex operations.
        /// </summary>
        static public RegexOptions RegexOptions { get; set; } = (RegexOptions.Singleline|RegexOptions.CultureInvariant);

        /// <summary>
        /// Execute a Regex Search/Replace 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string RegexOnce(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement, RegexOptions, Timeout);
        }

        /// <summary>
        /// Execute a Regex Search/Replace loop until the pattern disappears.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string RegexBoucle(this string input, string pattern, string replacement)
        {
            DateTime dt = DateTime.Now + Timeout;
            string rslt = input;
            do
            {
                if (dt < DateTime.Now)
                    throw new TimeoutException();

                rslt = RegexOnce(rslt, pattern, replacement);
            } while (Regex.Matches(rslt, pattern, RegexOptions, Timeout).Count > 0);

            return rslt;
        }

        /// <summary>
        /// Test if the Regex pattern exist.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        static public bool RegexIsMatch(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern, RegexOptions, Timeout);
        }

    }
}
