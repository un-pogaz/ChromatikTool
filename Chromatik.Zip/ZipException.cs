using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Chromatik.Zip
{
    /// <summary>
    /// Exception levée lorsqu'un probléme survie avec le fichier OPF de l'ePub
    /// </summary>
    public class ZipException : Exception
    {
        /// <summary>
        /// Default Exception
        /// </summary>
        public ZipException() : base() { }
        /// <summary>
        /// Come on, you know how exceptions work. Why are you looking at this documentation?
        /// </summary>
        /// <param name="message">The message in the exception.</param>
        public ZipException(string message) : base(message) { }
        /// <summary>
        /// Come on, you know how exceptions work. Why are you looking at this documentation?
        /// </summary>
        /// <param name="message">he message in the exception.</param>
        /// <param name="innerException">The innerException for this exception.</param>
        public ZipException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Come on, you know how exceptions work. Why are you looking at this documentation?
        /// </summary>
        /// <param name="info">The serialization info for the exception.</param>
        /// <param name="context">The streaming context from which to deserialize.</param>
        protected ZipException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        static internal ZipException FromIonic(Ionic.Zlib.ZlibException ex) { return new ZipException(ex.Message, ex.InnerException); }

        static internal ZipException FromIonic(Ionic.Zip.ZipException ex) { return new ZipException(ex.Message, ex.InnerException); }
    }
}