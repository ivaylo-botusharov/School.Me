namespace Application.Data.Repositories
{
    using System;
    using System.Linq;
    using Application.Models;

    public class DeletableEntityRepository<T> : GenericRepository<T>, IDeletableEntityRepository<T> where T : class, IDeletableEntity
    {
        public DeletableEntityRepository(IApplicationDbContext context) : base(context)
        {
        }

        public override IQueryable<T> All()
        {
            return base.All().Where(x => !x.IsDeleted);
        }
        
        public IQueryable<T> AllWithDeleted()
        {
            return base.All();
        }
    }
}
