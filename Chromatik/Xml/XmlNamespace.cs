using System;
using System.Collections.Generic;
using System.Text;

namespace System.Xml
{
    /// <summary>
    /// Represents a Namespace XML
    /// </summary>
    public class XmlNamespace
    {
        /// <summary>
        /// Prefix associated of this namespace
        /// </summary>
        public string Prefix { get; }
        /// <summary>
        /// URI of this namespace
        /// </summary>
        public string URI { get; }
        /// <summary>
        /// xmlns attribut of this XML namespace
        /// </summary>
        public string xmlns { get; }

        /// <summary>
        /// Create a Namespace XML with a specified URI and prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="URI"></param>
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
