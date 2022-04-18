using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CriminalRecordManagementAssignment.Models;

namespace CriminalRecordManagementAssignment.Controllers
{
    public class CriminalsController : Controller
    {
        private CrimalProjectEntities1 db = new CrimalProjectEntities1();

        // GET: Criminals
        public ActionResult Index()
        {
            var criminals = db.Criminals.Include(c => c.Jail).Include(c => c.Victim);
            return View(criminals.ToList());
        }
        [HttpPost]
        public ActionResult Index(string SearchFilter)
        {
            var criminals = db.Criminals.Where(c => c.Criminal_Name.Contains(SearchFilter));
            if (criminals.ToList().Count > 0)
                return View(criminals.ToList());
            return RedirectToAction("NoRecordsFound", "Errors");
        }
        // GET: Criminals/Details/5
        [Authorize(Roles = "ACP,PC,DCP,JCP")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criminal criminal = db.Criminals.Find(id);
            if (criminal == null)
            {
                return HttpNotFound();
            }
            return View(criminal);
        }

        // GET: Criminals/Create
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Create()
        {
            ViewBag.Jail_id = new SelectList(db.Jails, "Jail_ID", "Jail_Name");
            ViewBag.Victim_id = new SelectList(db.Victims, "Victim_ID", "Victim_Name");
            return View();
        }

        // POST: Criminals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Criminal criminal)
        {
            if (ModelState.IsValid)
            {
                string filename = Path.GetFileNameWithoutExtension(criminal.ImageFile.FileName);
                string extension = Path.GetExtension(criminal.ImageFile.FileName);
                filename = criminal.Criminal_Name + extension;
                criminal.Image = "../CriminalsImg/" + filename;
                filename = Path.Combine(Server.MapPath("../CriminalsImg/"), filename);
                criminal.ImageFile.SaveAs(filename);
                db.Criminals.Add(criminal);
                db.SaveChanges();
                ModelState.Clear();
                return RedirectToAction("Index");
            }

            ViewBag.Jail_id = new SelectList(db.Jails, "Jail_ID", "Jail_Name", criminal.Jail_id);
            ViewBag.Victim_id = new SelectList(db.Victims, "Victim_ID", "Victim_Name", criminal.Victim_id);
            return View(criminal);
        }

        // GET: Criminals/Edit/5
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criminal criminal = db.Criminals.Find(id);
            if (criminal == null)
            {
                return HttpNotFound();
            }
            ViewBag.Jail_id = new SelectList(db.Jails, "Jail_ID", "Jail_Name", criminal.Jail_id);
            ViewBag.Victim_id = new SelectList(db.Victims, "Victim_ID", "Victim_Name", criminal.Victim_id);
            return View(criminal);
        }

        // POST: Criminals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Criminal_ID,Criminal_Name,Gender,DOB,Jail_id,Victim_id,Image")] Criminal criminal)
        {
            if (ModelState.IsValid)
            {
                Criminal changed = db.Criminals.Find(criminal.Criminal_ID);
                changed.Criminal_Name = criminal.Criminal_Name;
                changed.Gender = criminal.Gender;
                if (criminal.DOB != null)
                {
                    changed.DOB = criminal.DOB;
                }
                changed.Jail_id = criminal.Jail_id;
                changed.Victim_id = criminal.Victim_id;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Jail_id = new SelectList(db.Jails, "Jail_ID", "Jail_Name", criminal.Jail_id);
            ViewBag.Victim_id = new SelectList(db.Victims, "Victim_ID", "Victim_Name", criminal.Victim_id);
            return View(criminal);
        }

        // GET: Criminals/Delete/5
        [Authorize(Roles = "ACP,PC,DCP")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Criminal criminal = db.Criminals.Find(id);
            if (criminal == null)
            {
                return HttpNotFound();
            }
            return View(criminal);
        }

        // POST: Criminals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Criminal criminal = db.Criminals.Find(id);
            db.Criminals.Remove(criminal);
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
