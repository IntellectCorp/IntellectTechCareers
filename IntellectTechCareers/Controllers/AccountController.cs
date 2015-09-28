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
            {
                ViewBag.Message = "Username or Password is incorrect ! ";
                return View("LoginFailed");
            }
            else if (role.Equals("Disabled"))
            {
                ViewBag.Message = "Your account has been disabled !";
                return View("LoginFailed");
            }
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

        [HttpPost]
        public string CheckUserNameAvailibility(RegisterModel user)
        {
            return AccountDAL.IsUserNameAvailable(user.UserName);
        }

        [HttpGet]
        public ActionResult DeleteCandidate()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }
            else if (!Navigator.UserRoleValidation(Session, "candidate"))
            {
                @ViewBag.Message = "Access Denied !   You are not allowed to visit this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            int user_id = ((User)Session["user"]).user_id;

            AccountDAL.DeleteUser(user_id);
            return RedirectToAction("Logout", "Account");
        }

        public ActionResult ChangePassword()
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            User user = (User)Session["user"];
            ChangePasswordModel model = new ChangePasswordModel();
            model.user_id = user.user_id;
            model.username = user.username;
            model.password = user.password;

            ViewBag.ErrorMessage = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!Navigator.IsUserLoggedIn(Session))
            {
                @ViewBag.Message = "Sorry! You need to login to view this page.";
                return View("Message");
                //return RedirectToAction("Login", "Account");
            }

            User user = (User)Session["user"];
            String currentPasswordHash = StringUtils.GetMD5Hash(StringUtils.Reverse(model.currentPassword));
            if (!currentPasswordHash.Equals(user.password))
            {
                ViewBag.ErrorMessage = "Current password is incorrect !";
                return View(model);
            }

            if (user.password.Equals(StringUtils.GetMD5Hash(StringUtils.Reverse(model.newPassword))))
            {
                ViewBag.ErrorMessage = " New Password cannot be same as current Password !";
                return View(model);
            }

            bool isSuccess = AccountDAL.ChangePassword(user.user_id, StringUtils.GetMD5Hash(StringUtils.Reverse(model.newPassword)));

            if (isSuccess)
                return RedirectToAction("Logout", "Account");
            else
            {
                ViewBag.ErrorMessage = "Failed to change the password";
                return View(model);
            }
        }

    }
}
