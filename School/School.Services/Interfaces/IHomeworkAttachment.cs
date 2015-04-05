namespace School.Services.Interfaces
{
    using School.Models;

    public interface IHomeworkAttachment : IRepositoryService<HomeworkAttachment>
    {
        HomeworkAttachment GetById(int id);
    }
}
