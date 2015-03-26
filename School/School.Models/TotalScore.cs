namespace School.Models
{
    using System;

    public class TotalScore
    {
        public int Id { get; set; }

        public int HomeworkGrade { get; set; }

        public virtual Subject Subject { get; set; }

        public int SubjectId { get; set; }

        public virtual Student Student { get; set; }

        public Guid StudentId { get; set; }
    }
}
