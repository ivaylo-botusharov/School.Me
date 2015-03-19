using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Grade
    {
        private List<SchoolClass> schoolClasses;

        private IList<Subject> subjects;

        public Grade()
        {
            this.schoolClasses = new List<SchoolClass>();
            this.subjects = new List<Subject>();
        }

        public int Id { get; set; }

        public virtual List<SchoolClass> SchoolClasses
        {
            get { return this.schoolClasses; }
            set { this.schoolClasses = value; }
        }

        public virtual IList<Subject> Subjects
        {
            get { return this.subjects; }
            set { this.subjects = value; }
        }

        public virtual AcademicYear AcademicYear { get; set; }

        public Guid? AcademicYearId { get; set; }

        public int GradeYear { get; set; }
    }
}
