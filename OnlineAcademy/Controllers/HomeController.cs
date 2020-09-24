using Microsoft.AspNet.Identity;
using OnlineAcademy.Areas.StudentArea.Data;
using OnlineAcademy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAcademy.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private StudentDbContext sdb = new StudentDbContext();
        string student = "طالب";
        string pteacher = "مدرس خاص";
        string gteacher = "مدرس حكومي";
        string cteacher = "دكتور جامعي";
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var student = sdb.Users.Where(x => x.Id == userId).SingleOrDefault();
            if (student != null)
            {
                return RedirectToAction("StudentHome", "StudentHome", new { area = "StudentArea" });
            }

                return View();
        }

        public ActionResult Welcom()
        {
            var userId = User.Identity.GetUserId();
            var user = db.Users.Where(x => x.Id == userId).SingleOrDefault();
            var profile = sdb.StudentProfiles.Where(x => x.UserId == userId).SingleOrDefault();
            if (user.RoleName == student)
            {
                if(profile != null)
                {
                return RedirectToAction("StudentHome", "StudentHome", new { area = "StudentArea" });

                }
                else
                {
                    return RedirectToAction("Create", "StudentProfiles", new { area = "StudentArea" });
                }
            }
            else if(user.RoleName == pteacher)
            {
                return RedirectToAction("", "", new { area = "" });
            }else if(user.RoleName == gteacher)
            {
                return RedirectToAction("", "", new { area = "" });
            }else if(user.RoleName == cteacher)
            {
                return RedirectToAction("", "", new { area = "" });
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}