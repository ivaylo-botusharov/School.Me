namespace School.Data.Repositories
{
    using School.Models;

    public class GradeRepository : DeletableEntityRepository<Grade>, IGradeRepository
    {
        public GradeRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
