namespace School.Data.Repositories
{
    using School.Models;

    public class ApplicationUserRepository : DeletableEntityRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}