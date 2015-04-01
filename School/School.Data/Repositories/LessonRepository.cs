namespace School.Data.Repositories
{
    using School.Models;

    public class LessonRepository : DeletableEntityRepository<Lesson>, ILessonRepository
    {
        public LessonRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
