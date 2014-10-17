namespace Application.Services
{
    using System;
    using System.Linq;
    using Application.Data;
    using Application.Models;
    using Application.Services.Interfaces;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity;
    
    public class StudentService : IStudentService
    {
        private readonly UnitOfWork unitOfWork;

        public StudentService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Student> All()
        {
            return this.unitOfWork.Students.All();
        }

        public Student GetById(Guid id)
        {
            return this.unitOfWork.Students.GetById(id);
        }

        //TODO: Create StudentsRepository and move GetByUserName() there.
        //This way Services will be decoupled from Microsoft.AspNet.Identity and ApplicationDbContext.
        public Student GetByUserName(string username)
        {
            var store = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(store);

            ApplicationUser user = userManager.FindByNameAsync(username).Result;
            Student student = this.unitOfWork.Students.All().Where(s => s.ApplicationUserId == user.Id).FirstOrDefault();

            return student;
        }

        public void Add(Student student)
        {
            this.unitOfWork.Students.Add(student);
            this.unitOfWork.Save();
        }

        public void Update(Student student)
        {
            this.unitOfWork.Students.Update(student);
            this.unitOfWork.Save();
        }

        public void Delete(Student student)
        {
            this.unitOfWork.Students.Delete(student);
            this.unitOfWork.Save();
        }

        //TODO: Create StudentsRepository and move it there. 
        public IQueryable<Student> SearchByName(string searchString)
        {
            var query = this.unitOfWork.Students.All().Where(student => student.Name.Contains(searchString));
            //var query = unitOfWork.Students.Get(filter: student => student.Name.Contains(searchString));
            return query;
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
