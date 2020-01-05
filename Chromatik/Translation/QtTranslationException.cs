using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace System.Globalization
{
    /// <summary></summary>
    public class QtTranslationException : SystemException
    {
        /// <summary></summary>
        public QtTranslationException() : base()
        { }
        /// <summary></summary>
        public QtTranslationException(string message) : base(message)
        { }
        /// <summary></summary>
        public QtTranslationException(Exception innerException) : base(innerException.Message, innerException)
        { }
        /// <summary></summary>
        public QtTranslationException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary></summary>
        static public QtTranslationException InvalideNoNodeFound(string nodeName)
        {
            return new QtTranslationException("Invalid Translation file. No '"+ nodeName + "' node was found.");
        }

        /// <summary></summary>
        static public QtTranslationException InvalideNoAttributeFound(string attributeName, string nodeName)
        {
            return new QtTranslationException("The '" + attributeName + "' attribute as not defined in the '" + nodeName + "' node.");
        }
    }
}
