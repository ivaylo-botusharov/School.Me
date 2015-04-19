namespace School.Web.Areas.Administration.Models
{
    using System;

    public class SchoolClassCreateSubmitModel
    {
        public Guid Id { get; set; }

        public string ClassLetter { get; set; }

        public int GradeId { get; set; }
    }
}