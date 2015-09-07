namespace School.Web.Validators
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;
    
    public class YearShoudNotExistInDbAttribute : ValidationAttribute
    {
        public YearShoudNotExistInDbAttribute()
        {
            this.ErrorMessage = "Academic Year with the entered start year or end year already exists.";
        }

        public override bool IsValid(object value)
        {
            /*Type originalType = value.GetType();*/

            /*var acadYear = value as originalType;*/

            AcademicYearCreateSubmitModel academicYear = value as AcademicYearCreateSubmitModel;

            IAcademicYearService academicYearService = DependencyResolver.Current.GetService<IAcademicYearService>();

            bool academicYearValid = academicYearService
                .AcademicYearExistsInDb(academicYear.StartDate, academicYear.EndDate) ? false : true;

            return academicYearValid;
        }
    }
}