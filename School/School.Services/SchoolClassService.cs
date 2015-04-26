namespace School.Services
{
    using System;
    using System.Linq;
    using School.Data.Repositories;
    using School.Models;
    using School.Services.Interfaces;

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

        public SchoolClass GetByDetails(int gradeYear, string letter, int startYear)
        {
            return this.schoolClassRepository.GetByDetails(gradeYear, letter, startYear);
        }

        public void Add(SchoolClass schoolClass)
        {
            this.schoolClassRepository.Add(schoolClass);
            this.schoolClassRepository.SaveChanges();
        }

        public void Update(SchoolClass schoolClass)
        {
            this.schoolClassRepository.Update(schoolClass);
            this.schoolClassRepository.SaveChanges();
        }

        public void Delete(SchoolClass schoolClass)
        {
            this.schoolClassRepository.Delete(schoolClass);
            this.schoolClassRepository.SaveChanges();
        }

        public void HardDelete(SchoolClass schoolClass)
        {
            this.schoolClassRepository.HardDelete(schoolClass);
            this.schoolClassRepository.SaveChanges();
        }
    }
}
