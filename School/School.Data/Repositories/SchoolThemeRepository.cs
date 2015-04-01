namespace School.Data.Repositories
{
    using School.Models;

    public class SchoolThemeRepository : DeletableEntityRepository<SchoolTheme>, ISchoolThemeRepository
    {
        public SchoolThemeRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
