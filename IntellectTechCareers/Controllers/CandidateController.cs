using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntellectTechCareers.Data_Access_Layer;
using IntellectTechCareers.Models;

namespace IntellectTechCareers.Controllers
{
    public class CandidateController : Controller
    {
        //
        // GET: /Candidate/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateProfile()
        {
            CandidateModel candidate = new CandidateModel();

            int user_id = ((User)Session["user"]).user_id;
            candidate = CandidateDAL.getCandidateDetails(user_id);
            candidate.experienceDetails = CandidateDAL.getCandidateExperienceDetails(user_id);
            return View(candidate);
        }

        public ActionResult ViewJobDetails()
        {
            return View(Session["user"]);
        }

        public ActionResult UpdateProfileToDB(CandidateModel candidate, string returnUrl)
        {
            return View();
        }

    }
}
