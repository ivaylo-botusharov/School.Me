using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class SchoolClass
    {
        private ICollection<Student> students;

        private ICollection<Subject> subjects;

        private ICollection<Homework> homeworks;

        public SchoolClass()
        {
            this.students = new HashSet<Student>();
            this.subjects = new HashSet<Subject>();
            this.homeworks = new HashSet<Homework>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public int Grade { get; set; }

        public string ClassLetter { get; set; }

        public virtual AcademicYear AcademicYear { get; set; }

        public Guid? AcademicYearId { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }

        public virtual ICollection<Subject> Subjects
        {
            get { return this.subjects; }
            set { this.subjects = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }
            set { this.homeworks = value; }
        }
    }
}
