namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;

    public interface ITotalScore : IRepositoryService<TotalScore>
    {
        TotalScore GetById(int id);

        UnitOfWork UnitOfWork { get; }
    }
}
