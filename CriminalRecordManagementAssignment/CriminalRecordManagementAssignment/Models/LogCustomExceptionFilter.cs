using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CriminalRecordManagementAssignment.Models
{
    public class LogCustomExceptionFilter:FilterAttribute,IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                var exceptionMessage = filterContext.Exception.Message;
                var stackTrace = filterContext.Exception.StackTrace;
                var controllerName = filterContext.RouteData.Values["controller"].ToString();
                var actionName = filterContext.RouteData.Values["action"].ToString();

                string message = "Date :" + DateTime.Now.ToString() + "\n Controller :" + controllerName + "\n Action :" +
                    actionName + "\n Error Message : " + exceptionMessage + " \n stackTrace :" + stackTrace + "\n";

                File.AppendAllText(HttpContext.Current.Server.MapPath("~/Logs/ExceptionLog.txt"), message);
                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Errors"
                };
            }
        }
    }
}