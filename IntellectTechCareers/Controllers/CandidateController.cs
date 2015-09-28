using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntellectTechCareers.Data_Access_Layer;
using IntellectTechCareers.Models;
using IntellectTechCareers.Utils;

namespace IntellectTechCareers.Controllers
{
    public class CandidateController : Controller
    {
        //
        // GET: /Candidate/

        public ActionResult Index()
        {
           if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpGet]
        public ActionResult UpdateProfile()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            CandidateModel candidate = new CandidateModel();

            int user_id = ((User)Session["user"]).user_id;
            candidate = CandidateDAL.GetCandidateDetails(user_id);
            candidate.experienceDetails = CandidateDAL.GetCandidateExperienceDetails(user_id);

            List<QualificationModel> qualifications = CandidateDAL.GetQualificationDetails();
            List<SelectListItem> ugQualifications = new List<SelectListItem>();
            List<SelectListItem> pgQualifications = new List<SelectListItem>();

            ugQualifications.Add(new SelectListItem { Selected = true, Text = "<-Select->", Value = "0" });
            pgQualifications.Add(new SelectListItem { Selected = true, Text = "<-Select->", Value = "0" });

            foreach (var item in qualifications)
            {
                if (candidate.ugQualifications.Contains(Convert.ToString(item.qualification_id)))
                    continue;

                if (candidate.pgQualifications.Contains(Convert.ToString(item.qualification_id)))
                    continue;

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
            CandidateDAL.UpdatePersonalInfo(candidate);
            return "";
        }

        public string UpdateQualificationDetails(CandidateModel candidate)
        {
            CandidateDAL.AddEducationDetails(candidate);
            return "";
        }

        public ActionResult ViewJobDetails()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            User user = (User)Session["user"];

            CandidateModel candidate = new CandidateModel();
            candidate = CandidateDAL.GetCandidateDetails(user.user_id);
            candidate.experienceDetails = CandidateDAL.GetCandidateExperienceDetails(user.user_id);

            int totalExperience = 0;
            foreach (var item in candidate.experienceDetails)
            {
                totalExperience += item.experience;
            }

            JobViewModel jobViewModel = new JobViewModel();
            jobViewModel.jobs = CandidateDAL.GetApplicableJobs(user.user_id);;
            jobViewModel.qualifications = CandidateDAL.GetQualificationsIdToName();
            jobViewModel.candidateUgQualification = candidate.ugQualifications;
            jobViewModel.candidatePgQualification = candidate.pgQualifications;
            jobViewModel.totalExperience = totalExperience;
            jobViewModel.jobsAlreadyApplied = CandidateDAL.GetNumJobsAppliedFor(user.user_id);
            jobViewModel.appliedJobs = CandidateDAL.GetAppliedJobs(user.user_id);

            return View(jobViewModel);
        }

        [HttpPost]
        public ActionResult ApplyForJob(JobViewModel model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            User user = (User)Session["user"];
            CandidateDAL.ApplyForJobs(model.selectedJobs, user.user_id);

            return View();
        }

        public ActionResult JobApplicationSuccess()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            ViewBag.Message = "You have successfully applied for the selected jobs ! You can view their status in job status view !";
            return View("Message");
        }

        public string UpdateExperienceInfo(ExperienceModel expModel)
        {
            CandidateDAL.AddExperienceDetails(expModel);
            return "";
        }

        public ActionResult ViewJobStatus()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            int user_id = ((User)Session["user"]).user_id;
            List<ApplicationModel> model = CandidateDAL.GetApplicationDetails(user_id);
            return View(model);
        }

        public ActionResult GetCandidateEducationDetails()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            int user_id = ((User)Session["user"]).user_id;

            QualificationViewModel viewModel = new QualificationViewModel();
            viewModel.qualifications = CandidateDAL.GetCandidateEducationDetails(user_id);

            return Json(viewModel);
        }

        public ActionResult GetCandidateExperienceDetails()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            int user_id = ((User)Session["user"]).user_id;

            ExperienceViewModel viewModel = new ExperienceViewModel();
            viewModel.experience = CandidateDAL.GetCandidateExperienceDetails(user_id);

            return Json(viewModel);
        }
    }
}
