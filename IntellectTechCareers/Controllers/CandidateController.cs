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
           if (!Navigator.isUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.userRoleValidation(Session, "candidate"))
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
            if (!Navigator.isUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.userRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

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
            if (!Navigator.isUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.userRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            User user = (User)Session["user"];

            CandidateModel candidate = new CandidateModel();
            candidate = CandidateDAL.getCandidateDetails(user.user_id);
            candidate.experienceDetails = CandidateDAL.getCandidateExperienceDetails(user.user_id);

            int totalExperience = 0;
            foreach (var item in candidate.experienceDetails)
            {
                totalExperience += item.experience;
            }

            JobViewModel jobViewModel = new JobViewModel();
            jobViewModel.jobs = CandidateDAL.getApplicableJobs(user.user_id);;
            jobViewModel.qualifications = CandidateDAL.getQualificationsIdToName();
            jobViewModel.candidateUgQualification = candidate.ugQualifications;
            jobViewModel.candidatePgQualification = candidate.pgQualifications;
            jobViewModel.totalExperience = totalExperience;
            jobViewModel.jobsAlreadyApplied = CandidateDAL.getNumJobsAppliedFor(user.user_id);
            jobViewModel.appliedJobs = CandidateDAL.getAppliedJobs(user.user_id);

            return View(jobViewModel);
        }

        [HttpPost]
        public ActionResult ApplyForJob(JobViewModel model)
        {
            if (!Navigator.isUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.userRoleValidation(Session, "candidate"))
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
            if (!Navigator.isUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.userRoleValidation(Session, "candidate"))
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
            CandidateDAL.addExperienceDetails(expModel);
            return "";
        }

        public ActionResult ViewJobStatus()
        {
            if (!Navigator.isUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.userRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            int user_id = ((User)Session["user"]).user_id;
            List<ApplicationModel> model = CandidateDAL.getApplicationDetails(user_id);
            return View(model);
        }

        public ActionResult GetCandidateEducationDetails()
        {
            if (!Navigator.isUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.userRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            int user_id = ((User)Session["user"]).user_id;

            QualificationViewModel viewModel = new QualificationViewModel();
            viewModel.qualifications = CandidateDAL.getCandidateEducationDetails(user_id);

            return Json(viewModel);
        }

        public ActionResult GetCandidateExperienceDetails()
        {
            if (!Navigator.isUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.userRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            int user_id = ((User)Session["user"]).user_id;

            ExperienceViewModel viewModel = new ExperienceViewModel();
            viewModel.experience = CandidateDAL.getCandidateExperienceDetails(user_id);

            return Json(viewModel);
        }
    }
}
