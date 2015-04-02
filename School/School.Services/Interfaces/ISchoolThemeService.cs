namespace School.Services.Interfaces
{
    using School.Models;

    public interface ISchoolThemeService : IRepositoryService<SchoolTheme>
    {
        SchoolTheme GetById(int id);
    }
}
