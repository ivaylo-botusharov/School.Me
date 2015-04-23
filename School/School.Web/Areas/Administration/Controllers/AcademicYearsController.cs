namespace School.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using School.Common;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;
    using School.Web.Infrastructure;
    
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

            IQueryable<AcademicYearListViewModel> sortedAcademicYears = academicYears
                .Project().To<AcademicYearListViewModel>();

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

            var academicYear = Mapper.Map<AcademicYearCreateSubmitModel, AcademicYear>(model);

            const int HighestGrade = 12;

            this.academicYearService.Add(academicYear, HighestGrade);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int startYear)
        {
            AcademicYear academicYear = this.academicYearService.All()
                .FirstOrDefault(y => y.StartDate.Year == startYear);

            AcademicYearDetailsViewModel academicYearViewModel = Mapper
                .Map<AcademicYear, AcademicYearDetailsViewModel>(academicYear);

            var redirectParamaters = new RouteValueDictionary()
            {
                { "startYear", startYear }
            };

            RedirectUrl redirectUrl = new RedirectUrl(this.ControllerContext, redirectParamaters);

            Session["redirectUrl"] = redirectUrl;
 
            return View(academicYearViewModel);
        }

        public ActionResult Edit(int startYear)
        {
            AcademicYear academicYear = this.academicYearService.All()
                .FirstOrDefault(y => y.StartDate.Year == startYear);

            AcademicYearDetailsEditModel model = Mapper.Map<AcademicYear, AcademicYearDetailsEditModel>(academicYear);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AcademicYearDetailsEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AcademicYear academicYear = this.academicYearService.GetById(model.Id);

            if (academicYear == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid or not existing year.");

                return View(model);
            }

            Mapper.Map(model, academicYear);

            this.academicYearService.Update(academicYear);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int startYear)
        {
            AcademicYear academicYear = this.academicYearService
                .All()
                .FirstOrDefault(ay => ay.StartDate.Year == startYear);

            var model = Mapper.Map<AcademicYear, AcademicYearDetailsDeleteModel>(academicYear);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(AcademicYearDetailsDeleteModel model)
        {
            AcademicYear academicYear = this.academicYearService.GetById(model.Id);

            if (academicYear.StartDate < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Academic years that have already started cannot be deleted");

                return View(model);
            }

            this.academicYearService.HardDelete(academicYear);

            return RedirectToAction("Index");
        }
    }
}