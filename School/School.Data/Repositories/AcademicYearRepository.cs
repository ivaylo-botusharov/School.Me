namespace School.Data.Repositories
{
    using School.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AcademicYearRepository : DeletableEntityRepository<AcademicYear>, IAcademicYearRepository
    {
        public AcademicYearRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
