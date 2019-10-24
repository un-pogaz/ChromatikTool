﻿using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// Hexadecimal variable 
    /// </summary>
    [Serializable]
    [System.Runtime.InteropServices.StructLayout(Runtime.InteropServices.LayoutKind.Sequential)]
    [System.Runtime.InteropServices.ComVisible(true)]
    public struct Hexa : IComparable, IFormattable, IConvertible, IComparable<Hexa>, IEquatable<Hexa>
    {
        /// <summary>
        /// The <see cref="ulong"/> absolute value of variable
        /// </summary>
        public ulong AbsolutValue { get; set; }
        /// <summary>
        /// The <see cref="string"/> format value of variable
        /// </summary>
        public string StringFormatValue
        {
            get { return ToString(); }
            set
            {
                try
                {
                    AbsolutValue = Parse(value).AbsolutValue;
                }
                catch
                {
                }
            }
        }
        /// <summary>
        /// The minimal lenght of the <see cref="string"/> format for this variable
        /// </summary>
        public int MinStringLenght
        {
            get { return _MinStringLenght; }
            set
            {
                if (value < 0)
                    value = 0;
                _MinStringLenght = value;
            }
        }
        private int _MinStringLenght;

        /// <summary>
        /// Minimal value = 0
        /// </summary>
        static public Hexa MinValue { get { return New(ulong.MinValue); } }
        /// <summary>
        /// <see cref="byte"/> value = FF (255)
        /// </summary>
        static public Hexa ByteValue { get { return New(byte.MaxValue); } }
        /// <summary>
        /// <see cref="ushort"/> value = FFFF (65535)
        /// </summary>
        static public Hexa UShortValue { get { return New(ushort.MaxValue); } }
        /// <summary>
        /// <see cref="uint"/> value = FFFFFFFF (4294967295)
        /// </summary>
        static public Hexa UIntValue { get { return New(uint.MaxValue); } }
        /// <summary>
        /// Maximal value = FFFFFFFFFFFFFFFF (18446744073709551615)
        /// </summary>
        static public Hexa MaxValue { get { return New(ulong.MaxValue); } }

        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        /// <param name="s"></param>
        public Hexa(string s) : this(s, 0)
        { }
        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        public Hexa(string s, int minStringLenght) : this(Parse(s).AbsolutValue, minStringLenght)
        { }


        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        public Hexa(long value) : this(value, 0)
        { }
        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        public Hexa(long value, int minStringLenght) : this((ulong)value, minStringLenght)
        { }

        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        public Hexa(ulong value) : this(value, 0)
        { }
        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        public Hexa(ulong value, int minStringLenght)
        {
            AbsolutValue = value;
            _MinStringLenght = 0;

            MinStringLenght = minStringLenght;
        }

        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        static public Hexa New(string value)
        {
            return new Hexa(value);
        }
        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        static public Hexa New(string value, int minStringLenght)
        {
            return new Hexa(value, minStringLenght);
        }
        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        static public Hexa New(ulong value)
        {
            return new Hexa(value);
        }
        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        static public Hexa New(ulong value, int minStringLenght)
        {
            return new Hexa(value, minStringLenght);
        }
        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        static public Hexa New(long value)
        {
            return new Hexa(value);
        }
        /// <summary>
        /// Create new hexadecimal variable
        /// </summary>
        static public Hexa New(long value, int minStringLenght)
        {
            return new Hexa(value, minStringLenght);
        }

        /// <summary> </summary>
        static NumberStyles defaultNumberStyles = (NumberStyles.HexNumber);
        /// <summary> </summary>
        static IFormatProvider defaultFormatProvider = NumberFormatInfo.InvariantInfo;

        /// <summary> </summary>
        public override string ToString()
        {
            return ToString(MinStringLenght);
        }
        /// <summary> </summary>
        public string ToString(IFormatProvider provider)
        {
            return ToString(MinStringLenght, provider);
        }
        /// <summary>
        /// Get the <see cref="string"/> format of this <see cref="Hexa"/> with a specified minimal lenght
        /// </summary>
        /// <param name="minStringLenght">Minimal lenght of the generate <see cref="string"/></param>
        /// <returns></returns>
        public string ToString(int minStringLenght)
        {
            return ToString(minStringLenght, defaultFormatProvider);
        }
        /// <summary>
        /// Get the <see cref="string"/> format of this <see cref="Hexa"/> with a specified minimal lenght
        /// </summary>
        /// <param name="minStringLenght">Minimal lenght of the generate <see cref="string"/></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(int minStringLenght, IFormatProvider provider)
        {
            if (minStringLenght < 1)
                minStringLenght = 1;

            return ToString("X" + minStringLenght, provider);
        }
        /// <summary>
        ///See <see cref="ulong.ToString(string)"/> for valide formats
        /// </summary>
        /// <param name="format">See <see cref="ulong.ToString(string)"/> for valide formats</param>
        /// <returns></returns>
        public string ToString(string format)
        {
            return AbsolutValue.ToString(format, defaultFormatProvider);
        }
        /// <summary>
        ///See <see cref="ulong.ToString(string)"/> for valide formats
        /// </summary>
        /// <param name="format">See <see cref="ulong.ToString(string)"/> for valide formats</param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public string ToString(string format, IFormatProvider provider)
        {
            return AbsolutValue.ToString(format, provider);
        }

        /// <summary> </summary>
        public static Hexa Parse(string s)
        {
            return Parse(s, defaultNumberStyles);
        }
        /// <summary> </summary>
        public static Hexa Parse(string s, IFormatProvider provider)
        {
            return Parse(s, defaultNumberStyles, provider);
        }
        /// <summary> </summary>
        public static Hexa Parse(string s, NumberStyles style)
        {
            return Parse(s, defaultNumberStyles, defaultFormatProvider);
        }
        /// <summary> </summary>
        public static Hexa Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            return new Hexa(ulong.Parse(s, style, provider));
        }

        /// <summary> </summary>
        public static bool TryParse(string s, out Hexa result)
        {
            return TryParse(s, defaultNumberStyles, out result);
        }
        /// <summary> </summary>
        public static bool TryParse(string s, NumberStyles style, out Hexa result)
        {
            return TryParse(s, style, defaultFormatProvider, out result);
        }
        /// <summary> </summary>
        public static bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Hexa result)
        {
            ulong v = 0;
            bool rslt = ulong.TryParse(s, style, provider, out v);
            result = new Hexa(v);
            return rslt;
        }

        /// <summary> </summary>
        public int CompareTo(object value)
        {
            if (value == null)
                return 1;
            if (value is Hexa)
                return CompareTo((Hexa)value);
            if (value is string)
                return CompareTo((string)value);
            if (value is ulong)
                return CompareTo((ulong)value);

            else
                return AbsolutValue.CompareTo(value);
        }
        /// <summary> </summary>
        public int CompareTo(string value)
        {
            if (value == null)
                return 1;
            return CompareTo(new Hexa(value));
        }
        /// <summary> </summary>
        public int CompareTo(Hexa value)
        {
            return CompareTo(value.AbsolutValue);
        }
        /// <summary> </summary>
        private int CompareTo(ulong value)
        {
            return AbsolutValue.CompareTo(value);
        }

        /// <summary> </summary>
        public override bool Equals(object obj)
        {
            if (obj is Hexa)
                return Equals((Hexa)obj);
            else if (obj is string)
                return Equals((string)obj);
            else if (obj is ulong)
                return Equals((ulong)obj);
            else
                return false;
        }
        /// <summary> </summary>
        public bool Equals(string obj)
        {
            Hexa hexa = MinValue;
            if (TryParse(obj, out hexa))
                return Equals(hexa);
            else
                return false;
        }
        /// <summary> </summary>
        public bool Equals(Hexa obj)
        {
            return Equals(obj.AbsolutValue);
        }
        /// <summary> </summary>
        private bool Equals(ulong obj)
        {
            return (AbsolutValue == obj);
        }

        /// <summary> </summary>
        public override int GetHashCode()
        {
            return AbsolutValue.GetHashCode();
        }

        /// <summary> </summary>
        public TypeCode GetTypeCode()
        {
            return AbsolutValue.GetTypeCode();
        }

        #region IConvertible internal

        /// <summary> </summary>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(AbsolutValue);
        }

        /// <summary> </summary>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(AbsolutValue);
        }

        /// <summary> </summary>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(AbsolutValue);
        }

        /// <summary> </summary>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(AbsolutValue);
        }

        /// <summary> </summary>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(AbsolutValue);
        }

        /// <summary> </summary>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(AbsolutValue);
        }

        /// <summary> </summary>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(AbsolutValue);
        }

        /// <summary> </summary>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(AbsolutValue);
        }

        /// <summary> </summary>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(AbsolutValue);
        }

        /// <summary> </summary>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return AbsolutValue;
        }

        /// <summary> </summary>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(AbsolutValue);
        }

        /// <summary> </summary>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(AbsolutValue);
        }

        /// <summary> </summary>
        Decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(AbsolutValue);
        }

        /// <summary> </summary>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(AbsolutValue);
        }

        /// <summary> </summary>
        Object IConvertible.ToType(Type type, IFormatProvider provider)
        {
            return Convert.ChangeType(this, type, provider);
        }
        #endregion


        #region Operator

        #region cast

        /// <summary> </summary>
        public static implicit operator byte(Hexa value)
        {
            return (byte)value.AbsolutValue;
        }
        /// <summary> </summary>
        public static implicit operator ushort(Hexa value)
        {
            return (ushort)value.AbsolutValue;
        }
        /// <summary> </summary>
        public static implicit operator uint(Hexa value)
        {
            return (uint)value.AbsolutValue;
        }
        /// <summary> </summary>
        public static implicit operator ulong(Hexa value)
        {
            return (ulong)value.AbsolutValue;
        }

        /// <summary> </summary>
        public static explicit operator short(Hexa value)
        {
            return (short)value.AbsolutValue;
        }
        /// <summary> </summary>
        public static explicit operator int(Hexa value)
        {
            return (int)value.AbsolutValue;
        }
        /// <summary> </summary>
        public static explicit operator long(Hexa value)
        {
            return (long)value.AbsolutValue;
        }

        /// <summary> </summary>
        public static explicit operator float(Hexa value)
        {
            return value.AbsolutValue;
        }
        /// <summary> </summary>
        public static explicit operator double(Hexa value)
        {
            return value.AbsolutValue;
        }
        /// <summary> </summary>
        public static explicit operator decimal(Hexa value)
        {
            return value.AbsolutValue;
        }

        #endregion

        #region compare Hexa & string

        #region =
        /// <summary> </summary>
        public static bool operator ==(Hexa left, Hexa right)
        {
            return left.Equals(right);
        }
        /// <summary> </summary>
        public static bool operator !=(Hexa left, Hexa right)
        {
            return !(left == right);
        }
        
        /// <summary> </summary>
        public static bool operator ==(Hexa left, string right)
        {
            return left.Equals(right);
        }
        /// <summary> </summary>
        public static bool operator !=(Hexa left, string right)
        {
            return !(left == right);
        }
        /// <summary> </summary>
        public static bool operator ==(string left, Hexa right)
        {
            return (right == left);
        }
        /// <summary> </summary>
        public static bool operator !=(string left, Hexa right)
        {
            return (right != left);
        }
        #endregion

        #region <

        /// <summary> </summary>
        public static bool operator <(Hexa left, Hexa right)
        {
            return (left.AbsolutValue < right.AbsolutValue);
        }
        /// <summary> </summary>
        public static bool operator >(Hexa left, Hexa right)
        {
            return (right < left);
        }
        

        /// <summary> </summary>
        public static bool operator <(Hexa left, string right)
        {
            Hexa hexa = MinValue;
            if (TryParse(right, out hexa))
                return (left < hexa);
            else
                return false;
        }
        /// <summary> </summary>
        public static bool operator >(Hexa left, string right)
        {
            Hexa hexa = MinValue;
            if (TryParse(right, out hexa))
                return (left > hexa);
            else
                return false;
        }

        /// <summary> </summary>
        public static bool operator <(string left, Hexa right)
        {
            return (right > left);
        }
        /// <summary> </summary>
        public static bool operator >(string left, Hexa right)
        {
            return (right < left);
        }
        #endregion

        #region <= 

        /// <summary> </summary>
        public static bool operator <=(Hexa left, Hexa right)
        {
            return ((left == right) || (left < right));
        }
        /// <summary> </summary>
        public static bool operator >=(Hexa left, Hexa right)
        {
            return ((left == right) || (left > right));
        }


        /// <summary> </summary>
        public static bool operator <=(Hexa left, string right)
        {
            return ((left == right) || (left < right));
        }
        /// <summary> </summary>
        public static bool operator >=(Hexa left, string right)
        {
            return ((left == right) || (left > right));
        }

        /// <summary> </summary>
        public static bool operator <=(string left, Hexa right)
        {
            return ((left == right) || (left < right));
        }
        /// <summary> </summary>
        public static bool operator >=(string left, Hexa right)
        {
            return ((left == right) || (left > right));
        }
        #endregion

        #endregion

        static private int BigStringLenght(Hexa left, Hexa right)
        {
            if (left.MinStringLenght > right.MinStringLenght)
                return left.MinStringLenght;
            else
                return right.MinStringLenght;

        }

        #region operation plus/moins

        /// <summary> </summary>
        public static Hexa operator ++(Hexa value)
        {
            return value + 1;
        }
        /// <summary> </summary>
        public static Hexa operator +(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue + right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary> </summary>
        public static Hexa operator +(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue + right, left.MinStringLenght);
        }
        /// <summary> </summary>
        public static Hexa operator +(Hexa left, long right)
        {
            if (right >= 0)
                return (left + (ulong)right);
            else
                return (left - (ulong)-right);
        }
        

        /// <summary> </summary>
        public static Hexa operator --(Hexa value)
        {
            return value - 1;
        }
        /// <summary> </summary>
        public static Hexa operator -(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue - right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary> </summary>
        public static Hexa operator -(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue - right, left.MinStringLenght);
        }
        /// <summary> </summary>
        public static Hexa operator -(Hexa left, long right)
        {
            return (left + (-right));
        }

        #endregion

        #region operation mul/div/modulo

        /// <summary> </summary>
        public static Hexa operator *(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue * right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary> </summary>
        public static Hexa operator *(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue * right, left.MinStringLenght);
        }
        /// <summary> </summary>
        public static Hexa operator *(Hexa left, long right)
        {
            return (left * (ulong)right);
        }

        /// <summary> </summary>
        public static Hexa operator %(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue % right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary> </summary>
        public static Hexa operator %(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue % right, left.MinStringLenght);
        }
        /// <summary> </summary>
        public static Hexa operator %(Hexa left, long right)
        {
            return (left % (ulong)right);
        }

        /// <summary> </summary>
        public static Hexa operator /(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue / right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary> </summary>
        public static Hexa operator /(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue / right, left.MinStringLenght);
        }
        /// <summary> </summary>
        public static Hexa operator /(Hexa left, long right)
        {
            return (left / (ulong)right);
        }
        #endregion

        #endregion
    }
}
