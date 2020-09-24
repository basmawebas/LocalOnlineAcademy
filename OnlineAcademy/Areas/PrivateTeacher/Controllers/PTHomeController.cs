using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAcademy.Areas.PrivateTeacher.Controllers
{
    public class PTHomeController : Controller
    {
        // GET: PrivateTeacher/PTHome
        public ActionResult PTHome()
        {
            return View();
        }
    }
}