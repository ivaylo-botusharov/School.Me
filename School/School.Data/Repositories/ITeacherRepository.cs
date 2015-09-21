namespace School.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using School.Models;

    public interface ITeacherRepository : IDeletableEntityRepository<Teacher>
    {
        Task<Teacher> GetByUserName(string username);

        IQueryable<Teacher> SearchByName(string searchString);

        bool IsUserNameUniqueOnEdit(Teacher teacher, string username);
    }
}
