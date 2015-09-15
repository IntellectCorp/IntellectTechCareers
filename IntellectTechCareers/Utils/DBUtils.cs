using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using IntellectTechCareers.Models;

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

        public static User validateUserAndGetRole(string username, string passwd)
        {
            SqlConnection con = getDBConnection(); 
            con.Open();

            SqlCommand command = new SqlCommand("select user_id, password, role, name, state from dbo.Users where username='" + username + "';", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }

            //Creating a user object
            User user = new User();
            user.user_id = Convert.ToInt32(reader[0]);
            string pwd = Convert.ToString(reader[1]);
            user.role = Convert.ToString(reader[2]);
            user.name = Convert.ToString(reader[3]);
            user.state = Convert.ToString(reader[4]);
            user.username = username;

            con.Close();
            string passwdHash = StringUtils.getMD5Hash(StringUtils.Reverse(passwd));
            if (!passwdHash.Equals(pwd))
                user.role = "INVALID";

            return user;
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
            SqlCommand command = new SqlCommand("insert into Users (username, password, role, account_act_date, name, state) values ('" + uname + "', '" + passwdHash + "', 'candidate','" + DateTime.Today + "'" + name + "', 'active');", con);
            command.ExecuteNonQuery();

            command = new SqlCommand("insert into Applicant (candidate_id, name, email_id, contact_num, gender, dob, address)"
            + " values (" + getCandidateId(con, uname) + ",'" + uname + "', '" + email + "', '" + contact + "','" + gender + "','" + dob + "','" + address + "')", con);

            command.ExecuteNonQuery();
            con.Close();
        }

        public static ManagerDashBoardModels getManagerHome()
        {
            ManagerDashBoardModels model = new ManagerDashBoardModels();
            SqlConnection con = getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select COUNT(1) from dbo.Job ;", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }
            model.TotalJobs = Convert.ToString(reader[0]);
            model.TotalResults = "5";
            model.ResultsNotReleased = "15";
            model.UnscheduledInterviewJobs = "2";
            model.TotalScheduledInterview = "3";

            con.Close();
            return model;
        }
    }
}