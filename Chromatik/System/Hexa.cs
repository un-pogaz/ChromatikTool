using System;
using System.Globalization;
using System.Collections;
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
    public struct Hexa : IComparerEquatable<Hexa>, IFormattable, IConvertible, IEquatable<Hexa>, Collections.Generic.IEqualityComparer<Hexa>, Collections.IEqualityComparer, IComparable<Hexa>, IComparable, Collections.Generic.IComparer<Hexa>, Collections.IComparer
    {
        static Hexa _null { get; } = new Hexa(0);
        /// <summary></summary>
        static public IEqualityComparer<Hexa> EqualityComparer { get; } = _null;
        /// <summary></summary>
        static public IComparer<Hexa> Comparator { get; } = _null;
        /// <summary></summary>
        public override int GetHashCode() { return AbsolutValue.GetHashCode(); }


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
        static public Hexa MinValue { get { return new Hexa(ulong.MinValue); } }
        /// <summary>
        /// <see cref="byte"/> value = FF (255)
        /// </summary>
        static public Hexa ByteValue { get { return new Hexa(byte.MaxValue); } }
        /// <summary>
        /// <see cref="ushort"/> value = FFFF (65535)
        /// </summary>
        static public Hexa UShortValue { get { return new Hexa(ushort.MaxValue); } }
        /// <summary>
        /// <see cref="uint"/> value = FFFFFFFF (4294967295)
        /// </summary>
        static public Hexa UIntValue { get { return new Hexa(uint.MaxValue); } }
        /// <summary>
        /// Maximal value = FFFFFFFFFFFFFFFF (18446744073709551615)
        /// </summary>
        static public Hexa MaxValue { get { return new Hexa(ulong.MaxValue); } }

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


        /// <summary></summary>
        static NumberStyles defaultNumberStyles = (NumberStyles.HexNumber);
        /// <summary></summary>
        static IFormatProvider defaultFormatProvider = NumberFormatInfo.InvariantInfo;

        /// <summary></summary>
        public override string ToString()
        {
            return ToString(MinStringLenght);
        }
        /// <summary></summary>
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
        

        /// <summary></summary>
        static public Hexa Parse(string s)
        {
            return Parse(s, defaultNumberStyles);
        }
        /// <summary></summary>
        static public Hexa Parse(string s, IFormatProvider provider)
        {
            return Parse(s, defaultNumberStyles, provider);
        }
        /// <summary></summary>
        static public Hexa Parse(string s, NumberStyles style)
        {
            return Parse(s, defaultNumberStyles, defaultFormatProvider);
        }
        /// <summary></summary>
        static public Hexa Parse(string s, NumberStyles style, IFormatProvider provider)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));

            return new Hexa(ulong.Parse(FiltreString(s), style, provider));
        }

        static private string FiltreString(string s)
        {

            s = s.Trim(WhiteCharacter.WhiteCharacters);
            if (s.StartsWith("#"))
            {
                s = s.TrimStart('#');
                if (s.Length == 3)
                {
                    s = s[0].ToString() + s[0].ToString() +
                        s[1].ToString() + s[1].ToString() +
                        s[2].ToString() + s[2].ToString();
                }
            }
            else
                s = s.Regex("^((0|h|%|#)?x|&h|U+|%|$)", "", RegexHelper.RegexOptions | Text.RegularExpressions.RegexOptions.IgnoreCase);

            return s;
        }

        /// <summary></summary>
        static public bool TryParse(string s, out Hexa result)
        {
            return TryParse(s, defaultNumberStyles, out result);
        }
        /// <summary></summary>
        static public bool TryParse(string s, NumberStyles style, out Hexa result)
        {
            return TryParse(s, style, defaultFormatProvider, out result);
        }
        /// <summary></summary>
        static public bool TryParse(string s, NumberStyles style, IFormatProvider provider, out Hexa result)
        {
            ulong v = 0;
            bool rslt = ulong.TryParse(FiltreString(s), style, provider, out v);
            result = new Hexa(v);
            return rslt;
        }

        static private bool TryParseObject(object obj, out Hexa result)
        {
            result = MinValue;
            if (obj != null)
            {
                if (obj is Hexa)
                {
                    result = (Hexa)obj;
                    return true;
                }
                else if (obj is string)
                {
                    result = new Hexa((string)obj);
                    return true;
                }
                //else if (obj is byte || obj is short || obj is int || obj is long)
                //{
                //    result = new Hexa((long)obj);
                //    return true;
                //}
                //else if (obj is sbyte || obj is ushort || obj is uint || obj is ulong)
                //{
                //    result = new Hexa((ulong)obj);
                //    return true;
                //}
            }

            return false;
        }

        #region IComparer 

        /// <summary>
        /// Compare two instance of <see cref="Hexa"/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static public int Compare(Hexa x, Hexa y)
        {
            return x.AbsolutValue.CompareTo(y.AbsolutValue);
        }
        int IComparer<Hexa>.Compare(Hexa x, Hexa y)
        {
            return Compare(x, y);
        }
        /// <summary>
        /// Compare two instance of <see cref="object"/> if they are valides <see cref="Hexa"/>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        static public int Compare(object x, object y)
        {
            if (x == null && y != null)
                return 1;
            if (x != null && y == null)
                return -1;
            else if (x == null && y == null)
                return 0;
            else
            {
                Hexa hx;
                Hexa hy;
                if (TryParseObject(x, out hx) && TryParseObject(y, out hy))
                    return Compare(hx, hy);

                return 0;
            }
        }
        int IComparer.Compare(object x, object y) { return Compare(x, y); }

        /// <summary></summary>
        public int CompareTo(object obj) { return Compare(this, obj); }
        /// <summary></summary>
        public int CompareTo(string value) { return Compare(this, value); }
        /// <summary></summary>
        public int CompareTo(Hexa value) { return Compare(this, value); }

        #endregion

        #region IEquatable

        /// <summary></summary>
        static public bool Equals(Hexa x, Hexa y)  { return Compare(x, y) == 0; }
        bool IEqualityComparer<Hexa>.Equals(Hexa x, Hexa y) { return Equals(x, y); }

        /// <summary></summary>
        new static public bool Equals(object x, object y)
        {
            if (x == null && y != null)
                return false;
            if (x != null && y == null)
                return false;
            if (x == null && y == null)
                return true;
            else 
            {
                Hexa hx;
                Hexa hy;
                if (TryParseObject(x, out hx) && TryParseObject(y, out hy))
                    return Equals(hx, hy);

                return false;
            }
        }
        bool IEqualityComparer.Equals(object x, object y) { return Equals(x, y); }


        /// <summary></summary>
        public override bool Equals(object obj) { return Equals(this, obj); }
        /// <summary></summary>
        public bool Equals(string obj) { return Equals(this, obj); }
        /// <summary></summary>
        public bool Equals(Hexa hexa) { return Equals(this, hexa); }

        int IEqualityComparer<Hexa>.GetHashCode(Hexa obj) { return obj.GetHashCode(); }
        int IEqualityComparer.GetHashCode(object obj) { return obj.GetHashCode(); }
        
        #endregion


        #region Operator

        #region cast

        /// <summary></summary>
        static public implicit operator byte(Hexa value)
        {
            return (byte)value.AbsolutValue;
        }
        /// <summary></summary>
        static public implicit operator ushort(Hexa value)
        {
            return (ushort)value.AbsolutValue;
        }
        /// <summary></summary>
        static public implicit operator uint(Hexa value)
        {
            return (uint)value.AbsolutValue;
        }
        /// <summary></summary>
        static public implicit operator ulong(Hexa value)
        {
            return (ulong)value.AbsolutValue;
        }

        /// <summary></summary>
        static public explicit operator short(Hexa value)
        {
            return (short)value.AbsolutValue;
        }
        /// <summary></summary>
        static public explicit operator int(Hexa value)
        {
            return (int)value.AbsolutValue;
        }
        /// <summary></summary>
        static public explicit operator long(Hexa value)
        {
            return (long)value.AbsolutValue;
        }

        /// <summary></summary>
        static public explicit operator float(Hexa value)
        {
            return value.AbsolutValue;
        }
        /// <summary></summary>
        static public explicit operator double(Hexa value)
        {
            return value.AbsolutValue;
        }
        /// <summary></summary>
        static public explicit operator decimal(Hexa value)
        {
            return value.AbsolutValue;
        }

        #endregion

        #region compare Hexa & string

        #region =
        /// <summary></summary>
        static public bool operator ==(Hexa left, Hexa right)
        {
            return left.Equals(right);
        }
        /// <summary></summary>
        static public bool operator !=(Hexa left, Hexa right)
        {
            return !(left == right);
        }

        /// <summary></summary>
        static public bool operator ==(Hexa left, string right)
        {
            return left.Equals(right);
        }
        /// <summary></summary>
        static public bool operator !=(Hexa left, string right)
        {
            return !(left == right);
        }
        /// <summary></summary>
        static public bool operator ==(string left, Hexa right)
        {
            return (right == left);
        }
        /// <summary></summary>
        static public bool operator !=(string left, Hexa right)
        {
            return (right != left);
        }
        #endregion

        #region <

        /// <summary></summary>
        static public bool operator <(Hexa left, Hexa right)
        {
            return (left.AbsolutValue < right.AbsolutValue);
        }
        /// <summary></summary>
        static public bool operator >(Hexa left, Hexa right)
        {
            return (right < left);
        }


        /// <summary></summary>
        static public bool operator <(Hexa left, string right)
        {
            Hexa hexa = MinValue;
            if (TryParse(right, out hexa))
                return (left < hexa);
            else
                return false;
        }
        /// <summary></summary>
        static public bool operator >(Hexa left, string right)
        {
            Hexa hexa = MinValue;
            if (TryParse(right, out hexa))
                return (left > hexa);
            else
                return false;
        }

        /// <summary></summary>
        static public bool operator <(string left, Hexa right)
        {
            return (right > left);
        }
        /// <summary></summary>
        static public bool operator >(string left, Hexa right)
        {
            return (right < left);
        }
        #endregion

        #region <= 

        /// <summary></summary>
        static public bool operator <=(Hexa left, Hexa right)
        {
            return ((left == right) || (left < right));
        }
        /// <summary></summary>
        static public bool operator >=(Hexa left, Hexa right)
        {
            return ((left == right) || (left > right));
        }


        /// <summary></summary>
        static public bool operator <=(Hexa left, string right)
        {
            return ((left == right) || (left < right));
        }
        /// <summary></summary>
        static public bool operator >=(Hexa left, string right)
        {
            return ((left == right) || (left > right));
        }

        /// <summary></summary>
        static public bool operator <=(string left, Hexa right)
        {
            return ((left == right) || (left < right));
        }
        /// <summary></summary>
        static public bool operator >=(string left, Hexa right)
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

        /// <summary></summary>
        static public Hexa operator ++(Hexa value)
        {
            return value + 1;
        }
        /// <summary></summary>
        static public Hexa operator +(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue + right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary></summary>
        static public Hexa operator +(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue + right, left.MinStringLenght);
        }
        /// <summary></summary>
        static public Hexa operator +(Hexa left, long right)
        {
            if (right >= 0)
                return (left + (ulong)right);
            else
                return (left - (ulong)-right);
        }


        /// <summary></summary>
        static public Hexa operator --(Hexa value)
        {
            return value - 1;
        }
        /// <summary></summary>
        static public Hexa operator -(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue - right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary></summary>
        static public Hexa operator -(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue - right, left.MinStringLenght);
        }
        /// <summary></summary>
        static public Hexa operator -(Hexa left, long right)
        {
            return (left + (-right));
        }

        #endregion

        #region operation mul/div/modulo

        /// <summary></summary>
        static public Hexa operator *(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue * right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary></summary>
        static public Hexa operator *(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue * right, left.MinStringLenght);
        }
        /// <summary></summary>
        static public Hexa operator *(Hexa left, long right)
        {
            return (left * (ulong)right);
        }

        /// <summary></summary>
        static public Hexa operator %(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue % right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary></summary>
        static public Hexa operator %(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue % right, left.MinStringLenght);
        }
        /// <summary></summary>
        static public Hexa operator %(Hexa left, long right)
        {
            return (left % (ulong)right);
        }

        /// <summary></summary>
        static public Hexa operator /(Hexa left, Hexa right)
        {
            return new Hexa(left.AbsolutValue / right.AbsolutValue, BigStringLenght(left, right));
        }
        /// <summary></summary>
        static public Hexa operator /(Hexa left, ulong right)
        {
            return new Hexa(left.AbsolutValue / right, left.MinStringLenght);
        }
        /// <summary></summary>
        static public Hexa operator /(Hexa left, long right)
        {
            return (left / (ulong)right);
        }
        #endregion

        #endregion

        #region IConvertible

        /// <summary></summary>
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(AbsolutValue);
        }

        /// <summary></summary>
        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(AbsolutValue);
        }

        /// <summary></summary>
        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(AbsolutValue);
        }

        /// <summary></summary>
        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(AbsolutValue);
        }

        /// <summary></summary>
        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(AbsolutValue);
        }

        /// <summary></summary>
        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(AbsolutValue);
        }

        /// <summary></summary>
        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(AbsolutValue);
        }

        /// <summary></summary>
        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(AbsolutValue);
        }

        /// <summary></summary>
        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(AbsolutValue);
        }

        /// <summary></summary>
        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return AbsolutValue;
        }

        /// <summary></summary>
        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(AbsolutValue);
        }

        /// <summary></summary>
        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(AbsolutValue);
        }

        /// <summary></summary>
        Decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(AbsolutValue);
        }

        /// <summary></summary>
        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(AbsolutValue);
        }

        /// <summary></summary>
        Object IConvertible.ToType(Type type, IFormatProvider provider)
        {
            return Convert.ChangeType(this, type, provider);
        }

        /// <summary></summary>
        public TypeCode GetTypeCode()
        {
           return AbsolutValue.GetTypeCode();
        }
        #endregion
    }
}
