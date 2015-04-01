namespace School.Data.Repositories
{
    using School.Models;

    public class HomeworkAttachmentRepository : DeletableEntityRepository<HomeworkAttachment>, IHomeworkAttachmentRepository
    {
        public HomeworkAttachmentRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
