namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;
    using System;

    public interface IAcademicYearService : IRepositoryService<AcademicYear>
    {
        AcademicYear GetById(Guid id);

        UnitOfWork UnitOfWork { get; }
    }
}
