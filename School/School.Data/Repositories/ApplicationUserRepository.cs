namespace School.Data.Repositories
{
    using School.Models;
    using System;
    using System.Linq;

    public class ApplicationUserRepository : DeletableEntityRepository<ApplicationUser>, IApplicationUserRepository
    {
        public ApplicationUserRepository(IApplicationDbContext context)
            : base(context)
        {
        }
    }
}