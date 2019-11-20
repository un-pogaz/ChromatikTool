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



        static public XmlNamespace IDpub { get; } = new XmlNamespace("IDpub", @"http://software.chromatik.com/IDpub/");
        
        static public XmlNamespace OPF { get; } = new XmlNamespace("opf", "http://www.idpf.org/2007/opf");

        static public XmlNamespace DublinCore { get; } = new XmlNamespace("dc", @"http://purl.org/dc/elements/1.1/");
        
        static public XmlNamespace DublinCoreTermes { get; } = new XmlNamespace("dcterms", @"http://purl.org/dc/terms/");
        
        static public XmlNamespace MARCcode { get; } = new XmlNamespace("marc", @"http://id.loc.gov/vocabulary/relators");
        
        static public XmlNamespace Rendition { get; } = new XmlNamespace("rendition", @"http://www.idpf.org/vocab/rendition/#");
        
        static public XmlNamespace Calibre { get; } = new XmlNamespace("calibre", @"https://sw.kovidgoyal.net/calibre/");
    }
}
