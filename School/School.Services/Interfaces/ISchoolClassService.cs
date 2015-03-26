namespace School.Services.Interfaces
{
    using School.Data;
    using School.Models;
    using System;

    public interface ISchoolClassService : IRepositoryService<SchoolClass>
    {
        SchoolClass GetById(Guid id);

        UnitOfWork UnitOfWork { get; }
    }
}
