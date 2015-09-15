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
            return String.Format("Data Source=(LocalDB)\\v11.0; Integrated Security=True; AttachDbFilename={0};",LocalDBConfig.getDBAddress());
            //return "Data Source=(LocalDB)\\v11.0;AttachDbFilename=C:\\Users\\kaumahat\\documents\\visual studio 2012\\Projects\\IntellectTechCareers\\IntellectTechCareers\\App_Data\\Database1.mdf;Integrated Security=True";
        }

        public static SqlConnection getDBConnection()
        {
            string connectionString = DBUtils.getDBConnectionString();
            SqlConnection con = new SqlConnection(connectionString);

            return con;
        }

        public static string validateUserAndGetRole(string username, string passwd)
        {
            SqlConnection con = getDBConnection(); 
            con.Open();

            SqlCommand command = new SqlCommand("select password, role from dbo.Users where username='" + username + "';", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return "INVALID";
            }

            string pwd = Convert.ToString(reader[0]);
            string role = Convert.ToString(reader[1]);

            con.Close();

            if (passwd.Equals(pwd))
                return role;
            else
                return "INVALID";
        }

        public static void registerUser(string uname, string address, DateTime dob, string contact, string email, string gender, string passwd)
        {
            SqlConnection con = getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("insert into Users values ('" + uname + "', '" + passwd + "', 'Candidate')", con);
            command.ExecuteNonQuery();

            command = new SqlCommand("insert into Candidate values ('" + uname  + "', '" + contact + "', '" + 
                address + "','" + email + "','" + gender + "','" + dob + "')", con);

            int rows = command.ExecuteNonQuery();
            con.Close();
        }
    }
}