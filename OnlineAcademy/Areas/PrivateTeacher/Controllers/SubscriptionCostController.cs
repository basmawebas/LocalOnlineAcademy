using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineAcademy.Areas.PrivateTeacher.Controllers
{
    public class SubscriptionCostController : Controller
    {
        // GET: PrivateTeacher/SubscriptionCost
        public ActionResult Index()
        {
            return View();
        } 
        public ActionResult ShowSubscriptionTypes()
        {
            return View();
        }
    }
}