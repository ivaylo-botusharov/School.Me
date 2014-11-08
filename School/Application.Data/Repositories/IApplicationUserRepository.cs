namespace Application.Data.Repositories
{
    using Application.Models;
    using System;
    using System.Linq;

    public interface IApplicationUserRepository : IDeletableEntityRepository<ApplicationUser>
    {
    }
}
