namespace School.Data.Repositories
{
    using School.Models;

    public class HomeworkRepository : DeletableEntityRepository<Homework>, IHomeworkRepository
    {
        public HomeworkRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
