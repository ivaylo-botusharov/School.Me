using Application.Models;
using Application.Services.Interfaces;
using Application.Web.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Web.Routing;
using Application.Web.Infrastructure;

namespace Application.Web.Areas.Administration.Controllers
{
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