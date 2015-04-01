namespace School.Data.Repositories
{
    using School.Models;

    public class HomeworkSolutionRepository : DeletableEntityRepository<HomeworkSolution>, IHomeworkSolutionRepository
    {
        public HomeworkSolutionRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
