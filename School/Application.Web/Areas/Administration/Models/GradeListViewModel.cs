using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Application.Web.Areas.Administration.Models
{
    public class GradeListViewModel
    {
        public int Id { get; set; }

        public int GradeYear { get; set; }

        public int SchoolClassesCount { get; set; }
    }
}