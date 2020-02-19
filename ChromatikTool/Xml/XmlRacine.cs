using System;
using System.Collections.Generic;
using System.Text;

namespace System.Xml
{
    /// <summary></summary>
    public class XmlRacine : XmlElement
    {
        /// <summary>
        /// Create a basic <see cref="XmlElement"/>
        /// </summary>
        /// <param name="localName"></param>
        public XmlRacine(string localName) : this(localName, "")
        { }
        /// <summary>
        /// Create a basic <see cref="XmlElement"/> with the specified URI
        /// </summary>
        public XmlRacine(string localName, string namespaceURI) : base("", localName, namespaceURI, new XmlDocument())
        {
            OwnerDocument.AppendChild(this);
        }

        /// <summary>
        /// Create a basic <see cref="XmlElement"/>
        /// </summary>
        static public XmlRacine Create(string name)
        {
            return Create(name, "");
        }
        /// <summary>
        /// Create a basic <see cref="XmlElement"/> with the specified Namespace
        /// </summary>
        static public XmlRacine Create(string name, XmlNamespace ns)
        {
            return Create(name, ns.Uri);
        }
        /// <summary>
        /// Create a basic <see cref="XmlElement"/> with the specified URI
        /// </summary>
        static public XmlRacine Create(string name, string namespaceURI)
        {
            return new XmlRacine(name, namespaceURI);
        }
    }
}
