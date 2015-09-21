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

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            CandidateModel candidate = new CandidateModel();

            int user_id = ((User)Session["user"]).user_id;
            candidate = CandidateDAL.getCandidateDetails(user_id);
            candidate.experienceDetails = CandidateDAL.getCandidateExperienceDetails(user_id);

            List<QualificationModel> qualifications = CandidateDAL.getQualificationDetails();
            List<SelectListItem> ugQualifications = new List<SelectListItem>();
            List<SelectListItem> pgQualifications = new List<SelectListItem>();

            ugQualifications.Add(new SelectListItem { Selected = true, Text = "<-Select->", Value = "0" });
            pgQualifications.Add(new SelectListItem { Selected = true, Text = "<-Select->", Value = "0" });

            foreach (var item in qualifications)
            {
                if (item.type.Equals("UG"))
                    ugQualifications.Add(new SelectListItem { Text = item.qualification, Value = item.qualification });
                else
                    pgQualifications.Add(new SelectListItem { Text = item.qualification, Value = item.qualification });
            }

            ViewBag.ugListData = ugQualifications;
            ViewBag.pgListData = pgQualifications;
            return View(candidate);
        }

        public string UpdatePersonalInfo(CandidateModel candidate)
        {
            CandidateDAL.updatePersonalInfo(candidate);
            return "";
        }

        public string UpdateQualificationDetails(CandidateModel candidate)
        {
            CandidateDAL.addEducationDetails(candidate);
            return "";
        }

        public ActionResult ViewJobDetails()
        {
            User user = (User)Session["user"];
            List<JobModel> jobs = CandidateDAL.getApplicableJobs(user.user_id);

            JobViewModel jobViewModel = new JobViewModel();
            jobViewModel.jobs = jobs;

            return View(jobViewModel);
        }

        [HttpPost]
        public ActionResult ViewJobDetails(JobViewModel model)
        {
            return View();
        }

        public string UpdateExperienceInfo(ExperienceModel expModel)
        {
            CandidateDAL.addExperienceDetails(expModel);
            return "";
        }
    }
}
