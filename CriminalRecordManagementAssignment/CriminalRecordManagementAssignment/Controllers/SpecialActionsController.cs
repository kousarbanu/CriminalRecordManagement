using CriminalRecordManagementAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CriminalRecordManagementAssignment.Controllers
{
    public class SpecialActionsController : Controller
    {
        // GET: SpecialActions
        private CrimalProjectEntities1 db = new CrimalProjectEntities1();
        public ActionResult Index(int id)
        {
            Victim victim = db.Victims.FirstOrDefault(v => v.Victim_ID == id);
            var criminals = db.Criminals.Where(c => c.Victim_id == id);
            ViewBag.Victim = victim;
            return View(criminals.ToList());
        }
    }
}