using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models
{
    public class AcademicYearListViewModel
    {
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
    }
}