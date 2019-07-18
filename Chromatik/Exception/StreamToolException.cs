using System;
using System.Runtime.Serialization;

namespace System.IO
{

    /// <summary>
    /// Exception levée lorsque qu'un chemin n'est pas valide
    /// </summary>
    public class StreamToolException : IOException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidPathException
        /// </summary>
        public StreamToolException() : base() { }
        
        /// <summary>
        /// 
        /// </summary>
        static public StreamToolException NoEmpty { get { return new StreamToolException("Le Stream de destination n'est pas vide"); } }

        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidPathException avec un message d'erreur spécifié
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception.</param>
        public StreamToolException(string message) : base(message) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidPathException avec un message d'erreur spécifié
        /// et une référence à l'exception interne ayant provoqué cette exception
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception</param>
        /// <param name="innerException">Exception qui constitue la cause de l'exception actuelle.
        /// Si le paramètre innerException n'est pas une référence null,
        /// l'exception actuelle est levée dans un bloc catch qui gère l'exception interne</param>
        public StreamToolException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe avec des données sérialisées
        /// </summary>
        /// <param name="info">Objet qui contient les données sérialisées de l'objet</param>
        /// <param name="context">Informations contextuelles sur la source ou la destination</param>
        protected StreamToolException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    
}
