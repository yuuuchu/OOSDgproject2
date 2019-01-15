using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBClasses {
	public class ProductsDB {

		public static BindingList<KeyValuePair<string, int>> Products = new BindingList<KeyValuePair<string, int>>();
		public static BindingSource ProductsBindingSource = new BindingSource();

		public static void InitializeList() {
			using (SqlConnection con = new SqlConnection(ConnectionString.Connection.Value())) {
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM Products;", con)) {
					using (SqlDataReader sdr = cmd.ExecuteReader()) {
						while (sdr.Read()) {
							Products.Add(new KeyValuePair<string, int>(sdr.GetString(1), sdr.GetInt32(0)));
						}


					}
				}
			}

			ProductsBindingSource.DataSource = Products;
		}
	}
}
