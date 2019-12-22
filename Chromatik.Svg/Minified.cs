using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Threading.Tasks;
using System.Globalization;

namespace Svg
{
    /// <summary>
    /// Static class for minified a svg picture
    /// </summary>
    static public class Minified
    {
        /// <summary>
        /// </summary>
        /// <param name="indent"></param>
        /// <returns></returns>
        static XmlWriterSettings WriterSetting(bool indent)
        {
            return new XmlWriterSettings()
            {
                OmitXmlDeclaration = false,
                Encoding = new UTF8Encoding(false),
                Indent = indent,
                IndentChars = "",
                NewLineChars = "\n",
                NewLineOnAttributes = false,
                CheckCharacters = true,
                NamespaceHandling = NamespaceHandling.OmitDuplicates,
                ConformanceLevel = ConformanceLevel.Auto,
                NewLineHandling = NewLineHandling.None,
                DoNotEscapeUriAttributes = false,
                WriteEndDocumentOnClose = true,
                CloseOutput = true
            };
        }

        /// <summary>
        /// Minified svg picture
        /// </summary>
        /// <param name="file"></param>
        static public void File(string file)
        {
            File(file, true);
        }
        /// <summary>
        /// Get a minified svg picture
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="indent"></param>
        static public void File(string filePath, bool indent)
        {
            if (System.IO.File.Exists(filePath))
            {
                XmlDocument load = XmlCreate.Document(filePath);

                if (load != null && load.FirstElement("svg") != null)
                {
                    string svg_minifed = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(filePath) + "-minifed.svg";

                    XmlWriterDocument.Document(svg_minifed, Document(load), WriterSetting(indent));

                    string text = System.IO.File.ReadAllText(svg_minifed).ReplaceLoop(" />", "/>");
                    if (!indent)
                        text = text.Regex("(\r|\n)", "");
                    System.IO.File.WriteAllText(svg_minifed, text, Encoding.UTF8);
                }
            }
        }

        /// <summary>
        /// Get a minified <see cref="XmlDocument"/> represents a svg picture
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        static public XmlDocument Document(XmlDocument doc)
        {
            XmlDocument rslt = new XmlDocument();
            XmlElement svg = rslt.AppendElement("svg", @"http://www.w3.org/2000/svg");
            XmlElement rac = doc.FirstElement("svg");

            svg.SetAttribute("viewBox", rac.GetAttribute("viewBox"));
            svg.SetAttribute("version", rac.GetAttribute("version"));
            Element(rac, svg);

            return rslt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="element"></param>
        static void Element(XmlElement source, XmlElement element)
        {
            foreach (XmlAttribute item in source.Attributes)
                if (item.Prefix == "xmlns" &&
                    (item.Value == "http://creativecommons.org/ns#" ||
                    item.Value == "http://purl.org/dc/elements/1.1/" ||
                    item.Value == "http://www.w3.org/1999/02/22-rdf-syntax-ns#"))
                {
                    element.AddNamespace(item.LocalName, item.Value);
                }

            foreach (XmlAttribute item in source.Attributes)
                if (item.Prefix != "xmlns" &&
                    (item.NamespaceURI == "" ||
                    item.NamespaceURI == "http://www.w3.org/XML/1998/namespace" ||
                    item.NamespaceURI == "http://www.w3.org/2000/svg" || 
                    item.NamespaceURI == "http://creativecommons.org/ns#" ||
                    item.NamespaceURI == "http://purl.org/dc/elements/1.1/" ||
                    item.NamespaceURI == "http://www.w3.org/1999/02/22-rdf-syntax-ns#"))
                {
                    if (item.LocalName != "id")
                    {
                        if (element.Name == "g")
                        {
                            if (item.Name == "style")
                            {
                                if (item.Value.ToLower().Contains("font-family"))
                                {
                                    string brut_force = item.Value.Substring(item.Value.ToLower().IndexOf("font-family"));
                                    brut_force = brut_force.Substring(brut_force.IndexOf(":") + 1);
                                    brut_force = brut_force.Substring(0, brut_force.IndexOf(";"));
                                    element.SetAttribute("font-family", item.NamespaceURI, brut_force);
                                }
                            }
                            else if (item.Name == "aria-label")
                                element.SetAttribute(item.LocalName, item.NamespaceURI, item.Value.Regex("(\r\n|\r|\n)", " "));
                            else
                                element.SetAttribute(item.LocalName, item.NamespaceURI, item.Value);
                        }
                        else if (element.Name == "path" && item.Name == "d")
                        {
                            string[] split = item.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            for (int i = 0; i < split.Length; i++)
                            {
                                if (split[i].Contains(","))
                                {
                                    string[] sub_split = split[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int ii = 0; ii < sub_split.Length; ii++)
                                        sub_split[ii] = (Math.Round(double.Parse(sub_split[ii], CultureInfo.InvariantCulture) * 100) / 100).ToString(CultureInfo.InvariantCulture);

                                    split[i] = string.Empty;
                                    foreach (var sub in sub_split)
                                        split[i] += " " + sub;

                                    split[i] = split[i].Trim();
                                }
                                if (split[i].Regex("[0-9.-]", "") == "")
                                {

                                    if (split[i].Contains("-"))
                                    {
                                        string[] sub_split = split[i].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                        for (int ii = 0; ii < sub_split.Length; ii++)
                                            sub_split[ii] = (Math.Round(double.Parse(sub_split[ii], CultureInfo.InvariantCulture) * 100) / 100).ToString(CultureInfo.InvariantCulture);

                                        string r = string.Empty;
                                        foreach (var sub in sub_split)
                                            r += "-" + sub;

                                        if (split[i].StartsWith("-"))
                                            split[i] = "-" + r.Trim('-');
                                        else
                                            split[i] = r.Trim('-');

                                    }
                                    else
                                        split[i] = (Math.Round(double.Parse(split[i], CultureInfo.InvariantCulture) * 100) / 100).ToString(CultureInfo.InvariantCulture);
                                }

                            }

                            string rslt = string.Empty;
                            foreach (var sub in split)
                                if (sub.Length == 1 &&
                                    sub[0] != '0' && sub[0] != '1' &&
                                    sub[0] != '2' && sub[0] != '3' &&
                                    sub[0] != '4' && sub[0] != '5' &&
                                    sub[0] != '6' && sub[0] != '7' &&
                                    sub[0] != '8' && sub[0] != '9')
                                    rslt += sub;
                                else
                                    rslt += " " + sub + " ";

                            rslt = rslt.RegexLoop("([A-z]) ", "$1").RegexLoop(" ([A-z])", "$1").RegexLoop("  -([0-9])", "-$1").ReplaceLoop("  ", " ").Trim();

                            element.SetAttribute(item.LocalName, item.NamespaceURI, rslt);
                        }
                        else if (element.Name == "path" && item.Name == "points")
                        {
                            string[] coor = item.Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < coor.Length; i++)
                            {
                                string[] split = coor[i].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                for (int ii = 0; ii < split.Length; ii++)
                                    split[ii] = (Math.Round(double.Parse(split[ii], CultureInfo.InvariantCulture) * 100) / 100).ToString(CultureInfo.InvariantCulture);

                                coor[i] = string.Empty;
                                foreach (var sub in split)
                                    coor[i] += "," + sub;
                                coor[i] = coor[i].Trim(',');
                            }

                            string rslt = string.Empty;
                            foreach (var sub in coor)
                                rslt += " " + sub;
                            rslt = rslt.Trim(' ');

                            element.SetAttribute(item.LocalName, item.NamespaceURI, rslt);
                        }
                        else
                            element.SetAttribute(item.LocalName, item.NamespaceURI, item.Value);
                    }

                }

            foreach (XmlNode item in source.ChildNodes)
            {
                if (item is XmlElement &&
                    (item.NamespaceURI == "http://www.w3.org/2000/svg" ||
                    item.NamespaceURI == "http://creativecommons.org/ns#" ||
                    item.NamespaceURI == "http://purl.org/dc/elements/1.1/" ||
                    item.NamespaceURI == "http://www.w3.org/1999/02/22-rdf-syntax-ns#"))
                {
                    if (item.Name == "g" && !item.HasChildNodes)
                    {
                        // nope
                    }
                    else
                        Element(item as XmlElement, element.AppendElement(item.Prefix, item.LocalName, item.NamespaceURI));
                }

                if (item is XmlText)
                    element.AppendText(item.InnerText);
            }
        }
    }
}
