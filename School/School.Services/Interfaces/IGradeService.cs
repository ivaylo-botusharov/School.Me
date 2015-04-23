namespace School.Services.Interfaces
{
    using School.Models;

    public interface IGradeService : IRepositoryService<Grade>
    {
        Grade GetById(int id);

        bool IsGradeUniqueOnEdit(Grade grade);

        void HardDelete(Grade grade);
    }
}
