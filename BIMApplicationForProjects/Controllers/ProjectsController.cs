using BIMApplicationForProjects.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace BIMApplicationForProjects.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectsDbContext db = new ProjectsDbContext();

        // GET: Projects
        public ActionResult Index()
        {
            var c01_Projects = db.C01_Projects.Include(c => c.C04_ProjectPhase);
            //Phase
            var phase = db.C04_ProjectPhase.ToList();
            ViewBag.Phase = phase;
            //Project with BIM
            var BIMproject = db.C03_ProjectAppDetails.ToList();
            ViewBag.BIMProjects = BIMproject;
            //AppList Detail
            var AppList = db.C02_AppLists.ToList();
            ViewBag.AppList = AppList;
            //Tình trạng
            var status = db.C06_Status.ToList();
            ViewBag.Status = status;
            //Kết quả - Result
            var result = db.C07_Result.ToList();
            ViewBag.Result = result;
            //Request Type
            var RequestType = db.C08_RequestType.ToList();
            ViewBag.RequestType = RequestType;

            return View(c01_Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(string id)
        {
            if (id == null) return RedirectToAction("Index");

            //Phase
            var phase = db.C04_ProjectPhase.ToList();
            ViewBag.Phase = phase;
            //Project with BIM
            var BIMproject = db.C03_ProjectAppDetails.ToList();
            ViewBag.BIMProjects = BIMproject;
            //AppList Detail
            var AppList = db.C02_AppLists.ToList();
            ViewBag.AppList = AppList;
            //Tình trạng
            var status = db.C06_Status.ToList();
            ViewBag.Status = status;
            //Kết quả - Result
            var result = db.C07_Result.ToList();
            ViewBag.Result = result;
            //Request Type
            var RequestType = db.C08_RequestType.ToList();
            ViewBag.RequestType = RequestType;
            C01_Projects c01_Projects = db.C01_Projects.Find(id);

            if (c01_Projects == null)
            {
                return HttpNotFound();
            }
            return View(c01_Projects);
        }
        public ActionResult _AppListPartialView(string id)
        {
            if (id == null || id.Trim() == "") return RedirectToAction("Index", "Projects");
            try
            {
                var phase = db.C04_ProjectPhase.ToList();
                ViewBag.Phase = phase;
                //Project with BIM
                var BIMproject = db.C03_ProjectAppDetails.ToList();
                ViewBag.BIMProjects = BIMproject;
                //AppList Detail
                var AppList = db.C02_AppLists.ToList();
                ViewBag.AppList = AppList;
                //Tình trạng
                var status = db.C06_Status.ToList();
                ViewBag.Status = status;
                //Kết quả - Result
                var result = db.C07_Result.ToList();
                ViewBag.Result = result;
                //Request Type
                var RequestType = db.C08_RequestType.ToList();
                ViewBag.RequestType = RequestType;

                C01_Projects project = db.C01_Projects.FirstOrDefault(s => s.ProjectID == id);
                ViewBag.Project = project;

                List<C03_ProjectAppDetails> items = db.C03_ProjectAppDetails.Where(s => s.ProjectID == id).ToList();

                return PartialView(items);
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = "Có lỗi " + ex.Message;
                return RedirectToAction("Index", "Projects");
            }

        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            ViewBag.Phase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName");
            Session["ThongBao"] = "";
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(C01_Projects c01_Projects)
        {
            if (ModelState.IsValid)
            {                
                var curProjectID = db.C01_Projects.FirstOrDefault(s => s.ProjectID == c01_Projects.ProjectID);
                
                if (curProjectID == null)
                {
                    db.C01_Projects.Add(c01_Projects);
                    db.SaveChanges();

                    string log = ChangeLog.WriteProjectLog(c01_Projects, "Test user", "Add New Project");
                    Session["ThongBao"] = "Thêm Dự án " + c01_Projects.ProjectID + " thành công và ghi Log " + log;
                    return RedirectToAction("Index");
                }
                else
                {
                    Session["ThongBao"] = "Đã có Dự án " + c01_Projects.ProjectID + " trong Database ";
                    return RedirectToAction("Create");
                }
            }

            ViewBag.Phase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c01_Projects.Phase);
            return View(c01_Projects);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null) return RedirectToAction("Details", new { id = id });

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
        public ActionResult Edit(C01_Projects c01_Projects)
        {
            if (ModelState.IsValid)
            {
                db.Entry(c01_Projects).State = EntityState.Modified;
                db.SaveChanges();

                string log = ChangeLog.WriteProjectLog(c01_Projects, "Test user", "Edit Project details");
                Session["ThongBao"] = "Cập nhật thông tin dự án " + c01_Projects.ProjectID + " thành công và ghi Log " + log;

                var phase = db.C04_ProjectPhase.ToList();
                ViewBag.Phase = phase;
                return View("Details", c01_Projects);
                //return RedirectToAction("Details","Projects", c01_Projects);
            }
            ViewBag.Phase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c01_Projects.Phase);
            return View("Details", c01_Projects);
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

            string log = ChangeLog.WriteProjectLog(c01_Projects, "Test user", "Delete Project");
            Session["ThongBao"] = "Xóa dự án " + c01_Projects.ProjectID + " thành công và ghi Log " + log;

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
