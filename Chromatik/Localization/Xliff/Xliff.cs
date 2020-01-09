using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class Xliff : XliffIdentifiedListe<XliffFile>
    {
        public const string NodeName = "xliff";

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
        
        public CultureInfo SourceLang
        {
            set
            {
                if (value == null)
                    throw new XliffException("The '"+ nameof(SourceLang) + "' can't be null.");
                _sourceLang = value;
            }
            get { return _sourceLang; }
        }
        CultureInfo _sourceLang;
        public CultureInfo TargetLang
        {
            set
            {
                if (value == null)
                    throw new XliffException("The '" + nameof(TargetLang) + "' can't be null.");
                _targetLang = value;
            }
            get { return _targetLang; }
        }
        CultureInfo _targetLang;

        private Xliff() : base()
        { }

        protected Xliff(XmlDocument document) : base()
        {
            XmlElement xliff = document.FirstElement(NodeName);

            if (xliff == null)
                throw new XliffException("Invalid Xliff file. The root node isn't '"+NodeName+"'.");
            
            if (xliff.TestAttribut("version") != "2.0")
                throw new XliffException("Unsuported version of Xliff File.\nThe 2.0 version is the only supported.");
            
            SourceLang = CultureInfo.GetCultureInfo(xliff.TestAttribut("srcLang"));
            TargetLang = CultureInfo.GetCultureInfo(xliff.TestAttribut("trgLang"));
            
            foreach (XliffFile item in XliffFile.GetFiles(xliff))
                AddFile(item);

            if (Count == 0)
                throw new XliffException("Invalid Xliff file.\nA minimum of one <"+XliffFile.NodeName+"> with the attribute 'id' is required.");
        }

        public void AddFile(XliffFile file)
        {
            Add(file);
        }
    }
}
