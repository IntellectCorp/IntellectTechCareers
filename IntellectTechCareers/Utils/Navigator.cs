using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IntellectTechCareers.Models;

namespace IntellectTechCareers.Utils
{
    public class Navigator
    {
        public static bool isUserLoggedIn(HttpSessionStateBase session)
        {
            Object user = session["User"];
            if (user == null)
                return false;
            else
                return true;
        }

        public static bool UserRoleValidation(HttpSessionStateBase session, string role)
        {
            Object user = session["User"];
            if (user == null)
                return false;
            else
                return true;
        }

        public static bool isStaffAllowed(HttpSessionStateBase session, string responsibility)
        {
            Object user = session["User"];
            if (user == null)
                return false;
            else
                return true;
        }
    }
}