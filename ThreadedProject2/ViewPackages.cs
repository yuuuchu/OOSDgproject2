using DBClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * Project: OOSD Threaded Project 2
 * 
* ViewPackages Form with navigation buttons. This page is for displaying package information for
* easier read and access
* 
* Author: Eugenia Chiu / Brandon Ezekiel
* Date: Jan 2019
* Commenter: Eugenia Chiu
*/

namespace ThreadedProject2
{
    public partial class ViewPackages : Form
    {

        public ViewPackages()
        {
            InitializeComponent();
        }

        private Package packages;

        private void GetPackages(int packID)
        {
            try
            {
                packages = PackagesDB.GetPackages(packID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString());
            }
        }

        private void DisplayPackages()
        {
            txtName.Text = packages.PkgName;
            txtSDate.Text = packages.PkgStartDate.ToString();
            txtPrice.Text = packages.PkgBasePrice.ToString();
            richTextBox1.Text = packages.PkgDesc;

        }

        private void ViewPackages_Load(object sender, EventArgs e)
        {
            
        }
        private void btnBack_Click(object sender, EventArgs e)
        { 
           
            this.Close();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // bind combo box

            var packageIDs = "SELECT PackageId FROM Packages WHERE PackageId = @PackageId";

            comboBox1.DataSource = packageIDs;


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            
            //this.GetPackages(packID);
            this.DisplayPackages();

        }

        private void btnSearch_Click_1(object sender, EventArgs e)
        {
            int packageID = Convert.ToInt32(comboBox1.Text);
            this.GetPackages(packageID);
            this.DisplayPackages();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedValue != null)
            {
                using (SqlConnection con = new SqlConnection(ConnectionString.Connection.Value()))
                {
                    con.Open();

                    string sqlCommand = "SELECT PackageId, PkgName, PkgStartDate, PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission "
                                        + "FROM Packages " + "WHERE PackageId = @PkgName";

                    using (SqlCommand cmd = new SqlCommand(sqlCommand, con))
                    {
                        cmd.Parameters.AddWithValue("@PkgName", comboBox1.SelectedValue);

                    }
                }
            }
        }
    }
}
