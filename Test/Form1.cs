﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            contextMenuStrip1.Renderer = Chromatik.Forms.ChromatikToolStrip.Defaut;
            menuStrip1.Renderer = Chromatik.Forms.ChromatikToolStrip.Defaut;
        }
    }
}
