namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AcademicYearListViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Academic Year")]
        public int StartYear 
        {
            get { return this.StartDate.Year; } 
        }

        public int EndYear
        {
            get { return this.EndDate.Year; }
        }

        [Display(Name = "Start date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}