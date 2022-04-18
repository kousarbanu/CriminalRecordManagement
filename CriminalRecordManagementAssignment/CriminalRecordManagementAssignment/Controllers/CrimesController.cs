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
    public class CrimesController : Controller
    {
        private CrimalProjectEntities1 db = new CrimalProjectEntities1();

        // GET: Crimes
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Index()
        {
            var crimes = db.Crimes.Include(c => c.Victim);
            return View(crimes.ToList());
        }
        [HttpPost]
        public ActionResult Index(string SearchFilter)
        {
            var crimes = db.Crimes.Where(c => c.Crime_Name.Contains(SearchFilter));
            if (crimes.ToList().Count > 0)
                return View(crimes.ToList());
            return RedirectToAction("NoRecordsFound", "Errors");
        }
        // GET: Crimes/Details/5
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crime crime = db.Crimes.Find(id);
            if (crime == null)
            {
                return HttpNotFound();
            }
            return View(crime);
        }

        // GET: Crimes/Create
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Create()
        {
            ViewBag.Victim_id = new SelectList(db.Victims, "Victim_ID", "Victim_Name");
            return View();
        }

        // POST: Crimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Crime_ID,Crime_Name,Victim_id,Officer,Officer_Position")] Crime crime)
        {
            if (ModelState.IsValid)
            {
                db.Crimes.Add(crime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Victim_id = new SelectList(db.Victims, "Victim_ID", "Victim_Name", crime.Victim_id);
            return View(crime);
        }

        // GET: Crimes/Edit/5
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crime crime = db.Crimes.Find(id);
            if (crime == null)
            {
                return HttpNotFound();
            }
            ViewBag.Victim_id = new SelectList(db.Victims, "Victim_ID", "Victim_Name", crime.Victim_id);
            return View(crime);
        }

        // POST: Crimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Crime_ID,Crime_Name,Victim_id,Officer,Officer_Position")] Crime crime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(crime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Victim_id = new SelectList(db.Victims, "Victim_ID", "Victim_Name", crime.Victim_id);
            return View(crime);
        }

        // GET: Crimes/Delete/5
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Crime crime = db.Crimes.Find(id);
            if (crime == null)
            {
                return HttpNotFound();
            }
            return View(crime);
        }

        // POST: Crimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Crime crime = db.Crimes.Find(id);
            db.Crimes.Remove(crime);
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
