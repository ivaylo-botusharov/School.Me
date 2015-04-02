namespace School.Data.Repositories
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using School.Models;
    using System.Linq;
    
    public class StudentRepository : DeletableEntityRepository<Student>, IStudentRepository
    {
        private readonly IApplicationDbContext context;

        public StudentRepository(IApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Student GetByUserName(string username)
        {
            var store = new UserStore<ApplicationUser>((ApplicationDbContext)context);
            var userManager = new UserManager<ApplicationUser>(store);

            ApplicationUser user = userManager.FindByNameAsync(username).Result;

            if (user == null)
            {
                //TODO: Implement exception/error handling if there is no such user
            }

            Student student = this.All().Where(s => s.ApplicationUserId == user.Id).FirstOrDefault();

            return student;
        }

        public IQueryable<Student> SearchByName(string searchString)
        {
            var query = this.All().Where(student => student.Name.Contains(searchString));
            //var query = this.Get(filter: student => student.Name.Contains(searchString));
            return query;
        }

        public bool IsUserNameUniqueOnEdit(Student student, string username)
        {
            bool isUserNameUnique = !this.AllWithDeleted()
                .Any(s => 
                    (s.ApplicationUser.UserName == username) &&
                    (s.ApplicationUserId != student.ApplicationUserId));

            return isUserNameUnique;
        }

        public bool IsEmailUniqueOnEdit(Student student, string email)
        {
            bool IsEmailUnique = !this.AllWithDeleted()
                .Any(s => 
                    (s.ApplicationUser.Email == email) &&
                    (s.ApplicationUserId != student.ApplicationUserId));

            return IsEmailUnique;
        }
    }
}