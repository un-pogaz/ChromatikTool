using System;
using System.Runtime.Serialization;

namespace Chromatik.Xml
{
    /// <summary>
    /// Exception levée lorsqu'une erreur survient dans la classe XMLparentColection
    /// </summary>
    public class InvalidParentException : ChromatikXMLtoolException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidParentException
        /// </summary>
        public InvalidParentException() : base() { }

        /// <summary>
        /// Permet de levé le message par si le Parent de l'Element ne correspond pas a celui de la collection
        /// </summary>
        static public InvalidParentException DefautParent { get { return new InvalidParentException("Le Parent ne correspond pas a celui de la collection"); } }

        /// <summary>
        /// Permet de levé le message par si le Prefix du Parent de l'Element ne correspond pas a celui de la collection
        /// </summary>
        static public InvalidParentException DefautPrefix { get { return new InvalidParentException("Le Prefix ne correspond pas a celui du Parent de la collection"); } }

        /// <summary>
        /// Permet de levé le message par si le Nom du Parent de l'Element ne correspond pas a celui de la collection
        /// </summary>
        static public InvalidParentException DefautName { get { return new InvalidParentException("Le Nom ne correspond pas a celui du Parent de la collection"); } }


        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidParentPrefixException avec un message d'erreur spécifié
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception</param>
        public InvalidParentException(string message) : base(message) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidParentPrefixException avec un message d'erreur spécifié
        /// et une référence à l'exception interne ayant provoqué cette exception
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception</param>
        /// <param name="innerException">Exception qui constitue la cause de l'exception actuelle.
        /// Si le paramètre innerException n'est pas une référence null,
        /// l'exception actuelle est levée dans un bloc catch qui gère l'exception interne</param>
        public InvalidParentException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe avec des données sérialisées
        /// </summary>
        /// <param name="info">Objet qui contient les données sérialisées de l'objet</param>
        /// <param name="context">Informations contextuelles sur la source ou la destination</param>
        protected InvalidParentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
