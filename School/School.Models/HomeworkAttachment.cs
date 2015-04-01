namespace School.Models
{
    public class HomeworkAttachment : DeletableEntity
    {
        public int Id { get; set; }

        public virtual Attachment Attachment { get; set; }

        public int AttachmentId { get; set; }

        public int HomeworkId { get; set; }
    }
}
