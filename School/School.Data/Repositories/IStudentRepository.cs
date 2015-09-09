namespace School.Data.Repositories
{
    using System.Linq;
    using School.Models;
    using System.Threading.Tasks;


    public interface IStudentRepository : IDeletableEntityRepository<Student>
    {
        Task<Student> GetByUserName(string username);

        IQueryable<Student> SearchByName(string searchString);

        bool IsUserNameUniqueOnEdit(Student student, string username);

        bool IsEmailUniqueOnEdit(Student student, string email);
    }
}