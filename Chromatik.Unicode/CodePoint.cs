using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Globalization;

namespace Chromatik.Unicode
{
    /// <summary>
    /// Contains the Methodes for manipulet Hexadecimal CodePoint, integer and characters of Unicode 
    /// </summary>
    public class CodePoint
    {
        /// <summary>
        /// Get the range between two Code Point 
        /// </summary>
        /// <param name="Start">First Code Point in hexadecimal format</param>
        /// <param name="Last">Last Code Point (included)</param>
        /// <returns></returns>
        static public string[] Range(string Start, string Last)
        {
            List<string> lst = new List<string>();

            try
            {
                int hexStart = IntFromHex(Start);
                int hexLast = IntFromHex(Last);

                for (int i = hexStart; i <= hexLast; i++)
                    lst.Add(HexFromInt(i));
            }
            catch (Exception e)
            {
                throw e;
            }

            return lst.ToArray();
        }

        /// <summary>
        /// Remove the duplicate entries and short the array
        /// </summary>
        /// <param name="CodePoints"></param>
        /// <returns></returns>
        static public string[] RangeFilter(string[] CodePoints)
        {
            List<int> srt = new List<int>();
            foreach (string item in CodePoints)
                srt.Add(IntFromHex(item));
            srt.Sort();

            List<string> lst = new List<string>();
            foreach (int item in srt.Distinct())
                lst.Add(HexFromInt(item));

            return lst.ToArray();
        }

        /// <summary>
        /// Get the range from a string format 
        /// </summary>
        /// <param name="CodeRange"></param>
        /// <returns></returns>
        static public string[] RangeFromString(string CodeRange)
        {
            List<string> lst = new List<string>();
            CodeRange = CleanCoderange(CodeRange);

            foreach (string item in CodeRange.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (item.Contains("-"))
                {
                    string[] RangeSplit = item.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string subitem in Range(RangeSplit[0], RangeSplit[1]))
                        lst.Add(subitem);
                }
                else
                    lst.Add(item);

            }

            return lst.ToArray();
        }

        /// <summary>
        /// Get the Code Point from all character in a String
        /// </summary>
        /// <param name="Search">Code Point in hexadecimal format</param>
        /// <returns></returns>
        static public string[] HexFromString(string Search)
        {
            List<string> lst = new List<string>();
            for (int i = 0; i < Search.Length; i++)
            {
                string cara = "";
                if (char.IsSurrogate(Search[i]))
                {
                    cara = Search.Substring(i, 2);
                    i++;
                }
                else
                    cara = Search[i].ToString();

                lst.Add(HexFromChar(cara));
            }
            return lst.ToArray();
        }

        /// <summary>
        /// Get the  string format from range array CodePoints
        /// </summary>
        /// <param name="CodePoints">Array of code points</param>
        /// <returns></returns>
        static public string StringFromRange(string[] CodePoints)
        {
            List<string> lst = new List<string>();

            if (CodePoints.Length == 1)
                lst.Add(CodePoints[0]);
            else if (CodePoints.Length > 1)
            {                
                if (IntFromHex(CodePoints[CodePoints.Length - 1]) - IntFromHex(CodePoints[0]) == CodePoints.Length - 1)
                {
                    lst.Add(CodePoints[0] + "-" + CodePoints[CodePoints.Length - 1]);
                }
                else
                {
                    string start = CodePoints[0];
                    string last = CodePoints[0];

                    for (int i = 1; i < CodePoints.Length; i++)
                    {
                        string next = CodePoints[i];
                        int I_start = IntFromHex(start);
                        int I_last = IntFromHex(last);
                        int I_next = IntFromHex(next);

                        if (I_start == I_last && (I_last + 1 < I_next || I_start > I_next))
                        {
                            lst.Add(start);
                            start = next;
                            last = next;

                            if (i == CodePoints.Length - 1)
                                lst.Add(start);
                        }
                        else
                        {
                            if (I_last + 1 < I_next || I_start > I_next)
                            {
                                lst.Add(start + "-" + last);
                                start = next;
                                last = next;
                            }
                            else
                            {
                                last = next;

                                if (i == CodePoints.Length - 1)
                                    lst.Add(start + "-" + last);
                            }
                        }
                    }
                }

            }

            string result = "";
            foreach (string item in lst)
            {
                if (item.Contains("-"))
                {
                    string[] split = item.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                    if (IntFromHex(split[0]) + 1 == IntFromHex(split[1]))
                        result += "," + split[0] +"," + split[1];
                    else
                        result += "," + item;
                }
                else
                    result += "," + item;
            }

            return CleanCoderange(result);
        }

        /// <summary>
        /// Remove common error
        /// </summary>
        /// <param name="CodeRange"></param>
        /// <returns></returns>
        static public string CleanCoderange(string CodeRange)
        {
            CodeRange = CodeRange.Replace("U+", "").Replace("U-", "");

            while (CodeRange.StartsWith(" ") || CodeRange.EndsWith(" ") ||
                   CodeRange.StartsWith(",") || CodeRange.EndsWith(",") ||
                   CodeRange.StartsWith("-") || CodeRange.EndsWith("-"))
            {
                CodeRange = CodeRange.Trim().Trim(',').Trim('-');
            }

            while (CodeRange.Contains(", ") || CodeRange.Contains(" ,") ||
                   CodeRange.Contains("- ") || CodeRange.Contains(" -"))
            {
                CodeRange = CodeRange.Replace(", ", ",");
                CodeRange = CodeRange.Replace(" ,", ",");
                CodeRange = CodeRange.Replace("- ", "-");
                CodeRange = CodeRange.Replace(" -", "-");
            }

            while (CodeRange.Contains(",,") || CodeRange.Contains("--") ||
                   CodeRange.Contains("-,") || CodeRange.Contains(",-"))
            {
                CodeRange = CodeRange.Replace(",,", ",");
                CodeRange = CodeRange.Replace("--", "-");

                CodeRange = CodeRange.Replace("-,", ",");
                CodeRange = CodeRange.Replace(",-", ",");
            }

            return CodeRange;
        }


        #region FromHex

        /// <summary>
        /// Get the character of the Code Point
        /// </summary>
        /// <param name="hex">Code Point in hexadecimal format</param>
        /// <returns></returns>
        static public string CharFromHex(string hex)
        {
            return CharFromInt(IntFromHex(hex));
        }

        /// <summary>
        /// Get the character of the Code Point
        /// </summary>
        /// <param name="hex">Code Point in hexadecimal format</param>
        /// <returns></returns>
        static public string[] CharFromHex(string[] hex)
        {
            string[] lst = new string[hex.Length];
            for (int i = 0; i < hex.Length; i++)
                lst[i] = CharFromHex(hex[i]);
            return lst;
        }

        /// <summary>
        /// Get the Integer of the Code Point
        /// </summary>
        /// <param name="hex">Code Point in hexadecimal format</param>
        /// <returns></returns>
        static public int IntFromHex(string hex)
        {
            int code;
            try
            {
                code = int.Parse(hex.Trim(), System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception e)
            {
                throw e;
            }
            return code;
        }

        /// <summary>
        /// Get the Integer of the Code Point
        /// </summary>
        /// <param name="hex">Code Point in hexadecimal format</param>
        /// <returns></returns>
        static public int[] IntFromHex(string[] hex)
        {
            int[] lst = new int[hex.Length];
            for (int i = 0; i < hex.Length; i++)
                lst[i] = IntFromHex(hex[i]);
            return lst;
        }



        #endregion

        #region FromInt

        /// <summary>
        /// Get the character of the number
        /// </summary>
        /// <param name="code">Code Point in integer number</param>
        /// <returns></returns>
        static public string CharFromInt(int code)
        {
            string unicodeString = string.Empty;
            try
            {
                unicodeString = char.ConvertFromUtf32(code);
            }
            catch (Exception e)
            {
                throw e;
            }
            return unicodeString;
        }

        /// <summary>
        /// Get the Code Point of the number
        /// </summary>
        /// <param name="code">Code Point in integer number</param>
        /// <returns></returns>
        static public string HexFromInt(int code)
        {
            string s = code.ToString("X");

            if (s.Length < 2)
                s = "0" + s;
            if (s.Length < 3)
                s = "0" + s;
            if (s.Length < 4)
                s = "0" + s;

            return s;
        }

        #endregion

        #region FromChar

        /// <summary>
        /// Get the Code Point from a character
        /// </summary>
        /// <param name="Char">Character to convert</param>
        /// <returns></returns>
        static public string HexFromChar(string Char)
        {
            return HexFromInt(IntFromChar(Char));
        }

        /// <summary>
        /// Get the Code Point from a character
        /// </summary>
        /// <param name="Char">Array of character to convert</param>
        /// <returns></returns>
        static public string[] HexFromChar(string[] tblChar)
        {
            string[] tbl = new string[tblChar.Length];
            for (int i = 0; i < tblChar.Length; i++)
                tbl[i] = HexFromChar(tblChar[i]);
            return tbl;
        }

        /// <summary>
        /// Get the Code Point from a character
        /// </summary>
        /// <param name="Char">Character to convert</param>
        /// <returns></returns>
        static public int IntFromChar(string Char)
        {
            return char.ConvertToUtf32(Char, 0);
        }

        #endregion
        
    }
}
