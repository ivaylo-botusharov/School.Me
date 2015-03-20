using System.Web.Mvc;

namespace Application.Web.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                name: "Administration_academicyear_details",
                url: "Administration/{controller}/{action}/{startYear}",
                defaults: new { controller = "Home", action = "Index", startYear = UrlParameter.Optional },
                namespaces: new string[] { "Application.Web.Areas.Administration.Controllers" }
            );
           
            context.MapRoute(
                name: "Administration_student_edit",
                url: "Administration/{controller}/{action}/{username}",
                defaults: new { controller = "Students", action = "Edit", username = ""},
                namespaces: new string[] { "Application.Web.Areas.Administration.Controllers" }
            );

            context.MapRoute(
                name: "Administration_default",
                url: "Administration/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Application.Web.Areas.Administration.Controllers"}
            );
        }
    }
}