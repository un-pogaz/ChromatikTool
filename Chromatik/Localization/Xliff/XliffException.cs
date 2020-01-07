using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization.Localization
{
    /// <summary></summary>
    public class XliffException : SystemException
    {
        /// <summary></summary>
        public XliffException() : base()
        { }
        /// <summary></summary>
        public XliffException(string message) : base(message)
        { }
        /// <summary></summary>
        public XliffException(Exception innerException) : base(innerException.Message, innerException)
        { }
        /// <summary></summary>
        public XliffException(string message, Exception innerException) : base(message, innerException)
        { }

        /// <summary></summary>
        static public XliffException InvalideNoNodeFound(string nodeName)
        {
            return new XliffException("Invalid Xliff file. No '" + nodeName + "' node was found.");
        }

        /// <summary></summary>
        static public XliffException InvalideNoNodeFound(string nodeName, string attributeName)
        {
            return new XliffException("Invalid Xliff file. No '" + nodeName + "' node with an attribute '"+ attributeName + "' was found.");
        }

        /// <summary></summary>
        static public XliffException InvalideNoAttributeFound(string attributeName, string nodeName)
        {
            return new XliffException("The '" + attributeName + "' attribute as not defined in the '" + nodeName + "' node.");
        }
    }
}
