using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Text
{
    static public class FindReplace
    {
        /// <summary>
        /// Executera une fois un Cherché/Remplacé simple
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string ReplaceOnce(this string input, string pattern, string replacement)
        {
            return input.Replace(pattern, replacement);
        }

        /// <summary>
        /// Executera le Cherché/Remplacé simple en boucle jusqu'a disparition du pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string ReplaceBoucle(this string input, string pattern, string replacement)
        {
            string rslt = input;
            do
            {
                rslt = ReplaceOnce(rslt, pattern, replacement);
            } while (rslt.Contains(pattern));

            return rslt;
        }

        /// <summary>
        /// Temps d'exectution des Cherché/Remplacé Regex (1 minute)
        /// </summary>
        static public TimeSpan tRrgex { get; set; } = new TimeSpan(0, 1, 0);

        static public RegexOptions RegexOptions { get; set; } = (RegexOptions.Singleline|RegexOptions.CultureInvariant);

        /// <summary>
        /// Executera une fois le Cherché/Remplacé Regex
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string RegexOnce(this string input, string pattern, string replacement)
        {
            return Regex.Replace(input, pattern, replacement, RegexOptions, tRrgex);
        }

        /// <summary>
        /// Executera le Cherché/Remplacé Regex en boucle jusqu'a disparition du pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public string RegexBoucle(this string input, string pattern, string replacement)
        {
            string rslt = input;
            do
            {
                rslt = RegexOnce(rslt, pattern, replacement);
            } while (Regex.Matches(rslt, pattern, RegexOptions, tRrgex).Count > 0);

            return rslt;
        }

        /// <summary>
        /// Executera le Cherché/Remplacé Regex en boucle jusqu'a disparition du pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        static public bool RegexIsMatch(this string input, string pattern)
        {
            return Regex.IsMatch(input, pattern, RegexOptions, tRrgex);
        }

    }
}
