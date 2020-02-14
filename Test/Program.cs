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
using System.Globalization.Localization;


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

            System.Text.RegularExpressions.CompiledRegex comp = System.Text.RegularExpressions.CompiledRegex.LoadAssemblyFile(@"F:\Projet\GitHub\RegexLib.dll");

            comp[0].

            XmlHtmlEntity h = new XmlHtmlEntity("nbsp", 160, false);

            string t1 = Settings.Args.GetNextArg("--test1");
            ;
            string t2 = t1;

            t2 = "00";

            string[] tbl = new string[] {
                HashKey.DigestFile(HashAlgorithmEnum.MD5, "XLIFF_2.xlf"),
                HashKey.DigestFile(HashAlgorithmEnum.SHA1, "XLIFF_2.xlf"),
                HashKey.DigestFile(HashAlgorithmEnum.SHA256, "XLIFF_2.xlf"),
                HashKey.DigestFile(HashAlgorithmEnum.SHA384, "XLIFF_2.xlf"),
                HashKey.DigestFile(HashAlgorithmEnum.SHA512, "XLIFF_2.xlf"),
                HashKey.DigestFile(HashAlgorithmEnum.KeyedHashAlgorithm, "XLIFF_2.xlf"),
                HashKey.DigestFile(HashAlgorithmEnum.RIPEMD160, "XLIFF_2.xlf"),
            };

            tbl.ForEach(Incremente);

            //System.Globalization.Localization.QtTranslation trs = System.Globalization.Localization.QtTranslation.LoadTranslation(@"for_translation_sigil_sigil_fr.ts.xml");
            //trs.Save("test.ts.xml");

            ;
            Xliff xliff = Xliff.LoadXliff("XLIFF_2.xlf");

            string fI = xliff.IDs[1];
            XliffFile fS = xliff.Identifieds[1];


            xliff[0].ID = "f3";
            xliff[0].ID = "f2";


            XmlDocument xml = XmlDocumentCreate.DocumentXML("<xml><span>kkkkkkk</span> <span de=\"\">yyyy</span><i> 65246541 </i><span>sdfwsfd</span></xml>");
            
            XmlDocumentWriter.Document("test.0.xml", xml, DocumentType.HTML5);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

       

        static public void Incremente(string input)
        {
            input = "456";
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
