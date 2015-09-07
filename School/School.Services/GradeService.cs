namespace School.Services
{
    using System.Linq;
    using School.Data.Repositories;
    using School.Models;
    using School.Services.Interfaces;

    public class GradeService : IGradeService
    {
        private readonly IGradeRepository gradeRepository;

        public GradeService(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }

        public Grade GetById(int id)
        {
            return this.gradeRepository.GetById(id);
        }

        public bool IsGradeUniqueOnEdit(Grade grade)
        {
            bool gradeUnique = !this.gradeRepository
                .All()
                .Any(
                g => (g.GradeYear == grade.GradeYear) &&
                    (g.AcademicYear.StartDate.Year == grade.AcademicYear.StartDate.Year) &&
                    (g.Id != grade.Id));

            return gradeUnique;
        }

        public IQueryable<Grade> All()
        {
            return this.gradeRepository.All();
        }

        public void Add(Grade grade)
        {
            this.gradeRepository.Add(grade);
            this.gradeRepository.SaveChanges();
        }

        public void Update(Grade grade)
        {
            this.gradeRepository.Update(grade);
            this.gradeRepository.SaveChanges();
        }

        public void Delete(Grade grade)
        {
            this.gradeRepository.Delete(grade);
            this.gradeRepository.SaveChanges();
        }

        public void HardDelete(Grade grade)
        {
            this.gradeRepository.HardDelete(grade);
            this.gradeRepository.SaveChanges();
        }
    }
}
