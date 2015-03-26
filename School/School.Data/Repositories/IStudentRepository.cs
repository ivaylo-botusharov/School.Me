namespace School.Data.Repositories
{
    using System;
    using System.Linq;
    using School.Models;

    public interface IStudentRepository : IDeletableEntityRepository<Student>
    {
    }
}