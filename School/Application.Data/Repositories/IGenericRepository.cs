namespace Application.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> All();

        T GetById(Guid id);

        IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>,
            IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Detach(T entity);

        void SaveChanges();
    }
}
