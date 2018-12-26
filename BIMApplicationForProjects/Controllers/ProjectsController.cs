using BIMApplicationForProjects.Models;
using System;
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
        //private AspNetUser LoginUser = new AspNetUser();
        ApplicationUser LoginUser = new ApplicationUser();
        // GET: Projects
        public ActionResult Index()
        {

            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;

            if (LoginUser != null)
            {
                //Lọc danh sách các dự án theo User Login 
                var c01_Projects = db.C01_Projects.Include(c => c.C04_ProjectPhase).Where(s => s.PMname == LoginUser.UserName);
                //Nếu ko có dự án nào, thông báo và Direct tới trang Tạo dự án
                if (c01_Projects == null || c01_Projects.Count() == 0)
                {
                    Session["ThongBao"] = "Bạn chưa có dự án, vui lòng đăng ký dự án";
                    return RedirectToAction("Create", "Projects");
                }
                //Nếu đã có dự án
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
            return RedirectToAction("Login", "Account");
        }

        // GET: Projects/Details/5
        public ActionResult Details(string id)
        {
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;
            Session["ThongBao"] = "Trang thông tin của dự án.";
            if (LoginUser != null)
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
            return RedirectToAction("Login", "Account");
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
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;
            if (LoginUser != null)
            {
                if (ModelState.IsValid)
                {
                    var curProjectID = db.C01_Projects.FirstOrDefault(s => s.ProjectID == c01_Projects.ProjectID);

                    if (curProjectID == null)
                    {
                        if (c01_Projects.PMname == null)
                        {
                            c01_Projects.PMname = LoginUser.UserName;
                            c01_Projects.DateCreate = DateTime.Now;
                        }
                        else
                        {
                            c01_Projects.PMname = "Chưa cập nhật";
                        }
                         
                        db.C01_Projects.Add(c01_Projects);
                        db.SaveChanges();

                        string log = ChangeLog.WriteProjectLog(c01_Projects, LoginUser.UserName, "Add New Project");
                        Session["ThongBao"] = "Thêm Dự án " + c01_Projects.ProjectID + " thành công và ghi Log " + log;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Session["ThongBao"] = "Đã có Dự án " + c01_Projects.ProjectID + " - " + c01_Projects.ProjectName + " trong Database ";
                        return RedirectToAction("Create");
                    }
                }

                ViewBag.Phase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c01_Projects.Phase);
                return View(c01_Projects);
            }
            return RedirectToAction("Login", "Account");
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
            Session["ThongBao"] = "Vì tính toàn vẹn dữ liệu nên một số trường sẽ không cho phép chỉnh sửa";

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
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;
            if (LoginUser != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(c01_Projects).State = EntityState.Modified;
                    db.SaveChanges();

                    string log = ChangeLog.WriteProjectLog(c01_Projects, LoginUser.UserName, "Edit Project details");
                    Session["ThongBao"] = "Cập nhật thông tin dự án " + c01_Projects.ProjectID + " thành công và ghi Log " + log;

                    var phase = db.C04_ProjectPhase.ToList();
                    ViewBag.Phase = phase;
                    return View("Details", c01_Projects);
                    //return RedirectToAction("Details","Projects", c01_Projects);
                }
                ViewBag.Phase = new SelectList(db.C04_ProjectPhase, "PhaseID", "PhaseName", c01_Projects.Phase);
                return View("Details", c01_Projects);
            }
            return RedirectToAction("Login", "Account");
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

            string log = ChangeLog.WriteProjectLog(c01_Projects, LoginUser.UserName, "Delete Project");
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
