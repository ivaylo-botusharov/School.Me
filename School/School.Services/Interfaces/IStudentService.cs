namespace School.Services.Interfaces
{
    using School.Data.Repositories;
    using School.Models;
    using System.Linq;

    public interface IStudentService : IRepositoryService<Student>
    {
        Student GetById(int id);

        IQueryable<Student> SearchByName(string searchString);

        Student GetByUserName(string username);

        bool IsUserNameUniqueOnEdit(Student student, string username);

        bool IsEmailUniqueOnEdit(Student student, string email);

        IApplicationUserRepository UserRepository { get; }
    }
}