using IntellectTechCareers.Models;
using IntellectTechCareers.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntellectTechCareers.Data_Access_Layer
{
    public class CandidateDAL
    {
        public static CandidateModel getCandidateDetails(int userId)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select candidate_id, name, email_id, contact_num, gender, dob, address," + 
                " graduation, post_graduation from dbo.Applicant where candidate_id=" + userId + ";", con);
            SqlDataReader reader = command.ExecuteReader();

            CandidateModel can = new CandidateModel();
            if (reader == null || !reader.Read())
            {
                return null;
            }
            can.user_id = Convert.ToInt32(reader[0]);
            can.Name = Convert.ToString(reader[1]);
            can.EmailID = Convert.ToString(reader[2]);
            can.ContactNo = Convert.ToString(reader[3]);
            can.gender = Convert.ToString(reader[4]);
            can.dob = Convert.ToDateTime(reader[5]);
            can.Address = Convert.ToString(reader[6]);

            String[] ugList = Convert.ToString(reader[7]).Split(',');
            can.ugQualifications = new List<string>();
            foreach (var item in ugList)
            {
                can.ugQualifications.Add(item);
            }

            String[] pgList = Convert.ToString(reader[8]).Split(',');
            can.pgQualifications = new List<string>();
            foreach (var item in pgList)
            {
                can.pgQualifications.Add(item);
            }

            con.Close();
            return can;
        }

        public static List<ExperienceModel> getCandidateExperienceDetails(int userId)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select company, designation, period from dbo.Experience where user_id=" + userId + ";", con);
            SqlDataReader reader = command.ExecuteReader();

            List<ExperienceModel> experienceList = new List<ExperienceModel>();
            while(reader.Read())
            {
                ExperienceModel exp = new ExperienceModel();
                exp.company = Convert.ToString(reader[0]);
                exp.designation = Convert.ToString(reader[1]);
                exp.experience = Convert.ToInt32(reader[2]);

                experienceList.Add(exp);
            }

            con.Close();
            return experienceList;
        }

        public static List<QualificationModel> getQualificationDetails()
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("select qualification_id, qualification, type from dbo.Qualification;", con);
            SqlDataReader reader = command.ExecuteReader();

            List<QualificationModel> qualificationList = new List<QualificationModel>();
            while (reader.Read())
            {
                QualificationModel qual = new QualificationModel();
                qual.qualification_id = Convert.ToInt32(reader[0]);
                qual.qualification = Convert.ToString(reader[1]);
                qual.type = Convert.ToString(reader[2]);

                qualificationList.Add(qual);
            }

            con.Close();
            return qualificationList;
        }

        public static void addEducationDetails(CandidateModel candidate)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            String query = "";
            if (candidate.newUgQualification.Equals("0"))
            {
                query = "update dbo.Applicant set post_graduation = post_graduation + '," + 
                    candidate.newPgQualification + "'" + "where candidate_id = " + candidate.user_id;
            }
            else if (candidate.newPgQualification.Equals("0"))
            {
                query = "update dbo.Applicant set graduation = graduation + '," + 
                    candidate.newUgQualification + "'" + "where candidate_id = " + candidate.user_id;
            }
            else
            {
                query = "update dbo.Applicant set " + "graduation = graduation + '," + candidate.newUgQualification + "'" +
                    ", " + "post_graduation = post_graduation + '," + candidate.newPgQualification + "'" + "where candidate_id = " + candidate.user_id;
            }
   
            SqlCommand command = new SqlCommand(query, con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public static void addExperienceDetails(ExperienceModel exp)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("insert into dbo.Experience (user_id, company, designation, period) values "
                + "(" + exp.user_id + ",'" + exp.company + "', '" + exp.designation + "', " + exp.experience +")" , con);
            command.ExecuteNonQuery();
            con.Close();
        }

        public static void updatePersonalInfo(CandidateModel can)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand("update dbo.Applicant set address = '" + can.Address + "', " +
                "contact_num = '" + can.ContactNo + "', email_id = '" + can.EmailID + "' where candidate_id = " + can.user_id, con);

            command.ExecuteNonQuery();
            con.Close();
        }

        public static List<JobModel> getApplicableJobs(int userId)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" select job_id, job_description, job_role_id, skill_set, vacancies, " + 
                "min_experience, max_experience, age_limit, posted_by from dbo.Jobs ", con);
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            List<JobModel> jobs = new List<JobModel>();
            while (reader.Read())
            {
                JobModel job = new JobModel();
                job.jobId = reader.GetInt32(0);
                job.JobDesc = reader.GetString(1);
                job.JobRole = reader.GetInt32(2);
                job.Skills = new List<string>(reader.GetString(3).Split(','));
                job.Vacancies = reader.GetInt32(4);
                job.MinExperience = reader.GetInt32(5);
                job.MaxExperience = reader.GetInt32(6);

                jobs.Add(job);
            }

            con.Close();
            return jobs;
        }

        public static List<ApplicationModel> getApplicationDetails(int user_id)
        {
            List<ApplicationModel> data = new List<ApplicationModel>();
            
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" SELECT app_id, candidate_id, job_id, status_code, status, app_date FROM dbo.Application WHERE candidate_id="+user_id+";", con);
            command.ExecuteNonQuery();

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ApplicationModel model = new ApplicationModel();
                model.AppId = reader.GetInt32(0);
                model.CandidateId = reader.GetInt32(1);
                model.JobId = reader.GetInt32(2);
                model.StatusCode = reader.GetString(3);
                model.Status = reader.GetString(4);
                model.Date = reader.GetDateTime(5);

                data.Add(model);
            }
            reader.Close();
            con.Close();

            return data;
        }
    }
}