using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Chromatik.Xml;

/// <summary>
/// https://archive.codeplex.com/?p=xmlrichtextbox
/// {
/// 	"Name": "The MIT License (MIT)",
/// 	"Text": "Copyright (c) 2015 Jeff Jones\r\n\r\nPermission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the \"Software\"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:\r\n\r\nThe above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.\r\n\r\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.",
/// 	"Id": 605595,
/// 	"ShortName": "MIT",
/// 	"StartDate": "\/Date(1450126856507-0800)\/"
/// }
/// </summary>

namespace Chromatik.Forms
{
    public partial class uscXmlRichTextBox : RichTextBox
    {
        #region Constructor
        public uscXmlRichTextBox()
        {
            InitializeComponent();

            this.Font = new System.Drawing.Font("Consolas", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            miCopyRtf.Click += new EventHandler(miCopyRtf_Click);
            miCopyText.Click += new EventHandler(miCopyText_Click);
            miSelectAll.Click += new EventHandler(miSelectAll_Click);
        }
        public uscXmlRichTextBox(XmlTokenColor XmlTokenColor) : this()
        {
            this.XmlTokenColor = XmlTokenColor;
        }
        #endregion Constructor

        #region Properties
        public override string Text
        {
            get { return _Xml; }
            set { Xml = value; }
        }

        private string _Xml = "";
        public string Xml
        {
            get { return _Xml; }
            set 
            {
                if (value == null)
                    value = "";

                _Xml = value;
                SetXml(_Xml); 
            }
        }
        
        public XmlTokenColor XmlTokenColor { get; set; } = new XmlTokenColor();
        #endregion Properties

        #region Private Methods
        private void SetXml(string s)
        {
            if (String.IsNullOrEmpty(s)) return;

            XDocument xdoc = XDocument.Parse(s);

            string formattedText = xdoc.ToString().Trim();

            if (String.IsNullOrEmpty(formattedText)) return;

            XmlStateMachine machine = new XmlStateMachine(XmlTokenColor);
            
            if (s.StartsWith("<?"))
            {
                string xmlDeclaration = machine.GetXmlDeclaration(s);
                if(xmlDeclaration != String.Empty) formattedText =  xmlDeclaration + Environment.NewLine + formattedText;
            }

            int location = 0;
            int failCount = 0;
            int tokenTryCount = 0;
            XmlTokenType ttype = XmlTokenType.Unknown;
            while (location < formattedText.Length)
            {
                string token = machine.GetNextToken(formattedText, location, out ttype);
                Color color = machine.GetTokenColor(ttype);
                this.AppendText(token, color);
                location += token.Length;
                tokenTryCount++;

                // Check for ongoing failure
                if (token.Length == 0) failCount++;
                if (failCount > 10 || tokenTryCount > formattedText.Length)
                {
                    string theRestOfIt = formattedText.Substring(location, formattedText.Length - location);
                    //this.AppendText(Environment.NewLine + Environment.NewLine + theRestOfIt); // DEBUG
                    this.AppendText(theRestOfIt);
                    break;
                }
            }
        }
        #endregion Private Methods

        #region Context Menu
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(this.SelectedText))
            {
                miCopyText.Enabled = false;
                miCopyRtf.Enabled = false;
            }
            else
            {
                miCopyText.Enabled = true;
                miCopyRtf.Enabled = true;
            }
        }

        void miCopyText_Click(object sender, EventArgs e)
        {
            string s = this.SelectedText;
            try
            {
                XDocument doc = XDocument.Parse(s);
                s = doc.ToString();
            }
            catch { }
            Clipboard.SetText(s);
        }

        void miCopyRtf_Click(object sender, EventArgs e)
        {
            DataObject dto = new DataObject();
            dto.SetText(this.SelectedRtf, TextDataFormat.Rtf);
            dto.SetText(this.SelectedText, TextDataFormat.UnicodeText);
            Clipboard.Clear();
            Clipboard.SetDataObject(dto);
        }

        private void miSelectAll_Click(object sender, EventArgs e)
        {
            this.SelectAll();
        }
        #endregion Context Menu
    }

    #region Extension Methods
    static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
    #endregion Extension Methods
}
