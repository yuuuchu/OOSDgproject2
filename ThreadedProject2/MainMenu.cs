﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThreadedProject2
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnViewPackages_Click(object sender, EventArgs e)
        {
			new ViewPackages().ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
			new ViewSuppliers().ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
