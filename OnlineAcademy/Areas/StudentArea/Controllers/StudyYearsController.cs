using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineAcademy.Areas.StudentArea.Data;

namespace OnlineAcademy.Areas.StudentArea.Controllers
{
    public class StudyYearsController : Controller
    {
        private StudentDbContext db = new StudentDbContext();

        // GET: StudentArea/StudyYears
        public async Task<ActionResult> Index()
        {
            return View(await db.StudyYears.ToListAsync());
        }

        // GET: StudentArea/StudyYears/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyYear studyYear = await db.StudyYears.FindAsync(id);
            if (studyYear == null)
            {
                return HttpNotFound();
            }
            return View(studyYear);
        }

        // GET: StudentArea/StudyYears/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentArea/StudyYears/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] StudyYear studyYear)
        {
            if (ModelState.IsValid)
            {
                db.StudyYears.Add(studyYear);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(studyYear);
        }

        // GET: StudentArea/StudyYears/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyYear studyYear = await db.StudyYears.FindAsync(id);
            if (studyYear == null)
            {
                return HttpNotFound();
            }
            return View(studyYear);
        }

        // POST: StudentArea/StudyYears/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] StudyYear studyYear)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studyYear).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(studyYear);
        }

        // GET: StudentArea/StudyYears/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudyYear studyYear = await db.StudyYears.FindAsync(id);
            if (studyYear == null)
            {
                return HttpNotFound();
            }
            return View(studyYear);
        }

        // POST: StudentArea/StudyYears/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            StudyYear studyYear = await db.StudyYears.FindAsync(id);
            db.StudyYears.Remove(studyYear);
            await db.SaveChangesAsync();
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
