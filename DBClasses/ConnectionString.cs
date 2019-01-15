using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBClasses
{
   public static class ConnectionString
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = @"Data Source=oosddeserver.database.windows.net;Initial Catalog=TravelExperts;User ID=Eugenia;Password=SAITdem0";
            SqlConnection con = new SqlConnection(connectionString);
            return con;
        }
    }
}
