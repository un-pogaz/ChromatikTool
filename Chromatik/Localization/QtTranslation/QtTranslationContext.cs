using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;

namespace System.Globalization.Localization
{
    [Diagnostics.DebuggerDisplay("{Name} Count = {Count}")]
    public class QtTranslationContext : List<QtTranslationMessage>
    {
        public string Name { get; }
        
        public QtTranslationContext(string name)
        {
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));
            
            Name = name.Trim();
        }
        
        public void AddMessage(string location, int line, string source, string translation)
        {
            Add(new QtTranslationMessage(location, line, source, translation));
        }

        internal QtTranslationContext(XmlElement element)
        {
            XmlElement name = element.LastElement("name");
            if (name == null)
                throw QtTranslationException.InvalideNoNodeFound("name");

            Name = name.InnerText.Trim();
            
            foreach (var item in element.GetElements("message"))
            {
                try
                {
                    Add(new QtTranslationMessage(item));
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
