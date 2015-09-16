using IntellectTechCareers.Models;
using IntellectTechCareers.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Data_Access_Layer
{
    public class AdminDAL
    {
        public static ManagerDashBoardModels getManagerHome()
        {
            ManagerDashBoardModels model = new ManagerDashBoardModels();
            SqlConnection con = DBUtils.getDBConnection();
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