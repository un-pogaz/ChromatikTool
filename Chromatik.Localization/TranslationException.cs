using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace System.Globalization
{
    /// <summary></summary>
    public class TranslationException : SystemException
    {
        /// <summary></summary>
        public TranslationException() : base()
        { }
        /// <summary></summary>
        public TranslationException(string message) : base(message)
        { }
        /// <summary></summary>
        public TranslationException(Exception innerException) : base(innerException.Message, innerException)
        { }
        /// <summary></summary>
        public TranslationException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
