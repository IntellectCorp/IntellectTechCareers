using IntellectTechCareers.Data_Access_Layer;
using IntellectTechCareers.Models;
using IntellectTechCareers.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntellectTechCareers.Controllers
{
    public class InterviewerController : Controller
    {
        //
        // GET: /Interviewer/

        public ActionResult Index()
        {
            return RedirectToAction("ReleaseResults");
        }

        public ActionResult ReleaseResults()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "interviewer"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            User user = ((User)Session["user"]);
            List<JobModel> jobs = InterviewerDAL.GetJobsForReleasingResultForInterviewer(user);
            List<JobWithApplicantsModel> model = new List<JobWithApplicantsModel>();
            foreach (var item in jobs)
            {
                JobWithApplicantsModel jobWithAppl = new JobWithApplicantsModel(item);
                jobWithAppl.ApplicantCount = ASCommonDAL.GetApplicantCount(item.JobId);
                model.Add(jobWithAppl);
            }
            @ViewBag.Layout = "~/Views/Shared/_Layout.cshtml";
            return View("../Staff/ReleaseResults", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ReleaseResultsDialog(JobModel jobModel)
        {
            ResultModel model = new ResultModel();
            model.JobId = jobModel.JobId;
            model.JobDesc = jobModel.JobDesc;
            model.Vacancies = jobModel.Vacancies;
            model.Candidates = ASCommonDAL.GetApplicantForTheJob(jobModel.JobId);
            @ViewBag.Controller = "Interviewer";
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
            else if (!Navigator.UserRoleValidation(Session, "interviewer"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            User user = ((User)Session["user"]);
            InterviewerDAL.releaseResultToDB(resultModel, user);
            @ViewBag.Message = "Result Released for Job - [" + resultModel.JobId + "] " + resultModel.JobDesc + ".";
            return View("Message");
        }


    }
}
