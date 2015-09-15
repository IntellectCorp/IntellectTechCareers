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
        // GET: /Login/

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            string role = DBUtils.validateUserAndGetRole(model.UserName, model.Password);
            Session["Role"] = role;
            Session["User"] = model.UserName;

            if (role.Equals("INVALID"))
                return RedirectToAction("Login", "Account");
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel user, string returnUrl)
        {
            DBUtils.registerUser(user.UserName, user.Address, user.dob, user.ContactNo, user.EmailID, user.gender, user.Password);
            return View("RegistrationSuccess");
        }

    }
}
