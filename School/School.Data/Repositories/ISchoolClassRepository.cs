namespace School.Data.Repositories
{
    using School.Models;

    public interface ISchoolClassRepository : IDeletableEntityRepository<SchoolClass>
    {
        SchoolClass GetByDetails(int gradeYear, string letter, int startYear);
    }
}
