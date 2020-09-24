using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineAcademy.Models;

namespace OnlineAcademy.Areas.StudentArea.Controllers
{
    public class SchoolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: StudentArea/Schooles
        public ActionResult Index()
        {
            return View(db.Schooles.ToList());
        }

        // GET: StudentArea/Schooles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schoole schoole = db.Schooles.Find(id);
            if (schoole == null)
            {
                return HttpNotFound();
            }
            return View(schoole);
        }

        // GET: StudentArea/Schooles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentArea/Schooles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Schoole schoole)
        {
            if (ModelState.IsValid)
            {
                db.Schooles.Add(schoole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoole);
        }

        // GET: StudentArea/Schooles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schoole schoole = db.Schooles.Find(id);
            if (schoole == null)
            {
                return HttpNotFound();
            }
            return View(schoole);
        }

        // POST: StudentArea/Schooles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Schoole schoole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoole);
        }

        // GET: StudentArea/Schooles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schoole schoole = db.Schooles.Find(id);
            if (schoole == null)
            {
                return HttpNotFound();
            }
            return View(schoole);
        }

        // POST: StudentArea/Schooles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schoole schoole = db.Schooles.Find(id);
            db.Schooles.Remove(schoole);
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
