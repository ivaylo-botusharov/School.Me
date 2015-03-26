namespace School.Models
{
    using System;
    using System.Collections.Generic;

    public class Homework
    {
        private ICollection<HomeworkAttachment> homeworkAttachments;

        private ICollection<HomeworkSolution> homeworkSolutions;

        private ICollection<SchoolClass> schoolClasses;

        public Homework()
        {
            this.homeworkAttachments = new HashSet<HomeworkAttachment>();
            this.homeworkSolutions = new HashSet<HomeworkSolution>();
            this.schoolClasses = new HashSet<SchoolClass>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public ICollection<HomeworkAttachment> HomeworkAttachments
        {
            get { return this.homeworkAttachments; }
            set { this.homeworkAttachments = value; }
        }

        public ICollection<SchoolClass> SchoolClasses
        {
            get { return this.schoolClasses; }
            set { this.schoolClasses = value; }
        }

        public ICollection<HomeworkSolution> HomeworkSolutions
        {
            get { return this.homeworkSolutions; }
            set { this.homeworkSolutions = value; }
        }

        public virtual Teacher Teacher { get; set; }

        public Guid? TeacherId { get; set; }
    }
}
