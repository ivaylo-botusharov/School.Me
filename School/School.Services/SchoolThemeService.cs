namespace School.Services
{
    using School.Data;
    using School.Models;
    using School.Services.Interfaces;
    using System.Linq;

    public class SchoolThemeService : ISchoolThemeService
    {
        private readonly UnitOfWork unitOfWork;

        public SchoolThemeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        public SchoolTheme GetById(int id)
        {
            return this.unitOfWork.SchoolThemes.GetById(id);
        }

        public IQueryable<SchoolTheme> All()
        {
            return this.unitOfWork.SchoolThemes.All();
        }

        public void Add(SchoolTheme schoolTheme)
        {
            this.unitOfWork.SchoolThemes.Add(schoolTheme);
            this.unitOfWork.Save();
        }

        public void Update(SchoolTheme schoolTheme)
        {
            this.unitOfWork.SchoolThemes.Update(schoolTheme);
            this.unitOfWork.Save();
        }

        public void Delete(SchoolTheme schoolTheme)
        {
            this.unitOfWork.SchoolThemes.Delete(schoolTheme);
            this.unitOfWork.Save();
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
