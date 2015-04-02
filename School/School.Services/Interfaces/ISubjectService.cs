namespace School.Services.Interfaces
{
    using School.Models;

    public interface ISubjectService : IRepositoryService<Subject>
    {
        Subject GetById(int id);
    }
}
