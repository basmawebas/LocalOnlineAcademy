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
    public class CurrentTermsController : Controller
    {
        private StudentDbContext db = new StudentDbContext();

        // GET: StudentArea/CurrentTerms
        public async Task<ActionResult> Index()
        {
            return View(await db.CurrentTerms.ToListAsync());
        }

        // GET: StudentArea/CurrentTerms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentTerm currentTerm = await db.CurrentTerms.FindAsync(id);
            if (currentTerm == null)
            {
                return HttpNotFound();
            }
            return View(currentTerm);
        }

        // GET: StudentArea/CurrentTerms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentArea/CurrentTerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] CurrentTerm currentTerm)
        {
            if (ModelState.IsValid)
            {
                db.CurrentTerms.Add(currentTerm);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(currentTerm);
        }

        // GET: StudentArea/CurrentTerms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentTerm currentTerm = await db.CurrentTerms.FindAsync(id);
            if (currentTerm == null)
            {
                return HttpNotFound();
            }
            return View(currentTerm);
        }

        // POST: StudentArea/CurrentTerms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] CurrentTerm currentTerm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(currentTerm).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(currentTerm);
        }

        // GET: StudentArea/CurrentTerms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CurrentTerm currentTerm = await db.CurrentTerms.FindAsync(id);
            if (currentTerm == null)
            {
                return HttpNotFound();
            }
            return View(currentTerm);
        }

        // POST: StudentArea/CurrentTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CurrentTerm currentTerm = await db.CurrentTerms.FindAsync(id);
            db.CurrentTerms.Remove(currentTerm);
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
