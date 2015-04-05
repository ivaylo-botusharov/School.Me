namespace School.Data.Repositories
{
    using System.Linq;
    using School.Models;

    public interface ITeacherRepository : IDeletableEntityRepository<Teacher>
    {
        Teacher GetByUserName(string username);

        IQueryable<Teacher> SearchByName(string searchString);

        bool IsUserNameUniqueOnEdit(Teacher teacher, string username);
    }
}
