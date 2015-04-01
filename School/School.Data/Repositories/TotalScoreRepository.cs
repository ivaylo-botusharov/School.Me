namespace School.Data.Repositories
{
    using School.Models;

    public class TotalScoreRepository : DeletableEntityRepository<TotalScore>, ITotalScoreRepository
    {
        public TotalScoreRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
