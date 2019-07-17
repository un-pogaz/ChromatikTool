using System;

namespace System.IO
{

    /// <summary>
    /// Exception for invalide path
    /// </summary>
    public class InvalidPathException : IOException
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidPathException"/>
        /// </summary>
        public InvalidPathException() : base() { }

        /// <summary>
        /// Message for characters
        /// </summary>
        static public InvalidPathException Chars { get { return new InvalidPathException("The path contains invalid characters."); } }

        /// <summary>
        /// Message for
        /// </summary>
        static public InvalidPathException Name { get { return new InvalidPathException("The path contains a forbidden directory name."); } }

        /// <summary>
        /// Message for end
        /// </summary>
        static public InvalidPathException End { get { return new InvalidPathException("The path contains a directory or a file name ending or start with a Point '.' or a Space ' '."); } }

        /// <summary>
        /// Initializes a new instance of <see cref="InvalidPathException"/>
        /// </summary>
        /// <param name="message"></param>
        public InvalidPathException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of <see cref="InvalidPathException"/>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InvalidPathException(string message, Exception innerException) : base(message, innerException) { }
    }
    
}
