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
            return "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\akankari\\Source\\Repos\\IntellectTechCareers\\IntellectTechCareers\\App_Data\\Database1.mdf;Integrated Security=True";
        }

        public static SqlConnection getDBConnection()
        {
            string connectionString = DBUtils.getDBConnectionString();
            SqlConnection con = new SqlConnection(connectionString);

            return con;
        }

        public static bool validateUser(string username, string passwd)
        {
            SqlConnection con = getDBConnection(); 
            con.Open();

            SqlCommand command = new SqlCommand("select password, role from dbo.Users where username='" + username + "';", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || reader.Read() == null)
            {
                return false;
            }

            string pwd = Convert.ToString(reader[0]);
            string role = Convert.ToString(reader[1]);

            con.Close();
            if (passwd.Equals(pwd))
                return true;
            else
                return false;
        }
    }
}