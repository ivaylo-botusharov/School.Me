using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class Grade
    {
        private List<SchoolClass> schoolClasses;

        public Grade()
        {
            this.schoolClasses = new List<SchoolClass>();
        }

        public int Id { get; set; }

        public virtual List<SchoolClass> SchoolClasses
        {
            get { return this.schoolClasses; }
            set { this.schoolClasses = value; }
        }

        public virtual AcademicYear AcademicYear { get; set; }

        public Guid? AcademicYearId { get; set; }

        public int GradeYear { get; set; }


    }
}
