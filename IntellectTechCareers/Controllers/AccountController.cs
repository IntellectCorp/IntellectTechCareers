using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IntellectTechCareers.Models;
using IntellectTechCareers.Utils;

namespace IntellectTechCareers.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            bool isValid = DBUtils.validateUser(model.UserName, model.Password);

            if (!isValid)
                return RedirectToAction("Login", "Account");
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
