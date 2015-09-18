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

            string skillSet = String.Join(",", model.Skills.Select(x => x.Id).ToArray());
            SqlCommand command = new SqlCommand("insert into Job (job_description, job_role_id, skill,set, vacancies, min_experience, max_experience, age_limit, posted_by) values ('" + model.JobDesc + "', '" + model.JobRole + "', " + skillSet + ",'" + model.Vacancies + "', '" + model.MinExperience + "', '" + model.MaxExperience + "', '" + model.AgeLimit + "', '" + poster.user_id + ");", con);
            command.ExecuteNonQuery();

            con.Close();
        }
    }
}