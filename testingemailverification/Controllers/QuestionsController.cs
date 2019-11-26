using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using testingemailverification.Models;

namespace testingemailverification.Controllers
{
    [Authorize]
    public class QuestionsController : Controller
    {
        private Entities db = new Entities();
        DataTable dt;
        SqlBulkCopy sqlBulk;
      
       
        // GET: Questions
        public ActionResult Index()
        {
            return View(db.Questions.ToList());
        }

        public FileResult Download()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"Files\question.xlsx");
            string fileName = "question.xlsx";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        // GET: Questions/Details/5
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

        [HttpGet]
        public ActionResult UploadQuestions()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public ActionResult Uploadquestions()
        {
            if (Request.Files["FileUpload1"].ContentLength > 0)
            {
                string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
                string connString = "";
                string[] validFileTypes = { ".xls", ".xlsx", ".csv" };

                string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
                if (!Directory.Exists(path1))
                {
                    Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
                }
                if (validFileTypes.Contains(extension))
                {
                    if (System.IO.File.Exists(path1))
                    { System.IO.File.Delete(path1); }
                    Request.Files["FileUpload1"].SaveAs(path1);
                    if (extension == ".csv")
                    {
                        dt = Utility.ReadAsDataTable(path1);
                        ViewBag.Data = dt;
                    }
                    //Connection String to Excel Workbook  
                    else if (extension.Trim() == ".xls")
                    {
                        connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path1 + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                        dt = Utility.ReadAsDataTable(path1);
                        ViewBag.Data = dt;
                    }
                    else if (extension.Trim() == ".xlsx")
                    {
                        connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                        dt = Utility.ReadAsDataTable(path1);
                        ViewBag.Data = dt;
                    }

                }
                else
                {
                    ViewBag.Error = "Please Upload Files in .xls, .xlsx or .csv format";

                }


            }


            sqlBulk = new SqlBulkCopy(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            sqlBulk.DestinationTableName = "Question";
            sqlBulk.ColumnMappings.Add("Department Name", "Department Name");
            sqlBulk.ColumnMappings.Add("Semster No", "Semster No");
            sqlBulk.ColumnMappings.Add("Subject Name", "Subject Name");
            sqlBulk.ColumnMappings.Add("chapter1_twomarkque", "chapter1_twomarkque");
            sqlBulk.ColumnMappings.Add("chapter1_fifteenmarkque", "chapter1_fifteenmarkque");
            sqlBulk.ColumnMappings.Add("chapter2_twomarkque", "chapter2_twomarkque");
            sqlBulk.ColumnMappings.Add("chapter2_fifteenmarkque", "chapter2_fifteenmarkque");
            sqlBulk.ColumnMappings.Add("chapter3_twomarkque", "chapter3_twomarkque");
            sqlBulk.ColumnMappings.Add("chapter3_fifteenmarkque", "chapter3_fifteenmarkque");
            sqlBulk.WriteToServer(dt);


            return View();

        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
       

        // GET: Questions/Edit/5
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

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "qid,Department_Name,Semster_No,Coordinator_Name,Subject_Name,chapter1_twomarkque,chapter1_fifteenmarkque,chapter2_twomarkque,chapter2_fifteenmarkque,chapter3_twomarkque,chapter3_fifteenmarkque")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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
