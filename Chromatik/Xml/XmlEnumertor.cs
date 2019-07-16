using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace System.Xml
{
    static public class XmlEnumertor
    {
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNode node)
        {
            return node.ChildNodes.EnumerableElement();
        }
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNodeList nodeList)
        {
            foreach (XmlNode item in nodeList)
                if (item is XmlElement)
                    yield return item as XmlElement;
        }
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNode node, string name)
        {
            return node.ChildNodes.EnumerableElement(name);
        }
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNodeList nodeList, string name)
        {
            foreach (XmlElement item in nodeList.EnumerableElement())
                if (item.Name == name)
                    yield return item;
        }
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNode node, string name, string attribute)
        {
            return node.ChildNodes.EnumerableElement(name, attribute);
        }
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNodeList nodeList, string name, string attribute)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name))
                if (item.Name == name && item.HasAttribute(attribute))
                    yield return item;
        }
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNode node, string name, string attribute, string value)
        {
            return node.ChildNodes.EnumerableElement(name, attribute, value);
        }
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNodeList nodeList, string name, string attribute, string value)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name, attribute))
                if (item.Name == name && item.HasAttribute(attribute) && item.Name == name && item.GetAttribute(attribute) == value)
                    yield return item;
        }
        static public IEnumerable<XmlElement> EnumerableElement_Reverse(this XmlNode node)
        {
            return node.ChildNodes.EnumerableElement_Reverse();
        }
        static public IEnumerable<XmlElement> EnumerableElement_Reverse(this XmlNodeList nodeList)
        {
            for (int i = nodeList.Count - 1; i >= 0; i--)
                if (nodeList[i] is XmlElement)
                    yield return nodeList[i] as XmlElement;
        }
        
        #region Element From

        /// <summary>
        /// Obtient les Elements
        /// </summary>
        /// <param name="node"></param>
        static public XmlElement[] GetElements(this XmlNode node)
        { return node.ChildNodes.GetElements(); }
        /// <summary>
        /// Obtient les Elements
        /// </summary>
        /// <param name="nodeList"></param>
        static public XmlElement[] GetElements(this XmlNodeList nodeList)
        {
            return nodeList.EnumerableElement().ToArray();
        }

        /// <summary>
        /// Obtient les Elements de nom correspondant
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        static public XmlElement[] GetElements(this XmlNode node, string name)
        { return node.ChildNodes.GetElements(name); }
        /// <summary>
        /// Obtient les Elements de nom correspondant
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        static public XmlElement[] GetElements(this XmlNodeList nodeList, string name)
        {
            return nodeList.EnumerableElement(name).ToArray();
        }

        /// <summary>
        /// Obtient les Elements correspondant
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        static public XmlElement[] GetElements(this XmlNode node, string name, string attribute)
        { return node.ChildNodes.GetElements(name, attribute); }
        /// <summary>
        /// Obtient les Elements correspondant
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        static public XmlElement[] GetElements(this XmlNodeList nodeList, string name, string attribute)
        {
            return nodeList.EnumerableElement(name, attribute).ToArray();
        }

        /// <summary>
        /// Obtient les Elements correspondant
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        static public XmlElement[] GetElements(this XmlNode node, string name, string attribute, string value)
        { return node.ChildNodes.GetElements(name, attribute, value); }
        /// <summary>
        /// Obtient les Elements correspondant
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        static public XmlElement[] GetElements(this XmlNodeList nodeList, string name, string attribute, string value)
        {
            return nodeList.EnumerableElement(name, attribute, value).ToArray();
        }

        #endregion


        #region First Element

        /// <summary>
        /// Retourne le premier Element
        /// </summary>
        static public XmlElement FirstElement(this XmlNode node)
        { return node.ChildNodes.FirstElement(); }
        /// <summary>
        /// Retourne le premier Element
        /// </summary>
        static public XmlElement FirstElement(this XmlNodeList nodeList)
        {
            foreach (XmlElement item in nodeList.EnumerableElement())
                return item;

            return null;
        }

        /// <summary>
        /// Retourne le premier Element de nom correspondant
        /// </summary>
        static public XmlElement FirstElement(this XmlNode node, string name)
        { return node.ChildNodes.FirstElement(name); }
        /// <summary>
        /// Retourne le premier Element de nom correspondant
        /// </summary>
        /// <param name="name"></param>
        static public XmlElement FirstElement(this XmlNodeList nodeList, string name)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name))
                return item;

            return null;
        }

        /// <summary>
        /// Retourne le premier Element correspondant
        /// </summary>
        static public XmlElement FirstElement(this XmlNode node, string name, string attribute)
        { return node.ChildNodes.FirstElement(name, attribute); }
        /// <summary>
        /// Retourne le premier Element correspondant
        /// </summary>
        /// <param name="name"></param>
        static public XmlElement FirstElement(this XmlNodeList nodeList, string name, string attribute)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name, attribute))
                return item;

            return null;
        }

        /// <summary>
        /// Retourne le premier Element correspondant
        /// </summary>
        static public XmlElement FirstElement(this XmlNode node, string name, string attribute, string value)
        { return node.ChildNodes.FirstElement(name, attribute, value); }
        /// <summary>
        /// Retourne le premier Element correspondant
        /// </summary>
        /// <param name="name"></param>
        static public XmlElement FirstElement(this XmlNodeList nodeList, string name, string attribute, string value)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name, attribute, value))
                return item;

            return null;
        }

        #endregion


        #region Last Element

        /// <summary>
        /// Retourne le dernier Element
        /// </summary>
        static public XmlElement LastElement(this XmlNode node)
        { return node.ChildNodes.LastElement(); }
        /// <summary>
        /// Retourne le dernier Element
        /// </summary>
        static public XmlElement LastElement(this XmlNodeList nodeList)
        {
            foreach (XmlElement item in nodeList.EnumerableElement_Reverse())
                return item;

            return null;
        }

        /// <summary>
        /// Retourne le dernier Element de nom correspondant
        /// </summary>
        static public XmlElement LastElement(this XmlNode node, string name)
        { return node.ChildNodes.LastElement(name); }
        /// <summary>
        /// Retourne le dernier Element de nom correspondant
        /// </summary>
        static public XmlElement LastElement(this XmlNodeList nodeList, string name)
        {
            foreach (XmlElement item in nodeList.EnumerableElement_Reverse())
                if (item.Name == name)
                    return item;

            return null;
        }

        /// <summary>
        /// Retourne le dernier Element correspondant
        /// </summary>
        static public XmlElement LastElement(this XmlNode node, string name, string attribute)
        { return node.ChildNodes.LastElement(name, attribute); }
        /// <summary>
        /// Retourne le dernier Element correspondant
        /// </summary>
        static public XmlElement LastElement(this XmlNodeList nodeList, string name, string attribute)
        {
            foreach (XmlElement item in nodeList.EnumerableElement_Reverse())
                if (item.Name == name && item.HasAttribute(attribute))
                    return item;

            return null;
        }

        /// <summary>
        /// Retourne le dernier Element correspondant
        /// </summary>
        static public XmlElement LastElement(this XmlNode node, string name, string attribute, string value)
        { return node.ChildNodes.LastElement(name, attribute, value); }
        /// <summary>
        /// Retourne le dernier Element correspondant
        /// </summary>
        static public XmlElement LastElement(this XmlNodeList nodeList, string name, string attribute, string value)
        {
            foreach (XmlElement item in nodeList.EnumerableElement_Reverse())
                if (item.Name == name && item.GetAttribute(attribute) == value)
                    return item;

            return null;
        }

        #endregion
        
    }
}
