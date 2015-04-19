namespace School.Web.Areas.Administration.Models
{
    using System;
    
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using School.Models;    
    using School.Services.Interfaces;
    
    public class GradeCreateSubmitModel : IValidatableObject
    {
        public int Id { get; set; }

        public DateTime AcademicYearStartDate { get; set; }

        public DateTime AcademicYearEndDate { get; set; }

        [Display(Name = "Grade")]
        [Range(1, 15, ErrorMessage = "The grade number is out of the allowed range.")]
        public int GradeYear { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IGradeService gradeService = DependencyResolver.Current.GetService<IGradeService>();

            Grade grade = gradeService
                .All()
                .FirstOrDefault(
                    g => g.GradeYear == this.GradeYear && 
                         (g.AcademicYear.StartDate == this.AcademicYearStartDate ||
                         g.AcademicYear.EndDate == this.AcademicYearEndDate));

            if (grade != null)
            {
                yield return new ValidationResult(string.Format("Grade {0} already exists", this.GradeYear));
            }

            if (this.AcademicYearEndDate < DateTime.Now)
            {
                yield return new ValidationResult("Grades cannot be added to already completed academic years.");
            }
        }
    }
}