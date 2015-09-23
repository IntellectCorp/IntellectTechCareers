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
        private static Dictionary<string, string> qualificationIdToName = null;
        private static Dictionary<string, string> qualificationNameToId = null;

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
                if(item.Length > 0)
                can.ugQualifications.Add(item);
            }

            String[] pgList = Convert.ToString(reader[8]).Split(',');
            can.pgQualifications = new List<string>();
            foreach (var item in pgList)
            {
                if (item.Length > 0)
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

            candidate.newUgQualification = getQualificationsNameToId()[candidate.newUgQualification];
            candidate.newPgQualification = getQualificationsNameToId()[candidate.newPgQualification];

            String query = "";

            string graduation ="";
            string post_graduation = "";
            SqlCommand command = new SqlCommand("select graduation, post_graduation from dbo.Applicant where candidate_id = " + candidate.user_id + ";" , con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                try
                {
                    graduation = reader.GetString(0);
                }
                catch (Exception e)
                {
                    graduation = "";
                }

                try
                {
                    post_graduation = reader.GetString(1);
                }
                catch (Exception e)
                {
                    post_graduation = "";
                }
            }
                
            reader.Close();

            if (graduation.Equals("") && !candidate.newUgQualification.Equals("0"))
            {
                graduation = candidate.newUgQualification;
            }
            else if (!graduation.Equals("") && !candidate.newUgQualification.Equals("0"))
            {
                graduation += "," + candidate.newUgQualification;
            }
   
            if (post_graduation.Equals("") && !candidate.newPgQualification.Equals("0"))
            {
                post_graduation = candidate.newPgQualification;
            }
            else if (!post_graduation.Equals("") && !candidate.newPgQualification.Equals("0"))
            {
                post_graduation += "," + candidate.newPgQualification;
            }

            query = "update dbo.Applicant set " + "graduation = '" + graduation + "'" +
                ", " + "post_graduation = '" + post_graduation + "'" + " where candidate_id = " + candidate.user_id;

            command = new SqlCommand(query, con);
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
                "min_experience, max_experience, age_limit, posted_by, posted_on, status from dbo.Job ", con);
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
                job.AgeLimit = reader.GetInt32(7);
                job.PostedBy = reader.GetInt32(8);
                job.PostedOn = reader.GetDateTime(9);
                job.Status = reader.GetString(10);

                if (job.Status.Equals("P"))
                {
                    jobs.Add(job);
                }
            }

            con.Close();
            return jobs;
        }

        public static Dictionary<string, string> getQualificationsIdToName()
        {
            if (qualificationIdToName == null)
                getQualificationsFromDB();

            return qualificationIdToName;
        }

        public static Dictionary<string, string> getQualificationsNameToId()
        {
            if (qualificationNameToId == null)
                getQualificationsFromDB();

            return qualificationNameToId;
        }

        private static void getQualificationsFromDB()
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" select qualification_id, qualification from dbo.Qualification ", con);
            command.ExecuteNonQuery();
            qualificationIdToName = new Dictionary<string, string>();
            qualificationNameToId = new Dictionary<string, string>();

            qualificationIdToName.Add("0", "0");
            qualificationNameToId.Add("0", "0");

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                qualificationIdToName.Add(reader.GetInt32(0).ToString(), reader.GetString(1));
                qualificationNameToId.Add(reader.GetString(1), reader.GetInt32(0).ToString());
            }

            con.Close();
        }

        public static int getNumJobsAppliedFor(int candidate_id)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" select count(*) from dbo.Application where candidate_id = " + candidate_id , con);
            command.ExecuteNonQuery();

            int numJobsAppliedFor = 0;
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                numJobsAppliedFor = reader.GetInt32(0);
            }

            con.Close();
            return numJobsAppliedFor;
        }

        public static List<int> getAppliedJobs(int candidate_id)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" select job_id from dbo.Application where candidate_id = " + candidate_id, con);
            command.ExecuteNonQuery();

            List<int> appliedJobs = new List<int>();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                appliedJobs.Add(reader.GetInt32(0));
            }

            con.Close();
            return appliedJobs;
        }

        public static void ApplyForJobs(string jobs, int user_id)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            foreach (var item in jobs.Split(','))
            {
                SqlCommand command = new SqlCommand(" insert into dbo.Application (candidate_id, job_id, status_code, status, app_date) values " + 
                    " (" + user_id + ", " + Convert.ToInt32(item) + ", 'A', 'Applied', '" + DateTime.Today + "') ", con);
                command.ExecuteNonQuery();
            }
            
            con.Close();
        }

        public static List<ApplicationModel> getApplicationDetails(int user_id)
        {
            List<ApplicationModel> data = new List<ApplicationModel>();

            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(
                " SELECT a.app_id, a.candidate_id, a.job_id, a.status_code, a.status, a.app_date, j.job_description " +
                " FROM dbo.Application a inner join dbo.Job j on a.job_id = j.job_id" + 
                " WHERE candidate_id=" + user_id + ";", con);
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
                model.JobName = reader.GetString(6);

                data.Add(model);
            }
            reader.Close();
            con.Close();

            return data;
        }

        public static List<QualificationModel> getCandidateEducationDetails(int user_id)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" select graduation, post_graduation from dbo.Applicant where candidate_id=" + user_id + ";", con);
            command.ExecuteNonQuery();

            Dictionary<string, string> idToName = getQualificationsIdToName();

            List<QualificationModel> qualifications = new List<QualificationModel>();
            SqlDataReader reader = command.ExecuteReader();
            if(reader.Read())
            {
                string ug = reader.GetString(0);
                string pg = reader.GetString(1);

                foreach (var item in ug.Split(','))
                {
                    QualificationModel model = new QualificationModel();
                    model.qualification_id = Convert.ToInt32(item);
                    model.qualification = idToName[item];
                    model.type = "UG";

                    qualifications.Add(model);
                }

                foreach (var item in pg.Split(','))
                {
                    QualificationModel model = new QualificationModel();
                    model.qualification_id = Convert.ToInt32(item);
                    model.qualification = idToName[item];
                    model.type = "PG";

                    qualifications.Add(model);
                }

            }
            reader.Close();
            con.Close();

            return qualifications;
        }

        public static JobModel getJobForWhichUserIsSelected(int candidate_id)
        {
            SqlConnection con = DBUtils.getDBConnection();
            con.Open();

            SqlCommand command = new SqlCommand(" select a.job_id, j.job_description " + 
                "from dbo.Application a inner join job j on j.job_id = a.job_id where status_code = 'S' and candidate_id = " + candidate_id, con);
            command.ExecuteNonQuery();

            JobModel job = new JobModel();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                job.jobId = reader.GetInt32(0);
                job.JobDesc = reader.GetString(1);
            }

            con.Close();
            return job;
        }
    }
}