namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;

    public interface ISchoolThemeService : IRepositoryService<SchoolTheme>
    {
        SchoolTheme GetById(int id);

        UnitOfWork UnitOfWork { get; }
    }
}
