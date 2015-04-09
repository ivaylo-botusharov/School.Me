namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AcademicYearDetailsDeleteModel
    {
        public Guid Id { get; set; }

        public int StartYear { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime EndDate { get; set; }
        
        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [Display(Name = "Grades number")]
        public int GradesCount { get; set; }
    }
}