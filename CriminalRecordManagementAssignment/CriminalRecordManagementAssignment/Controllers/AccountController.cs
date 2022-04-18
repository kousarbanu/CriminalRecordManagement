using CriminalRecordManagementAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CriminalRecordManagementAssignment.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        CrimalProjectEntities1 entities = new CrimalProjectEntities1();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            using (CrimalProjectEntities1 entities = new CrimalProjectEntities1())
            {
                bool IsValidUser = entities.Users.Any(u => u.User_Name.ToLower() == model.User_Name.ToLower() &&
                u.User_Password == model.User_Password);
                if (IsValidUser)
                {
                    FormsAuthentication.SetAuthCookie(model.User_Name, false);
                    return RedirectToAction("Index", "Jails");
                }
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }

        public ActionResult Signup()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var users = entities.Positions.ToList();

            foreach (var user in users)
            {
                items.Add(new SelectListItem { Text = user.Position_Name, Value = user.Position_ID.ToString() });
            }
            ViewBag.PName = items;
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User model)
        {
            entities.Users.Add(model);
            entities.SaveChanges();
         
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}