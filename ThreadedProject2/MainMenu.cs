using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * SchoolProject: OOSD 2019 CPRG 200
 * Author: Eugenia Chiu
 * Date: Jan 2019
 * 
 * */
namespace ThreadedProject2
{
    //Main Menu setup
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        //View Package button click
        private void btnViewPackages_Click(object sender, EventArgs e)
        {
            ViewPackages viewPackages = new ViewPackages();
            viewPackages.Show();
            this.Hide();

            viewPackages.FormClosing += backButton;
        }

        //When backbutton is clicked, show current page
        private void backButton (object sender, FormClosingEventArgs e)
        {
            this.Show();
        }
    }
}
