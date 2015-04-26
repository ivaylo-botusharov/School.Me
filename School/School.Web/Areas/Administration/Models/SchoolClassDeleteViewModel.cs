namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SchoolClassDeleteViewModel
    {
        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        public int GradeYear { get; set; }

        [DataType(DataType.Date)]
        public DateTime AcademicYearStartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime AcademicYearEndDate { get; set; }

        [Display(Name = "School Theme")]
        public string SchoolThemeName { get; set; }

        [Display(Name = "Students number")]
        public int StudentsNumber { get; set; }
    }
}