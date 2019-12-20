using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Globalization
{
    public class TranslationContext : List<TranslationMessage>
    {
        public string Name { get; }

        public TranslationContext(string name)
        {
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));

            Name = name.Trim();
        }
        internal TranslationContext(XmlElement element)
        {
            XmlElement name = element.LastElement("name");
            if (name == null)
                throw new TranslationException("Invalid Translation file. No 'name' node was found.");

            Name = name.InnerText.Trim();

            XmlElement[] message = element.GetElements("message");
            if (message.Length == 0)
                throw new TranslationException("Invalid Translation file. No 'message' node was found.");
            foreach (var item in message)
                Add(new TranslationMessage(item));
        }
    }
}
