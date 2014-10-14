namespace Application.Services
{
    using System;
    using System.Linq;
    using Application.Models;

    public interface IStudentService : IGenericService<Student>
    {
        IQueryable<Student> SearchByName(string searchString);
    }
}