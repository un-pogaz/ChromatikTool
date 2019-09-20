using System;
using System.Collections.Generic;
using System.Text;

namespace System.Xml
{
    public class XmlRacine : XmlElement
    {
        public XmlRacine(string localName) : this(localName, "")
        { }
        public XmlRacine(string localName, string namespaceURI) : base("", localName, namespaceURI, new XmlDocument())
        { }

        static public XmlRacine Create(string name)
        {
            return Create(name, "");
        }
        static public XmlRacine Create(string name, XmlNamespace ns)
        {
            return Create(name, ns.URI);
        }
        static public XmlRacine Create(string name, string namespaceURI)
        {
            return new XmlRacine(name, namespaceURI);
        }
    }
}
