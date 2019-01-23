using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

namespace DBClasses
{
    public static class ViewPackagesDB
    {
        public static Package GetPackage(int packageID)
        {
            Package pack = null;
            SqlConnection con = new SqlConnection(ConnectionString.Connection.Value());
            string selectStatement = "SELECT PackageId, PkgName, PkgStartDate, PkgEndDate, PkgDesc, PkgBasePrice, PkgAgencyCommission " +
                                     "FROM Packages " + "WHERE PackageId = @PackageId";
            SqlCommand cmd = new SqlCommand(selectStatement, con);
            cmd.Parameters.AddWithValue("@PackageId", packageID);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if(reader.Read())
                {
                    pack = new Package();
                    pack.PackageId = (int)reader["PackageId"];
                    pack.PkgName = reader["PkgName"].ToString();
                    pack.PkgStartDate = (DateTime)reader["PkgStartDate"];
                    pack.PkgEndDate = (DateTime)reader["PkgEndDate"];
                    pack.PkgDesc = reader["PkgDesc"].ToString();
                    pack.PkgBasePrice = (int)reader["PkgBasePrice"];
                    pack.PkgAgencyCommission = (int)reader["PkgAgencyCommission"];
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            return pack;

        }
    }
}
