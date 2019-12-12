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
        public string Uri { get; }
        /// <summary>
        /// xmlns attribut of this XML namespace
        /// </summary>
        public string xmlns { get; }

        /// <summary>
        /// Create a Namespace XML with a specified URI and prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="uri"></param>
        public XmlNamespace(string prefix, string uri)
        {
            prefix = prefix.Trim(WhiteCharacter.WhiteCharacters);
            if (string.IsNullOrWhiteSpace(prefix))
                throw new ArgumentNullException("prefix");

            Prefix = prefix;
            if (this.Prefix.Contains(":") || this.Prefix.Contains(" ") || this.Prefix.Contains("\n") || this.Prefix.Contains("\r"))
                throw new ArgumentException("prefix");
            
            Uri = uri;
            
            xmlns = "xmlns:" + this.Prefix;
        }


        /// <summary>
        /// Standard namespace for XML
        /// </summary>
        static public XmlNamespace XML { get; } = new XmlNamespace("xml", @"http://www.w3.org/XML/1998/namespace");
        /// <summary>
        /// Standard namespace for XHTML
        /// </summary>
        static public XmlNamespace XHTML { get; } = new XmlNamespace("xhtml", @"http://www.w3.org/1999/xhtml");

        /// <summary>
        /// Standard namespace for Dublin Core
        /// </summary>
        static public XmlNamespace DublinCore { get; } = new XmlNamespace("dc", @"http://purl.org/dc/elements/1.1/");
        /// <summary>
        /// Standard namespace for Dublin Core Termes
        /// </summary>
        static public XmlNamespace DublinCoreTermes { get; } = new XmlNamespace("dcterms", @"http://purl.org/dc/terms/");
        /// <summary>
        /// Standard namespace for MARC code
        /// </summary>
        static public XmlNamespace MARCcode { get; } = new XmlNamespace("marc", @"http://id.loc.gov/vocabulary/relators");

        /// <summary>
        /// Standard namespace for ePub OPF file
        /// </summary>
        static public XmlNamespace OPF { get; } = new XmlNamespace("opf", "http://www.idpf.org/2007/opf");
        /// <summary>
        /// Standard namespace for ePub rendition vocabulary
        /// </summary>
        static public XmlNamespace Rendition { get; } = new XmlNamespace("rendition", @"http://www.idpf.org/vocab/rendition/#");

        /// <summary>
        /// Standard namespace for Calibre software
        /// </summary>
        static public XmlNamespace Calibre { get; } = new XmlNamespace("calibre", @"https://sw.kovidgoyal.net/calibre/");

        /// <summary>
        /// Standard namespace for IDpub software
        /// </summary>
        static public XmlNamespace IDpub { get; } = new XmlNamespace("IDpub", @"http://software.chromatik.com/IDpub/");
    }
}
