namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using School.Services.Interfaces;

    public class SchoolClassEditViewModel : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Class Letter")]
        [RegularExpression(@"[A-Za-z]", 
            ErrorMessage = "The selected class letter should be a single alphabet character")]
        public string ClassLetter { get; set; }

        [Required]
        [Display(Name = "Grade Year")]
        public int GradeYear { get; set; }
        
        public Guid AcademicYearId { get; set; }

        public IEnumerable<SelectListItem> Grades
        {
            get
            {
                IGradeService gradeService = DependencyResolver.Current.GetService<IGradeService>();

                List<int> grades = gradeService
                    .All()
                    .Where(g => g.AcademicYearId == this.AcademicYearId)
                    .Select(g => g.GradeYear).ToList();

                IEnumerable<SelectListItem> gradesList = grades.Select(
                    gradeYear => new SelectListItem
                    {
                        Value = gradeYear.ToString(),
                        Text = gradeYear.ToString()
                    });

                return gradesList;
            }
        }

        [Required]
        [Display(Name = "School Theme")]
        public int SchoolThemeId { get; set; }

        public IEnumerable<SelectListItem> SchoolThemes
        {
            get
            {
                ISchoolThemeService schoolThemeService = DependencyResolver.Current.GetService<ISchoolThemeService>();

                var schoolThemes = schoolThemeService
                    .All()
                    .Select(st => new
                    {
                        st.Id,
                        st.Name
                    }
                    ).ToList();

                IEnumerable<SelectListItem> schoolThemesList = schoolThemes.Select(
                    schoolTheme => new SelectListItem
                    {
                        Value = schoolTheme.Id.ToString(),
                        Text = schoolTheme.Name.ToString()
                    });

                return schoolThemesList;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ISchoolClassService schoolClassService = DependencyResolver.Current.GetService<ISchoolClassService>();

            var currentGradeAcademicYearId = schoolClassService.GetById(this.Id).Grade.AcademicYearId;

            bool letterExists = schoolClassService
                .All()
                .Any(
                    sc => sc.ClassLetter == this.ClassLetter && 
                        sc.Id != this.Id && sc.Grade.GradeYear == this.GradeYear &&
                        sc.Grade.AcademicYearId == currentGradeAcademicYearId);

            if (letterExists)
            {
                yield return new ValidationResult(
                    "There is already another class with the same letter in this grade.");
            }
        }
    }
}