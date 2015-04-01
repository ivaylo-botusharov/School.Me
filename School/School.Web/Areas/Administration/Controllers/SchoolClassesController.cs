namespace School.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using School.Common;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;
    using School.Web.Infrastructure;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class SchoolClassesController : Controller
    {
        private readonly ISchoolClassService schoolClassService;

        public SchoolClassesController(ISchoolClassService schoolClassService)
        {
            this.schoolClassService = schoolClassService;
        }

        public ActionResult Details(int gradeYear, string letter, int startYear)
        {
            SchoolClass schoolClass = this.schoolClassService.All()
                .FirstOrDefault(
                    sc => sc.Grade.GradeYear == gradeYear &&
                          sc.ClassLetter == letter &&
                          sc.Grade.AcademicYear.StartDate.Year == startYear
                );

            SchoolClassDetailsViewModel schoolClassModel = Mapper.Map<SchoolClass, SchoolClassDetailsViewModel>(schoolClass);

            RouteValueDictionary routeParameters = new RouteValueDictionary
            {
               { "gradeYear", gradeYear },
               { "letter", letter },
               { "startYear", startYear }
            };

            RedirectUrl redirectUrl = new RedirectUrl(this.ControllerContext, routeParameters);

            Session["redirectUrl"] = redirectUrl;

            return View(schoolClassModel);
        }
    }
}