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

            String[] pgList = Convert.ToString(reader[7]).Split(',');
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
    }
}