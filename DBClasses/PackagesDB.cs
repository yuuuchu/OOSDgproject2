using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBClasses;

/**
 * Project: OOSD Threaded Project 2
* Object class for building Packages objects and getting a list of such.
* 
* Author: Brandon Ezekiel
* Date: Jan 2019
* Commenter: Hayden Belanger
*/

namespace ThreadedProject2
{

    public class PackagesDB
    {
        public static string connectionString = ConnectionString.Connection.Value();

        public static Package GetPackages(int packID)
        {

            List<Package> pack = new List<Package>();
            Package p;
            SqlConnection con = new SqlConnection(ConnectionString.Connection.Value());
            string query = "SELECT PackageId, PkgName, PkgStartDate, PkgEndDate, PkgDesc FROM Packages WHERE PackageId = @PackageId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@PackageId", packID);
            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    p = new Package();
                    p.PackageId = (int)reader["PackageId"];
                    p.PkgName = reader["PkgName"].ToString();
                    p.PkgStartDate = (DateTime)reader["PkgStartDate"];
                    p.PkgEndDate = (DateTime)reader["PkgEndDate"];
                    p.PkgDesc = reader["PkgDesc"].ToString();
                    p.PkgBasePrice = (decimal)reader["PkgBasePrice"];

                    pack.Add(p);

                }
                return pack.Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }
    }
}