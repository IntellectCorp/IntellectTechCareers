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
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            return View("Home/ManagerHome", AdminDAL.getManagerHome());
        }

        public ActionResult AddJobRoles()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddJobRoles(JobRole model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            AdminDAL.AddNewJobRole(model);
         
            @ViewBag.Message = "Job Role - "+model.Name+" Added !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }

        public ActionResult AddQualification()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddQualification(Qualification model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            AdminDAL.AddNewQualification(model);

            @ViewBag.Message = "New Qualification - " + model.Name + " Added !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }

        public ActionResult PostJob()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            Job model = new Job();
            model.JobRoles = ASCommonDAL.GetJobRoles();
            model.Skills = ASCommonDAL.GetListOfSkills();
            return View("../Staff/PostJob",model);
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
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            Job mo = model;
            User user = ((User)Session["user"]);
            ASCommonDAL.PostJobInDB(model, user);
            //string desc = model.JobDesc;
            //string role = model.JobRole;
            @ViewBag.Message = "Job Posted !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }

        public ActionResult ViewJobApplicationStatus()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            //int user_id = ((User)Session["user"]).user_id;
            //List<ApplicationModel> model = CandidateDAL.getApplicationDetails(user_id);
            return View("../Staff/ViewJobApplicationStatus", ASCommonDAL.getApplicantList());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewJobApplicationStatus(ApplicantListModel model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
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
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            //int user_id = ((User)Session["user"]).user_id;
            //List<ApplicationModel> model = CandidateDAL.getApplicationDetails(user_id);
            return View("../Staff/ViewJobApplicationByJobId", ASCommonDAL.GetJobList());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewJobApplicationByJobId(JobListModel model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            IEnumerable<ApplicationModel> data = CandidateDAL.GetApplicationDetailsByJobId(model.JobId);
            return PartialView("_PartialJobApplicationStatus", data);
        }

        public ActionResult ChangeOthersPassword()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            List<User> userList = ASCommonDAL.GetUsers();

            List<SelectListItem> candidates = new List<SelectListItem>();
            List<SelectListItem> staffs = new List<SelectListItem>();

            foreach (var item in userList)
            {
                if (item.role.Equals("candidate"))
                    candidates.Add(new SelectListItem { Selected = true, Text = item.username, Value = item.user_id.ToString() });
                else if(item.role.Equals("staff"))
                    staffs.Add(new SelectListItem { Selected = true, Text = item.username, Value = item.user_id.ToString() });
            }

            ViewBag.Candidates = candidates;
            ViewBag.Staffs = staffs;

            return View(new ChangePasswordModel());
        }

        [HttpPost]
        public ActionResult ChangeOthersPassword(ChangePasswordModel model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            int userId = 0;
            if (model.isApplicant)
                userId = model.selectedApplicant;
            else
                userId = model.selectedStaff;

            bool isSuccess = AccountDAL.ChangePassword(userId, StringUtils.GetMD5Hash(StringUtils.Reverse(model.newPassword)));
            string userName = AccountDAL.GetUserName(userId);

            if (isSuccess)
                ViewBag.Message = "The password of " + userName + " has been changed successfully !";
            else
                ViewBag.Message = "Failed to modify the password of " + userName ;

            return View("Message");
        }

        public ActionResult ManageStaffResponsibilities()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            List<Staff> staffs = StaffDAL.GetStaffDetails();
            return View(staffs);
        }

        public string UpdateStaffResponsibilities(Staff staff)
        {
            AdminDAL.UpdateStaffResponsibilities(staff);
            return "";
        }

        public ActionResult ViewStaffResponsibilities()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            List<Staff> staffs = StaffDAL.GetStaffDetails();
            return View(staffs);
        }

        [HttpGet]
        public ActionResult AppointStaff()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            return View(new Staff());
        }

        [HttpPost]
        public ActionResult AppointStaff(Staff staff)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            AdminDAL.AddStaff(staff);

            ViewBag.Message = "Staff has been appointed successfully";
            return View("Message");
        }

        public ActionResult SelectedApplicants()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
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
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            List<ShowApplicantModel> candidates = CandidateDAL.GetCandidates("R");
            return View(candidates);
        }

        public ActionResult ScheduleInterview()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
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
            @ViewBag.Controller = "Admin";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("../Staff/ScheduleInterview", model);
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
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return PartialView("_PartialMessage");
                //return RedirectToAction("Login", "Account");
            }
            InterviewModel model = new InterviewModel();
            model.JobId = jobModel.JobId;
            model.JobDesc = jobModel.JobDesc.Replace(Environment.NewLine, "");
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            @ViewBag.Controller = "Admin";
            return PartialView("../Staff/_PartialScheduleInterview", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ScheduleInterview(InterviewModel interviewModel)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            User user = ((User)Session["user"]);
            ASCommonDAL.ScheduleInterviewToDB(interviewModel, user);
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
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
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
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
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("../Staff/ReleaseResults", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ReleaseResultsDialog(JobModel jobModel)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            ResultModel model = new ResultModel();
            model.JobId = jobModel.JobId;
            model.JobDesc = jobModel.JobDesc;
            model.Vacancies = jobModel.Vacancies;
            model.Candidates = ASCommonDAL.GetApplicantForTheJob(jobModel.JobId);
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            @ViewBag.Controller = "Admin";
            return PartialView("../Staff/_PartialReleaseResults", model);
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
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            User user = ((User)Session["user"]);
            ASCommonDAL.ReleaseResultToDB(resultModel, user);
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            @ViewBag.Message = "Result Released for Job - [" + resultModel.JobId + "] " + resultModel.JobDesc + ".";
            return View("Message");
        }

        [HttpGet]
        public ActionResult ManageInterviewer()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            InterviewerModel model = new InterviewerModel();
            model.Jobs = InterviewerDAL.GetSelectJobsForReleasingResult();
            model.ListOfInterviewers = InterviewerDAL.GetListOfInterviewers();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ManageInterviewer(InterviewerModel model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "manager"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            if (model.NewInterviewer)
            {
                InterviewerDAL.SetInterviewerInDB(model);
            }
            else
            {
                InterviewerDAL.ReallocateInterviewerInDB(model);
            }
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            @ViewBag.Message = "Interviewer Allocated.";
            return View("Message");
        }
    }
}
