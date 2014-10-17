namespace Application.Services.Interfaces
{
    using System;
    using System.Linq;
    using Application.Models;

    public interface IStudentService : IRepositoryService<Student>
    {
        IQueryable<Student> SearchByName(string searchString);

        Student GetByUserName(string username);
    }
}