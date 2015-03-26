namespace School.Data.Repositories
{
    using School.Models;
    using System;
    using System.Linq;

    public interface IApplicationUserRepository : IDeletableEntityRepository<ApplicationUser>
    {
    }
}
