using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClasses {
	public static class PackagesDB {

		public static List<Package> GetPackages(int supplierID) {
			List<Package> lp = new List<Package>();
			using(SqlConnection con = new SqlConnection(ConnectionString.Connection.Value())) {
				con.Open();
				using(SqlCommand cmd = new SqlCommand("SELECT * FROM Products PD FULL OUTER JOIN Products_Suppliers PS ON PS.ProductId = PD.ProductId WHERE PS.SupplierId = @sid;", con)) {
					cmd.Parameters.AddWithValue("@sid", supplierID);
					using(SqlDataReader sdr = cmd.ExecuteReader()) {
						lp.Add(sdr.);
											
					}
				}
			}

			return null;
		}
	}
}
