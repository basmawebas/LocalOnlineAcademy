using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OnlineAcademy.Areas.StudentArea.Data;
using OnlineAcademy.Models;

namespace OnlineAcademy.Areas.StudentArea.Controllers
{
    public class StudentProfilesController : Controller
    {
        private StudentDbContext db = new StudentDbContext();
        private ApplicationDbContext ddb = new ApplicationDbContext();

        // GET: StudentArea/StudentProfiles
        public ActionResult Index()
        {
            var studentProfiles = db.StudentProfiles;
            return View(studentProfiles.ToList());
        }

        // GET: StudentArea/StudentProfiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            if (studentProfile == null)
            {
                return HttpNotFound();
            }
            return View(studentProfile);
        }

        // GET: StudentArea/StudentProfiles/Create
        public ActionResult Create()
        {
            ViewBag.CityId = new SelectList(ddb.Cities, "Id", "Name");
            ViewBag.CountryId = new SelectList(ddb.Countries, "Id", "Name");
            ViewBag.TermId = new SelectList(db.CurrentTerms, "Id", "Name");
            ViewBag.EstateId = new SelectList(ddb.Estates, "Id", "Name");
            ViewBag.SchooleId = new SelectList(ddb.Schooles, "Id", "Name");
            ViewBag.StudyYearId = new SelectList(db.StudyYears, "Id", "Name");
            return View();
        }

        // POST: StudentArea/StudentProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( StudentProfile studentProfile,HttpPostedFileBase profileimage)
        {
            var userId = User.Identity.GetUserId();
            var profile = db.StudentProfiles.Where(x => x.UserId == userId).FirstOrDefault();
            var checkphone = db.StudentProfiles.Where(x => x.PhoneNumber == studentProfile.PhoneNumber).SingleOrDefault();
            string validation = "الرقم الذ ادخلته مسجل بحساب خر";
            if (ModelState.IsValid)
            {
                string profileImagePath = Server.MapPath("~/Uploads/Student/ProfileImage/");

                if (!Directory.Exists(profileImagePath))
                {
                    Directory.CreateDirectory(profileImagePath);
                }
                if (profileimage != null && profileimage.ContentLength > 0)
                {
                    var ImageName = Path.GetFileName(profileimage.FileName);
                    var extention = Path.GetExtension(profileimage.FileName);
                    string path = Path.Combine(profileImagePath + ImageName);
                    profileimage.SaveAs(path);
                    studentProfile.ProfileImage = ImageName;
                }
                studentProfile.IsActive = true;
                studentProfile.UserId = userId;
                studentProfile.CreateDateTime = DateTime.Now;
                if(profile == null)
                {
                    if(checkphone == null)
                    {
                        db.StudentProfiles.Add(studentProfile);
                    }
                    else
                    {
                        ViewBag.message = validation;
                      
                    }
                }
                else
                {
                    db.StudentProfiles.Attach(studentProfile);
                }
                db.SaveChanges();
                return RedirectToAction("StudentHome", "StudentHome", new { area= "StudentArea" });
            }

            ViewBag.CityId = new SelectList(ddb.Cities, "Id", "Name", studentProfile.CityId);
            ViewBag.CountryId = new SelectList(ddb.Countries, "Id", "Name", studentProfile.CountryId);
            ViewBag.TermId = new SelectList(db.CurrentTerms, "Id", "Name", studentProfile.TermId);
            ViewBag.EstateId = new SelectList(ddb.Estates, "Id", "Name", studentProfile.EstateId);
            ViewBag.SchooleId = new SelectList(ddb.Schooles, "Id", "Name", studentProfile.SchooleId);
            ViewBag.StudyYearId = new SelectList(db.StudyYears, "Id", "Name", studentProfile.StudyYearId);

            return View(studentProfile);
        }
      



        // GET: StudentArea/StudentProfiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            if (studentProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityId = new SelectList(ddb.Cities, "Id", "Name", studentProfile.CityId);
            ViewBag.CountryId = new SelectList(ddb.Countries, "Id", "Name", studentProfile.CountryId);
            ViewBag.TermId = new SelectList(db.CurrentTerms, "Id", "Name", studentProfile.TermId);
            ViewBag.EstateId = new SelectList(ddb.Estates, "Id", "Name", studentProfile.EstateId);
            ViewBag.SchooleId = new SelectList(db.Schooles, "Id", "Name", studentProfile.SchooleId);
            ViewBag.StudyYearId = new SelectList(db.StudyYears, "Id", "Name", studentProfile.StudyYearId);
            return View(studentProfile);
        }

        // POST: StudentArea/StudentProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,BirthDay,Gender,PhoneNumber,AddressDetails,CountryId,EstateId,CityId,SchooleId,TermId,StudyYearId")] StudentProfile studentProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CityId = new SelectList(ddb.Cities, "Id", "Name", studentProfile.CityId);
            ViewBag.CountryId = new SelectList(ddb.Countries, "Id", "Name", studentProfile.CountryId);
            ViewBag.TermId = new SelectList(db.CurrentTerms, "Id", "Name", studentProfile.TermId);
            ViewBag.EstateId = new SelectList(ddb.Estates, "Id", "Name", studentProfile.EstateId);
            ViewBag.SchooleId = new SelectList(db.Schooles, "Id", "Name", studentProfile.SchooleId);
            ViewBag.StudyYearId = new SelectList(db.StudyYears, "Id", "Name", studentProfile.StudyYearId);
            return View(studentProfile);
        }

        // GET: StudentArea/StudentProfiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            if (studentProfile == null)
            {
                return HttpNotFound();
            }
            return View(studentProfile);
        }

        // POST: StudentArea/StudentProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentProfile studentProfile = db.StudentProfiles.Find(id);
            db.StudentProfiles.Remove(studentProfile);
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
