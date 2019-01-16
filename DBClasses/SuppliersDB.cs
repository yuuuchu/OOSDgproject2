using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * Project: OOSD Threaded Project 2
 * 
* SuppliersDB class binds the data retrieved from the database to a KeyValue pair List
* and allows for display by binding data to list.
* 
* Author: Hayden Belanger
* Date: Jan 2019
* Commenter: Eugenia Chiu
*/

namespace DBClasses {
	public class SuppliersDB {

        //Create empty list and set up Data Binding Source
        public static BindingList<KeyValuePair<string, int>> Suppliers = new BindingList<KeyValuePair<string, int>>();
		public static BindingSource SuppliersBindingSource = new BindingSource();

        //Create the list and add values from database to list
        //InitializeList is just a variablename.
        public static void InitializeList() {

            //Establish and open the connection to database using connection string
            using (SqlConnection con = new SqlConnection(ConnectionString.Connection.Value())) {
				con.Open();

                //Once connection is open, run SQL query command. In this case, select all data from Products Table
                //cmd is passed the query and connection string
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Suppliers;", con)) {
					using (SqlDataReader sdr = cmd.ExecuteReader()) {

                        //While the data table still has data, continue reading
                        while (sdr.Read()) {

                            //Add the data (KeyValue Pairs) to the empty list
                            Suppliers.Add(new KeyValuePair<string, int>(sdr.GetString(1), sdr.GetInt32(0)));
						}
					}
				}
			}

            //Make the filled list the SuppliersBindingSource (for later use)
            SuppliersBindingSource.DataSource = Suppliers;
		}
	}
}
