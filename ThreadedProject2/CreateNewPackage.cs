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
using DBClasses;

/**
 * Project: OOSD Threaded Project 2
* CreateNewPackages GUI code to display suppliers, products and create new packages
* Author: Hayden Belanger and Eugenia Chiu
* Date: Jan 2019
* Commenter: Eugenia Chiu
*/

namespace ThreadedProject2
{
    public partial class CreateNewPackage : Form
    {
        //Create new empty BindingList and BindingSource
		BindingList<KeyValuePair<string, int>> FilteredList = new BindingList<KeyValuePair<string, int>>();
		BindingSource FilterSource = new BindingSource();

        public CreateNewPackage()
        {
            //Initialize form load and data
            InitializeComponent();
            
            //Display Key and Values in appropriate places on GUI
			comboBoxProducts.ValueMember = "Value";
			comboBoxProducts.DisplayMember = "Key";
			lstSuppliers.ValueMember = "Value";
			lstSuppliers.DisplayMember = "Key";

            //Bind the DataSource for the comboBox to the list generated from ProductsDB
			comboBoxProducts.DataSource = ProductsDB.ProductsBindingSource;

            //Filter the information based on the SelectValChanged (see below)
			FilterSource.DataSource = FilteredList;
			lstSuppliers.DataSource = FilterSource;
        }

		private void SelectValChanged(object sender, EventArgs e) {

            //If the comboBox is not null
			if(comboBoxProducts.SelectedValue != null) {

                //Establish connection to database and clear the filtered list
				using (SqlConnection con = new SqlConnection(ConnectionString.Connection.Value())) {
					FilteredList.Clear();
					con.Open();

                    //sql Command
                    string sqlCommand = "SELECT * FROM Products PD FULL OUTER JOIN Products_Suppliers PS ON " + 
                                        "PS.ProductId = PD.ProductId FULL OUTER JOIN Suppliers SP ON "+
                                        "SP.SupplierId = PS.SupplierId WHERE PS.ProductId = @prod;";

                    //using the connection and the string command,
                    using (SqlCommand cmd = new SqlCommand(sqlCommand, con)) {

                        //Ask Hayden about this part
						cmd.Parameters.AddWithValue("@prod", comboBoxProducts.SelectedValue);

                        //using the data reader, read the data while there is still data in table
						using (SqlDataReader sdr = cmd.ExecuteReader()) {
							while (sdr.Read()) {
								FilteredList.Add(new KeyValuePair<string, int>(sdr.GetString(6), sdr.GetInt32(4)));
							}
						}
					}
				}
			}
			
		}

        //Back button closes this form and returns to main menu form
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Exit button closes the application
        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
