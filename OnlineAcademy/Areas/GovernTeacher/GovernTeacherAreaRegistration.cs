using System.Web.Mvc;

namespace OnlineAcademy.Areas.GovernTeacher
{
    public class GovernTeacherAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GovernTeacher";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GovernTeacher_default",
                "GovernTeacher/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}