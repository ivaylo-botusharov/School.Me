using Application.Data;
using Application.Models;
using Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SchoolClassService : ISchoolClassService
    {
        private readonly UnitOfWork unitOfWork;

        public SchoolClassService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        public IQueryable<SchoolClass> All()
        {
            return this.unitOfWork.SchoolClasses.All();
        }

        public SchoolClass GetById(Guid id)
        {
            return this.unitOfWork.SchoolClasses.GetById(id);
        }

        public void Add(SchoolClass schoolClass)
        {
            this.unitOfWork.SchoolClasses.Add(schoolClass);
        }

        public void Update(SchoolClass schoolClass)
        {
            this.unitOfWork.SchoolClasses.Update(schoolClass);
        }

        public void Delete(SchoolClass schoolClass)
        {
            this.unitOfWork.SchoolClasses.Delete(schoolClass);
        }

        public void Dispose()
        {
            this.UnitOfWork.Dispose();
        }
    }
}
