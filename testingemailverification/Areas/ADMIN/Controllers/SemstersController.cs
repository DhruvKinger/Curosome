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
{
    [Authorize(Roles = "ADMIN")]
    public class SemstersController : Controller
    {
        private Entities db = new Entities();

        // GET: ADMIN/Semsters
        public ActionResult Index()
        {
            return View(db.Semsters.ToList());
        }

        // GET: ADMIN/Semsters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semster semster = db.Semsters.Find(id);
            if (semster == null)
            {
                return HttpNotFound();
            }
            return View(semster);
        }

        // GET: ADMIN/Semsters/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADMIN/Semsters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "sid,SemsterNo")] Semster semster)
        {
            if (ModelState.IsValid)
            {
                db.Semsters.Add(semster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(semster);
        }

        // GET: ADMIN/Semsters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semster semster = db.Semsters.Find(id);
            if (semster == null)
            {
                return HttpNotFound();
            }
            return View(semster);
        }

        // POST: ADMIN/Semsters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "sid,SemsterNo")] Semster semster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(semster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(semster);
        }

        // GET: ADMIN/Semsters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Semster semster = db.Semsters.Find(id);
            if (semster == null)
            {
                return HttpNotFound();
            }
            return View(semster);
        }

        // POST: ADMIN/Semsters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Semster semster = db.Semsters.Find(id);
            db.Semsters.Remove(semster);
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
