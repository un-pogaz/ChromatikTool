using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Globalization
{
    public class QtTranslation : System.Collections.ObjectModel.ReadOnlyDictionary<string, QtTranslationContext>
    {
        static public QtTranslation LoadTranslation(string path)
        {
            try
            {
                return new QtTranslation(XmlCreate.Document(path));
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
                return new QtTranslation(XmlCreate.DocumentXML(xml));
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

        public CultureInfo Culture { get; }
        public VersionClass Version { get; set; }
        
        protected QtTranslation(XmlDocument document) : base(new Dictionary<string, QtTranslationContext>())
        {
            XmlElement ts = document.FirstElement("TS");
            if (ts == null)
                throw new QtTranslationException("Invalid Translation file. The root node isn't 'TS'.");
            if (!ts.HasAttribute("language"))
                throw QtTranslationException.InvalideNoAttributeFound("language", "TS");
            if (!ts.HasAttribute("version"))
                throw QtTranslationException.InvalideNoAttributeFound("version", "TS");

            try
            {
                Culture = CultureInfo.GetCultureInfo(ts.GetAttribute("language"));
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
        
        public void Save(string filePath)
        {
            Save(filePath, XmlWriterDocument.Setting);
        }
        public void Save(string filePath, XmlWriterSettings settings)
        {
            XmlRacine ts = new XmlRacine("TS");
            ts.SetAttribute("version", Version.ToString());
            ts.SetAttribute("language", Culture.Name);

            foreach (var item in Dictionary)
            {

            }

            XmlWriterDocument.Document(filePath, ts, settings);
        }
    }
}
