using Microsoft.AspNet.Identity;
using OnlineAcademy.Areas.StudentArea.Data;
using OnlineAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAcademy.Areas.StudentArea.Controllers
{
    [Authorize(Roles ="طالب")]
    public class StudentHomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private StudentDbContext sdb = new StudentDbContext();
        // GET: StudentArea/StudentHome
        public ActionResult StudentHome()
        {
            var userId = User.Identity.GetUserId();
            var profile = sdb.StudentProfiles.Where(x => x.UserId == userId).SingleOrDefault();
            if(profile == null)
            {
                return RedirectToAction("Create", "StudentProfiles", new { area = "StudentArea" });
            }
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.CountryList = new SelectList(GetCountryList(), "Id", "Name");
            return View();
        }


        public List<Country> GetCountryList()
        {
            List<Country> countries = db.Countries.ToList();
            return countries;
        }
        public ActionResult GetStatesList (int CountryId)
        {
            List<Estate> estates = db.Estates.Where(x=>x.CountryId == CountryId).ToList();
            ViewBag.StateList = new SelectList(estates, "Id", "Name");
            return PartialView("DisplayStates");
        }

        public ActionResult GetCitysList(int StateId)
        {
            List<City> citys = db.Cities.Where(x => x.EstateId == StateId).ToList();
            ViewBag.citysList = new SelectList(citys, "Id", "Name");
            return PartialView("DisplayCities");
        }

    }
}