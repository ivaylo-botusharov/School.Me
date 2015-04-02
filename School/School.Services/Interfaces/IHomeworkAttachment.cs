namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;

    public interface IHomeworkAttachment: IRepositoryService<HomeworkAttachment>
    {
        HomeworkAttachment GetById(int id);
    }
}
