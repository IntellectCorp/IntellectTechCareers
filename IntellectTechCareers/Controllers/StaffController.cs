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
            return View("StaffHome");
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
            model.Skills = getListOfSkills();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult PostJob(Job model)
        {
            string desc = model.JobDesc;
            string role = model.JobRole;
            @ViewBag.Message = "Job Posted !";
            @ViewBag.Layout = "~/Views/Shared/_LayoutPageStaff.cshtml";
            return View("Message");
            //return RedirectToAction("Index","Home");
        }
        public List<Skill> getListOfSkills()
        {
            List<Skill> skills = new List<Skill>();
            List<Qualification> qualifications = ASCommonDAL.getQualifications();
            foreach (Qualification item in qualifications)
            {
                Skill skill = new Skill(item);
                skill.Checked = false;
                skills.Add(skill);
            }
            return skills;
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
    }
}
