using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;

namespace System.Globalization.Localization
{
    [Diagnostics.DebuggerDisplay("{Name} Count = {Count}")]
    public class QtTranslationContext : Collections.ObjectModel.ReadOnlyCollection<QtTranslationMessage>
    {
        public string Name { get; }
        
        private QtTranslationContext() : base(new List<QtTranslationMessage>())
        { }
        public QtTranslationContext(string name) : this()
        {
            if (name.IsNullOrWhiteSpace())
                throw new ArgumentNullException(nameof(name));
            
            Name = name.Trim();
        }
        internal QtTranslationContext(XmlElement element) : this()
        {
            XmlElement name = element.LastElement("name");
            if (name == null)
                throw QtTranslationException.InvalideNoNodeFound("name");

            Name = name.InnerText.Trim();

            foreach (var item in element.GetElements("message"))
            {
                try
                {
                    AddMessage(new QtTranslationMessage(item));
                }
                catch (Exception)
                {
                }
            }
        }

        /// <summary></summary>
        public void AddMessage(string location, int line, string source, string translation)
        {
            AddMessage(new QtTranslationMessage(new QtTranslationLocation(location, line), source, translation));
        }
        /// <summary></summary>
        public void AddMessage(QtTranslationMessage message)
        {
            Items.Add(message);
        }
        /// <summary></summary>
        public void InsertMessage(int index, QtTranslationMessage message)
        {
            Items.Insert(index, message);
        }

        /// <summary></summary>
        public bool RemoveMessage(QtTranslationMessage message)
        {
            return Items.Remove(message);
        }
        /// <summary></summary>
        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }
    }
}
