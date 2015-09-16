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
            return View("StaffHome");
        }

        public ActionResult PostJob()
        {
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";

            JobRoleModel model = new JobRoleModel();
            model.JobRoles = new List<JobRole>{
                        new JobRole {Id = 0, Name = "Cho"},
                        new JobRole {Id = 1, Name = "Chio"},
                        new JobRole {Id = 2, Name = "Choo"},
                        new JobRole {Id = 3, Name = "Chyo"}
                        };
            return View(model);
        }

    }
}
