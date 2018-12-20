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
    public class AdminProjectAppDetailsController : Controller
    {
        private ProjectsDbContext db = new ProjectsDbContext();

        // GET: AdminProjectAppDetails
        public ActionResult Index()
        {
            var c03_ProjectAppDetails = db.C03_ProjectAppDetails.Include(c => c.C01_Projects).Include(c => c.C02_AppLists).Include(c => c.C06_Status).Include(c => c.C07_Result).Include(c => c.C08_RequestType);
            return View(c03_ProjectAppDetails.ToList());
        }

        // GET: AdminProjectAppDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.Find(id);
            if (c03_ProjectAppDetails == null)
            {
                return HttpNotFound();
            }
            return View(c03_ProjectAppDetails);
        }

        // GET: AdminProjectAppDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName");
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name");
            ViewBag.StatusID = new SelectList(db.C06_Status, "ID", "Name");
            ViewBag.ResultID = new SelectList(db.C07_Result, "ID", "Name");
            ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName");
            return View();
        }

        // POST: AdminProjectAppDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProjectID,AppID,DateRequest,UserRequest,isAccept,OtherRequest,StatusID,BIMerName,DeadLine,ResultID,RequestID,Resource,AppCode")] C03_ProjectAppDetails c03_ProjectAppDetails)
        {
            if (ModelState.IsValid)
            {
                db.C03_ProjectAppDetails.Add(c03_ProjectAppDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", c03_ProjectAppDetails.ProjectID);
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", c03_ProjectAppDetails.AppID);
            ViewBag.StatusID = new SelectList(db.C06_Status, "ID", "Name", c03_ProjectAppDetails.StatusID);
            ViewBag.ResultID = new SelectList(db.C07_Result, "ID", "Name", c03_ProjectAppDetails.ResultID);
            ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName", c03_ProjectAppDetails.RequestID);
            return View(c03_ProjectAppDetails);
        }

        // GET: AdminProjectAppDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.Find(id);
            if (c03_ProjectAppDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", c03_ProjectAppDetails.ProjectID);
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", c03_ProjectAppDetails.AppID);
            ViewBag.StatusID = new SelectList(db.C06_Status, "ID", "Name", c03_ProjectAppDetails.StatusID);
            ViewBag.ResultID = new SelectList(db.C07_Result, "ID", "Name", c03_ProjectAppDetails.ResultID);
            ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName", c03_ProjectAppDetails.RequestID);
            return View(c03_ProjectAppDetails);
        }

        // POST: AdminProjectAppDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectID,AppID,DateRequest,UserRequest,isAccept,OtherRequest,StatusID,BIMerName,DeadLine,ResultID,RequestID,Resource,AppCode")] C03_ProjectAppDetails c03_ProjectAppDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c03_ProjectAppDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", c03_ProjectAppDetails.ProjectID);
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", c03_ProjectAppDetails.AppID);
            ViewBag.StatusID = new SelectList(db.C06_Status, "ID", "Name", c03_ProjectAppDetails.StatusID);
            ViewBag.ResultID = new SelectList(db.C07_Result, "ID", "Name", c03_ProjectAppDetails.ResultID);
            ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName", c03_ProjectAppDetails.RequestID);
            return View(c03_ProjectAppDetails);
        }

        // GET: AdminProjectAppDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.Find(id);
            if (c03_ProjectAppDetails == null)
            {
                return HttpNotFound();
            }
            return View(c03_ProjectAppDetails);
        }

        // POST: AdminProjectAppDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.Find(id);
            db.C03_ProjectAppDetails.Remove(c03_ProjectAppDetails);
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
