using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Text
{
    /// <summary>
    /// Extension class for advanced Search and Replace in a string.
    /// </summary>
    static public class RegexHelper
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
            do
            {
                if (dt < DateTime.Now)
                    throw new TimeoutException();

                input = input.Replace(pattern, replacement);
            } while (input.Contains(pattern));

            return input;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of the specifieds characters are replaced by a another character specified.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="oldValues"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        static public string Replace(this string input, char[] oldValues, char newValue)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (oldValues == null)
                oldValues = new char[0];

            return oldValues.Aggregate(input, (current, c) => current.Replace(c, newValue));
        }

        /// <summary>
        /// Returns a new string in which all occurrences of the specifieds string are replaced by a another string specified.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="oldValues"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        static public string Replace(this string input, string[] oldValues, string newValue)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (oldValues == null)
                oldValues = new string[0];

            return oldValues.Aggregate(input, (current, c) => current.Replace(c, newValue));
        }

        /// <summary>
        /// Default execution time for Regex Search/Replace. 
        /// </summary>
        static public TimeSpan DefaultTimeout { get; set; } = new TimeSpan(0, 0, 30);
        /// <summary>
        /// Execution time for Regex Search/Replace. 
        /// </summary>
        static public TimeSpan Timeout { get; set; } = DefaultTimeout;

        /// <summary>
        /// Default <see cref="RegularExpressions.RegexOptions"/> for regex operations.
        /// </summary>
        static public RegexOptions DefaultRegexOptions { get; } = (RegexOptions.Singleline|RegexOptions.Multiline| RegexOptions.CultureInvariant);
        /// <summary>
        /// <see cref="RegularExpressions.RegexOptions"/> for regex operations.
        /// </summary>
        static public RegexOptions RegexOptions { get; set; } = DefaultRegexOptions;

        /// <summary>
        /// Execute a single regex Search/Replace.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string Regex(this string input, string pattern, string replacement)
        {
            return input.Regex(pattern, replacement, RegexOptions);
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
            return input.Regex(pattern, replacement, options, Timeout);
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
            if (pattern == null)
                pattern = string.Empty;
            if (replacement == null)
                replacement = string.Empty;
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
        /// Test if the Regex pattern exist.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        static public bool RegexIsMatch(this string input, string pattern)
        {
            return input.RegexIsMatch(pattern, RegexOptions);
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
            return input.RegexIsMatch(pattern, options, Timeout);
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


        /// <summary>
        /// Get the first Regex match.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        static public string RegexGetMatch(this string input, string pattern)
        {
            return input.RegexGetMatch(pattern, RegexOptions);
        }
        /// <summary>
        /// Get the first Regex match.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        static public string RegexGetMatch(this string input, string pattern, RegexOptions options)
        {
            return input.RegexGetMatch(pattern, options, Timeout);
        }
        /// <summary>
        /// Get the first Regex match.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        static public string RegexGetMatch(this string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            string[] rslt = input.RegexGetMatches(pattern, options, matchTimeout);
            if (rslt.LongLength > 0)
                return rslt[0];
            else
                return string.Empty;
        }

        /// <summary>
        /// Get the Regex matchs patterns.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        static public string[] RegexGetMatches(this string input, string pattern)
        {
            return input.RegexGetMatches(pattern, RegexOptions);
        }
        /// <summary>
        /// Get the Regex matchs patterns.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        static public string[] RegexGetMatches(this string input, string pattern, RegexOptions options)
        {
            return input.RegexGetMatches(pattern, options, Timeout);
        }
        /// <summary>
        /// Get the Regex matchs patterns.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <param name="matchTimeout"></param>
        static public string[] RegexGetMatches(this string input, string pattern, RegexOptions options, TimeSpan matchTimeout)
        {
            MatchCollection matchs = RegularExpressions.Regex.Matches(input, pattern, options, matchTimeout);
            string[] rslt = new string[matchs.Count];
            for (int i = 0; i < matchs.Count; i++)
                rslt[i] = matchs[i].Value;
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
    }
}
