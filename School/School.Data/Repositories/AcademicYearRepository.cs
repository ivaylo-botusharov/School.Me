namespace School.Data.Repositories
{
    using School.Models;

    public class AcademicYearRepository : DeletableEntityRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
