using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Chromatik.Text
{
    internal static class RegexMail
    {
        internal const string EmailAddressPattern = @"(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))";


        static internal bool ContainsEmailAddress(string input)
        {
            return input.RegexIsMatch(EmailAddressPattern, RegexOptions.IgnoreCase);
        }

        static internal bool IsEmailAddress(string input)
        {
            return input.RegexIsMatch("^" + EmailAddressPattern + "$", RegexOptions.IgnoreCase);
        }
    }
}
