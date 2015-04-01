namespace School.Models
{
    using System;

    public class HomeworkSolution : DeletableEntity
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public virtual Attachment Attachment { get; set; }

        public int AttachmentId { get; set; }

        public virtual Homework Homework { get; set; }

        public int HomeworkId { get; set; }

        public virtual Student Student { get; set; }

        public Guid StudentId { get; set; }
    }
}
