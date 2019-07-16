using System;

namespace System.IO
{

    /// <summary>
    /// Exception levée lorsque qu'un chemin n'est pas valide
    /// </summary>
    public class InvalidPathException : IOException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidPathException
        /// </summary>
        public InvalidPathException() : base() { }

        /// <summary>
        /// Message pour un characte
        /// </summary>
        static public InvalidPathException Chars { get { return new InvalidPathException("Le chemin contient des caractères non valide."); } }

        /// <summary>
        /// Message pour les noms
        /// </summary>
        static public InvalidPathException Name { get { return new InvalidPathException("Le chemin contient un nom de répertoire interdit."); } }

        /// <summary>
        /// Message pour les fin
        /// </summary>
        static public InvalidPathException End { get { return new InvalidPathException("Le chemin contient un nom de répertoire ce terminant par un Point '.' ou un Espace ' '."); } }

        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidPathException avec un message d'erreur spécifié
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception.</param>
        public InvalidPathException(string message) : base(message) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidPathException avec un message d'erreur spécifié
        /// et une référence à l'exception interne ayant provoqué cette exception
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception</param>
        /// <param name="innerException">Exception qui constitue la cause de l'exception actuelle.
        /// Si le paramètre innerException n'est pas une référence null,
        /// l'exception actuelle est levée dans un bloc catch qui gère l'exception interne</param>
        public InvalidPathException(string message, Exception innerException) : base(message, innerException) { }
    }

    /// <summary>
    /// Exception levée lorsque qu'un nom de fichier n'est pas valide
    /// </summary>
    public class InvalidFileNameException : IOException
    {
        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidFileNameException
        /// </summary>
        public InvalidFileNameException() : base() { }

        /// <summary>
        /// Message pour un characte
        /// </summary>
        static public InvalidFileNameException Chars { get { return new InvalidFileNameException("Le nom de fichier contient des caractères non valide."); } }
        
        /// <summary>
        /// Message pour les noms
        /// </summary>
        static public InvalidPathException Name { get { return new InvalidPathException("Le nom de fichier est un nom interdit."); } }

        /// <summary>
        /// Message pour les fin
        /// </summary>
        static public InvalidPathException End { get { return new InvalidPathException("Le nom de fichier ce termine par un Point '.' ou un Espace ' '."); } }

        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidFileNameException avec un message d'erreur spécifié
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception.</param>
        public InvalidFileNameException(string message) : base(message) { }

        /// <summary>
        /// Initialise une nouvelle instance de la classe InvalidFileNameException avec un message d'erreur spécifié
        /// et une référence à l'exception interne ayant provoqué cette exception
        /// </summary>
        /// <param name="message">Message d'erreur indiquant la raison de l'exception</param>
        /// <param name="innerException">Exception qui constitue la cause de l'exception actuelle.
        /// Si le paramètre innerException n'est pas une référence null,
        /// l'exception actuelle est levée dans un bloc catch qui gère l'exception interne</param>
        public InvalidFileNameException(string message, Exception innerException) : base(message, innerException) { }
    }
}
