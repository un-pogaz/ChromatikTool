using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace System.Xml
{
    /// <summary>
    /// Extension class for enumerate or get a array of <see cref="XmlElement"/>
    /// </summary>
    static public class XmlEnumertor
    {
        #region EnumerableElement

        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> in the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNode node)
        {
            return node.ChildNodes.EnumerableElement();
        }
        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> in the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNodeList nodeList)
        {
            return Enumerable.OfType<XmlElement>(nodeList);
        }

        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name in the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNode node, string name)
        {
            return node.ChildNodes.EnumerableElement(name);
        }
        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name in the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNodeList nodeList, string name)
        {
            foreach (XmlElement item in nodeList.EnumerableElement())
                if (item.Name == name)
                    yield return item;
        }

        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name and containing the attribute in the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNode node, string name, string attribute)
        {
            return node.ChildNodes.EnumerableElement(name, attribute);
        }
        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name and containing the attribute in the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNodeList nodeList, string name, string attribute)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name))
                if (item.Name == name && item.HasAttribute(attribute))
                    yield return item;
        }

        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name and and with the corresponding value attribute in the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNode node, string name, string attribute, string value)
        {
            return node.ChildNodes.EnumerableElement(name, attribute, value);
        }
        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name and and with the corresponding value attribute in the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public IEnumerable<XmlElement> EnumerableElement(this XmlNodeList nodeList, string name, string attribute, string value)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name, attribute))
                if (item.Name == name && item.HasAttribute(attribute) && item.Name == name && item.GetAttribute(attribute) == value)
                    yield return item;
        }
        

        #endregion

        #region Element From

        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> in the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        static public XmlElement[] GetElements(this XmlNode node)
        {
            return node.ChildNodes.GetElements();
        }
        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> in the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        static public XmlElement[] GetElements(this XmlNodeList nodeList)
        {
            return nodeList.EnumerableElement().ToArray();
        }

        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name in the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        static public XmlElement[] GetElements(this XmlNode node, string name)
        {
            return node.ChildNodes.GetElements(name);
        }
        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name in the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        static public XmlElement[] GetElements(this XmlNodeList nodeList, string name)
        {
            return nodeList.EnumerableElement(name).ToArray();
        }

        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name and containing the attribute in the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        static public XmlElement[] GetElements(this XmlNode node, string name, string attribute)
        {
            return node.ChildNodes.GetElements(name, attribute);
        }
        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name and containing the attribute in the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        static public XmlElement[] GetElements(this XmlNodeList nodeList, string name, string attribute)
        {
            return nodeList.EnumerableElement(name, attribute).ToArray();
        }

        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name and and with the corresponding value attribute in the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        static public XmlElement[] GetElements(this XmlNode node, string name, string attribute, string value)
        {
            return node.ChildNodes.GetElements(name, attribute, value);
        }
        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> with the corresponding name and and with the corresponding value attribute in the <see cref="XmlNodeList"/>
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
        /// Get the first <see cref="XmlElement"/> of the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        static public XmlElement FirstElement(this XmlNode node)
        {
            return node.ChildNodes.FirstElement();
        }
        /// <summary>
        /// Get the first <see cref="XmlElement"/> of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        static public XmlElement FirstElement(this XmlNodeList nodeList)
        {
            foreach (XmlElement item in nodeList.EnumerableElement())
                return item;

            return null;
        }

        /// <summary>
        /// Get the first the <see cref="XmlElement"/> with the corresponding name of the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public XmlElement FirstElement(this XmlNode node, string name)
        {
            return node.ChildNodes.FirstElement(name);
        }
        /// <summary>
        /// Get the first the <see cref="XmlElement"/> with the corresponding name of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public XmlElement FirstElement(this XmlNodeList nodeList, string name)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name))
                return item;

            return null;
        }

        /// <summary>
        /// Get the first the <see cref="XmlElement"/> with the corresponding name and containing the attribute of the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        static public XmlElement FirstElement(this XmlNode node, string name, string attribute)
        {
            return node.ChildNodes.FirstElement(name, attribute);
        }
        /// <summary>
        /// Get the first the <see cref="XmlElement"/> with the corresponding name and containing the attribute of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        static public XmlElement FirstElement(this XmlNodeList nodeList, string name, string attribute)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name, attribute))
                return item;

            return null;
        }

        /// <summary>
        /// Get the first the <see cref="XmlElement"/> with the corresponding name and and with the corresponding value attribute of the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public XmlElement FirstElement(this XmlNode node, string name, string attribute, string value)
        {
            return node.ChildNodes.FirstElement(name, attribute, value);
        }
        /// <summary>
        /// Get the first the <see cref="XmlElement"/> with the corresponding name and and with the corresponding value attribute of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public XmlElement FirstElement(this XmlNodeList nodeList, string name, string attribute, string value)
        {
            foreach (XmlElement item in nodeList.EnumerableElement(name, attribute, value))
                return item;

            return null;
        }

        #endregion

        #region Last Element

        /// <summary>
        /// Enumerate the <see cref="XmlElement"/> in the <see cref="XmlNodeList"/> in reverse ordre
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        static IEnumerable<XmlElement> EnumerableElementReverse(this XmlNodeList nodeList)
        {
            return nodeList.EnumerableElement().Reverse();
        }

        /// <summary>
        /// Get the last <see cref="XmlElement"/> of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        static public XmlElement LastElement(this XmlNode node)
        {
            return node.ChildNodes.LastElement();
        }
        /// <summary>
        /// Get the last <see cref="XmlElement"/> of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        static public XmlElement LastElement(this XmlNodeList nodeList)
        {
            foreach (XmlElement item in nodeList.EnumerableElementReverse())
                return item;

            return null;
        }

        /// <summary>
        /// Get the last the <see cref="XmlElement"/> with the corresponding name of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public XmlElement LastElement(this XmlNode node, string name)
        {
            return node.ChildNodes.LastElement(name);
        }
        /// <summary>
        /// Get the last the <see cref="XmlElement"/> with the corresponding name of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static public XmlElement LastElement(this XmlNodeList nodeList, string name)
        {
            foreach (XmlElement item in nodeList.EnumerableElementReverse())
                if (item.Name == name)
                    return item;

            return null;
        }

        /// <summary>
        /// Get the last the <see cref="XmlElement"/> with the corresponding name and containing the attribute of the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        static public XmlElement LastElement(this XmlNode node, string name, string attribute)
        {
            return node.ChildNodes.LastElement(name, attribute);
        }
        /// <summary>
        /// Get the last the <see cref="XmlElement"/> with the corresponding name and containing the attribute of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        static public XmlElement LastElement(this XmlNodeList nodeList, string name, string attribute)
        {
            foreach (XmlElement item in nodeList.EnumerableElementReverse())
                if (item.Name == name && item.HasAttribute(attribute))
                    return item;

            return null;
        }

        /// <summary>
        /// Get the last the <see cref="XmlElement"/> with the corresponding name and and with the corresponding value attribute of the <see cref="XmlNode"/>
        /// </summary>
        /// <param name="node"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public XmlElement LastElement(this XmlNode node, string name, string attribute, string value)
        {
            return node.ChildNodes.LastElement(name, attribute, value);
        }
        /// <summary>
        /// Get the last the <see cref="XmlElement"/> with the corresponding name and and with the corresponding value attribute of the <see cref="XmlNodeList"/>
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="name"></param>
        /// <param name="attribute"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        static public XmlElement LastElement(this XmlNodeList nodeList, string name, string attribute, string value)
        {
            foreach (XmlElement item in nodeList.EnumerableElementReverse())
                if (item.Name == name && item.GetAttribute(attribute) == value)
                    return item;

            return null;
        }

        #endregion
    }
}
