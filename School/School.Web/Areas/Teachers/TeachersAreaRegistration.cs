namespace School.Web.Areas.Teachers
{
    using System.Web.Mvc;

    public class TeachersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Teachers";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Teachers_default",
                url: "Teachers/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "School.Web.Areas.Teachers.Controllers" }
            );
        }
    }
}