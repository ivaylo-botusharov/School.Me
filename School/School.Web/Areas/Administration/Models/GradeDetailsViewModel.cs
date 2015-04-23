namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class GradeDetailsViewModel
    {
        private ICollection<SchoolClassListViewModel> schoolClasses;

        public GradeDetailsViewModel()
        {
            this.schoolClasses = new List<SchoolClassListViewModel>();
        }

        public int Id { get; set; }

        [Display(Name = "Grade")]
        public int GradeYear { get; set; }

        public DateTime AcademicYearStartDate { get; set; }

        public DateTime AcademicYearEndDate { get; set; }

        public int SchoolClassesCount { get; set; }

        public ICollection<SchoolClassListViewModel> SchoolClasses
        {
            get { return this.schoolClasses; }
            set { this.schoolClasses = value; }
        }
    }
}