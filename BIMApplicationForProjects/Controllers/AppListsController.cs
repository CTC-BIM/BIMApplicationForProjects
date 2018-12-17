using BIMApplicationForProjects.Models;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

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
            //Số lượng ứng dụng
            int PA, PB, PSD, PST, PSC, DA, DS, DM, DC, DF = 0;
            //Advance
            PA = db.C02_AppLists.Where(s => s.AdvancePackage == 1 && s.isPublish == 1).Count();
            ViewBag.PA = PA;
            //Basic
            PB = db.C02_AppLists.Where(s => s.BasicPackage == 1 && s.isPublish == 1).Count();
            ViewBag.PB = PB;
            //Design
            PSD = db.C02_AppLists.Where(s => s.Design == 1 && s.isPublish == 1).Count();
            ViewBag.PSD = PSD;
            //Tender
            PST = db.C02_AppLists.Where(s => s.Tender == 1 && s.isPublish == 1).Count();
            ViewBag.PST = PST;
            //Construction
            PSC = db.C02_AppLists.Where(s => s.Construction == 1 && s.isPublish == 1).Count();
            ViewBag.PSC = PSC;
            //ARC
            DA = db.C02_AppLists.Where(s => s.Architech == 1 && s.isPublish == 1).Count();
            ViewBag.DA = DA;
            //STR
            DS = db.C02_AppLists.Where(s => s.Structure == 1 && s.isPublish == 1).Count();
            ViewBag.DS = DS;
            //MEP
            DM = db.C02_AppLists.Where(s => s.MEP == 1 && s.isPublish == 1).Count();
            ViewBag.DM = DM;
            //CIVIL
            DC = db.C02_AppLists.Where(s => s.Civil == 1 && s.isPublish == 1).Count();
            ViewBag.DC = DC;
            //FACADE
            DF = db.C02_AppLists.Where(s => s.Facade == 1 && s.isPublish == 1).Count();
            ViewBag.DF = DF;

            return View(c02_AppLists.ToList());
        }

        public ActionResult _ListApp(string id)
        {
            string filter = "";
            try
            {
                if (id == null || id.Trim() == "")
                {
                    var items = db.C02_AppLists.Where(s => s.isPublish == 1).ToList();
                    ViewBag.PackageName = "All sets";
                    return PartialView("_ListApp", items);
                }
                else
                {
                    var _items = db.C02_AppLists.Where(s => s.isPublish == 1).ToList();
                    ViewBag.PackageName = "All sets";
                    if (id == "PA") //Advance Package
                    {
                        var items = db.C02_AppLists.Where(s => s.AdvancePackage == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "Advance package";
                        return PartialView("_ListApp", items);
                    }
                    else if (id == "PB") //Basic Package
                    {
                        var items = db.C02_AppLists.Where(s => s.BasicPackage == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "Basic";
                        return PartialView("_ListApp", items);
                    }
                    else if (id == "PSD") //Design Package
                    {
                        var items = db.C02_AppLists.Where(s => s.Design == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "for Design state";
                        return PartialView("_ListApp", items);
                    }
                    else if (id == "PST") //Tender Package
                    {
                        var items = db.C02_AppLists.Where(s => s.Tender == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "for Tender state";
                        return PartialView("_ListApp", items);
                    }
                    else if (id == "PSC") //Construction Package
                    {
                        var items = db.C02_AppLists.Where(s => s.Construction == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "for Construction Project";
                        return PartialView("_ListApp", items);
                    }                    
                    else if (id == "DA") //Architech Package
                    {
                        var items = db.C02_AppLists.Where(s => s.Architech == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "for Descipline: Architech";
                        return PartialView("_ListApp", items);
                    }
                    else if (id == "DS") //Structure Package
                    {
                        var items = db.C02_AppLists.Where(s => s.Structure == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "for Descipline: Structure";
                        return PartialView("_ListApp", items);
                    }
                    else if (id == "DM") //MEP Package
                    {
                        var items = db.C02_AppLists.Where(s => s.MEP == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "for Descipline: MEP";
                        return PartialView("_ListApp", items);
                    }
                    else if (id == "DC") //Civil Package
                    {
                        var items = db.C02_AppLists.Where(s => s.Civil == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "for Descipline: Civil";
                        return PartialView("_ListApp", items);
                    }
                    else if (id == "DF") //Facade Package
                    {
                        var items = db.C02_AppLists.Where(s => s.Facade == 1 && s.isPublish == 1).ToList();
                        ViewBag.PackageName = "for Descipline: Facade";
                        return PartialView("_ListApp", items);
                    }
                    return PartialView(_items);
                }
            }
            catch (System.Exception ex)
            {
                Session["ThongBao"] = "Có lỗi xãy ra " + ex.Message;
                return PartialView();
            }

            //switch (name)
            //{
            //    case ("PB"):

            //        break;
            //    case ("PA"):

            //        break;
            //    case ("PSD"):
            //        var PSDitems = db.C02_AppLists.Where(s => s.Design == 1 && s.isPublish == 1).ToList();
            //        break;
            //    case ("PST"):
            //        var PSTitems = db.C02_AppLists.Where(s => s.Tender == 1 && s.isPublish == 1).ToList();
            //        break;
            //    case ("PSC"):
            //        var PSCitems = db.C02_AppLists.Where(s => s.Construction == 1 && s.isPublish == 1).ToList();
            //        break;
            //    case ("DA"):
            //        var DAitems = db.C02_AppLists.Where(s => s.Architech == 1 && s.isPublish == 1).ToList();
            //        break;
            //    case ("DS"):
            //        var DSitems = db.C02_AppLists.Where(s => s.Structure == 1 && s.isPublish == 1).ToList();
            //        break;
            //    case ("DM"):
            //        var DMitems = db.C02_AppLists.Where(s => s.MEP == 1 && s.isPublish == 1).ToList();
            //        break;
            //    case ("DC"):
            //        var DCitems = db.C02_AppLists.Where(s => s.Civil == 1 && s.isPublish == 1).ToList();
            //        break;
            //    case ("DF"):
            //        var DFitems = db.C02_AppLists.Where(s => s.Facade == 1 && s.isPublish == 1).ToList();
            //        break;
            //    default:
            //        break;
            //}



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
            Session["ThongBao"] = "";
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
            Session["ThongBao"] = "";
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
