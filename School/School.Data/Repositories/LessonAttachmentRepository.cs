namespace School.Data.Repositories
{
    using School.Models;

    public class LessonAttachmentRepository : DeletableEntityRepository<LessonAttachment>, ILessonAttachmentRepository
    {
        public LessonAttachmentRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
