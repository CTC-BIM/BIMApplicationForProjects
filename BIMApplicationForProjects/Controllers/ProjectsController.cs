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
    public class ProjectsController : Controller
    {
        private ProjectsDbContext db = new ProjectsDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            var c01_Projects = db.C01_Projects.Include(c => c.C04_ProjectPhase);
            var phase = db.C04_ProjectPhase.ToList();
            ViewBag.Phase = phase;
            return View(c01_Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_Projects c01_Projects = db.C01_Projects.Find(id);
            if (c01_Projects == null)
            {
                return HttpNotFound();
            }
            return View(c01_Projects);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.Phase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,ProjectName,ARCdesigner,ARCmakeModel,STRdesigner,STRmakeModel,MEPdesigner,MEPmakeModel,CIvilDesigner,CivilMakeModel,LandscapeDesigner,LandscapeMakeModel,Phase,BIMtarget")] C01_Projects c01_Projects)
        {
            if (ModelState.IsValid)
            {
                db.C01_Projects.Add(c01_Projects);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Phase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c01_Projects.Phase);
            return View(c01_Projects);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_Projects c01_Projects = db.C01_Projects.Find(id);
            if (c01_Projects == null)
            {
                return HttpNotFound();
            }
            ViewBag.Phase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c01_Projects.Phase);
            return View(c01_Projects);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,ProjectName,ARCdesigner,ARCmakeModel,STRdesigner,STRmakeModel,MEPdesigner,MEPmakeModel,CIvilDesigner,CivilMakeModel,LandscapeDesigner,LandscapeMakeModel,Phase,BIMtarget")] C01_Projects c01_Projects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c01_Projects).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Phase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c01_Projects.Phase);
            return View(c01_Projects);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C01_Projects c01_Projects = db.C01_Projects.Find(id);
            if (c01_Projects == null)
            {
                return HttpNotFound();
            }
            return View(c01_Projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            C01_Projects c01_Projects = db.C01_Projects.Find(id);
            db.C01_Projects.Remove(c01_Projects);
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
