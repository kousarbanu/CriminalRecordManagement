using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CriminalRecordManagementAssignment.Models;

namespace CriminalRecordManagementAssignment.Controllers
{
    public class JailsController : Controller
    {
        private CrimalProjectEntities1 db = new CrimalProjectEntities1();

        // GET: Jails
        public ActionResult Index()
        {
            return View(db.Jails.ToList());
        }

        // GET: Jails/Details/5
        [Authorize(Roles = "ACP,PC,DCP,JCP")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jail jail = db.Jails.Find(id);
            if (jail == null)
            {
                return HttpNotFound();
            }
            return View(jail);
        }

        // GET: Jails/Create
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Create([Bind(Include = "Jail_ID,Jail_Name,Location,Pincode,Jailer")] Jail jail)
        {
            if (ModelState.IsValid)
            {
                db.Jails.Add(jail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jail);
        }

        // GET: Jails/Edit/5
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jail jail = db.Jails.Find(id);
            if (jail == null)
            {
                return HttpNotFound();
            }
            return View(jail);
        }

        // POST: Jails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Edit([Bind(Include = "Jail_ID,Jail_Name,Location,Pincode,Jailer")] Jail jail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jail);
        }

        // GET: Jails/Delete/5
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jail jail = db.Jails.Find(id);
            if (jail == null)
            {
                return HttpNotFound();
            }
            return View(jail);
        }

        // POST: Jails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            Jail jail = db.Jails.Find(id);
            db.Jails.Remove(jail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
