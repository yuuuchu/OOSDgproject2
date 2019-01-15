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
        public CreateNewPackage()
        {
            InitializeComponent();


			using(SqlConnection con = new SqlConnection(ConnectionString.Connection.Value())) {
				con.Open();
				using(SqlCommand cmd = new SqlCommand("SELECT * FROM Suppliers;")) {
					using(SqlDataReader sdr = cmd.ExecuteReader()) {

					}
				}
			}
        }

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

		private void SelectValChanged(object sender, EventArgs e) {

		}
	}
}
