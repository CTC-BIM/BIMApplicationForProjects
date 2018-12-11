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
    public class AppResultsController : Controller
    {
        private ProjectsDbContext db = new ProjectsDbContext();

        // GET: AppResults
        public ActionResult Index()
        {
            var c02a_AppResults = db.C02a_AppResults.Include(c => c.C02_AppLists);
            return View(c02a_AppResults.ToList());
        }

        // GET: AppResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02a_AppResults c02a_AppResults = db.C02a_AppResults.Find(id);
            if (c02a_AppResults == null)
            {
                return HttpNotFound();
            }
            return View(c02a_AppResults);
        }

        // GET: AppResults/Create
        public ActionResult Create()
        {
            ViewBag.AppListID = new SelectList(db.C02_AppLists, "ID", "Name");
            return View();
        }

        // POST: AppResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AppListID,Result,Description")] C02a_AppResults c02a_AppResults)
        {
            if (ModelState.IsValid)
            {
                db.C02a_AppResults.Add(c02a_AppResults);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppListID = new SelectList(db.C02_AppLists, "ID", "Name", c02a_AppResults.AppListID);
            return View(c02a_AppResults);
        }

        // GET: AppResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02a_AppResults c02a_AppResults = db.C02a_AppResults.Find(id);
            if (c02a_AppResults == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppListID = new SelectList(db.C02_AppLists, "ID", "Name", c02a_AppResults.AppListID);
            return View(c02a_AppResults);
        }

        // POST: AppResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AppListID,Result,Description")] C02a_AppResults c02a_AppResults)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c02a_AppResults).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppListID = new SelectList(db.C02_AppLists, "ID", "Name", c02a_AppResults.AppListID);
            return View(c02a_AppResults);
        }

        // GET: AppResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02a_AppResults c02a_AppResults = db.C02a_AppResults.Find(id);
            if (c02a_AppResults == null)
            {
                return HttpNotFound();
            }
            return View(c02a_AppResults);
        }

        // POST: AppResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C02a_AppResults c02a_AppResults = db.C02a_AppResults.Find(id);
            db.C02a_AppResults.Remove(c02a_AppResults);
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
