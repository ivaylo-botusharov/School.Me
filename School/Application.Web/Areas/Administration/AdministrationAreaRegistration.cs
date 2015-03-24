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
                name: "Administration_schoolclass_details",
                url: "Administration/SchoolClasses/Details/{gradeYear}-{letter}-{startYear}",
                defaults: new { controller = "SchoolClasses", action = "Details" },
                namespaces: new string[] { "Application.Web.Areas.Administration.Controllers" }
            );

            context.MapRoute(
                name: "Administration_academicyear_details",
                url: "Administration/AcademicYears/{action}/{startYear}",
                defaults: new { controller = "AcademicYears", action = "Details" },
                namespaces: new string[] { "Application.Web.Areas.Administration.Controllers" }
            );

            context.MapRoute(
               name: "Administration_student_details",
               url: "Administration/Students/Details/{username}",
               defaults: new { controller = "Students", action = "Details" },
               namespaces: new string[] { "Application.Web.Areas.Administration.Controllers" }
           );

            context.MapRoute(
               name: "Administration_student_edit",
               url: "Administration/Students/Edit/{username}",
               defaults: new { controller = "Students", action = "Edit" },
               namespaces: new string[] { "Application.Web.Areas.Administration.Controllers" }
           );

            context.MapRoute(
               name: "Administration_teacher_details",
               url: "Administration/Teachers/Details/{username}",
               defaults: new { controller = "Teachers", action = "Details" },
               namespaces: new string[] { "Application.Web.Areas.Administration.Controllers" }
           );

            context.MapRoute(
              name: "Administration_teacher_edit",
              url: "Administration/Teachers/Edit/{username}",
              defaults: new { controller = "Teachers", action = "Edit" },
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