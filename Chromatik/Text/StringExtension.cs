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
        static public string[] Split(this string input, params string[] separator)
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
        /// Get a <see cref="string"/> array of all line on this text
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string[] SplitLine(this string input)
        {
            return input.SplitLine(StringSplitOptions.None);
        }
        /// <summary>
        /// Get a <see cref="string"/> array of all line on this text
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
            return input.Regex("(" + WhiteCharacter.EndLineString.Join("|") + ")", "\n");
        }
        
        /// <summary>
        /// Concatenates the members of a <see cref="IEnumerable{T}"/> collection built of <see cref="string"/> type, using the separator specified between each member.
        /// </summary>
        /// <param name="values">Collection that contains the chains to be concatenated.</param>
        /// <param name="separator">String to use as separator. Separator is included in the returned string only if values contains several elements.</param>
        /// <returns>String composed of the value elements delimited by the separator. If values is an empty array, the method returns <see cref="string.Empty"/>.</returns>
        static public string Join(this IEnumerable<string> values, string separator)
        {
            return values.ToArray().Join(separator);
        }
        /// <summary>
        /// Concatenates all the elements of a <see cref="string"/> array, using the separator specified between each element.
        /// </summary>
        /// <param name="values">Array containing the elements to be concatenated.</param>
        /// <param name="separator">String to use as separator. Separator is included in the returned string only if value contains several elements.</param>
        /// <returns>String composed of the value elements delimited by the separator. If values is an empty array, the method returns <see cref="string.Empty"/>.</returns>
        static public string Join(this string[] values, string separator)
        {
            return values.Join(separator, StringJoinOptions.NullToEmpty);
        }
        /// <summary>
        /// Concatenates the members of a <see cref="IEnumerable{T}"/> collection built of <see cref="string"/> type, using the separator specified between each member, and applies the specified behavior for the null, empty or WhiteSpace entries.
        /// </summary>
        /// <param name="values">Collection that contains the chains to be concatenated.</param>
        /// <param name="separator">String to use as separator. Separator is included in the returned string only if values contains several elements.</param>
        /// <param name="oneLineOptions">Behaviour to be use for the null, Empty or WhiteSpace entries.</param>
        /// <returns>String composed of the value elements delimited by the separator. If values is an empty array, the method returns <see cref="string.Empty"/>.</returns>
        static public string Join(this IEnumerable<string> values, string separator, StringJoinOptions oneLineOptions)
        {
            return values.ToArray().Join(separator, oneLineOptions);
        }
        /// <summary>
        /// Concatenates all the elements of a <see cref="string"/> array, using the separator specified between each element, and applies the specified behavior for the null, empty or WhiteSpace entries.
        /// </summary>
        /// <param name="values">Array containing the elements to be concatenated.</param>
        /// <param name="separator">String to use as separator. Separator is included in the returned string only if values contains several elements.</param>
        /// <param name="oneLineOptions">Behaviour to be use for the null, Empty or WhiteSpace entries.</param>
        /// <returns>String composed of the value elements delimited by the separator. If values is an empty array, the method returns <see cref="string.Empty"/>.</returns>
        static public string Join(this string[] values, string separator, StringJoinOptions oneLineOptions)
        {
            if (values == null || values.Length == 0)
                return string.Empty;

            if (values.Length == 1)
            {
                if (values != null)
                    return values[0].ToString();
                else
                    return string.Empty;
            }

            if (separator == null)
                separator = string.Empty;

            List<string> rslt = new List<string>(values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                switch (oneLineOptions)
                {
                    case StringJoinOptions.SkipNull:
                        if (values[i] != null)
                            rslt.Add(values[i]);
                        break;
                    case StringJoinOptions.NullToNull:
                        if (values[i] == null)
                            rslt.Add("Null");
                        else
                            rslt.Add(values[i]);
                        break;
                    case StringJoinOptions.SkipNullAndEmpty:
                        if (!values[i].IsNullOrEmpty())
                            rslt.Add(values[i]);
                        break;
                    case StringJoinOptions.SkipNullAndWhiteSpace:
                        if (!values[i].IsNullOrWhiteSpace())
                            rslt.Add(values[i]);
                        break;
                    default: //StringOneLineOptions.NullToEmpty
                        if (values[i] == null)
                            rslt.Add(string.Empty);
                        else
                            rslt.Add(values[i]);
                        break;
                }
            }

            return string.Join(separator, rslt);
        }

        /// <summary>
        /// Concatenates the members of a <see cref="IEnumerable{T}"/>, using the separator specified between each member.
        /// </summary>
        /// <param name="values">Collection that contains the chains to be concatenated.</param>
        /// <param name="separator">String to use as separator. Separator is included in the returned string only if values contains several elements.</param>
        /// <returns>String composed of the value elements delimited by the separator. If values is an empty array, the method returns <see cref="string.Empty"/>.</returns>
        static public string ToOneString<T>(this IEnumerable<T> values, string separator)
        {
            if (values == null)
                return string.Empty;
            return values.ToOneString(separator, StringJoinOptions.NullToEmpty);
        }
        /// <summary>
        /// Concatenates all the elements in a <see cref="string"/>, using the separator specified between each element.
        /// </summary>
        /// <param name="values">Array containing the elements to be concatenated.</param>
        /// <param name="separator">String to use as separator. Separator is included in the returned string only if value contains several elements.</param>
        /// <returns>String composed of the value elements delimited by the separator. If values is an empty array, the method returns <see cref="string.Empty"/>.</returns>
        static public string ToOneString(this object[] values, string separator)
        {
            if (values == null)
                return string.Empty;
            return values.ToOneString(separator, StringJoinOptions.NullToEmpty);
        }
        /// <summary>
        /// Concatenates the members of a <see cref="IEnumerable{T}"/> collection, using the separator specified between each member, and applies the specified behavior for the null, empty or WhiteSpace entries.
        /// </summary>
        /// <param name="values">Collection that contains the chains to be concatenated.</param>
        /// <param name="separator">String to use as separator. Separator is included in the returned string only if values contains several elements.</param>
        /// <param name="oneLineOptions">Behaviour to be use for the null, Empty or WhiteSpace entries.</param>
        /// <returns>String composed of the value elements delimited by the separator. If values is an empty array, the method returns <see cref="string.Empty"/>.</returns>
        static public string ToOneString<T>(this IEnumerable<T> values, string separator, StringJoinOptions oneLineOptions)
        {
            if (values == null)
                return string.Empty;
            object[] rslt = new object[values.Count()];
            values.ToArray().CopyTo(rslt, 0);

            return rslt.ToOneString(separator, oneLineOptions);
        }
        /// <summary>
        /// Concatenates all the elements in a <see cref="string"/>, using the separator specified between each element, and applies the specified behavior for the null, empty or WhiteSpace entries.
        /// </summary>
        /// <param name="values">Array containing the elements to be concatenated.</param>
        /// <param name="separator">String to use as separator. Separator is included in the returned string only if values contains several elements.</param>
        /// <param name="oneLineOptions">Behaviour to be use for the null, Empty or WhiteSpace entries.</param>
        /// <returns>String composed of the value elements delimited by the separator. If values is an empty array, the method returns <see cref="string.Empty"/>.</returns>
        static public string ToOneString(this object[] values, string separator, StringJoinOptions oneLineOptions)
        {
            if (values == null)
                return string.Empty;

            string[] rslt = new string[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != null)
                    rslt[i] = values[i].ToString();
                else
                    rslt[i] = null;
            }
            return rslt.Join(separator, oneLineOptions);
        }

        /// <summary>
        /// Indicates whether the specified string is null or a empty string.
        /// </summary>
        /// <param name="value">String to test.</param>
        /// <returns>true if the value parameter is null or an empty string (""); otherwise false.</returns>
        static public bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        /// <summary>
        /// Indicates whether a specified string is null, empty or consists only of white spaces.
        /// </summary>
        /// <param name="value">String to test.</param>
        /// <returns>true if the value parameter is null or empty string (""), or if value is composed exclusively of white spaces.</returns>
        static public bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
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
        /// <param name="input"></param>
        static public string ToOneString(this char[] input)
        {
            return input.ToOneString(null);
        }
        /// <summary>
        /// Combine a <see cref="char"/>[] to a simple <see cref="string"/>
        /// </summary>
        /// <param name="input"></param>
        /// <param name="join"><see cref="char"/> use to join</param>
        static public string ToOneString(this char[] input, char? join)
        {
            string s = null;
            if (join == null)
                s = null;
            else
                s = join.ToString();

            return input.ToStringArray().Join(s, StringJoinOptions.SkipNull);
        }

        /// <summary>
        /// TrimStart() all <see cref="string"/> in the array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string[] TrimStartAll(this string[] input)
        {
            return input.TrimStartAll(WhiteCharacter.WhiteCharacters);
        }
        /// <summary>
        /// TrimStart() all <see cref="string"/> in the array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        static public string[] TrimStartAll(this string[] input, params char[] trimChars)
        {
            if (input == null)
                return null;
            for (int i = 0; i < input.Length; i++)
                if (input[i] != null)
                    input[i] = input[i].TrimStart();

            return input;
        }
        /// <summary>
        /// TrimEnd() all <see cref="string"/> in the array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string[] TrimEndAll(this string[] input)
        {
            return input.TrimEndAll(WhiteCharacter.WhiteCharacters);
        }
        /// <summary>
        /// TrimEnd() all <see cref="string"/> in the array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        static public string[] TrimEndAll(this string[] input, params char[] trimChars)
        {
            if (input == null)
                return null;
            for (int i = 0; i < input.Length; i++)
                if (input[i] != null)
                    input[i] = input[i].TrimEnd();

            return input;
        }
        /// <summary>
        /// Trim() all <see cref="string"/> in the array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static public string[] TrimAll(this string[] input)
        {
            return input.TrimAll(WhiteCharacter.WhiteCharacters);
        }
        /// <summary>
        /// Trim() all <see cref="string"/> in the array
        /// </summary>
        /// <param name="input"></param>
        /// <param name="trimChars"></param>
        /// <returns></returns>
        static public string[] TrimAll(this string[] input, params char[] trimChars)
        {
            return input.TrimStartAll(trimChars).TrimEndAll(trimChars);
        }

        /// <summary>
        /// Determines if this instance and another specified <see cref="string"/> object have the same value.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        static public bool Equals(this string input, params string[] values)
        {
            if (values == null)
                return false;
            foreach (var item in values)
                if (input.Equals(item))
                    return true;

            return false;
        }

        /// <summary>
        /// Determines if this string and a specified <see cref="string"/> object have the same value, a parameter specifies the culture, case and sort rules used in the comparison.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="values"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        static public bool Equals(this string input, StringComparison comparisonType, params string[] values)
        {
            if (values == null)
                return false;
            foreach (var item in values)
                if (input.Equals(item, comparisonType))
                    return true;

            return false;
        }
        /// <summary>
        /// Replaces the formatting element of a specified string with the string representation of a corresponding object in a specified table.
        /// </summary>
        /// <param name="format">Composite format string.</param>
        /// <param name="args">Table of objects containing none or more objects to be formatted.</param>
        /// <returns></returns>
        public static string Format(this string format, params object[] args)
        {
            
            return string.Format(format, args);
        }

    }

    /// <summary>
    /// Enum for behaviour with a <see langword="null"/>, Empty and WhiteSpace
    /// </summary>
    public enum StringJoinOptions
    {
        /// <summary>
        /// Convert a <see langword="null"/> value to a Empty
        /// </summary>
        NullToEmpty,
        /// <summary>
        /// Skip a <see langword="null"/> value
        /// </summary>
        SkipNull,
        /// <summary>
        /// Skip a <see langword="null"/> and Empty value
        /// </summary>
        SkipNullAndEmpty,
        /// <summary>
        /// Skip a <see langword="null"/> and WhiteSpace value
        /// </summary>
        SkipNullAndWhiteSpace,
        /// <summary>
        /// Convert a <see langword="null"/> value to "Null"
        /// </summary>
        NullToNull,
    }
}
