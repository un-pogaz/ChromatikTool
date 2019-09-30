using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// Static class pour convertir différent valeur vers d'autre format
    /// </summary>
    static public class ConvertExtend
    {
        #region Hexa convert

        /// <summary>
        /// Get the Hexa value of a <see cref="int"/>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        static public string HexaFromInt(int value)
        {
            return HexaFromInt(value, 1);
        }
        /// <summary>
        /// Get the Hexa value of a <see cref="int"/>
        /// </summary>
        /// <param name="i"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        static public string HexaFromInt(int i, short length)
        {
            if (length < 1)
                length = 1;
            return i.ToString("X" + length).ToUpper();
        }

        /// <summary>
        /// Get the <see cref="int"/> of a Hexa value
        /// </summary>
        /// <param name="hexa"></param>
        /// <returns></returns>   
        public static int IntFromHexa(string hexa)
        {
            int i = 0;
            try
            {
                i = int.Parse(hexa, System.Globalization.NumberStyles.HexNumber);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }
        #endregion

        #region Char convert

        /// <summary>
        /// Get the character of a Code Point
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        static public string CharFromInt(int i)
        {
            string unicodeString = string.Empty;
            try
            {
                unicodeString = char.ConvertFromUtf32(i);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unicodeString;
        }
        /// <summary>
        /// Get the Code Point of a character
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static public int IntFromChar(string c)
        {
            return char.ConvertToUtf32(c, 0);
        }
        /// <summary>
        /// Get the Code Point of a character
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static public int IntFromChar(char c)
        {
            return IntFromChar(c.ToString());
        }


        /// <summary>
        /// Get the character of a Hexa value
        /// </summary>
        /// <param name="hexa"></param>
        /// <returns></returns>
        static public string CharFromHexa(string hexa)
        {
            return CharFromInt(IntFromHexa(hexa));
        }
        /// <summary>
        /// Get the Hexa value of a character
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static public string HexaFromChar(string c)
        {
            return HexaFromInt(IntFromChar(c), 4);
        }
        /// <summary>
        /// Get the Hexa value of a character
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        static public string HexaFromChar(char c)
        {
            return HexaFromInt(IntFromChar(c.ToString()));
        }


        /// <summary>
        /// Get the Hexa values of the content of a <see cref="string"/> 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static public string[] HexaFromString(string s)
        {
            return HexaFromChar(s.ToCharArray());
        }
        /// <summary>
        /// Get the Hexa values  of a <see cref="char"/>[]
        /// </summary>
        /// <param name="tblChar"></param>
        /// <returns></returns>
        static public string[] HexaFromChar(char[] tblChar)
        {
            string[] rslt = new string[tblChar.Length];
            for (int i = 0; i < tblChar.Length; i++)
                rslt[i] = HexaFromChar(tblChar[i]);
            return rslt;
        }
        #endregion


        struct BitCount
        {
            /// <summary>
            /// 8bit
            /// </summary>
            static public readonly byte Byte = 8;
            /// <summary>
            /// 16bit
            /// </summary>
            static public readonly byte Short = 16;
            /// <summary>
            /// 32bit
            /// </summary>
            static public readonly byte Int = 32;
            /// <summary>
            /// 64bit
            /// </summary>
            static public readonly byte Long = 64;
        }
        
        #region bool byte Array

        /// <summary>
        /// Converion d'un <see cref="byte"/> dans une table de bits (<see cref="bool"/>)
        /// </summary>
        /// <param name="b"><see cref="byte"/> a convertir</param>
        /// <returns>Tableau de 8 <see cref="bool"></see></returns>
        public static bool[] ToBoolArray(byte b)
        {
            return ToBoolArray(b, BitCount.Byte); ;
        }
        /// <summary>
        /// Converion d'une table de <see cref="bool"/> dans une valeur <see cref="byte"/>
        /// </summary>
        /// <param name="inputBoolArray">byte a convertir</param>
        /// <returns></returns>
        public static byte ToByte(bool[] inputBoolArray)
        {
            return Convert.ToByte(ToValue(inputBoolArray, BitCount.Byte));
        }
        /// <summary>
        /// Converion d'une table de <see cref="byte"/> dans une table de bits (<see cref="bool"/>)
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        public static bool[] ToBoolArray(byte[] byteArray)
        {
            // IEnumerable pour eviter la conversion en table a chaque concaténation
            IEnumerable<bool> rslt = new bool[0];
            foreach (var item in byteArray)
                rslt = rslt.Concat(ToBoolArray(item));

            return rslt.ToArray();
        }
        /// <summary>
        /// Converion d'une table de <see cref="bool"/> dans une table de <see cref="byte"/>
        /// </summary>
        /// <param name="inputBoolArray"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(bool[] inputBoolArray)
        {
            TestBoolJaggerArray(inputBoolArray, BitCount.Byte);
            List<byte> rslt = new List<byte>(Convert.ToInt32(inputBoolArray.LongLength / BitCount.Byte));
            for (int i = 0; i < inputBoolArray.LongLength; i++)
                rslt.Add(ToByte(inputBoolArray.SubArray(i, BitCount.Byte)));

            return rslt.ToArray();
        }
        #endregion
        
        #region bool Array

        static private bool[] ToBoolArray(ulong value, byte lenght)
        {
            bool[] myBits = new bool[lenght];

            for (byte x = 0; x < myBits.Length; x++)
            {
                myBits[x] = (((value >> x) & 0x01) == 0x01);
            }
            return myBits;
        }
        static private bool[] ToBoolArraySigned(long value, byte lenght)
        {
            bool[] rslt = new bool[0];

            if (value < 0)
            {
                rslt = ToBoolArray((ulong)(value * -1), lenght);
                rslt[lenght - 1] = true;
            }
            else
                rslt = ToBoolArray((ulong)value, lenght);

            return rslt;
        }


        static private void TestBoolArray(bool[] inputBoolArray, byte lenght)
        {
            if (inputBoolArray == null)
                throw new ArgumentNullException(nameof(inputBoolArray));

            if (inputBoolArray.Length != lenght)
                throw new ArgumentException("The input table does not have the valid number of bits. The table must contain " + lenght + "bits.");
        }

        static private ulong ToValue(bool[] inputBoolArray, byte lenght)
        {
            TestBoolArray(inputBoolArray, lenght);

            ulong rslt = 0;
            for (byte i = 0; i < lenght; i++)
                if (inputBoolArray[i])
                    rslt = rslt + Convert.ToUInt64(Math.Pow(2, i));

            return rslt;
        }
        static private long ToValueSigned(bool[] inputBoolArray, byte lenght)
        {
            TestBoolArray(inputBoolArray, lenght);

            bool IsSigned = inputBoolArray[lenght - 1];
            inputBoolArray[lenght - 1] = false;
            long rslt = Convert.ToInt64(ToValue(inputBoolArray, lenght));
            if (IsSigned)
                rslt = rslt * -1;
            return rslt;
        }
        

        static private void TestBoolJaggerArray(bool[] inputBoolArray, byte lenght)
        {
            if (inputBoolArray == null)
                throw new ArgumentNullException(nameof(inputBoolArray));

            if (inputBoolArray.Length % lenght != 0)
                throw new ArgumentException("The input table does not have the valid number of bits. The table must contain a multiple to " + lenght + "bits.");
        }
        
        #endregion
        
        #region bool value Array

        /// <summary>
        /// Converion d'un <see cref="ushort"/> dans une table de bits (<see cref="bool"/>[])
        /// </summary>
        /// <param name="s"><see cref="ushort"/> a convertir</param>
        /// <returns>Tableau de 16 <see cref="bool"></see></returns>
        public static bool[] ToBoolArray(ushort s)
        {
            return ToBoolArray(s, BitCount.Short);
        }
        /// <summary>
        /// Converion d'un <see cref="uint"/> dans une table de bits (<see cref="bool"/>[])
        /// </summary>
        /// <param name="i"><see cref="uint"/> a convertir</param>
        /// <returns>Tableau de 16 <see cref="bool"></see></returns>
        public static bool[] ToBoolArray(uint i)
        {
            return ToBoolArray(i, BitCount.Int);
        }
        /// <summary>
        /// Converion d'un <see cref="ulong"/> dans une table de bits (<see cref="bool"/>[])
        /// </summary>
        /// <param name="l"><see cref="ulong"/> a convertir</param>
        /// <returns>Tableau de 16 <see cref="bool"></see></returns>
        public static bool[] ToBoolArray(ulong l)
        {
            return ToBoolArray(l, BitCount.Long);
        }


        /// <summary>
        /// Converion d'une table de <see cref="bool"/> dans une valeur <see cref="ushort"/>
        /// </summary>
        /// <param name="inputBoolArray">byte a convertir</param>
        /// <returns></returns>
        public static ushort ToUInt16(bool[] inputBoolArray)
        {
            return Convert.ToUInt16(ToValue(inputBoolArray, BitCount.Short));
        }
        /// <summary>
        /// Converion d'une table de <see cref="bool"/> dans une valeur <see cref="uint"/>
        /// </summary>
        /// <param name="inputBoolArray">byte a convertir</param>
        /// <returns></returns>
        public static uint ToUInt32(bool[] inputBoolArray)
        {
            return Convert.ToUInt32(ToValue(inputBoolArray, BitCount.Int));
        }
        /// <summary>
        /// Converion d'une table de <see cref="bool"/> dans une valeur <see cref="ulong"/>
        /// </summary>
        /// <param name="inputBoolArray">byte a convertir</param>
        /// <returns></returns>
        public static ulong ToUInt64(bool[] inputBoolArray)
        {
            return Convert.ToUInt64(ToValue(inputBoolArray, BitCount.Long));
        }

        /// <summary>
        /// Converion d'un <see cref="short"/> dans une table de bits (<see cref="bool"/>)
        /// </summary>
        /// <param name="s"><see cref="short"/> a convertir</param>
        /// <returns>Tableau de 16 <see cref="bool"/></returns>
        public static bool[] ToBoolArray(short s)
        {
            return ToBoolArraySigned(s, BitCount.Short);
        }
        /// <summary>
        /// Converion d'un <see cref="int"/> dans une table de bits (<see cref="bool"/>)
        /// </summary>
        /// <param name="i"><see cref="int"/> a convertir</param>
        /// <returns>Tableau de 16 <see cref="bool"/></returns>
        public static bool[] ToBoolArray(int i)
        {
            return ToBoolArraySigned(i, BitCount.Short);
        }
        /// <summary>
        /// Converion d'un <see cref="long"/> dans une table de bits (<see cref="bool"/>)
        /// </summary>
        /// <param name="l"><see cref="long"/> a convertir</param>
        /// <returns>Tableau de 16 <see cref="bool"/></returns>
        public static bool[] ToBoolArray(long l)
        {
            return ToBoolArraySigned(l, BitCount.Long);
        }

        /// <summary>
        /// Converion d'une table de <see cref="bool"/> dans une valeur <see cref="short"/>
        /// </summary>
        /// <param name="inputBoolArray">byte a convertir</param>
        /// <returns></returns>
        public static short ToInt16(bool[] inputBoolArray)
        {
            return Convert.ToInt16(ToValueSigned(inputBoolArray, BitCount.Short));
        }
        /// <summary>
        /// Converion d'une table de <see cref="bool"/> dans une valeur <see cref="int"/>
        /// </summary>
        /// <param name="inputBoolArray">byte a convertir</param>
        /// <returns></returns>
        public static int ToInt32(bool[] inputBoolArray)
        {
            return Convert.ToInt32(ToValueSigned(inputBoolArray, BitCount.Int));
        }
        /// <summary>
        /// Converion d'une table de <see cref="bool"/> dans une valeur <see cref="long"/>
        /// </summary>
        /// <param name="inputBoolArray">byte a convertir</param>
        /// <returns></returns>
        public static long ToInt64(bool[] inputBoolArray)
        {
            return Convert.ToInt64(ToValueSigned(inputBoolArray, BitCount.Long));
        }
        
        #endregion
    }
}
