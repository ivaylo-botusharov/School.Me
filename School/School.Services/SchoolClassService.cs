namespace School.Services
{
    using School.Data.Repositories;
    using School.Models;
    using School.Services.Interfaces;
    using System;
    using System.Linq;

    public class SchoolClassService : ISchoolClassService
    {
        private readonly ISchoolClassRepository schoolClassRepository;

        public SchoolClassService(ISchoolClassRepository schoolClassRepository)
        {
            this.schoolClassRepository = schoolClassRepository;
        }

        public IQueryable<SchoolClass> All()
        {
            return this.schoolClassRepository.All();
        }

        public SchoolClass GetById(Guid id)
        {
            return this.schoolClassRepository.GetById(id);
        }

        public void Add(SchoolClass schoolClass)
        {
            this.schoolClassRepository.Add(schoolClass);
        }

        public void Update(SchoolClass schoolClass)
        {
            this.schoolClassRepository.Update(schoolClass);
        }

        public void Delete(SchoolClass schoolClass)
        {
            this.schoolClassRepository.Delete(schoolClass);
        }

        public void Dispose()
        {
            this.schoolClassRepository.Dispose();
        }
    }
}
