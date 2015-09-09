namespace School.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        private readonly IAcademicYearService academicYearService;

        public SchoolClassesController(
            ISchoolClassService schoolClassService, 
            IGradeService gradeService,
            IAcademicYearService academicYearService)
        {
            this.schoolClassService = schoolClassService;
            this.gradeService = gradeService;
            this.academicYearService = academicYearService;
        }

        public ActionResult Create(int startYear, int endYear, int gradeYear)
        {
            SchoolClassCreateSubmitModel model = new SchoolClassCreateSubmitModel();

            var academicYear = this.academicYearService.All().FirstOrDefault(ay =>
                ay.StartDate.Year == startYear &&
                ay.EndDate.Year == endYear) ?? new AcademicYear();

            var grade = this.gradeService.All().FirstOrDefault(g => 
                g.GradeYear == gradeYear &&
                g.AcademicYearId == academicYear.Id) ?? new Grade();


            if (!academicYear.IsActive)
            {
                return RedirectToAction("Details", "AcademicYears", new { startYear = academicYear.StartDate.Year});
            }

            model.AcademicYearId = academicYear.Id;
            model.GradeId = grade.Id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SchoolClassCreateSubmitModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);    
            }

            SchoolClass schoolClass = Mapper.Map<SchoolClassCreateSubmitModel, SchoolClass>(model);

            this.schoolClassService.Add(schoolClass);

            return RedirectToAction("Details", "Grades", new { id = schoolClass.GradeId });
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
                ModelState.AddModelError(string.Empty, "Such class does not exist");

                var redirectUrl = Session["redirectUrl"] as RedirectUrl;

                redirectUrl = redirectUrl ?? new RedirectUrl();

                return RedirectToAction(
                    redirectUrl.RedirectActionName,
                    redirectUrl.RedirectControllerName,
                    redirectUrl.RedirectParameters);
            }

            var model = Mapper.Map<SchoolClass, SchoolClassEditViewModel>(schoolClass);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SchoolClassEditViewModel model)
        {
            SchoolClass schoolClass = this.schoolClassService.GetById(model.Id);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

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
                    string.Empty, 
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
    }
}