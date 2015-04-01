namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;

    public interface ILessonService : IRepositoryService<Lesson>
    {
        Lesson GetById(int id);

        UnitOfWork UnitOfWork { get; }
    }
}