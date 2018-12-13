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
    public class AppListsController : Controller
    {
        private ProjectsDbContext db = new ProjectsDbContext();

        // GET: AppLists
        public ActionResult Index()
        {
            var c02_AppLists = db.C02_AppLists.Include(c => c.C04_ProjectPhase);
            return View(c02_AppLists.ToList());
        }

        // GET: AppLists
        public ActionResult UserIndex()
        {
            var c02_AppLists = db.C02_AppLists.OrderBy(s => s.ProjectPhase).Include(c => c.C04_ProjectPhase).Include(c => c.C02a_AppResults);
            return View(c02_AppLists.ToList());
        }

        // GET: AppLists/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02_AppLists c02_AppLists = db.C02_AppLists.Find(id);
            if (c02_AppLists == null)
            {
                return HttpNotFound();
            }
            return View(c02_AppLists);
        }

        // GET: AppLists/Create
        public ActionResult Create()
        {
            ViewBag.ProjectPhase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName");
            return View();
        }

        // POST: AppLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,ProjectPhase")] C02_AppLists c02_AppLists)
        {
            if (ModelState.IsValid)
            {
                db.C02_AppLists.Add(c02_AppLists);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectPhase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c02_AppLists.ProjectPhase);
            return View(c02_AppLists);
        }

        // GET: AppLists/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02_AppLists c02_AppLists = db.C02_AppLists.Find(id);
            if (c02_AppLists == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectPhase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c02_AppLists.ProjectPhase);
            return View(c02_AppLists);
        }

        // POST: AppLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,ProjectPhase")] C02_AppLists c02_AppLists)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c02_AppLists).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectPhase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c02_AppLists.ProjectPhase);
            return View(c02_AppLists);
        }

        // GET: AppLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C02_AppLists c02_AppLists = db.C02_AppLists.Find(id);
            if (c02_AppLists == null)
            {
                return HttpNotFound();
            }
            return View(c02_AppLists);
        }

        // POST: AppLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C02_AppLists c02_AppLists = db.C02_AppLists.Find(id);
            db.C02_AppLists.Remove(c02_AppLists);
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
