namespace School.Services.Interfaces
{
    using School.Models;

    public interface IHomeworkService : IRepositoryService<Homework>
    {
        Homework GetById(int id);
    }
}
