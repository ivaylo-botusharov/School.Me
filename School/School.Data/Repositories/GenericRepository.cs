namespace School.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly IDbSet<T> DbSet;

        private readonly IApplicationDbContext context;
        
        public GenericRepository(IApplicationDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            }

            this.context = context;
            this.DbSet = context.Set<T>();
        }

        public virtual IQueryable<T> All()
        {
            return this.DbSet;
        }

        public virtual T GetById(Guid id)
        {
            return this.DbSet.Find(id);
        }

        public virtual T GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public virtual IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = this.DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in 
                includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }
            else
            {
                return query;
            }
        }

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public virtual void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public void Detach(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Detached);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();
        }

        public void ChangeEntityState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = state;
        }
    }
}
