using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace System.Xml
{
    /// <summary>
    /// Static class extension pour <see cref="XmlElement"/>
    /// </summary>
    static public class XmlExtension
    {
        /// <summary>
        /// Ajoute un Namespace a l'Element
        /// </summary>
        /// <param name="node">Nœud cible</param>
        /// <param name="prefix">Prefix associé a ce Namespace</param>
        /// <param name="namespaceURI">URI du Namespace</param>
        static public void AddNamespace(this XmlElement node, string prefix, string namespaceURI)
        {
            node.AddNamespace(new XmlNamespace(prefix, namespaceURI));
        }
        /// <summary>
        /// Ajoute un <see cref="XmlNamespace"/> a l'Element
        /// </summary>
        /// <param name="node">Nœud cible</param>
        /// <param name="ns"><see cref="XmlNamespace"/> a ajouté</param>
        static public void AddNamespace(this XmlElement node, XmlNamespace ns)
        {
            node.SetAttribute(ns.xmlns, ns.URI);
        }

        /// <summary>
        /// Ajoute un <see cref="XmlElement"/> à la fin de la liste des nœuds enfants de ce nœud. 
        /// </summary>
        /// <param name="node">Nœud cible</param>
        /// <param name="name">Nom de l'élément ajouté</param>
        /// <returns>Élément ajouté</returns>
        static public XmlElement AppendElement(this XmlNode node, string name)
        {
            return node.AppendElement(node.Prefix, name, node.NamespaceURI);
        }
        /// <summary>
        /// Ajoute un <see cref="XmlElement"/> à la fin de la liste des nœuds enfants de ce nœud. 
        /// </summary>
        /// <param name="node">Nœud cible</param>
        /// <param name="name">Nom de l'élément ajouté</param>
        /// <param name="namespaceURI">Namespace du <see cref="XmlElement"/></param>
        /// <returns>Élément ajouté</returns>
        static public XmlElement AppendElement(this XmlNode node, string name, string namespaceURI)
        {
            return node.AppendElement(node.Prefix, name, namespaceURI);
        }
        /// <summary>
        /// Ajoute un <see cref="XmlElement"/> à la fin de la liste des nœuds enfants de ce nœud. 
        /// </summary>
        /// <param name="node">Nœud cible</param>
        /// <param name="localName">Nom de l'élément ajouté</param>
        /// <param name="ns"><see cref="XmlNamespace"/> de l'attribut ajouté</param>
        /// <returns>Élément ajouté</returns>
        static public XmlElement AppendElement(this XmlNode node, string localName, XmlNamespace ns)
        {
            return node.AppendElement(ns.Prefix, localName, ns.URI);
        }
        /// <summary>
        /// Ajoute un <see cref="XmlElement"/> à la fin de la liste des nœuds enfants de ce nœud. 
        /// </summary>
        /// <param name="node">Nœud cible</param>
        /// <param name="prefix">Prefix l'élément ajouté</param>
        /// <param name="localName">Nom local de l'élément ajouté</param>
        /// <param name="namespaceURI">Namespace du <see cref="XmlElement"/></param>
        /// <returns>Élément ajouté</returns>
        static public XmlElement AppendElement(this XmlNode node, string prefix, string localName, string namespaceURI)
        {
            if (node is XmlDocument)
            {
                node.RemoveAll();
                return (XmlElement)node.AppendChild(((XmlDocument)node).CreateElement(prefix, localName, namespaceURI));
            }
            else
                return (XmlElement)node.AppendChild(node.OwnerDocument.CreateElement(prefix, localName, namespaceURI));
        }

        /// <summary>
        /// Ajoute un <see cref="XmlElement"/> à la fin de la liste des nœuds enfants de ce nœud. 
        /// </summary>
        /// <param name="node">Nœud cible</param>
        /// <param name="text">Texte a ajouté</param>
        /// <returns>Élément ajouté</returns>
        static public XmlText AppendText(this XmlNode node, string text)
        {
            if (node is XmlDocument)
            {
                XmlElement element = node.FirstElement();
                if (element != null)
                    return (XmlText)element.AppendChild(((XmlDocument)node).CreateTextNode(text));
                else
                    return null;
            }
            else
                return (XmlText)node.AppendChild(node.OwnerDocument.CreateTextNode(text));
        }

        /// <summary>
        /// Définit la valeur de l'attribut avec le nom local et le Namespace
        /// </summary>
        /// <param name="node">Nœud cible</param>
        /// <param name="localName">Nom local de l'attribut</param>
        /// <param name="ns">Namespace du de l'attribut</param>
        /// <param name="value">Valeur à définir pour l'attribut</param>
        static public void SetAttribute(this XmlElement node, string localName, XmlNamespace ns, string value)
        {
            node.AddNamespace(ns);
            node.SetAttribute(localName, ns.URI, value);
        }
        /// <summary>
        /// Définit la valeur de l'attribut avec le nom local, le prefix et le Namespace
        /// </summary>
        /// <param name="node">Nœud cible</param>
        /// <param name="prefix">Prefix de l'attribut</param>
        /// <param name="localName">Nom local de l'attribut</param>
        /// <param name="namespaceURI">Namespace URI de l'attribut</param>
        /// <param name="value">Valeur à définir pour l'attribut</param>
        static public void SetAttribute(this XmlElement node, string prefix, string localName, string namespaceURI, string value)
        {
            if (string.IsNullOrWhiteSpace(prefix))
                node.SetAttribute(localName, value);
            else
                node.SetAttribute(localName, new XmlNamespace(prefix, namespaceURI), value);
        }

        /// <summary>
        /// Transfére un <see cref="XmlAttribute"/> entre 2 nœud de contexte différent (si il existe)
        /// </summary>
        /// <param name="destination">Nœud de destination</param>
        /// <param name="source">Nœud source</param>
        /// <param name="attribut">Attribut a transféré</param>
        /// <returns></returns>
        static public bool TransferAttribut(this XmlNode destination, XmlNode source, string attribut)
        {
            if ((source is XmlElement) && (destination is XmlElement) && (source as XmlElement).HasAttribute(attribut))
            {
                (destination as XmlElement).SetAttribute(attribut, (source as XmlElement).GetAttribute(attribut));
                return true;
            }
            return false;
        }
    }
}
