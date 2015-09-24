using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using IntellectTechCareers.Models;

namespace IntellectTechCareers.Utils
{
    public class AccountDAL
    {
        public static User validateUserAndGetRole(string username, string passwd)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select user_id, password, role, name, state, account_act_date from dbo.Users where username='" + username + "';", con);
            SqlDataReader reader = command.ExecuteReader();

            //Creating a user object
            User user = new User();

            if (reader == null || !reader.Read())
            {
                user.role = "INVALID";
                return user;
            }

            if (Convert.ToString(reader[4]).Equals("Disabled"))
            {
                user.role = "Disabled";
                return user;
            }

            user.user_id = Convert.ToInt32(reader[0]);
            string pwd = Convert.ToString(reader[1]);
            user.password = pwd;
            user.role = Convert.ToString(reader[2]);
            user.name = Convert.ToString(reader[3]);
            user.state = Convert.ToString(reader[4]);
            user.AccountActiveDate = Convert.ToDateTime(reader[5]);
            user.username = username;

            con.Close();
            string passwdHash = StringUtils.getMD5Hash(StringUtils.Reverse(passwd));
            if (!passwdHash.Equals(pwd))
                user.role = "INVALID";

            return user;
        }

        public static string GetUserName(int userId)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select username from dbo.Users where user_id=" + userId + ";", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                string userName = reader.GetString(0);
                con.Close();
                return userName;
            }

            return "Failed";
        }

        public static void GetNameOfUser(out string name, out string username, int userId)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select username, name from dbo.Users where user_id=" + userId + ";", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                username = reader.GetString(0);
                name = reader.GetString(1);
                con.Close();
            }
            else
            {
                username = "NA";
                name = "NA";
            }
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
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            string passwdHash = StringUtils.getMD5Hash(StringUtils.Reverse(passwd));
            SqlCommand command = new SqlCommand("insert into Users (username, password, role, account_act_date, name, state) values ('" + uname + "', '" + passwdHash + "', 'candidate','" +
                DateTime.Today + "', '" + name + "', 'Active');", con);
            command.ExecuteNonQuery();

            command = new SqlCommand("insert into Applicant (candidate_id, name, email_id, contact_num, gender, dob, address)"
            + " values (" + getCandidateId(con, uname) + ",'" + uname + "', '" + email + "', '" + contact + "','" + gender + "','" + dob + "','" + address + "')", con);

            command.ExecuteNonQuery();
            con.Close();
        }

        public static string IsUserNameAvailable(string userName)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select count(*) from dbo.Users where username='" + userName + "';", con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            int num = Convert.ToInt32(reader[0]);
            if (num == 0)
                return "true";
            else
                return "false";
        }

        public static void DeleteUser(int userId)
        {
            string queryUser = "update dbo.Users set state='Disabled' where user_id = " + userId + ";";

            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(queryUser, con);
            command.ExecuteNonQuery();

            con.Close();
        }

        public static bool ChangePassword(int userId, string passwdHash)
        {
            try
            {
                SqlConnection con = DBUtils.getDBConnection();
                con.Open();

                SqlCommand command = new SqlCommand("update dbo.Users set password = '" + passwdHash + "' where user_id=" + userId + ";", con);
                command.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}