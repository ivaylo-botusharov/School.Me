namespace School.Services
{
    using School.Data;
    using School.Models;
    using School.Services.Interfaces;
    using System;
    using System.Linq;

    public class AcademicYearService : IAcademicYearService
    {
        private readonly UnitOfWork unitOfWork;
        public AcademicYearService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public UnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        public IQueryable<AcademicYear> All()
        {
            return this.unitOfWork.AcademicYears.All();
        }

        public AcademicYear GetById(Guid id)
        {
            return this.unitOfWork.AcademicYears.GetById(id);
        }

        public void Add(AcademicYear academicYear)
        {
            this.unitOfWork.AcademicYears.Add(academicYear);
            this.unitOfWork.Save();
        }

        public void Update(AcademicYear academicYear)
        {
            this.unitOfWork.AcademicYears.Update(academicYear);
            this.unitOfWork.Save();
        }

        public void Delete(AcademicYear academicYear)
        {
            this.unitOfWork.AcademicYears.Delete(academicYear);
            this.unitOfWork.Save();
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
