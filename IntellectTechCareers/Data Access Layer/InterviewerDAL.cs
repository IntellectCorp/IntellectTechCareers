using IntellectTechCareers.Models;
using IntellectTechCareers.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Data_Access_Layer
{
    public class InterviewerDAL
    {
        public static void releaseResultToDB(ResultModel resultModel, User user)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();
            SqlCommand command;

            List<CandidateResult> selectedCandidates = new List<CandidateResult>();
            int count = 0;
            foreach (var item in resultModel.Candidates)
            {
                if (item.IsSelected)
                {
                    selectedCandidates.Add(item);
                    count++;

                    command = new SqlCommand("UPDATE Application SET status_code='S', status='Selected' WHERE candidate_id=" + item.UserID + " ;", con);
                    command.ExecuteNonQuery();

                    command = new SqlCommand("UPDATE Users SET state='Selected' WHERE user_id=" + item.UserID + " ;", con);
                    command.ExecuteNonQuery();
                }
                else
                {
                    command = new SqlCommand("UPDATE Application SET status_code='R', status='Rejected' WHERE candidate_id=" + item.UserID + " ;", con);
                    command.ExecuteNonQuery();

                    command = new SqlCommand("UPDATE Users SET state='Blocked', account_act_date='" + DateTime.Now.AddDays(90).ToShortDateString() + "' WHERE user_id=" + item.UserID + " ;", con);
                    command.ExecuteNonQuery();
                }
            }
            resultModel.numOfCandidatesSelected = count;

            string selectedCandidatesSet = String.Join(",", selectedCandidates.Select(x => x.UserID.ToString()).ToArray());
            command = new SqlCommand("INSERT INTO dbo.Results (job_id, declaration_date, num_of_candidates_selected, candidates, released_by) values (" + resultModel.JobId + ", '" + DateTime.Now.ToShortDateString() + "', " + resultModel.numOfCandidatesSelected + ", '" + selectedCandidatesSet + "', " + user.user_id + " );", con);
            command.ExecuteNonQuery();

            command = new SqlCommand("UPDATE Job SET status='R' WHERE job_id=" + resultModel.JobId + " ;", con);
            command.ExecuteNonQuery();

            con.Close();
        }

        public static List<JobModel> GetJobsForReleasingResultForInterviewer(Models.User user)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" SELECT j.job_id, j.job_description, j.job_role_id, j.skill_set, j.vacancies, " +
                "j.min_experience, j.max_experience, j.age_limit, j.posted_by, j.posted_on, j.status " +
                " FROM dbo.Job j inner join dbo.InterviewerJob i on j.job_id = i.job_id" +
                " WHERE i.interviewer_username='" + user.username + "';", con);

            SqlDataReader reader = command.ExecuteReader();
            List<JobModel> jobs = new List<JobModel>();
            while (reader.Read())
            {
                JobModel job = new JobModel();
                job.JobId = reader.GetInt32(0);
                job.JobDesc = reader.GetString(1);
                job.JobRole = reader.GetInt32(2);
                job.Skills = new List<string>(reader.GetString(3).Split(','));
                job.Vacancies = reader.GetInt32(4);
                job.MinExperience = reader.GetInt32(5);
                job.MaxExperience = reader.GetInt32(6);
                job.AgeLimit = reader.GetInt32(7);
                job.PostedBy = reader.GetInt32(8);
                job.PostedOn = reader.GetDateTime(9);
                job.Status = reader.GetString(10);

                //jobs.Add(job);
                if (job.Status.Equals("S"))
                {
                    jobs.Add(job);
                }
            }

            con.Close();
            return jobs;
        }

        public static void SetInterviewerInDB(InterviewerModel model)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();
            SqlCommand command;

            string hashPassword = StringUtils.GetMD5Hash(StringUtils.Reverse(model.Password));
            command = new SqlCommand("INSERT INTO dbo.Users (username, password, role, account_act_date, name, state) VALUES ('" + model.UserName + "', '" + hashPassword + "', 'interviewer', '" + DateTime.Now.ToShortDateString() + "', '" + model.Name + "', 'Active');", con);
            command.ExecuteNonQuery();

            foreach (var item in model.Jobs)
            {
                if (item.Checked)
                {
                    command = new SqlCommand("INSERT INTO dbo.InterviewerJob (interviewer_username, job_id ) values ('" + model.UserName + "', '" + item.JobId + "' );", con);
                    command.ExecuteNonQuery();
                }

            }
            con.Close();
        }

        public static List<JobSelectModel> GetSelectJobsToBeInterviewed()
        {
            List<JobSelectModel> jobsToBeSelect = new List<JobSelectModel>();
            List<JobModel> jobs = ASCommonDAL.GetJobsToBeInterviewed();
            foreach (var item in jobs)
            {
                JobSelectModel jobToBeSelected = new JobSelectModel(item);
                jobToBeSelected.Checked = false;
                jobsToBeSelect.Add(jobToBeSelected);
            }
            return jobsToBeSelect;
        }

        public static List<string> GetListOfInterviewers()
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" SELECT username FROM dbo.Users WHERE role='interviewer';", con);

            SqlDataReader reader = command.ExecuteReader();
            List<string> interviewers = new List<string>();
            while (reader.Read())
            {
                interviewers.Add(reader.GetString(0));
            }

            con.Close();
            return interviewers;
        }

        public static void ReallocateInterviewerInDB(InterviewerModel model)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();
            SqlCommand command;

            foreach (var item in model.Jobs)
            {
                if (item.Checked)
                {
                    command = new SqlCommand("INSERT INTO dbo.InterviewerJob (interviewer_username, job_id ) values ('" + model.SelectedInterviewer + "', '" + item.JobId + "' );", con);
                    command.ExecuteNonQuery();
                }

            }
            con.Close();
        }
    }
}