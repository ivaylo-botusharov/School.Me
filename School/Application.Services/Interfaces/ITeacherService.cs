using Application.Data;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ITeacherService : IRepositoryService<Teacher>
    {
        IQueryable<Teacher> SearchByName(string searchString);

        Teacher GetByUserName(string username);

        bool IsUserNameUniqueOnEdit(Teacher teacher, string username);

        UnitOfWork UnitOfWork { get; }
    }
}
