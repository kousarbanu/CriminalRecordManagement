using CriminalRecordManagementAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CriminalRecordManagementAssignment.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        CrimalProjectEntities1 db = new CrimalProjectEntities1();

        public JsonResult IsUserNameAvailable(string User_Name)
        {
            return Json(!db.Users.Any(u => u.User_Name == User_Name), JsonRequestBehavior.AllowGet);
        }
    }
}