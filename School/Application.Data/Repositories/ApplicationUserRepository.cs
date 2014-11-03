namespace Application.Data.Repositories
{
    using Application.Models;
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