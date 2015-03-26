namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AcademicYearDetailsViewModel
    {
        private IList<GradeListViewModel> grades;

        public AcademicYearDetailsViewModel()
        {
            this.grades = new List<GradeListViewModel>();
        }

        [Display(Name = "Academic Year")]
        public int StartYear
        {
            get { return this.StartDate.Year; }
        }

        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public IList<GradeListViewModel> Grades 
        {
            get { return this.grades; }
            set { this.grades = value; }
        }
    }
}