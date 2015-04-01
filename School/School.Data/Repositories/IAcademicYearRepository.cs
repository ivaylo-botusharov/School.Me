namespace School.Data.Repositories
{
    using School.Models;
    using System;

    public interface IAcademicYearRepository : IDeletableEntityRepository<AcademicYear>
    {
        bool ExistsInDb(DateTime startDate, DateTime endDate);
    }
}
