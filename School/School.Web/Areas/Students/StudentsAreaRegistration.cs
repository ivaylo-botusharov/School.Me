namespace School.Web.Areas.Students
{
    using System.Web.Mvc;

    public class StudentsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Students";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Students_default",
                url: "Students/{controller}/{action}/{id}",
                defaults: new { action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "School.Web.Areas.Students.Controllers" }
            );
        }
    }
}