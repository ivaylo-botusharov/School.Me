namespace School.Data.Repositories
{
    using School.Models;
    using System.Linq;

    public interface IStudentRepository : IDeletableEntityRepository<Student>
    {
        Student GetByUserName(string username);

        IQueryable<Student> SearchByName(string searchString);

        bool IsUserNameUniqueOnEdit(Student student, string username);

        bool IsEmailUniqueOnEdit(Student student, string email);
    }
}