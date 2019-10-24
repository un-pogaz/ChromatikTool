using System;
using System.Runtime.Serialization;

namespace Chromatik
{
    /// <summary>
    /// Exception levée lorsqu'une erreur survient dans le namespace ChromatikException
    /// </summary>
    public class ChromatikException : Exception
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe ChromatikException
        /// </summary>
        public ChromatikException() : base() { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe ChromatikException avec un message d'erreur spécifié
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception</param>
        public ChromatikException(string message) : base(message) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe ChromatikException avec un message d'erreur spécifié
        /// et une référence à l'exception interne ayant provoqué cette exception
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception</param>
        /// <param name="innerException">Exception qui constitue la cause de l'exception actuelle.
        /// Si le paramètre innerException n'est pas une référence null,
        /// l'exception actuelle est levée dans un bloc catch qui gère l'exception interne</param>
        public ChromatikException(string message, Exception innerException) : base(message, innerException) { }
        
    }
}
