namespace Application.Services.Interfaces
{
    using System;
    using System.Linq;

    public interface IRepositoryService<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        T GetById(Guid id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}