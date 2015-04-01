namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;

    public interface ISubjectService : IRepositoryService<Subject>
    {
        Subject GetById(int id);

        UnitOfWork UnitOfWork { get; }
    }
}
