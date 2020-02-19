using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Chromatik.Unicode
{
    /// <summary></summary>
    public class UnicodeExceptions : Exception
    {
        /// <summary></summary>
        public UnicodeExceptions() : base()
        { }
        /// <summary></summary>
        public UnicodeExceptions(string message) : base(message)
        { }
        /// <summary></summary>
        public UnicodeExceptions(string message, Exception innerException) : base(message, innerException)
        { }
    }

    /// <summary></summary>
    public class InvalidCodePointException : UnicodeExceptions
    {
        static private string Mess0 { get { return "A invalid HexaDecimal CodePoint a was found"; } }

        /// <summary></summary>
        public InvalidCodePointException() : base(Mess0)
        { }

        /// <summary></summary>
        public InvalidCodePointException(string hex) : base(TestNull(hex))
        { }

        static private string TestNull(string hex)
        {
            if (String.IsNullOrWhiteSpace(hex))
                return Mess0;
            else
                return String.Format("\"{0}\" is a invalid HexaDecimal CodePoint", hex);
        }
    }

    /// <summary></summary>
    public class InvalidXmlBlockException : UnicodeExceptions
    {
        /// <summary></summary>
        public InvalidXmlBlockException() : base() { }

        /// <summary></summary>
        static public InvalidXmlBlockException DefautMessage { get { return new InvalidXmlBlockException("A invalid CodeBlock XmlElement is used."); } }

        /// <summary></summary>
        public InvalidXmlBlockException(string message) : base(message) { }

        /// <summary></summary>
        public InvalidXmlBlockException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary></summary>
    public class InvalidXmlPlaneException : UnicodeExceptions
    {
        /// <summary></summary>
        public InvalidXmlPlaneException() : base() { }

        /// <summary></summary>
        static public InvalidXmlPlaneException DefautMessage { get { return new InvalidXmlPlaneException("A invalid Plane XmlElement is used."); } }

        /// <summary></summary>
        public InvalidXmlPlaneException(string message) : base(message) { }

        /// <summary></summary>
        public InvalidXmlPlaneException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary></summary>
    public class InvalidXmlUnicodeException : UnicodeExceptions
    {
        /// <summary></summary>
        public InvalidXmlUnicodeException() : base() { }

        /// <summary></summary>
        static public InvalidXmlUnicodeException DefautMessage { get { return new InvalidXmlUnicodeException("A invalid Unicode XmlElement is used."); } }

        /// <summary></summary>
        public InvalidXmlUnicodeException(string message) : base(message) { }

        /// <summary></summary>
        public InvalidXmlUnicodeException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary></summary>
    public class InvalidXmlConScriptException : UnicodeExceptions
    {
        /// <summary></summary>
        public InvalidXmlConScriptException() : base() { }

        /// <summary></summary>
        static public InvalidXmlConScriptException DefautMessage { get { return new InvalidXmlConScriptException("A invalid ConScript XmlElement is used."); } }

        /// <summary></summary>
        public InvalidXmlConScriptException(string message) : base(message) { }

        /// <summary></summary>
        public InvalidXmlConScriptException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary></summary>
    public class InvalidXmlCharsetException : UnicodeExceptions
    {
        /// <summary></summary>
        public InvalidXmlCharsetException() : base() { }

        /// <summary></summary>
        static public InvalidXmlCharsetException DefautMessage { get { return new InvalidXmlCharsetException("A invalid Charset XmlElement is used."); } }

        /// <summary></summary>
        public InvalidXmlCharsetException(string message) : base(message) { }

        /// <summary></summary>
        public InvalidXmlCharsetException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary></summary>
    public class InvalidCodeRangeException : UnicodeExceptions
    {
        /// <summary></summary>
        static private string Mess0 { get { return "A invalid HexaDecimal CodePoint a was found in the range"; } }

        /// <summary></summary>
        public InvalidCodeRangeException() : base(Mess0)
        { }

        /// <summary></summary>
        public InvalidCodeRangeException(string hexStart, string hexLast) : base(TestNull(hexStart, hexLast))
        { }

        static private string TestNull(string hexStart, string hexLast)
        {
            if (String.IsNullOrWhiteSpace(hexStart) && String.IsNullOrWhiteSpace(hexLast))
                return Mess0;
            else
                return String.Format("\"{0}\" to \"{1}\" is a invalid HexaDecimal CodeRange", hexStart, hexLast);
        }

    }

    /// <summary></summary>
    public class XmlUnicodeException : UnicodeExceptions
    {
        /// <summary></summary>
        public XmlUnicodeException() : base() { }

        /// <summary></summary>
        public XmlUnicodeException(string message) : base(message) { }
        /// <summary></summary>
        static public XmlUnicodeException NoLoaded { get { return new XmlUnicodeException("No Xml file has loaded"); } }
        /// <summary></summary>
        static public XmlUnicodeException NotFound { get { return new XmlUnicodeException("The Block was not found was not found"); } }

        /// <summary></summary>
        public XmlUnicodeException(string message, Exception innerException) : base(message, innerException) { }
    }

}
