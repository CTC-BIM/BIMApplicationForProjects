using BIMApplicationForProjects.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace BIMApplicationForProjects.Controllers
{
    public class ProjectAppDetailsController : Controller
    {
        private ProjectsDbContext db = new ProjectsDbContext();
        //private AspNetUser LoginUser = new AspNetUser();
        ApplicationUser LoginUser = new ApplicationUser();
        // GET: ProjectAppDetails
        public ActionResult Index()
        {
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;

            if (LoginUser != null)
            {
                var c03_ProjectAppDetails = db.C03_ProjectAppDetails.Include(c => c.C01_Projects).Include(c => c.C02_AppLists).Include(c => c.C07_Result).Include(c => c.C08_RequestType);
                return View(c03_ProjectAppDetails.ToList());
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: ProjectAppDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return RedirectToAction("Index", "Projects");
            C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.SingleOrDefault(s => s.ID == id);
            var AppResults = db.C02a_AppResults.ToList();
            ViewBag.AppResults = AppResults;
            if (c03_ProjectAppDetails == null) return RedirectToAction("Error");

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

            var ListAppResults = db.C02a_AppResults.Include(s => s.C02_AppLists).Where(s => s.AppListID == c03_ProjectAppDetails.AppID).ToList();
            ViewBag.ListAppResults = ListAppResults;

            return View(c03_ProjectAppDetails);
        }

        #region Origin Create

        // GET: ProjectAppDetails/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName");
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name");
            ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName");
            Session["ThongBao"] = "";
            return View();
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
                string log = ChangeLog.WriteAppLog(newEnity, "Test user", "Add new record");
                Session["ThongBao"] = "Thêm Ứng dụng cho dự án " + curEnity.ProjectID + " thành công và ghi Log " + log;

                return RedirectToAction("Index", "Projects");
                //return RedirectToAction("Index", "Projects");
            }

            ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", curEnity.ProjectID);
            ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", curEnity.AppID);
            return View(curEnity);
        }

        #endregion

        //GET
        public ActionResult CreateID(string id)
        {
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;

            if (LoginUser != null)
            {
                try
                {
                    if (id == null || id.Trim() == "") return RedirectToAction("Index", "Projects", "Không tìm thấy ID này");
                    C01_Projects projectName = db.C01_Projects.Find(id);
                    ViewBag.DateRequest = projectName.DateCreate;

                    //Lấy App được Publish
                    List<C02_AppLists> IssueApp = db.C02_AppLists.Where(s => s.isPublish == 1).ToList();

                    ViewBag.ProjectID = projectName.ProjectID;
                    ViewBag.ProjectName = projectName.ProjectName;


                    ViewBag.AppID = new SelectList(IssueApp, "ID", "Name");
                    ViewBag.StatusID = new SelectList(db.C06_Status, "ID", "Name");
                    ViewBag.ResultID = new SelectList(db.C07_Result, "ID", "Name");
                    ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName");

                    Session["ThongBao"] = "";
                    Session["DateCreate"] = projectName.DateCreate;
                    return View("CreateID");
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Có lỗi " + ex.Message;
                    return View("CreateID");
                }
            }
            return RedirectToAction("Login", "Account");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateID(C03_ProjectAppDetails curEnity, string id)
        {
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;
            if (LoginUser != null)
            {
                //Lấy ra tên ứng dụng muốn đăng ký
                string CurAppName = db.C02_AppLists.FirstOrDefault(s => s.ID == curEnity.AppID).Name;

                //kiểm tra đã có ứng dụng này trong dự án hay chưa
                var curAppInProject = db.C03_ProjectAppDetails.SingleOrDefault(s => s.ProjectID == id && s.AppID == curEnity.AppID);

                if (ModelState.IsValid && curAppInProject == null) // Chưa có ứng dụng này
                {
                    C03_ProjectAppDetails newEnity = new C03_ProjectAppDetails();
                    newEnity.AppID = curEnity.AppID;
                    newEnity.ProjectID = id;
                    if (curEnity.DateRequest == null)
                    {
                        newEnity.DateRequest = DateTime.Now;
                    }
                    else
                    {
                        newEnity.DateRequest = curEnity.DateRequest;
                    }
                    if (curEnity.DeadLine == null)
                    {
                        DateTime date = newEnity.DateRequest.Value;
                        newEnity.DeadLine = date.AddDays(7);
                    }
                    else
                    {
                        newEnity.DeadLine = curEnity.DeadLine;
                    }

                    newEnity.OtherRequest = curEnity.OtherRequest;
                    newEnity.Resource = curEnity.Resource;
                    newEnity.isAccept = false;
                    newEnity.StatusID = 1;

                    if (curEnity.UserRequest == null)
                    {
                        newEnity.UserRequest = LoginUser.UserName;
                    }
                    else
                    {
                        newEnity.UserRequest = curEnity.UserRequest;
                    }

                    newEnity.ResultID = 1;
                    newEnity.RequestID = curEnity.RequestID;

                    db.C03_ProjectAppDetails.Add(newEnity);
                    db.SaveChanges();

                    string log = ChangeLog.WriteAppLog(newEnity, LoginUser.UserName, "Add new BIM application");
                    Session["ThongBao"] = "Thêm Ứng dụng cho dự án " + id + " thành công và ghi Log " + log;

                    string TenUngDung = db.C02_AppLists.FirstOrDefault(s => s.ID == newEnity.AppID).Name;

                    #region Gui Email
                    //using (MailsController email = new MailsController())
                    //{
                    //    try
                    //    {
                    //        string nguoiGui = "teamcbimtech@gmail.com";
                    //        string UserEmail = "truongchinhan2010@gmail.com";
                    //        string AdminEmail= "nhantc@coteccons.vn";

                    //        if (email.SendToUserEmail(nguoiGui, UserEmail, TenUngDung, newEnity.ProjectID) == "OK")
                    //        {
                    //            Session["ThongBao"] = "Gửi Email to User thành công";
                    //        }
                    //        if (email.SendToAdminEmail(UserEmail, AdminEmail, TenUngDung,newEnity.ProjectID) == "OK"/* && email.SendToAdminEmail(UserEmail, "luatnkt@coteccons.vn", TenUngDung, newEnity.ProjectID) == "OK"*/)
                    //        {
                    //            Session["ThongBao"] += " - Gửi Email to Admin thành công";
                    //        }

                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Session["ThongBao"] = "Gửi Email Thất bại vì " + ex.Message;
                    //    }

                    //}

                    #endregion

                    return RedirectToAction("Index", "Projects");
                }
                // đã có ứng dụng này cho dự án này
                //ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", curEnity.ProjectID);
                ViewBag.ProjectID = id;
                string ProjectName = db.C01_Projects.FirstOrDefault(s => s.ProjectID == id).ProjectName;
                ViewBag.ProjectName = ProjectName;
                //Lấy App được Publish
                List<C02_AppLists> IssueApp = db.C02_AppLists.Where(s => s.isPublish == 1).ToList();
                ViewBag.AppID = new SelectList(IssueApp, "ID", "Name", curEnity.AppID);

                ViewBag.StatusID = new SelectList(db.C06_Status, "ID", "Name", curEnity.StatusID);
                ViewBag.ResultID = new SelectList(db.C07_Result, "ID", "Name", curEnity.ResultID);
                ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName", curEnity.RequestID);

                Session["ThongBao"] = "Dự án '" + ProjectName.ToUpper() + "' đã có ứng dụng '" + CurAppName + "', vui lòng xem lại";
                return View("CreateID", curEnity);
            }
            return RedirectToAction("Login", "Account");
        }


        // GET: ProjectAppDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;
            if (LoginUser != null)
            {
                if (id == null) return RedirectToAction("Index", "Projects");
                C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.SingleOrDefault(s => s.ID == id);
                if (c03_ProjectAppDetails == null) return RedirectToAction("Error");

                ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", c03_ProjectAppDetails.ProjectID);
                ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", c03_ProjectAppDetails.AppID);
                ViewBag.StatusID = new SelectList(db.C06_Status, "ID", "Name", c03_ProjectAppDetails.StatusID);
                ViewBag.ResultID = new SelectList(db.C07_Result, "ID", "Name", c03_ProjectAppDetails.ResultID);
                ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName", c03_ProjectAppDetails.RequestID);
                var AppList = db.C02_AppLists.ToList();
                ViewBag.AppList = AppList;

                return View(c03_ProjectAppDetails);
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: ProjectAppDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(C03_ProjectAppDetails c03_ProjectAppDetails)
        {
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;
            if (LoginUser != null)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(c03_ProjectAppDetails).State = EntityState.Modified;
                    db.SaveChanges();
                    string log = ChangeLog.WriteAppLog(c03_ProjectAppDetails, LoginUser.UserName, "Edit App details");
                    Session["ThongBao"] = "Cập nhật thông tin đăng ký cho dự án thành công và ghi log " + log;
                    return RedirectToAction("Index", "Projects");
                }
                ViewBag.ProjectID = new SelectList(db.C01_Projects, "ProjectID", "ProjectName", c03_ProjectAppDetails.ProjectID);
                ViewBag.AppID = new SelectList(db.C02_AppLists, "ID", "Name", c03_ProjectAppDetails.AppID);
                ViewBag.StatusID = new SelectList(db.C06_Status, "ID", "Name", c03_ProjectAppDetails.StatusID);
                ViewBag.ResultID = new SelectList(db.C07_Result, "ID", "Name", c03_ProjectAppDetails.ResultID);
                ViewBag.RequestID = new SelectList(db.C08_RequestType, "ID", "TypeName", c03_ProjectAppDetails.RequestID);
                return View("Details", "Projects", c03_ProjectAppDetails);
            }
            return RedirectToAction("Login", "Account");
        }

        // GET: ProjectAppDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;
            if (LoginUser != null)
            {
                try
                {
                    if (id == null) return RedirectToAction("Index", "Projects");

                    C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.SingleOrDefault(s => s.ID == id);

                    if (c03_ProjectAppDetails == null) throw new Exception("Không tìm thấy ID này ");
                    var project = db.C01_Projects.ToList();
                    ViewBag.Project = project;
                    var status = db.C06_Status.ToList();
                    ViewBag.Status = status;
                    var Applist = db.C02_AppLists.ToList();
                    ViewBag.Applist = Applist;
                    var ResultRequest = db.C08_RequestType.ToList();
                    ViewBag.ResultRequest = ResultRequest;

                    return View(c03_ProjectAppDetails);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View();
                }
            }
            return RedirectToAction("Login", "Account");
        }

        // POST: ProjectAppDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //LoginUser = Session["LoginUser"] as AspNetUser;
            LoginUser = Session["LoginUser"] as ApplicationUser;
            if (LoginUser != null)
            {
                try
                {
                    if (id < 0) throw new Exception("ID không đúng");

                    C03_ProjectAppDetails c03_ProjectAppDetails = db.C03_ProjectAppDetails.FirstOrDefault(s => s.ID == id);

                    db.C03_ProjectAppDetails.Remove(c03_ProjectAppDetails);

                    db.SaveChanges();
                    string log = ChangeLog.WriteAppLog(c03_ProjectAppDetails, LoginUser.UserName, "Delete App");
                    Session["ThongBao"] = "Hủy đăng ký cho dự án " + id + " thành công và ghi log " + log;
                    return RedirectToAction("Index", "Projects", ViewBag.ThongBao);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return View("Error");
                }
            }
            return RedirectToAction("Login", "Account");
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
