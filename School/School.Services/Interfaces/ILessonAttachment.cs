namespace School.Services.Interfaces
{
    using School.Models;

    public interface ILessonAttachment : IRepositoryService<LessonAttachment>
    {
        LessonAttachment GetById(int id);
    }
}
