namespace School.Services
{
    using System.Linq;
    using School.Data.Repositories;
    using School.Models;
    using School.Services.Interfaces;
    
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository subjectRepository;

        public SubjectService(ISubjectRepository subjectRepository)
        {
            this.subjectRepository = subjectRepository;
        }
        
        public Subject GetById(int id)
        {
            return this.subjectRepository.GetById(id);
        }

        public IQueryable<Subject> All()
        {
            return this.subjectRepository.All();
        }

        public void Add(Subject subject)
        {
            this.subjectRepository.Add(subject);
            this.subjectRepository.SaveChanges();
        }

        public void Update(Subject subject)
        {
            this.subjectRepository.Update(subject);
            this.subjectRepository.SaveChanges();
        }

        public void Delete(Subject subject)
        {
            this.subjectRepository.Delete(subject);
            this.subjectRepository.SaveChanges();
        }
    }
}
