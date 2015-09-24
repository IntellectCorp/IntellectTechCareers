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
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            return View("Home/ManagerHome", AdminDAL.getManagerHome());
        }

        public ActionResult AddJobRoles()
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddJobRoles(JobRole model)
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            AdminDAL.addNewJobRole(model);
         
            @ViewBag.Message = "Job Role - "+model.Name+" Added !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }

        public ActionResult AddQualification()
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddQualification(Qualification model)
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            AdminDAL.addNewQualification(model);

            @ViewBag.Message = "New Qualification - " + model.Name + " Added !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }

        public ActionResult PostJob()
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            Job model = new Job();
            model.JobRoles = ASCommonDAL.getJobRoles();
            model.Skills = ASCommonDAL.getListOfSkills();
            return View("../Staff/PostJob",model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostJob(Job model)
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            Job mo = model;
            User user = ((User)Session["user"]);
            ASCommonDAL.postJobinDB(model, user);
            //string desc = model.JobDesc;
            //string role = model.JobRole;
            @ViewBag.Message = "Job Posted !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }
        public ActionResult ViewJobApplicationStatus()
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            //int user_id = ((User)Session["user"]).user_id;
            //List<ApplicationModel> model = CandidateDAL.getApplicationDetails(user_id);
            return View("../Staff/ViewJobApplicationStatus", ASCommonDAL.getApplicantList());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewJobApplicationStatus(ApplicantListModel model)
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            IEnumerable<ApplicationModel> data = CandidateDAL.getApplicationDetails(model.CandidateId);
            return PartialView("_PartialJobApplicationStatus", data);
        }

        public ActionResult ChangeOthersPassword()
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            List<User> userList = ASCommonDAL.getUsers();

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
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            int userId = 0;
            if (model.isApplicant)
                userId = model.selectedApplicant;
            else
                userId = model.selectedStaff;

            bool isSuccess = AccountDAL.ChangePassword(userId, StringUtils.getMD5Hash(StringUtils.Reverse(model.newPassword)));
            string userName = AccountDAL.GetUserName(userId);

            if (isSuccess)
                ViewBag.Message = "The password of " + userName + " has been changed successfully !";
            else
                ViewBag.Message = "Failed to modify the password of " + userName ;

            return View("Message");
        }

        public ActionResult ManageStaffResponsibilities()
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

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
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            List<Staff> staffs = StaffDAL.GetStaffDetails();
            return View(staffs);
        }

        [HttpGet]
        public ActionResult AppointStaff()
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            return View(new Staff());
        }

        [HttpPost]
        public ActionResult AppointStaff(Staff staff)
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            AdminDAL.AddStaff(staff);

            ViewBag.Message = "Staff has been appointed successfully";
            return View("Message");
        }

        public ActionResult SelectedApplicants()
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            List<ShowApplicantModel> candidates = CandidateDAL.getCandidates("A");
            return View(candidates);
        }

        public ActionResult BlockedApplicants()
        {
            if (!Navigator.isUserLoggedIn(Session))
                return RedirectToAction("Login", "Account");

            List<ShowApplicantModel> candidates = CandidateDAL.getCandidates("R");
            return View(candidates);
        }
    }
}
