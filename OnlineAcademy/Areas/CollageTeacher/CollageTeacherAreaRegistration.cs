using System.Web.Mvc;

namespace OnlineAcademy.Areas.CollageTeacher
{
    public class CollageTeacherAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CollageTeacher";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CollageTeacher_default",
                "CollageTeacher/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}