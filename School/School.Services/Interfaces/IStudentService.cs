namespace School.Services.Interfaces
{
    using System.Linq;
    using School.Data.Repositories;
    using School.Models;
    
    public interface IStudentService : IRepositoryService<Student>
    {
        IApplicationUserRepository UserRepository { get; }

        Student GetById(int id);

        IQueryable<Student> SearchByName(string searchString);

        Student GetByUserName(string username);

        bool IsUserNameUniqueOnEdit(Student student, string username);

        bool IsEmailUniqueOnEdit(Student student, string email);
    }
}