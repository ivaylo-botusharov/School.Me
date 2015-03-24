namespace Application.Data
{
    using System;
    using System.Linq;
    using Application.Data.Repositories;
    using Application.Models;

    public interface IUnitOfWork
    {
        IApplicationUserRepository Users { get; }

        IStudentRepository Students { get; }

        ISchoolClassRepository SchoolClasses { get; }

        ApplicationDbContext Context { get; }

        void Save();
    }
}