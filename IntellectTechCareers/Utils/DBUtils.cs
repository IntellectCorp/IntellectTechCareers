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

            string passwdHash = StringUtils.getMD5Hash(StringUtils.Reverse(passwd));
            if (passwdHash.Equals(pwd))
                return role;
            else
                return "INVALID";
        }

        public static string getCandidateId(SqlConnection con, string uname)
        {
            SqlCommand command = new SqlCommand("select user_id from dbo.Users where username='" + uname + "';", con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            string candidateId = Convert.ToString(reader[0]);
            reader.Close();

            return candidateId;
        }

        public static void registerUser(string uname, string address, DateTime dob, string contact, string email, string gender, string passwd, string name)
        {
            SqlConnection con = getDBConnection();
            con.Open();

            string passwdHash = StringUtils.getMD5Hash(StringUtils.Reverse(passwd));
            SqlCommand command = new SqlCommand("insert into Users (username, password, role, account_act_date, name) values ('" + uname + "', '" + passwdHash + "', 'candidate','" + DateTime.Today + "'" + name + "');", con);
            command.ExecuteNonQuery();

            command = new SqlCommand("insert into Applicant (candidate_id, name, email_id, contact_num, gender, dob, address)"
            + " values (" + getCandidateId(con, uname) + ",'" + uname + "', '" + email + "', '" + contact + "','" + gender + "','" + dob + "','" + address + "')", con);

            command.ExecuteNonQuery();
            con.Close();
        }
    }
}