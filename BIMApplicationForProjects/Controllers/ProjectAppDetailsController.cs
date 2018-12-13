using BIMApplicationForProjects.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BIMApplicationForProjects.Controllers
{
    public class ProjectAppDetailsController : Controller
    {
        private ProjectsDbContext db = new ProjectsDbContext();

        // GET: ProjectAppDetails
        public ActionResult Index()
        {
            var c03_ProjectAppDetails = db.C03_ProjectAppDetails.Include(c => c.C01_Projects).Include(c => c.C02_AppLists).Include(c => c.C07_Result).Include(c => c.C08_RequestType);
            return View(c03_ProjectAppDetails.ToList());
        }

        // GET: ProjectAppDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.FirstOrDefault(s => s.ID == id);
            if (c03_ProjectAppDetails == null)
            {
                return HttpNotFound();
            }
            return View(c03_ProjectAppDetails);
        }

        // GET: ProjectAppDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName");
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name");
            ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName");

            return View();
        }
        public ActionResult CreateID(string id)
        {
            try
            {
                if (id == null || id.Trim() == "") return RedirectToAction("Index","Projects","Không tìm thấy ID này");
                C01_Projects projectName = db.C01_Projects.Find(id);
                ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name");
                ViewBag.ProjectID = projectName.ProjectID;
                ViewBag.ProjectName = projectName.ProjectName;
                ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName");

                return View("CreateID");
            }

            catch (Exception ex)
            {
                ViewBag.Error = "Có lỗi " + ex.Message;
                return View("CreateID");
            }
        }

        // POST: ProjectAppDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(C03_ProjectAppDetails curEnity)
        {
            if (ModelState.IsValid)
            {
                C03_ProjectAppDetails newEnity = new C03_ProjectAppDetails();
                newEnity.AppID = curEnity.AppID;
                newEnity.ProjectID = curEnity.ProjectID;
                newEnity.DateRequest = curEnity.DateRequest;
                newEnity.OtherRequest = curEnity.OtherRequest;
                newEnity.DeadLine = curEnity.DeadLine;
                newEnity.StatusID = 1;
                newEnity.UserRequest = curEnity.UserRequest;
                newEnity.ResultID = 1;
                newEnity.RequestID = curEnity.RequestID;

                db.C03_ProjectAppDetails.Add(newEnity);
                db.SaveChanges();

                return RedirectToAction("Index", "Projects");
            }

            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", curEnity.ProjectID);
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", curEnity.AppID);
            return View(curEnity);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateID(C03_ProjectAppDetails curEnity, string id)
        {
            if (ModelState.IsValid)
            {
                C03_ProjectAppDetails newEnity = new C03_ProjectAppDetails();
                newEnity.AppID = curEnity.AppID;
                newEnity.ProjectID = id;
                newEnity.DateRequest = curEnity.DateRequest;
                newEnity.OtherRequest = curEnity.OtherRequest;
                newEnity.DeadLine = curEnity.DeadLine;
                newEnity.StatusID = 1;
                newEnity.UserRequest = curEnity.UserRequest;
                newEnity.ResultID = 1;
                newEnity.RequestID = curEnity.RequestID;

                db.C03_ProjectAppDetails.Add(newEnity);
                db.SaveChanges();

                return RedirectToAction("Index", "Projects");
            }

            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", curEnity.ProjectID);
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", curEnity.AppID);
            return View(curEnity);
        }


        // GET: ProjectAppDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.SingleOrDefault(s => s.ID == id);
            if (c03_ProjectAppDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", c03_ProjectAppDetails.ProjectID);
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", c03_ProjectAppDetails.AppID);
            return View(c03_ProjectAppDetails);
        }

        // POST: ProjectAppDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProjectID,AppID,DateRequest,UserRequest,isAccept,OtherRequest,StatusID,BIMerName")] C03_ProjectAppDetails c03_ProjectAppDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c03_ProjectAppDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", c03_ProjectAppDetails.ProjectID);
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", c03_ProjectAppDetails.AppID);
            return View(c03_ProjectAppDetails);
        }

        // GET: ProjectAppDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null) throw new Exception("ID không đúng ");
                
                C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.SingleOrDefault(s => s.ID == id);

                if (c03_ProjectAppDetails == null) throw new Exception("Không tìm thấy ID này ");

                return View(c03_ProjectAppDetails);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // POST: ProjectAppDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                if (id < 0) throw new Exception("ID không đúng");

                C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.Find(id);

                db.C03_ProjectAppDetails.Remove(c03_ProjectAppDetails);

                db.SaveChanges();

                return RedirectToAction("Index", "Projects");
            }
            catch (Exception ex)
            {
                return ViewBag.Error = ex.Message;
            }
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
