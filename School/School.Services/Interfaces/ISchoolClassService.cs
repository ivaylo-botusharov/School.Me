namespace School.Services.Interfaces
{
    using School.Models;
    using System;

    public interface ISchoolClassService : IRepositoryService<SchoolClass>
    {
        SchoolClass GetById(Guid id);
    }
}
