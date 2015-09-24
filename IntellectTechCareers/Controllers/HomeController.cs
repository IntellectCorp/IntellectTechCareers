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
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            if (Session["user"] != null)
            {
                String role = ((User)Session["user"]).role ;
                
                if (role.Equals("candidate"))
                    return View("CandidateHome");
                else if (role.Equals("manager"))
                    return View("ManagerHome", AdminDAL.getManagerHome());
                else if (role.Equals("staff"))
                    return View("StaffHome", StaffDAL.getStaffHome((User)Session["user"]));
                else
                    return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }

        public ActionResult About()
        {
            ViewBag.Layout = "";
            User user = ((User)Session["user"]);
            if (user != null)
            {
                if (user.role.Equals("candidate"))
                    ViewBag.Layout = "~/Views/Shared/_LayoutPageCandidate.cshtml";
                else if (user.role.Equals("manager"))
                    ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
                else if (user.role.Equals("staff"))
                    ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            }

            ViewBag.Message = "ABOUT US.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Layout = "";
            User user = ((User)Session["user"]);
            if (user != null)
            {
                if (user.role.Equals("candidate"))
                    ViewBag.Layout = "~/Views/Shared/_LayoutPageCandidate.cshtml";
                else if (user.role.Equals("manager"))
                    ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
                else if (user.role.Equals("staff"))
                    ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            }

            ViewBag.Message = "CONTACT US";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

    }
}
