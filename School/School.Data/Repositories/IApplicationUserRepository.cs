namespace School.Data.Repositories
{
    using School.Models;

    public interface IApplicationUserRepository : IDeletableEntityRepository<ApplicationUser>
    {
        ApplicationDbContext Context { get; }
    }
}
