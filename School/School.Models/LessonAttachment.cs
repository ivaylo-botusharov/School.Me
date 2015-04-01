namespace School.Models
{
    using System;

    public class LessonAttachment : DeletableEntity
    {
        public int Id { get; set; }

        public int LessonId { get; set; }

        public virtual Attachment Attachment { get; set; }

        public int AttachmentId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
