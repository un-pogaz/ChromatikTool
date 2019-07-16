using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Chromatik.Unicode
{
    public class UnicodeExceptions : Exception
    {
        public UnicodeExceptions() : base()
        { }
        public UnicodeExceptions(string message) : base(message)
        { }
        public UnicodeExceptions(string message, Exception innerException) : base(message, innerException)
        { }
        public UnicodeExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    public class InvalidCodePointException : UnicodeExceptions
    {
        static private string Mess0 { get { return "A invalid HexaDecimal CodePoint a was found"; } }

        public InvalidCodePointException() : base(Mess0)
        { }

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

    public class InvalidXmlBlockException : UnicodeExceptions
    {
        public InvalidXmlBlockException() : base() { }

        static public InvalidXmlBlockException DefautMessage { get { return new InvalidXmlBlockException("A invalid CodeBlock XmlElement is used."); } }

        public InvalidXmlBlockException(string message) : base(message) { }

        public InvalidXmlBlockException(string message, Exception innerException) : base(message, innerException) { }

        public InvalidXmlBlockException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class InvalidXmlPlaneException : UnicodeExceptions
    {
        public InvalidXmlPlaneException() : base() { }

        static public InvalidXmlPlaneException DefautMessage { get { return new InvalidXmlPlaneException("A invalid Plane XmlElement is used."); } }

        public InvalidXmlPlaneException(string message) : base(message) { }

        public InvalidXmlPlaneException(string message, Exception innerException) : base(message, innerException) { }

        public InvalidXmlPlaneException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class InvalidXmlUnicodeException : UnicodeExceptions
    {
        public InvalidXmlUnicodeException() : base() { }

        static public InvalidXmlUnicodeException DefautMessage { get { return new InvalidXmlUnicodeException("A invalid Unicode XmlElement is used."); } }

        public InvalidXmlUnicodeException(string message) : base(message) { }

        public InvalidXmlUnicodeException(string message, Exception innerException) : base(message, innerException) { }

        public InvalidXmlUnicodeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class InvalidXmlConScriptException : UnicodeExceptions
    {
        public InvalidXmlConScriptException() : base() { }

        static public InvalidXmlConScriptException DefautMessage { get { return new InvalidXmlConScriptException("A invalid ConScript XmlElement is used."); } }

        public InvalidXmlConScriptException(string message) : base(message) { }

        public InvalidXmlConScriptException(string message, Exception innerException) : base(message, innerException) { }

        public InvalidXmlConScriptException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class InvalidXmlCharsetException : UnicodeExceptions
    {
        public InvalidXmlCharsetException() : base() { }

        static public InvalidXmlCharsetException DefautMessage { get { return new InvalidXmlCharsetException("A invalid Charset XmlElement is used."); } }

        public InvalidXmlCharsetException(string message) : base(message) { }

        public InvalidXmlCharsetException(string message, Exception innerException) : base(message, innerException) { }

        public InvalidXmlCharsetException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    public class InvalidCodeRangeException : UnicodeExceptions
    {
        static private string Mess0 { get { return "A invalid HexaDecimal CodePoint a was found in the range"; } }

        public InvalidCodeRangeException() : base(Mess0)
        { }

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
    
    public class XmlUnicodeException : UnicodeExceptions
    {

        public XmlUnicodeException() : base() { }

        public XmlUnicodeException(string message) : base(message) { }
        static public XmlUnicodeException NoLoaded { get { return new XmlUnicodeException("No Xml file has loaded"); } }
        static public XmlUnicodeException NotFound { get { return new XmlUnicodeException("The Block was not found was not found"); } }

        public XmlUnicodeException(string message, Exception innerException) : base(message, innerException) { }
        
        protected XmlUnicodeException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

}
