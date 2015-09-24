using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.ServiceModel.Channels;
using System.Web.Script.Serialization;

namespace IntellectTechWS
{
    public class UserNameValidation : IUserNameValidation
    {
        public string CheckAvailability(string userName) 
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["databaseConnectionString"].ConnectionString;
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();

                SqlCommand command = new SqlCommand("select count(*) from dbo.Users where username='" + userName + "';", con);
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();
                
                bool isUserNameAvailable = (Convert.ToInt32(reader[0]) == 0 );

                object jsonObject = new { available = isUserNameAvailable };
                var json = new JavaScriptSerializer().Serialize(jsonObject);
                return json.ToString();
            }
            catch (Exception ex)
            {
                return string.Format("Exception : {0}", ex.Message);
            }
        }
    }
}
