using IntellectTechCareers.Data_Access_Layer;
using IntellectTechCareers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}
