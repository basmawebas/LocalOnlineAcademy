using System.Web.Mvc;

namespace OnlineAcademy.Areas.PrivateTeacher
{
    public class PrivateTeacherAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PrivateTeacher";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PrivateTeacher_default",
                "PrivateTeacher/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}