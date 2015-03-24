using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models
{
    public class GradeListViewModel
    {
        private List<SchoolClassListViewModel> schoolClasses;

        public GradeListViewModel()
        {
            this.schoolClasses = new List<SchoolClassListViewModel>();
        }
        public int Id { get; set; }

        public int GradeYear { get; set; }

        public int SchoolClassesCount { get; set; }

        public List<SchoolClassListViewModel> SchoolClasses
        {
            get { return this.schoolClasses; }
            set { this.schoolClasses = value; }
        }
    }
}