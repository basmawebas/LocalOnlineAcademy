using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineAcademy.Models;

namespace OnlineAcademy.Controllers
{
    public class EstatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Estates
        public ActionResult Index()
        {
            var estates = db.Estates.Include(e => e.Country);
            return View(estates.ToList());
        }

        // GET: Estates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estate estate = db.Estates.Find(id);
            if (estate == null)
            {
                return HttpNotFound();
            }
            return View(estate);
        }

        // GET: Estates/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name");
            return View();
        }

        // POST: Estates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CountryId")] Estate estate)
        {
            if (ModelState.IsValid)
            {
                db.Estates.Add(estate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", estate.CountryId);
            return View(estate);
        }

        // GET: Estates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estate estate = db.Estates.Find(id);
            if (estate == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", estate.CountryId);
            return View(estate);
        }

        // POST: Estates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CountryId")] Estate estate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Name", estate.CountryId);
            return View(estate);
        }

        // GET: Estates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estate estate = db.Estates.Find(id);
            if (estate == null)
            {
                return HttpNotFound();
            }
            return View(estate);
        }

        // POST: Estates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Estate estate = db.Estates.Find(id);
            db.Estates.Remove(estate);
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
