using System;
using System.Linq;
using System.IO;
using System.Text;

namespace System.Xml
{
    /// <summary>
    /// Load quickly a <see cref="XmlDocument"/>
    /// </summary>
    static public partial class XmlDocumentCreate
    {
        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a file
        /// </summary>
        static public XmlDocument Document(string path)
        {
            return DocumentXML(File.ReadAllText(path));
        }
        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a stream
        /// </summary>
        static public XmlDocument Document(Stream stream)
        {
            long position = stream.Position;
            stream.Position = 0;
            
            XmlDocument rslt = DocumentXML(new StreamReader(stream).ReadToEnd());

            stream.Position = position;
            return rslt;
        }
        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a string
        /// </summary>
        static public XmlDocument DocumentXML(string xml)
        {
            XmlDocument rslt = new XmlDocument()
            {
                PreserveWhitespace = false
            };
            
            rslt.LoadXml(xml.RemoveDOCTYPE());
            rslt.RemoveDeclaration();
            if (xml.RegexIsMatch("<!DOCTYPE\\s+html[^>]*>", RegexHelper.RegexOptions| Text.RegularExpressions.RegexOptions.IgnoreCase))
                rslt.SetDocumentType("html");
            return rslt;
        }

        private static string RemoveDOCTYPE(this string input)
        {
            return input.Regex("<!DOCTYPE[^>]*>", "", RegexHelper.RegexOptions | Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a HTML file
        /// </summary>
        static public XmlDocument ParseHTML(string path)
        {
            return ParseHTMLtext(File.ReadAllText(path));
        }
        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a HTML stream
        /// </summary>
        static public XmlDocument ParseHTML(Stream stream)
        {
            long position = stream.Position;
            stream.Position = 0;

            XmlDocument rslt = ParseHTMLtext(new StreamReader(stream).ReadToEnd());

            stream.Position = position;
            return rslt;
        }

        /// <summary>
        /// Load quickly a <see cref="XmlDocument"/> from a HTML string
        /// </summary>
        static public XmlDocument ParseHTMLtext(string html)
        {
            XmlDocument rslt = new XmlDocument()
            {
                PreserveWhitespace = false,
                XmlResolver = null
            };

            using (Sgml.SgmlReader sgmlReader = CreateSgmlReader(html))
            {
                rslt.Load(sgmlReader);
            }

            rslt.RemoveDeclaration();
            rslt.SetDocumentType("html");

            return rslt;
        }

        static private Sgml.SgmlReader CreateSgmlReader(string sgml)
        {
            foreach (var item in XmlHtmlEntity.HtmlBase)
                sgml = item.ParseXMLtoHTML(sgml);

            sgml = XmlHtmlEntity.ParseToCHAR(sgml, XmlHtmlEntity.Html2.Concat(XmlHtmlEntity.Html3, XmlHtmlEntity.Html4));
            StreamReader reader = new StreamReader(new StreamString(sgml.RemoveDOCTYPE()));
            Sgml.SgmlReader rslt = new Sgml.SgmlReader()
            {
                DocType = "HTML",
                WhitespaceHandling = WhitespaceHandling.All,
                CaseFolding = Sgml.CaseFolding.ToLower,
                InputStream = reader,
            };
            reader.BaseStream.Position = 0;
            return rslt;
        }
        
    }
}
