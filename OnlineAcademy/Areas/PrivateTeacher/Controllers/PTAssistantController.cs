using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using OnlineAcademy.Areas.PrivateTeacher.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineAcademy.Areas.PrivateTeacher.Controllers
{
    public class PTAssistantController : Controller
    {

        PrivateTeacherDbContext db = new PrivateTeacherDbContext();

        

        
        // GET: PrivateTeacher/PTAssistant
        public ActionResult Index()
        {
            var model = db.PTAssistants.ToList();
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(PTAssistant model)
        {
            return View();
        }
        // GET: /PTAssistant
        
        public ActionResult AdvertiseForAssist()
        {
            //var userName = User.Identity.GetUserName();
            //PTAssistant model = new PTAssistant() { };
            //model.PTUser.UserName = "basma";
           
            
            

            return View(/*model*/);
        }
        [HttpPost]
        
        public ActionResult AdvertiseForAssist(PTAssistant model)
        {
            var userId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                model.Statue = true;
                model.CreateDate = DateTime.Now;
                model.UserId = userId;
                db.PTAssistants.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }





        public ActionResult GetTeachers()
        {
          var model=  db.PTAssistants.ToList();
            return PartialView("_ListOfTeacher", model);
        }
        //Get/AdvertiseDetails
        public ActionResult AdvertiseDetails(string UserId)
        {
            if (UserId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PTAssistant advdetails = db.PTAssistants.Find(Convert.ToInt32(UserId));
            if (advdetails == null)
            {
                return HttpNotFound();
            }
            return View(advdetails);
            
        }
        [HttpPost]
        public ActionResult AdvertiseDetails()
        {
            return View();
        }
        //Get/ApplyForAssisJob
        public ActionResult ApplyForAssisJob(string advertiseId)
        {
            string test = advertiseId;
            Session["Idads"] = test;
            return View();
        }
       // Post/ApplyForAssisJob
        [HttpPost]
        public ActionResult ApplyForAssisJob(ApplierInfo ApplicationInfo,string ReturnUrl)

        {

            if (ModelState.IsValid)
            {
                ApplicationInfo.advertiseId = Convert.ToInt32(Session["Idads"]);
                db.ApplierInfos.Add(ApplicationInfo);
                db.SaveChanges();
                return RedirectToAction("Index","Home",new {area=false });

            }
            return View(ApplicationInfo);
        }


    }
}