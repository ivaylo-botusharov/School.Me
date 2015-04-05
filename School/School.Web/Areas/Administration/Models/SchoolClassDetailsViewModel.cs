namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SchoolClassDetailsViewModel
    {
        private List<StudentListViewModel> students;
        
        public SchoolClassDetailsViewModel()
        {
            this.students = new List<StudentListViewModel>();
        }

        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        public int GradeYear { get; set; }

        public AcademicYearListViewModel AcademicYear { get; set; }

        public string SchoolThemeName { get; set; }

        [Display(Name = "Students number:")]
        public int StudentsNumber { get; set; }

        public List<StudentListViewModel> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }
    }
}