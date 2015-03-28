namespace School.Data.Repositories
{
    using School.Models;

    public class SchoolClassRepository : DeletableEntityRepository<SchoolClass>, ISchoolClassRepository
    {
        public SchoolClassRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
