using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAcademy.Areas.PrivateTeacher.Controllers
{
    public class HelpController : Controller
    {
        // GET: PrivateTeacher/Help
        public ActionResult HelpPage()
        {
            return View();
        }public ActionResult FrequentQuestions()
        {
            return View();
        }
    }
}