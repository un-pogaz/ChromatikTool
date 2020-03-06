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
        /// Pattern for test the validity of an email address (used in <see cref="System.Net.Mail.MailAddress"/>)
        /// </summary>
        public const string EmailAddress = @"(?("")("".+?(?<!\\)""@)|(([0-9A-Za-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9A-Za-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9A-Za-z][-\w]*[0-9A-Za-z]*\.)+[A-Za-z0-9][\-A-Za-z0-9]{0,22}[A-Za-z0-9]))";

        /// <summary>
        /// Basic pattern for test the validity of an email address
        /// </summary>
        /// <remarks>https://www.regular-expressions.info/email.html</remarks>
        public const string EmailAddressBasic = @"[A-Za-z0-9._%+\-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}";

        /// <summary>
        /// W3C pattern for test the validity of an email address
        /// </summary>
        public const string EmailAddressW3C = @"[A-Za-z0-9.!#$%&’*+/=?^_`{|}~-]+@[A-Za-z0-9-]+(?:\.[A-Za-z0-9-]+)*";
        
        /// <summary>
        /// .NET pattern for test the validity of an email address
        /// </summary>
        public const string EmailAddressNET = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)";

        /// <summary>
        /// Strict RFC 5322 pattern for test the validity of an email address
        /// </summary>
        /// <remarks>https://emailregex.com/</remarks>
        public const string EmailAddressRFC_5322_Strict = @"(?:[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01 -\x08\x0b\x0c\x0e -\x1f\x21\x23 -\x5b\x5d -\x7f] |\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[A-Za-z0-9-]*[A-Za-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";

        /// <summary>
        /// Simpliefed RFC 5322 pattern for test the validity of an email address
        /// </summary>
        /// <remarks>https://www.regular-expressions.info/email.html</remarks>
        public const string EmailAddressRFC_5322_Simpliefed = @"[A-Za-z0-9!#$%&'*+/=?^_‘{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_‘{|}~-]+)*@(?:[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?";

        /// <summary>
        /// Pattern for match a No ASCII character
        /// </summary>
        public const string NoASCII = @"[^ -~]";
        /// <summary>
        /// Pattern for match a ASCII character
        /// </summary>
        public const string ASCII = @"[ -~]";
        
        /// <summary>
        /// Pattern for match a ASCII <see langword="namespace"/> in C#
        /// </summary>
        public const string ASCII_forCsharpNameSpace = @"^([A-Z_a-z][0-9A-Z_a-z]*(\.[A-Z_a-z][0-9A-Z_a-z]*)*)$";
        /// <summary>
        /// Pattern for match a ASCII <see langword="class"/> in C#
        /// </summary>
        public const string ASCII_forCsharpNameClass = @"^([A-Z_a-z][0-9A-Z_a-z]*)$";

        /// <summary>
        /// Pattern for match a ISBN 10
        /// </summary>
        public const string ISBN_10 = @"(?:ISBN(?:-10)?:? ?)?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$)[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9X]";
        /// <summary>
        /// Pattern for match a ISBN 13
        /// </summary>
        public const string ISBN_13 = @"(?:ISBN(?:-13)?:? ?)?(?=[0-9]{13}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)97[89][- ]?[0-9]{1,5}[- ]?[0-9]+[- ]?[0-9]+[- ]?[0-9]";

        /// <summary>
        /// Execute a simple Search/Replace loop until the pattern disappears.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="oldValues"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        static public string ReplaceLoop(this string input, string oldValues, string newValue)
        {
            DateTime dt = DateTime.Now + Timeout;
            do
            {
                if (dt < DateTime.Now)
                    throw new TimeoutException();

                input = input.Replace(oldValues, newValue);
            } while (input.Contains(oldValues));

            return input;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of the specifieds characters are replaced by a another character.
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

            foreach (var item in oldValues)
                input = input.Replace(item, newValue);

            return input;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of the specifieds string are replaced by a another string.
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

            foreach (var item in oldValues)
                input = input.Replace(item, newValue);

            return input;
        }
        /// <summary>
        /// Returns a new string in which all occurrences of the specifieds string are replaced by a another string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="oldValues"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        static public string ReplaceLoop(this string input, string[] oldValues, string newValue)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (oldValues == null)
                oldValues = new string[0];

            foreach (var item in oldValues)
                input = input.ReplaceLoop(item, newValue);

            return input;
        }

        /// <summary>
        /// Default execution time for Regex Search/Replace (30s). 
        /// </summary>
        static public TimeSpan Timeout { get { return new TimeSpan(0, 0, 30); } }

        /// <summary>
        /// Default <see cref="RegularExpressions.RegexOptions"/> for regex operations.
        /// </summary>
        static public RegexOptions RegexOptions { get { return (RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant); } }


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
            Regex regexp = new Regex(pattern, options, matchTimeout);
            do
            {
                if (dt < DateTime.Now)
                    throw new RegexMatchTimeoutException(input, pattern, matchTimeout);

                input = regexp.Replace(input, replacement);

            } while (regexp.IsMatch(input));

            return input;
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
