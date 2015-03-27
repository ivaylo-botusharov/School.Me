namespace School.Data.Repositories
{
    using School.Models;
    
    public class StudentRepository : DeletableEntityRepository<Student>, IStudentRepository
    {
        public StudentRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}