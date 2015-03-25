namespace Application.Services.Interfaces
{
    using System;
    using System.Linq;
    using Application.Models;
    using Application.Data;

    public interface IStudentService : IRepositoryService<Student>
    {
        Student GetById(int id);

        IQueryable<Student> SearchByName(string searchString);

        Student GetByUserName(string username);

        bool IsUserNameUniqueOnEdit(Student student, string username);

        UnitOfWork UnitOfWork { get; }
    }
}