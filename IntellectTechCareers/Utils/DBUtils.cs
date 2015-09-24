using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Utils
{
    public class DBUtils
    {
        public static SqlConnection getDBConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["databaseConnectionString"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);

            return con;
        }
    }
}