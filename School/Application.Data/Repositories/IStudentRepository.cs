namespace Application.Data.Repositories
{
    using System;
    using System.Linq;
    using Application.Models;

    public interface IStudentRepository : IDeletableEntityRepository<Student>
    {
    }
}