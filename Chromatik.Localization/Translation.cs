using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace System.Globalization
{
    public class Translation : Collections.ObjectModel.Collection<TranslationContext>
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
                return new Translation(XmlCreate.DocumentXML(xml));
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

            }
            catch (Exception)
            {

                throw;
            }
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

        protected Translation(XmlDocument document) : base(GetContexts(document))
        {

        }

        static List<TranslationContext> GetContexts(XmlDocument document)
        {
            List<TranslationContext> rslt = new List<TranslationContext>();
            
            return rslt;
        }
    }
}
