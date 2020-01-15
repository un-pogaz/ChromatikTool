using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;


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
            /// RotorEnigma Ia = RotorEnigma.I;
            /// Ia.InitialPosition = Ia.OperatingAlphabet[5];
            /// Hexa h = new Hexa(byte.MaxValue);
            /// 
            /// Enigma enigma1 = new Enigma(Reflector.B, Ia, RotorEnigma.II, RotorEnigma.VIII);
            /// enigma1.ToString();
            /// string test = enigma1.Process("HELLO WORLD !");
            /// enigma1.Reset();
            /// string rslt0 = enigma1.Process(test);
            /// 
            /// Enigma enigma3 = enigma1.Clone(true);
            /// string rslt3 = enigma3.Process(test);

            ;

            string t1 = Settings.Args.GetNextArg("--test1");
            string t2 = Settings.Args.GetNextArg("--test2");
            
            //System.Globalization.Localization.QtTranslation trs = System.Globalization.Localization.QtTranslation.LoadTranslation(@"for_translation_sigil_sigil_fr.ts.xml");
            //trs.Save("test.ts.xml");

            /*
            System.Globalization.Localization.Xliff xliff = System.Globalization.Localization.Xliff.LoadXliff("XLIFF_2.xlf");

            xliff[0].ID = null;
            xliff[0].ID = "f3";
            xliff[0].ID = "f2";
            */
            

            List<string> rslt = new List<string>();

            foreach (var item in Directory.EnumerateFiles(@"D:\Ambient music\Brian Eno", "*.mp3", System.IO.SearchOption.AllDirectories))
            {
                MusicMetaData meta = new MusicMetaData(item)


                ;
            }
            ;

            XmlDocument xml = XmlDocumentCreate.DocumentXML("<xml><span>kkkkkkk</span> <span de=\"\">yyyy</span><i> 65246541 </i><span>sdfwsfd</span></xml>");
            
            XmlDocumentWriter.Document("test.0.xml", xml, DocumentType.HTML5);

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
