using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IntellectTechCareers.Controllers
{
    public class CandidateController : Controller
    {
        //
        // GET: /Candidate/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateProfile()
        {
            return View(Session["user"]);
        }

        public ActionResult ViewJobDetails()
        {
            return View(Session["user"]);
        }

    }
}
