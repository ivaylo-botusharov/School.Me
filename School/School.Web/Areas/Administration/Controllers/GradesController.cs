namespace School.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;
    using School.Web.Infrastructure;
    
    public class GradesController : Controller
    {
        private readonly IGradeService gradeService;

        private readonly IAcademicYearService academicYearService;

        public GradesController(IGradeService gradeService, IAcademicYearService academicYearService)
        {
            this.gradeService = gradeService;
            this.academicYearService = academicYearService;
        }

        public ActionResult Create()
        {
            var redirectUrl = this.Session["redirectUrl"] as RedirectUrl;

            redirectUrl = redirectUrl ?? new RedirectUrl();

            int startYear = (int)redirectUrl.RedirectParameters["startYear"];

            AcademicYear academicYear =
                this.academicYearService.All().FirstOrDefault(ay => ay.StartDate.Year == startYear);

            academicYear = academicYear ?? new AcademicYear();

            var model = new GradeCreateSubmitModel()
            {
                AcademicYearStartDate = academicYear.StartDate,
                AcademicYearEndDate = academicYear.EndDate
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GradeCreateSubmitModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Grade grade = Mapper.Map<GradeCreateSubmitModel, Grade>(model);

            var redirectUrl = this.Session["redirectUrl"] as RedirectUrl;

            redirectUrl = redirectUrl ?? new RedirectUrl();

            var startYear = (int)redirectUrl.RedirectParameters["startYear"];

            AcademicYear academicYear =
                this.academicYearService.All().FirstOrDefault(ay => ay.StartDate.Year == startYear);

            academicYear = academicYear ?? new AcademicYear();

            grade.AcademicYearId = academicYear.Id;

            this.gradeService.Add(grade);

            return RedirectToAction(
                redirectUrl.RedirectActionName, redirectUrl.RedirectControllerName, redirectUrl.RedirectParameters);
        }

        public ActionResult Details(int id)
        {
            Grade grade = this.gradeService.GetById(id);

            if (grade == null)
            {
                var redirectUrl = Session["redirectUrl"] as RedirectUrl;

                redirectUrl = redirectUrl ?? new RedirectUrl();

                return RedirectToAction(
                    redirectUrl.RedirectActionName, 
                    redirectUrl.RedirectControllerName,
                    redirectUrl.RedirectParameters);
            }

            GradeDetailsViewModel model = Mapper.Map<Grade, GradeDetailsViewModel>(grade);

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            Grade grade = this.gradeService.GetById(id);

            GradeDetailsViewModel model = Mapper.Map<Grade, GradeDetailsViewModel>(grade);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Grade grade = this.gradeService.GetById(id);

            if (grade.AcademicYear.StartDate < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "Grade for already started academic year cannot be deleted");
                GradeDetailsViewModel model = Mapper.Map<Grade, GradeDetailsViewModel>(grade);
                return View(model);
            }

            this.gradeService.HardDelete(grade);

            var redirectUrl = Session["redirectUrl"] as RedirectUrl;

            redirectUrl = redirectUrl ?? new RedirectUrl();

            return RedirectToAction(
                redirectUrl.RedirectActionName, 
                redirectUrl.RedirectControllerName,
                redirectUrl.RedirectParameters);
        }
    }
}