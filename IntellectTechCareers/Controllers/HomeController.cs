using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntellectTechCareers.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["Role"] != null){
                if (Session["Role"].Equals("candidate"))
                    return View("CandidateHome");
                else if (Session["Role"].Equals("manager")) 
                    return View("ManagerHome");
                else if (Session["Role"].Equals("staff"))
                    return View("StaffHome");
                else if (Session["Role"].Equals("interviewer"))
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
