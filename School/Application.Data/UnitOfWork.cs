namespace Application.Data
{
    using System;
    using System.Linq;
    using Application.Data.Repositories;
    using Application.Models;
    using System.Collections.Generic;

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IApplicationDbContext context;
        private readonly IDictionary<Type, object> repositories;
        private bool disposed = false;

        public UnitOfWork(IApplicationDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public ApplicationDbContext Context
        {
            get
            {
                return (ApplicationDbContext)this.context;
            }
        }

        public IApplicationUserRepository Users
        {
            get
            {
                var typeOfModel = typeof(ApplicationUser);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new ApplicationUserRepository(this.context));
                }
                return (IApplicationUserRepository)this.repositories[typeOfModel];
            }
        }

        public IStudentRepository Students
        {
            get
            {
                var typeOfModel = typeof(Student);
                if (!this.repositories.ContainsKey(typeOfModel))
                {
                    this.repositories.Add(typeOfModel, new StudentRepository(this.context));
                }
                return (IStudentRepository)this.repositories[typeOfModel];
            }
        }

        public IGenericRepository<Course> Courses
        {
            get
            {
                return this.GetRepository<Course>();
            }
        }
        
        public void Save()
        {
            this.context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
