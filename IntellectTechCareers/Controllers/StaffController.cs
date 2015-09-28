using IntellectTechCareers.Data_Access_Layer;
using IntellectTechCareers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntellectTechCareers.Utils;

namespace IntellectTechCareers.Controllers
{
    public class StaffController : Controller
    {
        //
        // GET: /Staff/

        public ActionResult Index()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            //return View("StaffHome");
            return View("Home/StaffHome", StaffDAL.GetStaffHome((User)Session["user"]));
        }

        public ActionResult PostJob()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.IsStaffAllowed(Session, "RightToPost"))
            {
                @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
                @ViewBag.Message = "User Rights Denied! You don't have permission for this.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";

            Job model = new Job();
            //model.JobRoles = new List<JobRole>{
            //            new JobRole {Id = 0, Name = "Cho"},
            //            new JobRole {Id = 1, Name = "Chio"},
            //            new JobRole {Id = 2, Name = "Choo"},
            //            new JobRole {Id = 3, Name = "Chyo"}
            //            };
            model.JobRoles = ASCommonDAL.GetJobRoles();
            model.Skills = ASCommonDAL.GetListOfSkills();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostJob(Job model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.IsStaffAllowed(Session, "RightToPost"))
            {
                @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
                @ViewBag.Message = "User Rights Denied! You don't have permission for this.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            User user = ((User)Session["user"]);
            ASCommonDAL.PostJobInDB(model, user);
            //string desc = model.JobDesc;
            //string role = model.JobRole;
            @ViewBag.Message = "Job Posted !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }

        //public Dictionary<int, string> getListOfSkills()
        //{
        //    Dictionary<int, string> skills = new Dictionary<int, string>();
        //    List<Qualification> qualifications = ASCommonDAL.getQualifications();
        //    foreach (var item in qualifications)
        //    {
        //        skills.Add(item.Id, item.Name);
        //    }
        //    return skills;
        //}
        //public Dictionary<int, bool> getListOfSkillsSelected()
        //{
        //    Dictionary<int, bool> skills = new Dictionary<int, bool>();
        //    List<Qualification> qualifications = ASCommonDAL.getQualifications();
        //    foreach (var item in qualifications)
        //    {
        //        skills.Add(item.Id, false);
        //    }
        //    return skills;
        //}

        public ActionResult ScheduleInterview()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.IsStaffAllowed(Session, "RightToSchedule"))
            {
                @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
                @ViewBag.Message = "User Rights Denied! You don't have permission for this.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            List<JobModel> jobs = ASCommonDAL.GetJobsToBeInterviewed();
            List<JobWithApplicantsModel> model = new List<JobWithApplicantsModel>();
            foreach (var item in jobs)
            {
                JobWithApplicantsModel jobWithAppl = new JobWithApplicantsModel(item);
                jobWithAppl.ApplicantCount = ASCommonDAL.GetApplicantCount(item.JobId);
                model.Add(jobWithAppl);
            }
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ScheduleInterviewDialog(JobModel jobModel)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.IsStaffAllowed(Session, "RightToSchedule"))
            {
                @ViewBag.Message = "User Rights Denied! You don't have permission for this.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            InterviewModel model = new InterviewModel();
            model.JobId = jobModel.JobId;
            model.JobDesc = jobModel.JobDesc.Replace(Environment.NewLine, "");
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            @ViewBag.Controller = "Staff";
            return PartialView("_PartialScheduleInterview", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ScheduleInterview(InterviewModel interviewModel)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to continue.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to continue.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.IsStaffAllowed(Session, "RightToSchedule"))
            {
                @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
                @ViewBag.Message = "User Rights Denied! You don't have permission for this.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            User user = ((User)Session["user"]);
            ASCommonDAL.ScheduleInterviewToDB(interviewModel, user);
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            @ViewBag.Message = "Interview Scheduled for Job - [" + interviewModel.JobId + "] " + interviewModel.JobDesc + ".";
            return View("Message");
        }

        public ActionResult ReleaseResults()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.IsStaffAllowed(Session, "RightToPublish"))
            {
                @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
                @ViewBag.Message = "User Rights Denied! You don't have permission for this.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            List<JobModel> jobs = ASCommonDAL.GetJobsForReleasingResult();
            List<JobWithApplicantsModel> model = new List<JobWithApplicantsModel>();
            foreach (var item in jobs)
            {
                JobWithApplicantsModel jobWithAppl = new JobWithApplicantsModel(item);
                jobWithAppl.ApplicantCount = ASCommonDAL.GetApplicantCount(item.JobId);
                model.Add(jobWithAppl);
            }
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ReleaseResultsDialog(JobModel jobModel)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to continue.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to continue.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.IsStaffAllowed(Session, "RightToPublish"))
            {
                @ViewBag.Message = "User Rights Denied! You don't have permission for this.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }

            ResultModel model = new ResultModel();
            model.JobId = jobModel.JobId;
            model.JobDesc = jobModel.JobDesc;
            model.Vacancies = jobModel.Vacancies;
            model.Candidates = ASCommonDAL.GetApplicantForTheJob(jobModel.JobId);
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            @ViewBag.Controller = "Staff";
            return PartialView("_PartialReleaseResults", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ReleaseResults(ResultModel resultModel)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.IsStaffAllowed(Session, "RightToPublish"))
            {
                @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
                @ViewBag.Message = "User Rights Denied! You don't have permission for this.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            User user = ((User)Session["user"]);
            ASCommonDAL.ReleaseResultToDB(resultModel, user);
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            @ViewBag.Message = "Result Released for Job - [" + resultModel.JobId + "] " + resultModel.JobDesc + ".";
            return View("Message");
        }

        public ActionResult ViewJobApplicationStatus()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            //int user_id = ((User)Session["user"]).user_id;
            //List<ApplicationModel> model = CandidateDAL.getApplicationDetails(user_id);
            return View(ASCommonDAL.getApplicantList());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewJobApplicationStatus(ApplicantListModel model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            IEnumerable<ApplicationModel> data = CandidateDAL.GetApplicationDetails(model.CandidateId);
            return PartialView("_PartialJobApplicationStatus", data);
        }

        public ActionResult ViewJobApplicationByJobId()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            //int user_id = ((User)Session["user"]).user_id;
            //List<ApplicationModel> model = CandidateDAL.getApplicationDetails(user_id);
            return View(ASCommonDAL.GetJobList());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewJobApplicationByJobId(JobListModel model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            IEnumerable<ApplicationModel> data = CandidateDAL.GetApplicationDetailsByJobId(model.JobId);
            return PartialView("_PartialJobApplicationStatus", data);
        }

        public ActionResult SelectedApplicants()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            List<ShowApplicantModel> candidates = CandidateDAL.GetCandidates("A");
            return View(candidates);
        }

        public ActionResult BlockedApplicants()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "staff"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            List<ShowApplicantModel> candidates = CandidateDAL.GetCandidates("R");
            return View(candidates);
        }
    }
}
