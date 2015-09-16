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
            CandidateViewModel candidateViewModel = new CandidateViewModel();

            CandidateModel candidate = new CandidateModel();

            int user_id = ((User)Session["user"]).user_id;
            candidate = CandidateDAL.getCandidateDetails(user_id);
            candidate.experienceDetails = CandidateDAL.getCandidateExperienceDetails(user_id);

            candidateViewModel.candidate = candidate;

            List<QualificationModel> qualifications = CandidateDAL.getQualificationDetails();
            List<SelectListItem> ugQualifications = new List<SelectListItem>();
            List<SelectListItem> pgQualifications = new List<SelectListItem>();

            ugQualifications.Add(new SelectListItem { Selected = true, Text = "<-Select->" });
            pgQualifications.Add(new SelectListItem { Selected = true, Text = "<-Select->" });

            foreach (var item in qualifications)
            {
                if (item.type.Equals("UG"))
                    ugQualifications.Add(new SelectListItem { Text = item.qualification });
                else
                    pgQualifications.Add(new SelectListItem { Text = item.qualification });
            }

            candidateViewModel.ugList = ugQualifications;
            candidateViewModel.pgList = pgQualifications;
            ViewBag.ugListData = ugQualifications;
            return View(candidateViewModel);
        }

        public ActionResult ViewJobDetails()
        {
            return View(Session["user"]);
        }

        public ActionResult UpdateProfileToDB(CandidateViewModel candidate, string returnUrl)
        {
            return View();
        }

    }
}
