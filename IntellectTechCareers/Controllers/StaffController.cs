using IntellectTechCareers.Data_Access_Layer;
using IntellectTechCareers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntellectTechCareers.Controllers
{
    public class StaffController : Controller
    {
        //
        // GET: /Staff/

        public ActionResult Index()
        {
            //return View("StaffHome");
            return View("Home/StaffHome", StaffDAL.getStaffHome((User)Session["user"]));
        }

        public ActionResult PostJob()
        {
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";

            Job model = new Job();
            //model.JobRoles = new List<JobRole>{
            //            new JobRole {Id = 0, Name = "Cho"},
            //            new JobRole {Id = 1, Name = "Chio"},
            //            new JobRole {Id = 2, Name = "Choo"},
            //            new JobRole {Id = 3, Name = "Chyo"}
            //            };
            model.JobRoles = ASCommonDAL.getJobRoles();
            model.Skills = ASCommonDAL.getListOfSkills();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostJob(Job model)
        {
            User user = ((User)Session["user"]);
            ASCommonDAL.postJobinDB(model, user);
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
            List<JobModel> model = ASCommonDAL.getJobsToBeInterviewed();
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ScheduleInterviewDialog(JobModel jobModel)
        {
            InterviewModel model = new InterviewModel();
            model.JobId = jobModel.JobId;
            model.JobDesc = jobModel.JobDesc.Replace(Environment.NewLine,"");
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            @ViewBag.Controller = "Staff";
            return PartialView("_PartialScheduleInterview", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ScheduleInterview(InterviewModel interviewModel)
        {
            User user = ((User)Session["user"]);
            ASCommonDAL.scheduleInterviewToDB(interviewModel,user);
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            @ViewBag.Message = "Interview Scheduled for Job - ["+interviewModel.JobId+"] "+interviewModel.JobDesc+".";
            return View("Message");
        }

        public ActionResult ReleaseResults()
        {
            List<JobModel> jobs = ASCommonDAL.getJobsForReleasingResult();
            List<JobWithApplicantsModel> model = new List<JobWithApplicantsModel>();
            foreach (var item in jobs)
            {
                JobWithApplicantsModel jobWithAppl = new JobWithApplicantsModel(item);
                jobWithAppl.ApplicantCount = ASCommonDAL.getApplicantCount(item.JobId);
                model.Add(jobWithAppl);
            }
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ReleaseResultsDialog(JobModel jobModel)
        {
            ResultModel model = new ResultModel();
            model.JobId = jobModel.JobId;
            model.JobDesc = jobModel.JobDesc;
            model.Vacancies = jobModel.Vacancies;
            model.Candidates = ASCommonDAL.getApplicantForTheJob(jobModel.JobId);
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            @ViewBag.Controller = "Staff";
            return PartialView("_PartialReleaseResults", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ReleaseResults(ResultModel resultModel)
        {
            User user = ((User)Session["user"]);
            ASCommonDAL.releaseResultToDB(resultModel, user);
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            @ViewBag.Message = "Result Released for Job - [" + resultModel.JobId + "] " + resultModel.JobDesc + ".";
            return View("Message");
        }
        public ActionResult ViewJobApplicationStatus()
        {
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            //int user_id = ((User)Session["user"]).user_id;
            //List<ApplicationModel> model = CandidateDAL.getApplicationDetails(user_id);
            return View(ASCommonDAL.getApplicantList());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewJobApplicationStatus(ApplicantListModel model)
        {
            IEnumerable<ApplicationModel> data = CandidateDAL.getApplicationDetails(model.CandidateId);
            return PartialView("_PartialJobApplicationStatus", data);
        }
    }
}
