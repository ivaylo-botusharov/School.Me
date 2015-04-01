namespace School.Data.Repositories
{
    using School.Models;
    using System;
    using System.Linq;

    public class AcademicYearRepository : DeletableEntityRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(IApplicationDbContext context) : base(context)
        {
        }

        //Checks if AcademicYear with specified start year or end year exists in database
        public bool ExistsInDb(DateTime startDate, DateTime endDate)
        {
            bool academicYearExists = false;

            int academicYearsCount = this.All()
                .Where(ay => ay.StartDate.Year == startDate.Year || ay.EndDate.Year == endDate.Year).Count();

            if (academicYearsCount > 0)
            {
                academicYearExists = true;
            }

            return academicYearExists;
        }
    }
}