namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using School.Models;
    using School.Services.Interfaces;
    
    public class AcademicYearDetailsEditModel : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required]
        [UIHint("DatePicker")]
        public DateTime StartDate { get; set; }

        [Required]
        [UIHint("DatePicker")]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartDate >= this.EndDate)
            {
                yield return new ValidationResult("Start date should not be equal or greater than end date.");
            }

            IAcademicYearService academicYearService = DependencyResolver.Current.GetService<IAcademicYearService>();
            
            bool uniqueOnEdit = academicYearService.AcademicYearUniqueOnEdit(this.Id, this.StartDate, this.EndDate);
            
            if (!uniqueOnEdit)
            {
                yield return new ValidationResult(
                    "Academic Year with the entered start year or end year already exists.");
            }

            if (academicYearService.All().Any())
            {
                AcademicYear latestAcademicYear = academicYearService
                    .All()
                    .OrderByDescending(ay => ay.StartDate)
                    .First();

                if (latestAcademicYear != null && this.StartDate.Year > latestAcademicYear.StartDate.Year + 1)
                {
                    yield return new ValidationResult(
                        "New academic year cannot be more than 1 year later than latest academic year.");
                }
            }

            if (this.EndDate.Year > this.StartDate.Year + 1)
            {
                yield return new ValidationResult(
                    "Academic Year may not last more than 1 astronomical year.");
            }
        }
    }
}