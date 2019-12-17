using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace Test
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Random ran = new Random();
            List<string> lst = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                byte[] ba = new byte[byte.MaxValue + 1];
                ran.NextBytes(ba);
                string s = "ByteCharacter.b" + ba.ToOneString(", ByteCharacter.b");
                lst.Add("new char[] { " + s + " }");
            }

            textBox1.Text = lst.ToOneString(WhiteCharacter.EndLineWindows + WhiteCharacter.EndLineWindows);
        }
    }
}
