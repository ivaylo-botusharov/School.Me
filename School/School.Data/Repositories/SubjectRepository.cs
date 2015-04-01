namespace School.Data.Repositories
{
    using School.Models;

    public class SubjectRepository : DeletableEntityRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
