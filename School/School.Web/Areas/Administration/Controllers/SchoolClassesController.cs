using System.Linq;

namespace School.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
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

        private readonly IGradeService gradeService;

        public SchoolClassesController(ISchoolClassService schoolClassService, IGradeService gradeService)
        {
            this.schoolClassService = schoolClassService;
            this.gradeService = gradeService;
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

            if (schoolClass == null)
            {
                return RedirectToAction("Index", "AcademicYears");
            }

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

        public ActionResult Edit(int gradeYear, string letter, int startYear)
        {
            SchoolClass schoolClass = this.schoolClassService.GetByDetails(gradeYear, letter, startYear);

            if (schoolClass == null)
            {
                ModelState.AddModelError("", "Such class does not exist");

                var redirectUrl = Session["redirectUrl"] as RedirectUrl;

                redirectUrl = redirectUrl ?? new RedirectUrl();

                return RedirectToAction(
                    redirectUrl.RedirectActionName,
                    redirectUrl.RedirectControllerName,
                    redirectUrl.RedirectParameters);
            }

            var model = Mapper.Map<SchoolClass, SchoolClassEditViewModel>(schoolClass);

            model.Grades = this.GetAcademicYearGrades(schoolClass.Grade.AcademicYear);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SchoolClassEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            SchoolClass schoolClass = this.schoolClassService.GetById(model.Id);

            Mapper.Map<SchoolClassEditViewModel, SchoolClass>(model, schoolClass);

            Grade grade = this.gradeService.All().FirstOrDefault(g =>
                g.GradeYear == model.GradeYear &&
                g.AcademicYearId == schoolClass.Grade.AcademicYearId);

            grade = grade ?? new Grade();

            schoolClass.GradeId = grade.Id;

            this.schoolClassService.Update(schoolClass);

            var redirectUrl = Session["redirectUrl"] as RedirectUrl;

            redirectUrl = redirectUrl ?? new RedirectUrl();

            return RedirectToAction(
                redirectUrl.RedirectActionName,
                redirectUrl.RedirectControllerName,
                redirectUrl.RedirectParameters);
        }

        public ActionResult Delete(Guid id)
        {
            SchoolClass schoolClass = this.schoolClassService.GetById(id);

            var model = Mapper.Map<SchoolClass, SchoolClassDeleteViewModel>(schoolClass);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            SchoolClass schoolClass = this.schoolClassService.GetById(id);

            if (schoolClass.Grade.AcademicYear.StartDate < DateTime.Now)
            {
                ModelState.AddModelError(
                    "", 
                    "Classes for already started / completed academic years cannot be deleted");

                var model = Mapper.Map<SchoolClass, SchoolClassDeleteViewModel>(schoolClass);
                return View(model);
            }

            this.schoolClassService.HardDelete(schoolClass);

            var redirectUrl = Session["redirectUrl"] as RedirectUrl;

            redirectUrl = redirectUrl ?? new RedirectUrl();

            return RedirectToAction(
                redirectUrl.RedirectActionName,
                redirectUrl.RedirectControllerName,
                redirectUrl.RedirectParameters);
        }

        private IEnumerable<SelectListItem> GetAcademicYearGrades(AcademicYear academicYear)
        {
            List<int> grades = this.gradeService
                .All()
                .Where(g => g.AcademicYearId == academicYear.Id)
                .Select(g => g.GradeYear).ToList();

            IEnumerable<SelectListItem> gradesList = grades.Select(
                gradeYear => new SelectListItem
                {
                    Value = gradeYear.ToString(),
                    Text = gradeYear.ToString()
                });

            return new SelectList(gradesList, "Value", "Text");
        }
    }
}