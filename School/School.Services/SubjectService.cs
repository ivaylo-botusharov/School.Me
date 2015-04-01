namespace School.Services
{
    using School.Data;
    using School.Models;
    using School.Services.Interfaces;
    using System.Linq;

    public class SubjectService : ISubjectService
    {
        private readonly UnitOfWork unitOfWork;
        
        public SubjectService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        public Subject GetById(int id)
        {
            return this.unitOfWork.Subjects.GetById(id);
        }

        public IQueryable<Subject> All()
        {
            return this.unitOfWork.Subjects.All() ;
        }

        public void Add(Subject subject)
        {
            this.unitOfWork.Subjects.Add(subject);
            this.unitOfWork.Save();
        }

        public void Update(Subject subject)
        {
            this.unitOfWork.Subjects.Update(subject);
            this.unitOfWork.Save();
        }

        public void Delete(Subject subject)
        {
            this.unitOfWork.Subjects.Delete(subject);
            this.unitOfWork.Save();
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
