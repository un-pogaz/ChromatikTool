using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.Machine;
using System.Xml;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;


namespace Test
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string[] ss = new string[0].Concat(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)
            );

            string[] xx = new string[0].Concat(
                Settings.ApplicationFolder,
                Settings.LocalApplicationData,
                Settings.ProgramData
            );

            VersionClass vv = new VersionClass("v 1.0.3");

            RotorEnigma Ia = RotorEnigma.I;
            Ia.InitialPosition = Ia.OperatingAlphabet[5];
            Hexa h = new Hexa(byte.MaxValue);

            Enigma enigma1 = new Enigma(Reflector.B, Ia, RotorEnigma.II, RotorEnigma.VIII);
            enigma1.ToString();
            string test = enigma1.Process("HELLO WORLD !");
            enigma1.Reset();
            string rslt0 = enigma1.Process(test);

            Enigma enigma3 = enigma1.Clone(true);
            string rslt3 = enigma3.Process(test);
            
            XmlDocument xml = XmlCreate.DocumentXML("<xml><span>kkkkkkk</span> <span de=\"\">yyyy</span> 65246541 <span>sdfwsfd</span></xml>");
            renameXMLNode(xml, "span", "stripspan");

            string ddd = xml.OuterXml.Regex("<(|/)stripspan>", "");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        
        public static void renameXMLNode(XmlNode node, string oldName, string newName)
        {
            XmlDocument doc;
            if (node is XmlDocument)
                doc = node as XmlDocument;
            else
                doc = node.OwnerDocument;

            foreach (XmlNode item in node.GetElements())
            {
                if (item.LocalName == oldName && item.Attributes.Count == 0)
                {
                    XmlNode newRoot = doc.CreateElement(newName);
                    XmlNode oldRoot = item;
                    foreach (XmlNode child in item.ChildNodes)
                        newRoot.AppendChild(child.CloneNode(true));
                    
                    XmlNode parent = oldRoot.ParentNode;
                    parent.InsertBefore(newRoot, oldRoot);
                    parent.RemoveChild(oldRoot);
                }
                renameXMLNode(item, oldName, newName);
            }
        }
    }
}
