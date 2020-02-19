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
        static public XliffException NotSupported { get; } = new XliffException("This action is not supported.");

        /// <summary></summary>
        static public XliffException NotSupported_ReadOnlyCollection { get; } = new XliffException("This action is not supported in a Read Only Collection.");

        /// <summary></summary>
        static public XliffException NoAttributFound(string nodeName, string attributeName)
        {
            return new XliffException("The '" + attributeName + "' attribute as not defined in the '" + nodeName + "' node.");
        }
        /// <summary></summary>
        static public XliffException InvalideAttributID(string noID)
        {
            return new XliffException("'" + noID + "' is not a valid ID.");
        }


        /// <summary></summary>
        static public XliffException InvalideNodeName(string nodeName)
        {
            return new XliffException("The node name isn't '"+ nodeName + "' not the expected name..");
        }
    }
}
