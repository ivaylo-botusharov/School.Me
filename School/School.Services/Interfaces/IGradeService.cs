namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;
    using System.Linq;

    public interface IGradeService : IRepositoryService<Grade>
    {
        Grade GetById(int id);

        bool IsGradeUniqueOnEdit(Grade grade);

        UnitOfWork UnitOfWork { get; }
    }
}
