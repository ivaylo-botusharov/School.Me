namespace School.Web.Areas.Administration.Models
{
    using System;

    public class SchoolClassListViewModel
    {
        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        public int GradeYear { get; set; }

        public int StartYear { get; set; }

        public string SchoolThemeName { get; set; }
    }
}