namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using School.Models;
    using School.Services.Interfaces;

    public class SchoolClassCreateSubmitModel : IValidatableObject
    {
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Class Letter")]
        [RegularExpression(@"[A-Za-z]",
            ErrorMessage = "The selected class letter should be a single alphabet character")]
        public string ClassLetter { get; set; }

        public int GradeId { get; set; }

        public Guid AcademicYearId { get; set; }


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

            bool letterExists = schoolClassService
                .All()
                .Any(
                    sc => sc.ClassLetter == this.ClassLetter &&
                        sc.GradeId == this.GradeId &&
                        sc.Grade.AcademicYearId == this.AcademicYearId);

            if (letterExists)
            {
                yield return new ValidationResult(
                    "There is already another class with the same letter in this grade.");
            }
        }
    }
}