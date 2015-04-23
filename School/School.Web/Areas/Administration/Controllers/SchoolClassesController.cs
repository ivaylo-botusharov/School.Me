namespace School.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using AutoMapper;
    using School.Common;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;
    using School.Web.Infrastructure;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class SchoolClassesController : Controller
    {
        private readonly ISchoolClassService schoolClassService;

        public SchoolClassesController(ISchoolClassService schoolClassService)
        {
            this.schoolClassService = schoolClassService;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchoolClassCreateSubmitModel model)
        {
            return View();
        }

        public ActionResult Details(int gradeYear, string letter, int startYear)
        {
            SchoolClass schoolClass = this.schoolClassService.GetByDetails(gradeYear, letter, startYear);

            SchoolClassDetailsViewModel schoolClassModel = 
                Mapper.Map<SchoolClass, SchoolClassDetailsViewModel>(schoolClass);

            RouteValueDictionary routeParameters = new RouteValueDictionary
            {
               { "gradeYear", gradeYear },
               { "letter", letter },
               { "startYear", startYear }
            };

            RedirectUrl redirectUrl = new RedirectUrl(this.ControllerContext, routeParameters);

            this.Session["redirectUrl"] = redirectUrl;

            return View(schoolClassModel);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            return View();
        }
    }
}