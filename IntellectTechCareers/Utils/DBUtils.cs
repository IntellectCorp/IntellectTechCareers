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
            //return "Data Source=(LocalDB)\\v11.0;AttachDbFilename=\\App_Data\\Database2.mdf;Integrated Security=True";
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

            if (reader == null || !reader.Read())
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

        public static void registerUser(string uname, string address, DateTime dob, string contact, string email, string gender)
        {
            SqlConnection con = getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("insert into Candidate values (@username, @contact_no, @address, @date_of_birth, @email_id, @gender)", con);
            command.Parameters.AddWithValue("@username", uname);
            command.Parameters.AddWithValue("@contact_no", contact);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@date_of_birth", dob);
            command.Parameters.AddWithValue("@email_id", email);
            command.Parameters.AddWithValue("@gender", gender);

            command.ExecuteNonQuery();            
            con.Close();
        }
    }
}