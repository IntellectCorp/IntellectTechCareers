using IntellectTechCareers.Models;
using IntellectTechCareers.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Data_Access_Layer
{
    public class ASCommonDAL
    {
        public static List<JobRole> getJobRoles()
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select job_role_id, job_role from dbo.JobRole;", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }

            //Creating a JobRole List
            List<JobRole> listJobRole = new List<JobRole>();
            do
            {
                JobRole objJobRole = new JobRole();
                objJobRole.Id = Convert.ToInt32(reader[0]);
                objJobRole.Name = Convert.ToString(reader[1]);
                listJobRole.Add(objJobRole);
            } 
            while (reader.Read());
         
            con.Close();

            return listJobRole;
        }

        public static List<Skill> getListOfSkills()
        {
            List<Skill> skills = new List<Skill>();
            List<Qualification> qualifications = ASCommonDAL.getQualifications();
            foreach (Qualification item in qualifications)
            {
                Skill skill = new Skill(item);
                skill.Checked = false;
                skills.Add(skill);
            }
            return skills;
        }

        public static List<Qualification> getQualifications()
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select qualification_id, qualification, type from dbo.Qualification;", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }

            //Creating a JobRole List
            List<Qualification> listQualification = new List<Qualification>();
            do
            {
                Qualification obj = new Qualification();
                obj.Id = Convert.ToInt32(reader[0]);
                obj.Name = Convert.ToString(reader[1]);
                obj.Type = Convert.ToString(reader[2]);
                listQualification.Add(obj);
            }
            while (reader.Read());
            reader.Close();
            con.Close();

            return listQualification;
        }


        public static void postJobinDB(Job model, User poster)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();
            List<Skill> selectedSkills = new List<Skill>();
            foreach (var item in model.Skills)
            {
                if(item.Checked){
                    selectedSkills.Add(item);
                }
            }

            string skillSet = String.Join(",", selectedSkills.Select(x => x.Id.ToString()).ToArray());
            SqlCommand command = new SqlCommand("insert into Job (job_description, job_role_id, skill_set, vacancies, min_experience, max_experience, age_limit, posted_by, posted_on, status) values ('" + model.JobDesc + "', " + model.JobRole + ", '" + skillSet + "'," + model.Vacancies + ", " + model.MinExperience + ", " + model.MaxExperience + ", " + model.AgeLimit + ", " + poster.user_id + ", '" + DateTime.Now.ToShortDateString() + "', 'P' );", con);
            command.ExecuteNonQuery();

            con.Close();
        }

        public static ApplicantListModel getApplicantList()
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select candidate_id from dbo.Applicant;", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }

            //Creating a JobRole List
            List<int> listApplicant = new List<int>();
            do
            { 
                listApplicant.Add(Convert.ToInt32(reader[0]));
            }
            while (reader.Read());

            con.Close();
            ApplicantListModel model = new ApplicantListModel();
            model.CandidateIdList = listApplicant;
            return model;
        }

        public static JobListModel getJobList()
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select job_id from dbo.Job;", con);
            SqlDataReader reader = command.ExecuteReader();
                
            if (reader == null || !reader.Read())
            {
                return null;
            }

            //Creating a JobRole List
            List<int> listItems = new List<int>();
            do
            {
                listItems.Add(Convert.ToInt32(reader[0]));
            }
            while (reader.Read());

            con.Close();
            JobListModel model = new JobListModel();
            model.JobIdList = listItems;
            return model;
        }

        private static List<JobModel> getJobs()
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" select job_id, job_description, job_role_id, skill_set, vacancies, " +
                "min_experience, max_experience, age_limit, posted_by, posted_on, status from dbo.Job ", con);
            command.ExecuteNonQuery();

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
                
                jobs.Add(job);
            }

            con.Close();
            return jobs;
        }

        public static int getApplicantCount(int jobID)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("SELECT COUNT(1) FROM dbo.Application WHERE job_id="+jobID+";", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return -1;
            }
            int applicantCount = Convert.ToInt32(reader[0]);
            reader.Close();
            con.Close();
            return applicantCount;
        }

        public static List<CandidateResult> getApplicantForTheJob(int jobID)
        {
            List<CandidateResult> candidates = new List<CandidateResult>();

            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("SELECT candidate_id FROM dbo.Application WHERE job_id=" + jobID + ";", con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader == null || !reader.Read())
            {
                return null;
            }
            do
            {
                CandidateResult candi = new CandidateResult();
                candi.UserID = Convert.ToInt32(reader[0]);
                string username, name;
                AccountDAL.GetNameOfUser(out name, out username, candi.UserID);
                candi.Username = username;
                candi.Name = name;
                candi.IsSelected = false;

                candidates.Add(candi);

            } while (reader.Read());

            reader.Close();
            con.Close();
            return candidates;
        }

        public static List<JobModel> getJobsToBeInterviewed()
        {
            List<JobModel> toBeInterviewedModel = new List<JobModel>();
            List<JobModel> allJobs = getJobs();
            foreach (var item in allJobs)
            {
                if (item.Status.Equals("P"))
                {
                    toBeInterviewedModel.Add(item);
                }
            }
            return toBeInterviewedModel;
        }

        public static List<JobModel> getJobsForReleasingResult()
        {
            List<JobModel> newModel = new List<JobModel>();
            List<JobModel> allJobs = getJobs();
            foreach (var item in allJobs)
            {
                if (item.Status.Equals("S"))
                {
                    newModel.Add(item);
                }
            }
            return newModel;
        }

        public static void scheduleInterviewToDB(InterviewModel interviewModel, User user)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();
            SqlCommand command;

            command = new SqlCommand("insert into Interview (job_id, date, time, venue, scheduled_by) values (" + interviewModel.JobId + ", '" + interviewModel.Date.ToShortDateString() + "', '" + interviewModel.Time.ToString() + "', '" + interviewModel.Venue + "', " + user.user_id + ");", con);
            command.ExecuteNonQuery();

            command = new SqlCommand("UPDATE Job SET status='S' WHERE job_id=" + interviewModel.JobId + " ;", con);
            command.ExecuteNonQuery();

            command = new SqlCommand("UPDATE Application SET status_code='I', status='Interview Scheduled' WHERE job_id=" + interviewModel.JobId + " ;", con);
            command.ExecuteNonQuery();

            con.Close();
        }

        public static List<User> getUsers()
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select user_id, username, role from dbo.Users ;", con);
            command.ExecuteNonQuery();

            List<User> userList = new List<User>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                User user = new User();
                user.user_id = reader.GetInt32(0);
                user.username = reader.GetString(1);
                user.role = reader.GetString(2);

                userList.Add(user);
            }
            reader.Close();
            con.Close();

            return userList;
        }


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

                    command = new SqlCommand("UPDATE Users SET state='Blocked', account_act_date='"+DateTime.Now.AddDays(90).ToShortDateString()+"' WHERE user_id=" + item.UserID + " ;", con);
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
    }
}