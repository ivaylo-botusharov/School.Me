namespace Application.Services
{
    using Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IGenericService<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        T GetById(Guid id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}