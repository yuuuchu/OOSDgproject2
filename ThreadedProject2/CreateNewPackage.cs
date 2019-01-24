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
		BindingList<KeyValuePair<string, int>> FilteredList = new BindingList<KeyValuePair<string, int>>();//list on the left
		BindingSource FilterSource = new BindingSource(); //using a bindingsource, makes it so that when you add a value to the list, it will auto update the list view. No button is needed to refresh.

		BindingList<KeyValuePair<string, int>> SelectedList = new BindingList<KeyValuePair<string, int>>(); //list on the right
		BindingSource SelectedSource = new BindingSource(); //using a bindingsource, makes it so that when you add a value to the list, it will auto update the list view. No button is needed to refresh.
		private static readonly KeyValuePair<string, int> DummyVal = new KeyValuePair<string, int>("ERR", -999); //readonly means that you cannot change this value

		private static readonly Color ColorEnabled = Color.FromArgb(82, 117, 167);
		private static readonly Color ColorDisabled = Color.Gray;

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
					//PD is assigning a variable value to the table Products so that PD can be used throughout the query string.
					//FULL OUTER JOIN means taking a FK on one table and a FK on another table that matches and getting values from the joined tables with the same FK.
					//
					//RESEARCH WHAT OUTER AND INNER JOIN MEANS
					//WHERE means filtering a selection to only show matching values. @prod is needed because the string needs to proper 'escape' (convert) to only text values.
					//This is so that it will not break the data in the table.
                    string sqlCommand = "SELECT * FROM Products PD FULL OUTER JOIN Products_Suppliers PS ON " +
                                        "PS.ProductId = PD.ProductId FULL OUTER JOIN Suppliers SP ON "+
                                        "SP.SupplierId = PS.SupplierId WHERE PS.ProductId = @prod;";

                    //using the connection and the string command,
                    using (SqlCommand cmd = new SqlCommand(sqlCommand, con)) {

                        //Take the selected value from the combo box and list/show all the values from the database into this list
						//Get value and assign to parameter.
						cmd.Parameters.AddWithValue("@prod", comboBoxProducts.SelectedValue);

                        //using the data reader, read the data while there is still data in table
						using (SqlDataReader sdr = cmd.ExecuteReader()) {
							while (sdr.Read()) {

								//while there is still data, add it to the list view
								FilteredList.Add(new KeyValuePair<string, int>(sdr.GetString(6), sdr.GetInt32(4)));
							}
						}
					}
				}


				//This method scans through the selectedList and the FilteredList
				//and compares the two lists. If the product exists on the right list, remove that product from the left list

				foreach(KeyValuePair<string, int> kva in SelectedList) {

					bool found = false;
					KeyValuePair<string, int> kvX = DummyVal;
					//string is the product name, int is the product ID
					//for each key Value pair, if the product is on the right list, removed it from the left list.
					foreach(KeyValuePair<string, int> kvb in FilteredList) {
						if(kva.Value == kvb.Value) {
							found = true;
							kvX = kvb;
							break;
						}
					}

					//this is the remove statement
					if (found) {
						FilteredList.Remove(kvX);
					}
				}


			}

		}

		private void ExitApp(object sender, EventArgs e) {
			Application.Exit();
		}

		private void BackClick(object sender, EventArgs e) {
			this.Close();
		}

		//this method is when you press the right button to add to the new package list
		// Currently, you can only add five values to this empty list for new packages

		private void BtnAddSelected(object sender, EventArgs e) {
			if(btnAdd.Enabled == true) {
				if (lstSuppliers.SelectedValue != null) { //this checks to make sure that the left list has a value selected
					KeyValuePair<string, int> val = DummyVal; //DummyVal is needed because the key value pair cannot be null. See above for variable

					foreach (KeyValuePair<string, int> v in FilteredList) { //filtered list
						if (v.Value == (int)lstSuppliers.SelectedValue) {
							val = v;
							break;
						}
					}

					if (val.Value != -999) {
						FilteredList.Remove(val);

						KeyValuePair<string, int> xa = (KeyValuePair<string, int>)comboBoxProducts.SelectedItem;
						KeyValuePair<string, int> nval = new KeyValuePair<string, int>(xa.Key + " - " + val.Key, val.Value);
						SelectedList.Add(nval);
					}
				}

				if (btnRemove.Enabled == false) {
					btnRemove.Enabled = true;
					btnRemove.BackColor = ColorEnabled;
				}

				if (SelectedList.Count == 5) {
					btnAdd.Enabled = false;
					btnAdd.BackColor = ColorDisabled;
				}

				if(SelectedList.Count > 0) {
					btnCreatePackage.Enabled = true;
					btnCreatePackage.BackColor = ColorEnabled;
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
