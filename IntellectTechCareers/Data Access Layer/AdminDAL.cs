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
        public static ManagerDashBoardModel getManagerHome()
        {
            ManagerDashBoardModel model = new ManagerDashBoardModel();
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select COUNT(1) from dbo.Job ;", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }
            model.TotalJobs = Convert.ToInt32(reader[0]);
            reader.Close();

            command = new SqlCommand("select COUNT(1) from dbo.Results ;", con);
            reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }
            model.TotalResults = Convert.ToInt32(reader[0]);
            reader.Close();

            command = new SqlCommand("select COUNT(1) from dbo.Interview ;", con);
            reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }
            model.TotalScheduledInterview = Convert.ToInt32(reader[0]);
            reader.Close();

            //command = new SqlCommand("select COUNT(DISTINCT job_id) from dbo.Application WHERE status_code=3;", con);
            //reader = command.ExecuteReader();

            //if (reader == null || !reader.Read())
            //{
            //    return null;
            //}
            //model.ResultsNotReleased = Convert.ToString(reader[0]);
            //reader.Close();

            //command = new SqlCommand("select COUNT(DISTINCT job_id) from dbo.Application WHERE status_code=1;", con);
            //reader = command.ExecuteReader();

            //if (reader == null || !reader.Read())
            //{
            //    return null;
            //}
            //model.UnscheduledInterviewJobs = Convert.ToString(reader[0]);
            //reader.Close();

            model.UnscheduledInterviewJobs = model.TotalJobs - model.TotalScheduledInterview;
            model.ResultsNotReleased = model.TotalScheduledInterview - model.TotalResults ;
            con.Close();
            return model;
        }
    }
}