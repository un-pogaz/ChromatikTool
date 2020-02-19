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
        /// <param name="start">First Code Point in hexadecimal format</param>
        /// <param name="last">Last Code Point (included)</param>
        /// <returns></returns>
        static public Hexa[] Range(string start, string last)
        {
            Hexa hexStart = Hexa.MinValue;
            Hexa hexLast = Hexa.MinValue;
            Hexa.TryParse(start, out hexStart);
            Hexa.TryParse(last, out hexLast);
            return Range(hexStart, hexLast);
        }
        /// <summary>
        /// Get the range between two Code Point 
        /// </summary>
        /// <param name="start">First Code Point in hexadecimal format</param>
        /// <param name="last">Last Code Point (included)</param>
        /// <returns></returns>
        static public Hexa[] Range(Hexa start, Hexa last)
        {
            List<Hexa> lst = new List<Hexa>();
            
            if (start < last)
            {
                try
                {
                    Hexa hexStart = start;
                    Hexa hexLast = last;

                    for (Hexa i = hexStart; i <= hexLast; i++)
                        lst.Add(i);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            else
            {

            }

            return lst.ToArray();
        }

        /// <summary>
        /// Remove the duplicate entries and short the array
        /// </summary>
        /// <param name="CodePoints"></param>
        /// <returns></returns>
        static public Hexa[] RangeFilter(Hexa[] CodePoints)
        {
            List<Hexa> rslt = new List<Hexa>();
            foreach (Hexa item in CodePoints)
                if (item != null)
                    rslt.Add(item);

            rslt = rslt.Distinct().ToList();
            rslt.Sort();

            return rslt.ToArray();
        }

        /// <summary>
        /// Get the range from a string format 
        /// </summary>
        /// <param name="CodeRange"></param>
        /// <returns></returns>
        static public Hexa[] RangeFromString(string CodeRange)
        {
            List<Hexa> lst = new List<Hexa>();
            CodeRange = CleanCoderange(CodeRange);

            foreach (string item in CodeRange.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (item.Contains("-"))
                {
                    string[] RangeSplit = item.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);


                    foreach (Hexa subitem in Range(RangeSplit[0], RangeSplit[1]))
                        lst.Add(new Hexa(subitem));
                }
                else
                    lst.Add(new Hexa(item));

            }

            return lst.ToArray();
        }

        /// <summary>
        /// Get the Code Point from all character in a String
        /// </summary>
        /// <param name="Search">Code Point in hexadecimal format</param>
        /// <returns></returns>
        static public Hexa[] HexaFromString(string Search)
        {
            List<Hexa> lst = new List<Hexa>();
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

                lst.Add(HexaFromChar(cara));
            }
            return lst.ToArray();
        }

        /// <summary>
        /// Get the  string format from range array CodePoints
        /// </summary>
        /// <param name="CodePoints">Array of code points</param>
        /// <returns></returns>
        static public string StringFromRange(Hexa[] CodePoints)
        {
            List<string> lst = new List<string>();

            if (CodePoints.Length == 1)
                lst.Add(CodePoints[0].ToString());
            else if (CodePoints.Length > 1)
            {                
                if (CodePoints[CodePoints.Length - 1] - CodePoints[0] == CodePoints.Length - 1)
                {
                    lst.Add(CodePoints[0] + "-" + CodePoints[CodePoints.Length - 1]);
                }
                else
                {
                    Hexa start = CodePoints[0];
                    Hexa last = CodePoints[0];

                    for (int i = 1; i < CodePoints.Length; i++)
                    {
                        Hexa next = CodePoints[i];
                        Hexa I_start = start;
                        Hexa I_last = last;
                        Hexa I_next = next;

                        if (I_start == I_last && (I_last + 1 < I_next || I_start > I_next))
                        {
                            lst.Add(start.ToString());
                            start = next;
                            last = next;

                            if (i == CodePoints.Length - 1)
                                lst.Add(start.ToString());
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
                    if (IntFromHexa(split[0]) + 1 == IntFromHexa(split[1]))
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
        /// <param name="codeRange"></param>
        /// <returns></returns>
        static public string CleanCoderange(string codeRange)
        {
            codeRange = codeRange.Regex("U(\\+|\\-)", "") ;

            codeRange = codeRange.Trim(WhiteCharacter.WhiteCharacters.Concat(',', '-'));

            codeRange = codeRange.RegexLoop("-\\s+|\\s+-|--", "-").RegexLoop(",\\s+|\\s+,|,,", ",");

            codeRange = codeRange.RegexLoop("-,|,-", ",");

            return codeRange;
        }
        

        /// <summary>
        /// Get the <see cref="int"/> value of the Code Point
        /// </summary>
        /// <param name="hexa">Code Point in hexadecimal format</param>
        /// <returns></returns>
        static public int IntFromHexa(string hexa)
        {
            return ConvertExtend.IntFromHexa(hexa);
        }
        /// <summary>
        /// Get the Code Point of the number
        /// </summary>
        /// <param name="code">Code Point in integer number</param>
        /// <returns></returns>
        static public Hexa HexaFromInt(int code)
        {
            return new Hexa(code);
        }
        

        /// <summary>
        /// Get the Code Point from a character
        /// </summary>
        /// <param name="c">Character to convert</param>
        /// <returns></returns>
        static public Hexa HexaFromChar(string c)
        {
            return new Hexa(ConvertExtend.HexaFromChar(c));
        }
        /// <summary>
        /// Get the character of the Code Point
        /// </summary>
        /// <param name="hexa">Code Point in hexadecimal format</param>
        /// <returns></returns>
        static public string CharFromHexa(string hexa)
        {
            return ConvertExtend.CharFromHexa(hexa);
        }


        /// <summary>
        /// Get the character of a <see cref="int"/> value
        /// </summary>
        /// <param name="code">Code Point to convert</param>
        /// <returns></returns>
        static public string CharFromInt(int code)
        {
            return ConvertExtend.CharFromInt(code);
        }
        /// <summary>
        /// Get the Code Point from a character
        /// </summary>
        /// <param name="c">Character to convert</param>
        /// <returns></returns>
        static public int IntFromChar(string c)
        {
            return ConvertExtend.IntFromChar(c);
        }
        
        
    }
}
