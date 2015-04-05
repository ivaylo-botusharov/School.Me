namespace School.Data.Repositories
{
    using System.Linq;
    using School.Models;

    public interface IStudentRepository : IDeletableEntityRepository<Student>
    {
        Student GetByUserName(string username);

        IQueryable<Student> SearchByName(string searchString);

        bool IsUserNameUniqueOnEdit(Student student, string username);

        bool IsEmailUniqueOnEdit(Student student, string email);
    }
}