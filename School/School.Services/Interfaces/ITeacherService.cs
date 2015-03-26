namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;
    using System;
    using System.Linq;

    public interface ITeacherService : IRepositoryService<Teacher>
    {
        Teacher GetById(Guid id);

        IQueryable<Teacher> SearchByName(string searchString);

        Teacher GetByUserName(string username);

        bool IsUserNameUniqueOnEdit(Teacher teacher, string username);

        UnitOfWork UnitOfWork { get; }
    }
}
