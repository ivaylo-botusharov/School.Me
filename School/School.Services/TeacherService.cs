namespace School.Services
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using School.Data;
    using School.Models;
    using School.Services.Interfaces;
    using System;
    using System.Linq;

    public class TeacherService : ITeacherService
    {
        private readonly UnitOfWork unitOfWork;
        
        public TeacherService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UnitOfWork UnitOfWork
        {
            get { return this.unitOfWork;  }
        }

        public IQueryable<Teacher> All()
        {
            return this.unitOfWork.Teachers.All();
        }

        public Teacher GetById(Guid id)
        {
            return this.unitOfWork.Teachers.GetById(id);
        }

        //TODO: Move GetByUserName() in the TeachersRepository.
        //This way Services will be decoupled from Microsoft.AspNet.Identity and ApplicationDbContext.
        public Teacher GetByUserName(string username)
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);

            ApplicationUser user = userManager.FindByNameAsync(username).Result;
            Teacher teacher = this.unitOfWork.Teachers.All().Where(t => t.ApplicationUserId == user.Id).FirstOrDefault();

            return teacher;
        }

        public void Add(Teacher teacher)
        {
            this.unitOfWork.Teachers.Add(teacher);
            this.unitOfWork.Save();
        }

        public void Update(Teacher teacher)
        {
            this.unitOfWork.Teachers.Update(teacher);
            this.unitOfWork.Save();
        }

        public void Delete(Teacher teacher)
        {
            teacher.ApplicationUser.DeletedBy = teacher.DeletedBy;

            this.unitOfWork.Users.Delete(teacher.ApplicationUser);
            this.unitOfWork.Teachers.Delete(teacher);

            this.unitOfWork.Save();
        }

        //TODO: move method in the TeacherRepository. 
        public IQueryable<Teacher> SearchByName(string searchString)
        {
            var query = this.unitOfWork.Teachers.All().Where(t => t.Name.Contains(searchString));
            //var query = unitOfWork.Teachers.Get(filter: teacher => teacher.Name.Contains(searchString));
            return query;
        }

        public bool IsUserNameUniqueOnEdit(Teacher teacher, string username)
        {
            bool isUserNameUnique = !this.unitOfWork.Teachers.AllWithDeleted()
                .Any(t => (t.ApplicationUser.UserName == username) &&
                    (t.ApplicationUserId != teacher.ApplicationUserId));

            return isUserNameUnique;
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
