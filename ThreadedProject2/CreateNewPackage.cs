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

		BindingList<KeyValuePair<string, int>> SelectedList = new BindingList<KeyValuePair<string, int>>();
		BindingSource SelectedSource = new BindingSource();

		private static readonly KeyValuePair<string, int> DummyVal = new KeyValuePair<string, int>("ERR", -999);

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
			lstPackageProducts.ValueMember = "Value";
			lstPackageProducts.DisplayMember = "Key";

            //Bind the DataSource for the comboBox to the list generated from ProductsDB
			comboBoxProducts.DataSource = ProductsDB.ProductsBindingSource;

            //Filter the information based on the SelectValChanged (see below)
			FilterSource.DataSource = FilteredList;
			lstSuppliers.DataSource = FilterSource;

			SelectedSource.DataSource = SelectedList;
			lstPackageProducts.DataSource = SelectedSource;
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

				

				foreach(KeyValuePair<string, int> kva in SelectedList) {

					bool found = false;
					KeyValuePair<string, int> kvX = DummyVal;
					foreach(KeyValuePair<string, int> kvb in FilteredList) {
						if(kva.Value == kvb.Value) {
							found = true;
							kvX = kvb;
							break;
						}
					}

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

		private void BtnAddSelected(object sender, EventArgs e) {
			if(btnAdd.Enabled == true) {
				if (lstSuppliers.SelectedValue != null) {
					KeyValuePair<string, int> val = DummyVal;
					foreach (KeyValuePair<string, int> v in FilteredList) {
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

				if (SelectedList.Count == 5) {
					btnAdd.Enabled = false;
					btnAdd.BackColor = ColorDisabled;
				}
			}

			
			
		}

		public static Control FindFocusedControl(Control control) {
			var container = control as IContainerControl;
			while (container != null) {
				control = container.ActiveControl;
				container = control as IContainerControl;
			}
			return control;
		}

		private void MoveItem(object sender, KeyEventArgs e) {
			if (e.KeyCode == Keys.Right) {
				if(btnAdd.Enabled == true) {
					if (lstSuppliers.SelectedIndex != -1) {
						BtnAddSelected(null, null);
						int xI = lstSuppliers.SelectedIndex;
						xI--;
						if (xI >= -1) {
							lstSuppliers.SelectedIndex -= 1;
						}
					}
				}
				
			}
		}
	}
}
