using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Data.Repositories
{
    public interface ITeacherRepository : IDeletableEntityRepository<Teacher>
    {
    }
}
