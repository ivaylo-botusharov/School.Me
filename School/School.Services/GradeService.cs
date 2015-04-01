namespace School.Services
{
    using School.Data;
    using School.Models;
    using School.Services.Interfaces;
    using System.Linq;

    public class GradeService : IGradeService
    {
        private readonly UnitOfWork unitOfWork;

        public GradeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        public Grade GetById(int id)
        {
            return this.unitOfWork.Grades.GetById(id);
        }

        public bool IsGradeUniqueOnEdit(Grade grade)
        {
            bool isGradeUnique = !this.unitOfWork.Grades
                .All()
                .Any(
                g => (g.GradeYear == grade.GradeYear) &&
                    (g.AcademicYear.StartDate.Year == grade.AcademicYear.StartDate.Year) &&
                    (g.Id != grade.Id)
                );

            return isGradeUnique;
        }

        public IQueryable<Grade> All()
        {
            return this.unitOfWork.Grades.All();
        }

        public void Add(Grade grade)
        {
            this.unitOfWork.Grades.Add(grade);
            this.unitOfWork.Save();
        }

        public void Update(Grade grade)
        {
            this.unitOfWork.Grades.Update(grade);
            this.unitOfWork.Save();
        }

        public void Delete(Grade grade)
        {
            this.unitOfWork.Grades.Delete(grade);
            this.unitOfWork.Save();
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
