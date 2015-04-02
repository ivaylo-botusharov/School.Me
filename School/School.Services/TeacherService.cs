namespace School.Services
{
    using School.Data.Repositories;
    using School.Models;
    using School.Services.Interfaces;
    using System;
    using System.Linq;

    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository teacherRepository;

        private readonly IApplicationUserRepository userRepository;

        public TeacherService(ITeacherRepository teacherRepository, IApplicationUserRepository userRepository)
        {
            this.teacherRepository = teacherRepository;
            this.userRepository = userRepository;
        }

        public IApplicationUserRepository UserRepository
        {
            get { return this.userRepository; }
        }

        public IQueryable<Teacher> All()
        {
            return this.teacherRepository.All();
        }

        public Teacher GetById(Guid id)
        {
            return this.teacherRepository.GetById(id);
        }

        public Teacher GetByUserName(string username)
        {
            return this.teacherRepository.GetByUserName(username);
        }

        public void Add(Teacher teacher)
        {
            this.teacherRepository.Add(teacher);
            this.teacherRepository.SaveChanges();
        }

        public void Update(Teacher teacher)
        {
            this.teacherRepository.Update(teacher);
            this.teacherRepository.SaveChanges();
        }

        public void Delete(Teacher teacher)
        {
            teacher.ApplicationUser.DeletedBy = teacher.DeletedBy;

            this.userRepository.Delete(teacher.ApplicationUser);
            this.teacherRepository.Delete(teacher);

            this.teacherRepository.SaveChanges();
        }

        public IQueryable<Teacher> SearchByName(string searchString)
        {
            return this.teacherRepository.SearchByName(searchString);
        }

        public bool IsUserNameUniqueOnEdit(Teacher teacher, string username)
        {
            return this.teacherRepository.IsUserNameUniqueOnEdit(teacher, username);
        }

        public void Dispose()
        {
            this.teacherRepository.Dispose();
        }
    }
}
