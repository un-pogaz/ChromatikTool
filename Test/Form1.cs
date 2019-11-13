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

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            contextMenuStrip1.Renderer = Chromatik.Forms.ChromatikToolStrip.Defaut;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += wc_DownloadProgressChanged;
                wc.DownloadFileAsync(
                    // Param1 = Link of file
                    new System.Uri("https://www2.yggtorrent.ch/rss?action=generate&type=cat&id=2140&passkey=BMOENcZtYHNA6vCowZpBmDn9zacrYKxp"),
                    // Param2 = Path to save
                    Settings.TempFolder + "yggtorrent.rss"
                );
            }*/
        }

        // Event to track the progress
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
        }
    }
}
