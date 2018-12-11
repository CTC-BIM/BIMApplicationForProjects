using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BIMApplicationForProjects.Models;

namespace BIMApplicationForProjects.Controllers
{
    public class AppWorkDetailsController : Controller
    {
        private ProjectsDbContext db = new ProjectsDbContext();

        // GET: AppWorkDetails
        public ActionResult Index()
        {
            var c02b_AppWorkDetails = db.C02b_AppWorkDetails.Include(c => c.C02_AppLists);
            return View(c02b_AppWorkDetails.ToList());
        }

        // GET: AppWorkDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02b_AppWorkDetails c02b_AppWorkDetails = db.C02b_AppWorkDetails.Find(id);
            if (c02b_AppWorkDetails == null)
            {
                return HttpNotFound();
            }
            return View(c02b_AppWorkDetails);
        }

        // GET: AppWorkDetails/Create
        public ActionResult Create()
        {
            ViewBag.AppListID = new SelectList(db.C02_AppLists, "ID", "Name");
            return View();
        }

        // POST: AppWorkDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AppListID,WorkName")] C02b_AppWorkDetails c02b_AppWorkDetails)
        {
            if (ModelState.IsValid)
            {
                db.C02b_AppWorkDetails.Add(c02b_AppWorkDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppListID = new SelectList(db.C02_AppLists, "ID", "Name", c02b_AppWorkDetails.AppListID);
            return View(c02b_AppWorkDetails);
        }

        // GET: AppWorkDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02b_AppWorkDetails c02b_AppWorkDetails = db.C02b_AppWorkDetails.Find(id);
            if (c02b_AppWorkDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppListID = new SelectList(db.C02_AppLists, "ID", "Name", c02b_AppWorkDetails.AppListID);
            return View(c02b_AppWorkDetails);
        }

        // POST: AppWorkDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AppListID,WorkName")] C02b_AppWorkDetails c02b_AppWorkDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c02b_AppWorkDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppListID = new SelectList(db.C02_AppLists, "ID", "Name", c02b_AppWorkDetails.AppListID);
            return View(c02b_AppWorkDetails);
        }

        // GET: AppWorkDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02b_AppWorkDetails c02b_AppWorkDetails = db.C02b_AppWorkDetails.Find(id);
            if (c02b_AppWorkDetails == null)
            {
                return HttpNotFound();
            }
            return View(c02b_AppWorkDetails);
        }

        // POST: AppWorkDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C02b_AppWorkDetails c02b_AppWorkDetails = db.C02b_AppWorkDetails.Find(id);
            db.C02b_AppWorkDetails.Remove(c02b_AppWorkDetails);
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
