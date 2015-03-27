namespace School.Data.Repositories
{
    using School.Models;

    public class TeacherRepository : DeletableEntityRepository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(IApplicationDbContext context)  : base(context)
        {
        }
    }
}
