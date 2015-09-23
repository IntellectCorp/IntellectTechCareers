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
            return View("Home/ManagerHome", AdminDAL.getManagerHome());
        }

        public ActionResult AddJobRoles()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddJobRoles(JobRole model)
        {
            AdminDAL.addNewJobRole(model);
         
            @ViewBag.Message = "Job Role - "+model.Name+" Added !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }

        public ActionResult AddQualification()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AddQualification(Qualification model)
        {
            AdminDAL.addNewQualification(model);

            @ViewBag.Message = "New Qualification - " + model.Name + " Added !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }

        public ActionResult PostJob()
        {
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
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            //int user_id = ((User)Session["user"]).user_id;
            //List<ApplicationModel> model = CandidateDAL.getApplicationDetails(user_id);
            return View("../Staff/ViewJobApplicationStatus", ASCommonDAL.getApplicantList());
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ViewJobApplicationStatus(ApplicantListModel model)
        {
            IEnumerable<ApplicationModel> data = CandidateDAL.getApplicationDetails(model.CandidateId);
            return PartialView("_PartialJobApplicationStatus", data);
        }

        public ActionResult ChangeOthersPassword()
        {
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
    }
}
