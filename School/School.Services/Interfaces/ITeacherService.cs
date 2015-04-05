namespace School.Services.Interfaces
{
    using System;
    using System.Linq;
    using School.Data.Repositories;
    using School.Models;

    public interface ITeacherService : IRepositoryService<Teacher>
    {
        IApplicationUserRepository UserRepository { get; }

        Teacher GetById(Guid id);

        IQueryable<Teacher> SearchByName(string searchString);

        Teacher GetByUserName(string username);

        bool IsUserNameUniqueOnEdit(Teacher teacher, string username);
    }
}
