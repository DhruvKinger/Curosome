using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testingemailverification.Models;
using System.Threading;

namespace testingemailverification.Areas.ADMIN.Controllers
{
    public class RandomizeController : Controller
    {
        private Entities db = new Entities();

        // GET: ADMIN/Randomize
        [HttpGet]
        [Authorize(Roles = "ADMIN")]

        public ActionResult Index(string Dept, string subj, string sem)
        {
           
            ViewBag.Department_Name = db.Questions.Select(r => r.Department_Name).Distinct();
            ViewBag.Subject_Name = db.Questions.Select(r => r.Subject_Name).Distinct();
            ViewBag.Semster_No = db.Questions.Select(r => r.Semster_No).Distinct();

            var model = db.Questions
                         .Where(r => r.Department_Name == Dept || (Dept == null))
                         .Where(r => r.Subject_Name == subj || (subj == null))
                         .Where(r => r.Semster_No.ToString() == sem||(sem==null));

              if (Dept ==null)
            {
                return View(model);

            }
            
           
            else if(Dept.Length>0)
            {
                
                Random x = new Random();
                int y = model.Count();
                int offset = x.Next(0, y);
                var M = model.OrderBy(r => r.Department_Name).Skip(offset).Take(1);
                return View(M);
            }
            return View();
        }
            
        public ActionResult GeneratePDF(int id)
        {
            return new Rotativa.ActionAsPdf("/Details",new {id=id});
        }        
        
        // GET: ADMIN/Randomize/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: ADMIN/Randomize/Create
       

        // POST: ADMIN/Randomize/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        

        // GET: ADMIN/Randomize/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: ADMIN/Randomize/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "qid,Department_Name,Semster_No,Subject_Name,chapter1_twomarkque,chapter1_fifteenmarkque,chapter2_twomarkque,chapter2_fifteenmarkque,chapter3_twomarkque,chapter3_fifteenmarkque")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
               return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: ADMIN/Randomize/Delete/5
       


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
