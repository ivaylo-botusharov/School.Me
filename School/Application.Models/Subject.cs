using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Models
{
    public class Subject
    {
        private ICollection<Lesson> lessons;
        private ICollection<SchoolClass> schoolClasses;
        private ICollection<Teacher> teachers;

        public Subject()
        {
            this.lessons = new HashSet<Lesson>();
            this.schoolClasses = new HashSet<SchoolClass>();
            this.teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Grade { get; set; }

        public AcademicYear AcademicYear { get; set; }

        public Guid AcademicYearId { get; set; }

        public string Description { get; set; }

        public int TotalHours { get; set; }

        public virtual ICollection<Lesson> Lessons
        {
            get { return this.lessons; }
            set { this.lessons = value; }
        }

        public virtual ICollection<SchoolClass> SchoolClasses
        {
            get { return this.schoolClasses; }
            set { this.schoolClasses = value; }
        }
        
        public virtual ICollection<Teacher> Teachers
        {
            get { return this.teachers; }
            set { this.teachers = value; }
        }
    }
}
