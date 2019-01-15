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

namespace ThreadedProject2
{
    public partial class CreateNewPackage : Form
    {


		BindingList<KeyValuePair<string, int>> FilteredList = new BindingList<KeyValuePair<string, int>>();
		BindingSource FilterSource = new BindingSource();

        public CreateNewPackage()
        {
            InitializeComponent();

			
			

			comboBoxProducts.ValueMember = "Value";
			comboBoxProducts.DisplayMember = "Key";
			lstSuppliers.ValueMember = "Value";
			lstSuppliers.DisplayMember = "Key";

			comboBoxProducts.DataSource = ProductsDB.ProductsBindingSource;
			FilterSource.DataSource = FilteredList;
			lstSuppliers.DataSource = FilterSource;
        }

		private void SelectValChanged(object sender, EventArgs e) {

			if(comboBoxProducts.SelectedValue != null) {
				using (SqlConnection con = new SqlConnection(ConnectionString.Connection.Value())) {
					FilteredList.Clear();
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT * FROM Products PD FULL OUTER JOIN Products_Suppliers PS ON PS.ProductId = PD.ProductId FULL OUTER JOIN Suppliers SP ON SP.SupplierId = PS.SupplierId WHERE PS.ProductId = @prod;", con)) {
						cmd.Parameters.AddWithValue("@prod", comboBoxProducts.SelectedValue);
						using (SqlDataReader sdr = cmd.ExecuteReader()) {
							while (sdr.Read()) {
								FilteredList.Add(new KeyValuePair<string, int>(sdr.GetString(6), sdr.GetInt32(4)));
							}
						}
					}
				}
			}
			
		}
	}
}
