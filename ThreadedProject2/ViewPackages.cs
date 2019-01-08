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
    //Packages Page Load
    public partial class ViewPackages : Form
    {
        public ViewPackages()
        {
            InitializeComponent();
        }

        private void ViewPackages_Load(object sender, EventArgs e)
        {

        }

        //Back Button click
        private void btnBack_Click(object sender, EventArgs e)
        { 
           
            this.Close();
            
        }

        //Exit button click
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
