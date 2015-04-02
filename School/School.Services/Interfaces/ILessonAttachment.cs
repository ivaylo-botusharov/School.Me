namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;

    public interface ILessonAttachment : IRepositoryService<LessonAttachment>
    {
        LessonAttachment GetById(int id);
    }
}
