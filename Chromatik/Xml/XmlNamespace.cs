using System;
using System.Collections.Generic;
using System.Text;

namespace System.Xml
{
    /// <summary>
    /// Représente un Namespace XML
    /// </summary>
    public class XmlNamespace
    {
        public string Prefix { get; }
        public string URI { get; }
        public string xmlns { get; }
        
        public XmlNamespace(string prefix, string URI)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException("prefix");

            this.Prefix = prefix.Trim();
            if (this.Prefix.Contains(":") || this.Prefix.Contains(" ") || this.Prefix.Contains("\n") || this.Prefix.Contains("\r"))
                throw new ArgumentException("prefix");

            if (URI == null)
                throw new ArgumentNullException("URI");

            this.URI = URI.Trim();
            if (this.URI.Contains("\n") || this.URI.Contains("\r"))
                throw new ArgumentException("URI");
            
            xmlns = "xmlns:" + this.Prefix;
        }
    }
}
