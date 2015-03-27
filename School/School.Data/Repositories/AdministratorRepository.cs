namespace School.Data.Repositories
{
    using School.Models;

    public class AdministratorRepository : DeletableEntityRepository<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(IApplicationDbContext context) : base(context)
        {
        }
    }
}
