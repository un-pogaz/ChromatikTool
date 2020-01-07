using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class Xliff : Collections.ObjectModel.ReadOnlyDictionary<string, XliffFile>
    {
        static public XmlNamespace NamespaceXliff { get; } = new XmlNamespace("xliff", "urn:oasis:names:tc:xliff:document:2.0");

        static public Xliff LoadXliff(string path)
        {
            try
            {
                return new Xliff(XmlDocumentCreate.Document(path));
            }
            catch (Exception ex)
            {
                throw new XliffException(ex);
            }
        }
        static public Xliff LoadXliffXML(string xml)
        {
            try
            {
                return new Xliff(XmlDocumentCreate.DocumentXML(xml));
            }
            catch (Exception ex)
            {
                throw new XliffException(ex);
            }
        }
        
        public CultureInfo SourceLang { get; set; }
        public CultureInfo TargetLang { get; set; }

        protected bool xmlSpacePreserve = false;

        private Xliff() : base(new Dictionary<string, XliffFile>())
        { }

        protected Xliff(XmlDocument document) : this()
        {
            XmlElement xliff = document.FirstElement("xliff");

            if (xliff == null)
                throw new XliffException("Invalid Xliff file. The root node isn't 'xliff'.");
            if (!xliff.HasAttribute("version"))
                throw XliffException.InvalideNoAttributeFound("version", "xliff");
            if (xliff.GetAttribute("version") != "2.0")
                throw new XliffException("Unsuported version of Xliff File.\nThe 2.0 version is the only supported.");

            if (!xliff.HasAttribute("srcLang"))
                throw XliffException.InvalideNoAttributeFound("srcLang", "xliff");
            if (!xliff.HasAttribute("trgLang"))
                throw XliffException.InvalideNoAttributeFound("trgLang", "xliff");

            SourceLang = CultureInfo.GetCultureInfo(xliff.GetAttribute("srcLang"));
            TargetLang = CultureInfo.GetCultureInfo(xliff.GetAttribute("trgLang"));

            xmlSpacePreserve = xliff.XmlSpacePreserve();

            XmlElement[] xmlFile = xliff.GetElements("file", "id");
            if (xmlFile.Length == 0)
                throw XliffException.InvalideNoNodeFound("file", "id");

            foreach (XmlElement item in xmlFile)
                Add(new XliffFile(item));
        }

        protected void Add(XliffFile file)
        {
            Dictionary.Add(file.ID, file);
        }
    }
}
