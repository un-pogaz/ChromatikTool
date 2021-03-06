﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace System.Xml
{
    /// <summary>
    /// Static class extension for <see cref="XmlElement"/>
    /// </summary>
    static public class XmlExtension
    {
        #region Element
        /// <summary>
        /// Append a <see cref="XmlElement"/> to the node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public XmlElement AppendElement(this XmlNode node, string name)
        {
            return node.AppendElement(node.Prefix, name, node.NamespaceURI);
        }
        /// <summary>
        /// Append a <see cref="XmlElement"/> to the node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="namespaceURI">Namespace of the element</param>
        /// <returns></returns>
        static public XmlElement AppendElement(this XmlNode node, string name, string namespaceURI)
        {
            return node.AppendElement(node.Prefix, name, namespaceURI);
        }
        /// <summary>
        /// Append a <see cref="XmlElement"/> to the node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="localName"></param>
        /// <param name="ns"><see cref="XmlNamespace"/> of this element</param>
        /// <returns></returns>
        static public XmlElement AppendElement(this XmlNode node, string localName, XmlNamespace ns)
        {
            return node.AppendElement(ns.Prefix, localName, ns.Uri);
        }
        /// <summary>
        /// Append a <see cref="XmlElement"/> to the node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="prefix">Prefix of the element</param>
        /// <param name="localName"></param>
        /// <param name="namespaceURI">Namespace of the element</param>
        /// <returns></returns>
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

        #endregion

        #region Text
        /// <summary>
        /// Append a <see cref="XmlText"/> to the node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        static public XmlText AppendText(this XmlNode node, string text)
        {
            if (node is XmlDocument)
            {
                return (XmlText)node.AppendChild((node as XmlDocument).CreateTextNode(text));
            }
            else
            {
                return (XmlText)node.AppendChild(node.OwnerDocument.CreateTextNode(text));
            }
        }
        #endregion

        #region Attribut and Namespace
        /// <summary>
        /// Set a attribut with the value and the specified Namespace
        /// </summary>
        /// <param name="node"></param>
        /// <param name="localName"></param>
        /// <param name="ns"><see cref="XmlNamespace"/> of this element</param>
        /// <param name="value"></param>
        static public void SetAttribute(this XmlElement node, string localName, XmlNamespace ns, string value)
        {
            node.SetAttribute(localName, ns.Prefix, ns.Uri, value);
        }
        /// <summary>
        /// Set a attribut with the value and the specified Namespace
        /// </summary>
        /// <param name="node"></param>
        /// <param name="prefix">Prefix of the element</param>
        /// <param name="localName"></param>
        /// <param name="namespaceURI">Namespace of the element</param>
        /// <param name="value"></param>
        static public void SetAttribute(this XmlElement node, string prefix, string localName, string namespaceURI, string value)
        {
            if (!string.IsNullOrWhiteSpace(prefix))
                node.AddNamespace(prefix, namespaceURI);

            node.SetAttribute(localName, namespaceURI, value);
        }

        /// <summary>
        /// Transfer a attribute between 2 nodes of different context (if existe)
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="source"></param>
        /// <param name="attribut">Attribut to transfer</param>
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

        /// <summary>
        /// Add a Namespace composed of a prefix and a URI to the node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="ns"><see cref="XmlNamespace"/> to add</param>
        static public void AddNamespace(this XmlElement node, XmlNamespace ns)
        {
            node.AddNamespace(ns.Prefix, ns.Uri);
        }

        /// <summary>
        /// Add a Namespace composed of a prefix and a URI to the node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="localName">Prefix associated of this namespace</param>
        /// <param name="namespaceURI">URI associated of this namespace</param>
        static public void AddNamespace(this XmlElement node, string localName, string namespaceURI)
        {
            node.SetAttributeNode(localName, namespaceURI);
        }
        #endregion

        #region Others
        /// <summary>
        /// Append a <see cref="XmlCDataSection"/> to the node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        static public XmlCDataSection AppendCDataSection(this XmlNode node, string text)
        {
            if (node is XmlDocument)
            {
                return (XmlCDataSection)node.AppendChild((node as XmlDocument).CreateCDataSection(text));
            }
            else
            {
                return (XmlCDataSection)node.AppendChild(node.OwnerDocument.CreateCDataSection(text));
            }
        }

        /// <summary>
        /// Append a <see cref="XmlComment"/> to the node
        /// </summary>
        /// <param name="node"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        static public XmlComment AppendComment(this XmlNode node, string text)
        {
            if (node is XmlDocument)
            {
                return (XmlComment)node.AppendChild((node as XmlDocument).CreateComment(text));
            }
            else
            {
                return (XmlComment)node.AppendChild(node.OwnerDocument.CreateComment(text));
            }
        }

        /// <summary>
        /// Remove the <see cref="XmlDeclaration"/> of the OwnerDocument.
        /// </summary>
        /// <param name="node"></param>
        /// <returns>Return the node cleaned</returns>
        static public XmlNode RemoveDeclaration(this XmlNode node)
        {
            XmlDocument doc;
            if (node is XmlDocument)
                doc = node as XmlDocument;
            else
                doc = node.OwnerDocument;

            if (doc.FirstChild != null)
                if (doc.FirstChild.NodeType == XmlNodeType.XmlDeclaration)
                    doc.RemoveChild(doc.FirstChild);

            return node;
        }

        #endregion

        #region DocumentType
        /// <summary>
        /// Revove the <see cref="XmlDocumentType"/> of the document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        static public bool RevoveDocumentType(this XmlDocument document)
        {
            if (document.DocumentType != null)
            {
                document.RemoveChild(document.DocumentType);
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Create a <see cref="XmlDocumentType"/> without any Id
        /// </summary>
        /// <param name="document"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public XmlDocumentType CreateDocumentType(this XmlDocument document, string name)
        {
            return document.CreateDocumentType(name, null, null);
        }
        /// <summary>
        /// Create a <see cref="XmlDocumentType"/> with the specified Id
        /// </summary>
        /// <param name="document"></param>
        /// <param name="name"></param>
        /// <param name="publicId"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        static public XmlDocumentType CreateDocumentType(this XmlDocument document, string name, string publicId, string systemId)
        {
            return document.CreateDocumentType(name, publicId, systemId, null);
        }

        /// <summary>
        /// Set a <see cref="XmlDocumentType"/> without any Id
        /// </summary>
        /// <param name="document"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public XmlDocumentType SetDocumentType(this XmlDocument document, string name)
        {
            return document.SetDocumentType(name, null, null);
        }
        /// <summary>
        /// Set a <see cref="XmlDocumentType"/> with the specified Ids
        /// </summary>
        /// <param name="document"></param>
        /// <param name="name"></param>
        /// <param name="publicId"></param>
        /// <param name="systemId"></param>
        /// <returns></returns>
        static public XmlDocumentType SetDocumentType(this XmlDocument document, string name, string publicId, string systemId)
        {
            return document.SetDocumentType(name, publicId, systemId, null);
        }
        /// <summary>
        /// Set a <see cref="XmlDocumentType"/> with the specified Ids and internalSubset
        /// </summary>
        /// <param name="document"></param>
        /// <param name="name"></param>
        /// <param name="publicId"></param>
        /// <param name="systemId"></param>
        /// <param name="internalSubset"></param>
        /// <returns></returns>
        static public XmlDocumentType SetDocumentType(this XmlDocument document, string name, string publicId, string systemId, string internalSubset)
        {
            document.RevoveDocumentType();
            if (!name.IsNullOrWhiteSpace())
            {
                document.InsertBefore(document.CreateDocumentType(name, publicId, systemId, internalSubset), document.FirstElement());
            }
            return document.DocumentType;
        }

        /// <summary>
        /// Set a <see cref="DocumentType"/> with the specified Ids and internalSubset
        /// </summary>
        /// <param name="document"></param>
        /// <param name="doctype"></param>
        /// <returns></returns>
        static public XmlDocumentType SetDocumentType(this XmlDocument document, DocumentType doctype)
        {
            return document.SetDocumentType(doctype.Name, doctype.PublicId, doctype.SystemId, doctype.Subset);
        }
        /// <summary>
        /// Create a <see cref="XmlDocumentType"/>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="doctype"></param>
        /// <returns></returns>
        static public XmlDocumentType CreateDocumentType(this XmlDocument document, DocumentType doctype)
        {
            return document.CreateDocumentType(doctype.Name, doctype.PublicId, doctype.SystemId, doctype.Subset);
        }
        #endregion
    }
}
