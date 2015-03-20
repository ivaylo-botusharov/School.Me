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

        public ActionResult Index(int? page)
        {
            IQueryable<AcademicYear> academicYears = this.academicYearService.All().OrderBy(y => y.StartDate);

            IQueryable<AcademicYearListViewModel> sortedAcademicYears = academicYears.Project().To<AcademicYearListViewModel>();

            int pageSize = 10;
            int pageIndex = (page ?? 1);

            return View(sortedAcademicYears.ToPagedList(pageIndex, pageSize));
        }
    }
}