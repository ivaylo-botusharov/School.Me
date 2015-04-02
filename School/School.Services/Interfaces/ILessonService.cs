namespace School.Services.Interfaces
{
    using School.Models;

    public interface ILessonService : IRepositoryService<Lesson>
    {
        Lesson GetById(int id);
    }
}