using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Utils
{
    public class DBUtils
    {
        public static string getDBConnectionString()
        {
            return String.Format("Data Source=(LocalDB)\\v11.0; Integrated Security=True; AttachDbFilename={0};", LocalDBConfig.getDBAddress());
            //return "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\kaumahat\\documents\\visual studio 2012\\Projects\\IntellectTechCareers\\IntellectTechCareers\\App_Data\\Database1.mdf;Integrated Security=True";
        }

        public static SqlConnection getDBConnection()
        {
            string connectionString = getDBConnectionString();
            SqlConnection con = new SqlConnection(connectionString);

            return con;
        }
    }
}