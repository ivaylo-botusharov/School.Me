namespace School.Models
{
    using System.Collections.Generic;

    public class SchoolTheme : DeletableEntity
    {
        private ICollection<Subject> subjects;

        private ICollection<SchoolClass> schoolClasses;

        private ICollection<AcademicYear> academicYears;

        public SchoolTheme()
        {
            this.subjects = new HashSet<Subject>();
            this.schoolClasses = new HashSet<SchoolClass>();
            this.academicYears = new HashSet<AcademicYear>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int StartGradeYear { get; set; }

        public int EndGradeYear { get; set; }

        public virtual ICollection<Subject> Subjects
        {
            get { return this.subjects; }
            set { this.subjects = value; }
        }
        
        public virtual ICollection<SchoolClass> SchoolClasses
        {
            get { return this.schoolClasses; }
            set { this.schoolClasses = value; }
        }

        public virtual ICollection<AcademicYear> AcademicYears
        {
            get { return this.academicYears; }
            set { this.academicYears = value; }
        }
    }
}
