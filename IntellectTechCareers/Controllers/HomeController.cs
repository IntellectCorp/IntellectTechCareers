using IntellectTechCareers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntellectTechCareers.Models;
using IntellectTechCareers.Data_Access_Layer;

namespace IntellectTechCareers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                String role = ((User)Session["user"]).role ;
                
                if (role.Equals("candidate"))
                    return View("CandidateHome");
                else if (role.Equals("manager"))
                    return View("ManagerHome", AdminDAL.getManagerHome());
                else if (role.Equals("staff"))
                    return View("StaffHome");
                else if (role.Equals("interviewer"))
                    return View("InterviewerHome");
                else
                    return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
