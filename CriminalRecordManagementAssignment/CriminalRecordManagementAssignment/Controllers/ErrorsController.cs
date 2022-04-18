using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CriminalRecordManagementAssignment.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Errors
        public ActionResult PageNotFoundError()
        {
            return View();
        }
        public ActionResult InternalServerError()
        {
            return View();
        }
        public ActionResult UnauthorizedError()
        {
            return View();
        }
        public ActionResult GenericError()
        {
            return View();
        }
        public ActionResult NoRecordsFound()
        {
            return View();
        }

    }
}