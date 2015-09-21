namespace School.Services.Interfaces
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using School.Data.Repositories;
    using School.Models;

    public interface ITeacherService : IRepositoryService<Teacher>
    {
        IApplicationUserRepository UserRepository { get; }

        Teacher GetById(Guid id);

        IQueryable<Teacher> SearchByName(string searchString);

        Task<Teacher> GetByUserName(string username);

        bool IsUserNameUniqueOnEdit(Teacher teacher, string username);
    }
}
