namespace School.Data.Repositories
{
    using School.Models;
    using System.Linq;

    public interface ITeacherRepository : IDeletableEntityRepository<Teacher>
    {
        Teacher GetByUserName(string username);

        IQueryable<Teacher> SearchByName(string searchString);

        bool IsUserNameUniqueOnEdit(Teacher teacher, string username);
    }
}
