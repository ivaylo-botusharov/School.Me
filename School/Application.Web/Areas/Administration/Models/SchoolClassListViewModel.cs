using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models
{
    public class SchoolClassListViewModel
    {
        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        public int GradeYear { get; set; }

        public int StartYear { get; set; }

        public string SchoolThemeName { get; set;}
    }
}