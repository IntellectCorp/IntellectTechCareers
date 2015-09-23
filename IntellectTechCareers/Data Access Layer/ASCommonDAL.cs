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

        public static List<JobModel> getJobs()
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

    }
}