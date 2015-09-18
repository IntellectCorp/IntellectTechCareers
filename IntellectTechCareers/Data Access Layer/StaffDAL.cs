using IntellectTechCareers.Models;
using IntellectTechCareers.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Data_Access_Layer
{
    public class StaffDAL
    {
        public static StaffDashBoardModel getStaffHome(User user)
        {
            StaffDashBoardModel model = new StaffDashBoardModel();
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("SELECT right_to_post,right_to_schedule,right_to_publish FROM dbo.Staff WHERE staff_id='" + user.user_id + "';", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }
            if (Convert.ToBoolean(reader[0]))
            {
                model.RightToPost = "Yes";
            }
            else
            {
                model.RightToPost = "No";
            }
            if (Convert.ToBoolean(reader[1]))
            {
                model.RightToScheduleInterview = "Yes";
            }
            else
            {
                model.RightToScheduleInterview = "No";
            }
            if (Convert.ToBoolean(reader[2]))
            {
                model.RightToReleaseResults = "Yes";
            }
            else
            {
                model.RightToReleaseResults = "No";
            }
            reader.Close();

            command = new SqlCommand("SELECT COUNT(1) FROM dbo.Job WHERE posted_by='" + user.user_id + "';", con);
            reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }
            model.JobPosted = Convert.ToInt32(reader[0]);
            reader.Close();

            command = new SqlCommand("SELECT COUNT(1) FROM dbo.Interview WHERE scheduled_by='" + user.user_id + "';", con);
            reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }
            model.InterviewsScheduled = Convert.ToInt32(reader[0]);
            reader.Close();

            command = new SqlCommand("SELECT COUNT(1) FROM dbo.Results WHERE released_by='" + user.user_id + "';", con);
            reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }
            model.ResultsReleased = Convert.ToInt32(reader[0]);
            reader.Close();
            con.Close();
            return model;
        }
    }
}