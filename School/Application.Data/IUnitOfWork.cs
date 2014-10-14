namespace Application.Data
{
    using System;
    using System.Linq;
    using Application.Data.Repositories;
    using Application.Models;

    public interface IUnitOfWork
    {
        IGenericRepository<ApplicationUser> Users { get; }

        IGenericRepository<Student> Students { get; }

        IGenericRepository<Course> Courses { get; }

        void Save();
    }
}
