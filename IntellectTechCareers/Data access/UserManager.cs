using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntellectTechCareers.Utils;
using System.Data.SqlClient;

namespace IntellectTechCareers.Data_access
{
    public class UserManager
    {
        public void addUser()
        {

            string queryString = "select username, password, role from dbo.Users;";
            string connectionString = DBUtils.getDBConnectionString();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand command = new SqlCommand(queryString, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2}", reader[0], reader[1], reader[2]);
                }
            }
        }
    }
}