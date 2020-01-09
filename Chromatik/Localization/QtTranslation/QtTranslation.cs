using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Globalization.Localization
{
    public class QtTranslation : Collections.ObjectModel.ReadOnlyDictionary<string, QtTranslationContext>
    {
        static public QtTranslation LoadTranslation(string path)
        {
            try
            {
                return new QtTranslation(XmlDocumentCreate.Document(path));
            }
            catch (Exception ex)
            {
                throw new QtTranslationException(ex);
            }
        }
        static public QtTranslation LoadTranslationXML(string xml)
        {
            try
            {
                return new QtTranslation(XmlDocumentCreate.DocumentXML(xml));
            }
            catch (Exception ex)
            {
                throw new QtTranslationException(ex);
            }
        }

        static public QtTranslation CreatNewTranslation(string targetLang)
        {
            try
            {
                return CreatNewTranslation(CultureInfo.GetCultureInfo(targetLang));
            }
            catch (Exception ex)
            {
                throw new QtTranslationException(ex);
            }
        }
        static public QtTranslation CreatNewTranslation(CultureInfo targetCulture)
        {
            XmlRacine ts = new XmlRacine("TS");
            ts.SetAttribute("language", targetCulture.Name);
            ts.SetAttribute("version", "2.1");

            return new QtTranslation(ts.OwnerDocument);
        }
        
        public CultureInfo Language { get; }
        public VersionClass Version { get; }
        
        protected QtTranslation(XmlDocument document) : base(new Dictionary<string, QtTranslationContext>())
        {
            XmlElement ts = document.FirstElement("TS");
            if (ts == null)
                throw new QtTranslationException("Invalid Translation file. The root node isn't 'TS'.");
            if (!ts.HasAttribute("language"))
                throw QtTranslationException.InvalideNoNodeFound("TS", "language");
            if (!ts.HasAttribute("version"))
                throw QtTranslationException.InvalideNoNodeFound("TS", "version");
            if (ts.GetAttribute("version") != "2.1")
                throw new QtTranslationException("Unsuported version of QtTranslation file.\nThe 2.1 version is the only supported.");

            try
            {
                Language = CultureInfo.GetCultureInfo(ts.GetAttribute("language"));
                Version = new VersionClass(ts.GetAttribute("version"));
            }
            catch (Exception ex)
            {
                throw new QtTranslationException(ex);
            }
            
            XmlElement[] context = ts.GetElements("context");
            if (context.Length == 0)
                throw QtTranslationException.InvalideNoNodeFound("context");
            
            foreach (XmlElement item in context)
            {
                try
                {
                    AddContext(new QtTranslationContext(item));
                }
                catch
                {
                }
            }
        }

        public void AddContext(QtTranslationContext context)
        {
            if (!ContainsKey(context.Name))
                Dictionary.Add(context.Name, context);
            else
                foreach (QtTranslationMessage msg in context)
                    this[context.Name].Add(msg);
        }
        public void AddContext(string contextName)
        {
            AddContext(new QtTranslationContext(contextName));
        }
        public void RemoveContext(string contextName)
        {
            Dictionary.Remove(contextName);
        }
        
        public XmlDocument GetXmlDocument()
        {
            XmlRacine ts = new XmlRacine("TS");
            ts.SetAttribute("language", Language.Name);
            ts.SetAttribute("version", Version.ToString());

            foreach (QtTranslationContext ctx in Dictionary.Values)
            {
                XmlElement context = ts.AppendElement("context");
                context.AppendElement("name").AppendText(ctx.Name);

                foreach (QtTranslationMessage msg in ctx)
                {
                    XmlElement message = context.AppendElement("message");

                    foreach (var loc in msg.Locations)
                    {
                        XmlElement location = message.AppendElement("location");
                        location.SetAttribute("filename", loc.Filename);
                        location.SetAttribute("line", loc.Line.ToString());
                    }

                    message.AppendElement("source").AppendText(msg.Source);

                    if (!msg.Comment.IsNullOrWhiteSpace())
                        message.AppendElement("comment").AppendText(msg.Comment);

                    message.AppendElement("translation").AppendText(msg.Translation);
                }
            }

            return ts.OwnerDocument;
        }
        
        public void Save(string filePath)
        {
            Save(filePath, XmlDocumentWriter.Setting);
        }
        public void Save(string filePath, XmlWriterSettings settings)
        {
            XmlDocumentWriter.Document(filePath, GetXmlDocument(), settings, DocumentType);
        }

        static public DocumentType DocumentType { get; } = new DocumentType("TS", false, null, null);
    }
}
