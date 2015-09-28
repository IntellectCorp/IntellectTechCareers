using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntellectTechCareers.Models;
using IntellectTechCareers.Data_Access_Layer;

namespace IntellectTechCareers.Utils
{
    public class Navigator
    {
        public static bool IsUserLoggedIn(HttpSessionStateBase session)
        {
            var user = session["User"];
            if (user == null)
                return false;
            else
                return true;
        }

        public static bool UserRoleValidation(HttpSessionStateBase session, string role)
        {
            User user = (User)session["User"];
            if (user.role == role)
                return true;
            else
                return false;
        }

        public static bool IsStaffAllowed(HttpSessionStateBase session, string responsibility)
        {
            User user = (User)session["User"];
            if (user.role == "staff")
            {
                bool rightToPost, rightToSchedule, rightToPublish;
                StaffDAL.getResponsiblities(out rightToPost, out rightToSchedule, out rightToPublish, user.user_id);
                if(responsibility.Equals("RightToPost") && rightToPost){
                    return true;
                }
                else if(responsibility.Equals("RightToSchedule") && rightToSchedule){
                    return true;
                }
                else if(responsibility.Equals("RightToPublish") && rightToPublish){
                    return true;
                }
            }
                return false;
        }
    }
}