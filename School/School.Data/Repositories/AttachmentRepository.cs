namespace School.Data.Repositories
{
    using School.Models;

    public class AttachmentRepository : DeletableEntityRepository<Attachment>, IAttachmentRepository
    {
        public AttachmentRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}