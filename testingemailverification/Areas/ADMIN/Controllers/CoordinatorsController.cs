using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testingemailverification.Models;

namespace testingemailverification.Areas.ADMIN.Controllers
{[Authorize(Roles ="ADMIN")]
    public class CoordinatorsController : Controller
    {
        private Entities db = new Entities();

        // GET: ADMIN/Coordinators
        public ActionResult Index()
        {
            var coordinators = db.Coordinators.Include(c => c.Department).Include(c => c.Semster);
            return View(coordinators.ToList());
        }

        // GET: ADMIN/Coordinators/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordinator coordinator = db.Coordinators.Find(id);
            if (coordinator == null)
            {
                return HttpNotFound();
            }
            return View(coordinator);
        }

        // GET: ADMIN/Coordinators/Create
        public ActionResult Create()
        {
            ViewBag.dcid = new SelectList(db.Departments, "did", "DepartmentName");
            ViewBag.scid = new SelectList(db.Semsters, "sid", "SemsterNo");
            return View();
        }

        // POST: ADMIN/Coordinators/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cid,CoordinatorName,scid,dcid")] Coordinator coordinator)
        {
            if (ModelState.IsValid)
            {
                db.Coordinators.Add(coordinator);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.dcid = new SelectList(db.Departments, "did", "DepartmentName", coordinator.dcid);
            ViewBag.scid = new SelectList(db.Semsters, "sid", "SemsterNo", coordinator.scid);
            return View(coordinator);
        }

        // GET: ADMIN/Coordinators/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordinator coordinator = db.Coordinators.Find(id);
            if (coordinator == null)
            {
                return HttpNotFound();
            }
            ViewBag.dcid = new SelectList(db.Departments, "did", "DepartmentName", coordinator.dcid);
            ViewBag.scid = new SelectList(db.Semsters, "sid", "SemsterNo", coordinator.scid);
            return View(coordinator);
        }

        // POST: ADMIN/Coordinators/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cid,CoordinatorName,scid,dcid")] Coordinator coordinator)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coordinator).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.dcid = new SelectList(db.Departments, "did", "DepartmentName", coordinator.dcid);
            ViewBag.scid = new SelectList(db.Semsters, "sid", "SemsterNo", coordinator.scid);
            return View(coordinator);
        }

        // GET: ADMIN/Coordinators/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Coordinator coordinator = db.Coordinators.Find(id);
            if (coordinator == null)
            {
                return HttpNotFound();
            }
            return View(coordinator);
        }

        // POST: ADMIN/Coordinators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Coordinator coordinator = db.Coordinators.Find(id);
            db.Coordinators.Remove(coordinator);
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
