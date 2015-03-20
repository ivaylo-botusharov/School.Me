using Application.Services.Interfaces;
using Application.Web.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Models;
using PagedList;

namespace Application.Web.Areas.Administration.Controllers
{
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

        public ActionResult Details(int startYear)
        {
            AcademicYear academicYear = this.academicYearService.All().FirstOrDefault(y => y.StartDate.Year == startYear);

            AcademicYearDetailsViewModel academicYearViewModel = Mapper.Map<AcademicYear, AcademicYearDetailsViewModel>(academicYear);

            return View(academicYearViewModel);
        }
    }
}