namespace School.Models
{
    using System.Collections.Generic;

    public class Subject
    {
        private ICollection<Lesson> lessons;
        private ICollection<Teacher> teachers;

        public Subject()
        {
            this.lessons = new HashSet<Lesson>();
            this.teachers = new HashSet<Teacher>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int TotalHours { get; set; }

        public virtual SchoolTheme SchoolTheme { get; set; }

        public int SchoolThemeId { get; set; }

        public virtual Grade Grade { get; set; }

        public int GradeId { get; set; }

        public virtual ICollection<Lesson> Lessons
        {
            get { return this.lessons; }
            set { this.lessons = value; }
        }

        public virtual ICollection<Teacher> Teachers
        {
            get { return this.teachers; }
            set { this.teachers = value; }
        }
    }
}
