namespace School.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using School.Common;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;
    using System.Linq;
    using System.Web.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AcademicYearsController : Controller
    {
        private readonly IAcademicYearService academicYearService;

        public AcademicYearsController(IAcademicYearService academicYearService)
        {
            this.academicYearService = academicYearService;
        }

        public ActionResult Index()
        {
            IQueryable<AcademicYear> academicYears = this.academicYearService.All().OrderBy(y => y.StartDate);

            IQueryable<AcademicYearListViewModel> sortedAcademicYears = academicYears.Project().To<AcademicYearListViewModel>();

            return View(sortedAcademicYears);
        }

        public ActionResult Create()
        {
            AcademicYearCreateSubmitModel model = new AcademicYearCreateSubmitModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AcademicYearCreateSubmitModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AcademicYear academicYear = new AcademicYear();

            academicYear = Mapper.Map<AcademicYearCreateSubmitModel, AcademicYear>(model);

            this.academicYearService.Add(academicYear);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int startYear)
        {
            AcademicYear academicYear = this.academicYearService.All().FirstOrDefault(y => y.StartDate.Year == startYear);

            AcademicYearDetailsViewModel academicYearViewModel = Mapper.Map<AcademicYear, AcademicYearDetailsViewModel>(academicYear);

            return View(academicYearViewModel);
        }
    }
}