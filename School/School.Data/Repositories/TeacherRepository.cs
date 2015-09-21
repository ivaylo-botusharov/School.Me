namespace School.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using School.Models;

    public class TeacherRepository : DeletableEntityRepository<Teacher>, ITeacherRepository
    {
        private readonly IApplicationDbContext context;

        public TeacherRepository(IApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Teacher> GetByUserName(string username)
        {
            var store = new UserStore<ApplicationUser>((ApplicationDbContext)this.context);
            var userManager = new UserManager<ApplicationUser>(store);

            ApplicationUser user = await userManager.FindByNameAsync(username);
            Teacher teacher = this.All().Where(t => t.ApplicationUserId == user.Id).FirstOrDefault();

            return teacher;
        }

        public IQueryable<Teacher> SearchByName(string searchString)
        {
            // var query = this.Get(filter: teacher => teacher.Name.Contains(searchString));
            var query = this.All().Where(t => t.Name.Contains(searchString));

            return query;
        }

        public bool IsUserNameUniqueOnEdit(Teacher teacher, string username)
        {
            bool usernameUnique = !this.AllWithDeleted()
                .Any(
                t => (t.ApplicationUser.UserName == username) &&
                    (t.ApplicationUserId != teacher.ApplicationUserId));

            return usernameUnique;
        }
    }
}
