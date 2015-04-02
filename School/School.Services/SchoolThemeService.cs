namespace School.Services
{
    using School.Data;
    using School.Data.Repositories;
    using School.Models;
    using School.Services.Interfaces;
    using System.Linq;

    public class SchoolThemeService : ISchoolThemeService
    {
        private readonly ISchoolThemeRepository schoolThemeRepository;

        public SchoolThemeService(ISchoolThemeRepository schoolThemeRepository)
        {
            this.schoolThemeRepository = schoolThemeRepository;
        }
        
        public SchoolTheme GetById(int id)
        {
            return this.schoolThemeRepository.GetById(id);
        }

        public IQueryable<SchoolTheme> All()
        {
            return this.schoolThemeRepository.All();
        }

        public void Add(SchoolTheme schoolTheme)
        {
            this.schoolThemeRepository.Add(schoolTheme);
            this.schoolThemeRepository.SaveChanges();
        }

        public void Update(SchoolTheme schoolTheme)
        {
            this.schoolThemeRepository.Update(schoolTheme);
            this.schoolThemeRepository.SaveChanges();
        }

        public void Delete(SchoolTheme schoolTheme)
        {
            this.schoolThemeRepository.Delete(schoolTheme);
            this.schoolThemeRepository.SaveChanges();
        }

        public void Dispose()
        {
            this.schoolThemeRepository.Dispose();
        }
    }
}
