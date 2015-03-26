namespace School.Web.Areas.Administration.Models
{
    using System.Collections.Generic;

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