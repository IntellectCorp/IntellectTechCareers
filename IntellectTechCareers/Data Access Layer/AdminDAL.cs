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
            model.ResultsNotReleased = model.TotalScheduledInterview - model.TotalResults;
            con.Close();
            return model;
        }

        public static void addNewJobRole(JobRole jobRole)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("insert into JobRole ( job_role) values ( '" + jobRole.Name + "');", con);
            command.ExecuteNonQuery();

            con.Close();
        }

        public static void addNewQualification(Qualification model)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("insert into Qualification ( qualification, type) values ( '" + model.Name + "', '" + model.Type + "');", con);
            command.ExecuteNonQuery();

            con.Close();
        }

        public static void UpdateStaffResponsibilities(Staff staff)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            int post = (staff.RightToPost ? 1 : 0);
            int publish = (staff.RightToPublish ? 1 : 0);
            int schedule = (staff.RightToSchedule ? 1 : 0);

            SqlCommand command = new SqlCommand("update dbo.Staff set right_to_post = " + post +
                " , right_to_publish = " + publish + " , right_to_schedule = " + schedule + " where staff_id = " + staff.StaffId, con);
            command.ExecuteNonQuery();

            con.Close();
        }

        public static void AddStaff(Staff staff)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            string passwdHash = StringUtils.getMD5Hash(StringUtils.Reverse(staff.Password));
            SqlCommand command = new SqlCommand("insert into Users (username, password, role, account_act_date, name, state) values ('"
                + staff.StaffUserName + "', '" + passwdHash + "', 'staff','" +
                DateTime.Today + "', '" + staff.StaffName + "', 'Active');", con);
            command.ExecuteNonQuery();

            int publish = (staff.RightToPublish ? 1 : 0);
            int post = (staff.RightToPost ? 1 : 0);
            int schedule = (staff.RightToSchedule ? 1 : 0);

            command = new SqlCommand("insert into Staff (staff_id, right_to_schedule, right_to_publish, right_to_post)"
            + " values (" + AccountDAL.getCandidateId(con, staff.StaffUserName) + "," + schedule + "," + publish + "," + post + ")", con);

            command.ExecuteNonQuery();
            con.Close();
        }
    }
}