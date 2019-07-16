using System;
using System.Runtime.Serialization;

namespace Chromatik.Xml
{
    /// <summary>
    /// Exception levée lorsqu'une erreur survient dans le namespace Chromatik.Xml
    /// </summary>
    public class ChromatikXMLtoolException : ChromatikException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe ChromatikXMLtoolException
        /// </summary>
        public ChromatikXMLtoolException() : base() { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe ChromatikXMLtoolException avec un message d'erreur spécifié
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception</param>
        public ChromatikXMLtoolException(string message) : base(message) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe ChromatikException avec un message d'erreur spécifié
        /// et une référence à l'exception interne ayant provoqué cette exception
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception</param>
        /// <param name="innerException">Exception qui constitue la cause de l'exception actuelle.
        /// Si le paramètre innerException n'est pas une référence null,
        /// l'exception actuelle est levée dans un bloc catch qui gère l'exception interne</param>
        public ChromatikXMLtoolException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe avec des données sérialisées
        /// </summary>
        /// <param name="info">Objet qui contient les données sérialisées de l'objet</param>
        /// <param name="context">Informations contextuelles sur la source ou la destination</param>
        protected ChromatikXMLtoolException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
