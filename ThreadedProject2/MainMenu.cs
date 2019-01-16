﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * Project: OOSD Threaded Project 2
 * 
* Main Menu Form with navigation buttons
* 
* Author: Eugenia Chiu
* Date: Jan 2019
* Commenter: Eugenia Chiu
*/

namespace ThreadedProject2
{
    public partial class MainMenu : Form
    {
        //Initialize main form
        public MainMenu()
        {
            InitializeComponent();
        }

        //Directs to view packages page
        private void btnViewPackages_Click(object sender, EventArgs e)
        {
			new ViewPackages().ShowDialog(this);
        }

        //Directs to view suppliers page
        private void button1_Click(object sender, EventArgs e)
        {
			new ViewSuppliers().ShowDialog(this);
        }

        //Exit application
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //directs to Addpackages page
		private void btnAddPackages_Click(object sender, EventArgs e) {
			new CreateNewPackage().ShowDialog(this);
		}
	}
}
