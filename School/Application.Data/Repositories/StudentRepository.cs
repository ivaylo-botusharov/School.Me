namespace Application.Data.Repositories
{
    using Application.Models;
    using System;
    using System.Linq;

    public class StudentRepository : DeletableEntityRepository<Student>, IStudentRepository
    {
        public StudentRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}