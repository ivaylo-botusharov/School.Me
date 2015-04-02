namespace School.Services.Interfaces
{
    using School.Models;

    public interface ITotalScore : IRepositoryService<TotalScore>
    {
        TotalScore GetById(int id);
    }
}
