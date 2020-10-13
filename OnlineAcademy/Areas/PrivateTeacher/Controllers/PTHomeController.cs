using OnlineAcademy.Areas.PrivateTeacher.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAcademy.Areas.PrivateTeacher.Controllers
{
    public class PTHomeController : Controller
    {
        private PrivateTeacherDbContext db = new PrivateTeacherDbContext();


        // GET: PrivateTeacher/PTHome
        public ActionResult PTHome()
        {
            //var ass = db.PTAssistants.ToList();
            //var sd = db.PTAssistants.ToList();
            //ViewModelCalss model = new ViewModelCalss
            //{
            //    pTAssistants = ass,
            //    sd = sd,
            //};

            return View(/*model*/);
        }
    }
}