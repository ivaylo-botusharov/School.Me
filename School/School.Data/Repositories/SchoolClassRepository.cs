namespace School.Data.Repositories
{
    using System.Linq;
    using School.Models;

    public class SchoolClassRepository : DeletableEntityRepository<SchoolClass>, ISchoolClassRepository
    {
        public SchoolClassRepository(IApplicationDbContext context) : base(context)
        {
        }

        public SchoolClass GetByDetails(int gradeYear, string letter, int startYear)
        {
            SchoolClass schoolClass = this.All()
                .FirstOrDefault(
                    sc => sc.Grade.GradeYear == gradeYear &&
                          sc.ClassLetter == letter &&
                          sc.Grade.AcademicYear.StartDate.Year == startYear);

            return schoolClass;
        }
    }
}
