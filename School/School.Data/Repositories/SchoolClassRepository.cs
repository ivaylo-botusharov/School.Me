using School.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Repositories
{
    public class SchoolClassRepository : DeletableEntityRepository<SchoolClass>, ISchoolClassRepository
    {
        public SchoolClassRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}
