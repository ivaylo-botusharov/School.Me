namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;

    public interface IHomeworkService : IRepositoryService<Homework>
    {
        Homework GetById(int id);

        UnitOfWork UnitOfWork { get; }
    }
}
