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
            return View("ManagerHome", AdminDAL.getManagerHome());
        }

        public ActionResult PostJob()
        {
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            Job model = new Job();
            model.JobRoles = ASCommonDAL.getJobRoles();
            return View("../Staff/PostJob",model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostJob(Job model)
        {
            string desc = model.JobDesc;
            string role = model.JobRole;
            @ViewBag.Message = "Job Posted !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageManager.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }
    }
}
