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
            User user = AccountDAL.validateUserAndGetRole(model.UserName, model.Password);
            string role = user.role;
            
            if (role.Equals("INVALID"))
                return RedirectToAction("Login", "Account");
            else
            {
                Session["user"] = user;
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
            AccountDAL.registerUser(user.UserName, user.Address, user.dob, user.ContactNo, user.EmailID, user.gender, user.Password, user.Name);
            return View("RegistrationSuccess");
        }

        public ActionResult Logout()
        {
            Session.Remove("user");
            return RedirectToAction("Login", "Account");
        }

    }
}
