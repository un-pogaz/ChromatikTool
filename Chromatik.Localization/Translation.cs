using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Globalization
{
    public class Translation : Dictionary<string, TranslationContext>
    {
        static public Translation LoadTranslation(string path)
        {
            try
            {
                return LoadTranslationXML(System.IO.File.ReadAllText(path));
            }
            catch (Exception ex)
            {
                throw new TranslationException(ex);
            }
        }
        static public Translation LoadTranslationXML(string xml)
        {
            try
            {
                return new Translation(XmlCreate.DocumentXML(xml.ToLinux()));
            }
            catch (Exception ex)
            {
                throw new TranslationException(ex);
            }
        }

        static public Translation CreatNewTranslation(string targetLang)
        {
            try
            {
                return CreatNewTranslation(CultureInfo.GetCultureInfo(targetLang));
            }
            catch (Exception ex)
            {
                throw new TranslationException(ex);
            }
        }
        static public Translation CreatNewTranslation(CultureInfo targetCulture)
        {
            return null;
        }

        public CultureInfo Culture { get; }
        public VersionClass Version { get; set; }
        
        protected Translation(XmlDocument document) : base(new Dictionary<string, TranslationContext>())
        {
            XmlElement ts = document.FirstElement("TS");
            if (ts == null)
                throw new TranslationException("Invalid Translation file. The root node isn't 'TS'.");
            if (!ts.HasAttribute("language"))
                throw new TranslationException("The 'language' attribute as not defined.");
            if (!ts.HasAttribute("version"))
                throw new TranslationException("The 'version' attribute as not defined.");

            try
            {
                Culture = CultureInfo.GetCultureInfo(ts.GetAttribute("language"));
                Version = new VersionClass(ts.GetAttribute("version"));
            }
            catch (Exception ex)
            {
                throw new TranslationException(ex);
            }
            
            XmlElement[] context = ts.GetElements("context");
            if (context.Length == 0)
                throw new TranslationException("Invalid Translation file. No 'context' node was found.");

            List<TranslationContext> rslt = new List<TranslationContext>();
            foreach (XmlElement item in context)
            {
                TranslationContext trs = new TranslationContext(item);
                if (!ContainsKey(trs.Name))
                    Add(trs.Name, trs);
                else
                    foreach (TranslationMessage msg in trs)
                        this[trs.Name].Add(msg);
            }
        }
    }
}
