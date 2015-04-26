namespace School.Web.Areas.Administration.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class SchoolClassEditViewModel
    {
        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        public int GradeYear { get; set; }

        public string SchoolThemeName { get; set; }

        public IEnumerable<SelectListItem> Grades { get; set; }
    }
}