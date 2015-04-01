namespace School.Web.Validators
{
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class YearShoudNotExistInDb : ValidationAttribute
    {
        public YearShoudNotExistInDb()
        {
            ErrorMessage = "Academic Year with the entered start year or end year already exists.";
        }

        public override bool IsValid(object value)
        {
            //Type originalType = value.GetType();

            //var acadYear = value as originalType;

            AcademicYearCreateSubmitModel academicYear = value as AcademicYearCreateSubmitModel;

            IAcademicYearService academicYearService = DependencyResolver.Current.GetService<IAcademicYearService>();

            bool isValid = academicYearService.AcademicYearExistsInDb(academicYear.StartDate, academicYear.EndDate) ? false : true;

            return isValid;
        }
    }
}