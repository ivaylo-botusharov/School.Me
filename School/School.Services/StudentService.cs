namespace School.Services
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using School.Data;
    using School.Models;
    using School.Services.Interfaces;
    using System.Linq;
    
    public class StudentService : IStudentService
    {
        private readonly UnitOfWork unitOfWork;

        public StudentService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public UnitOfWork UnitOfWork
        {
            get { return this.unitOfWork; }
        }

        public IQueryable<Student> All()
        {
            return this.unitOfWork.Students.All();
        }

        public Student GetById(int id)
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
            student.ApplicationUser.DeletedBy = student.DeletedBy;

            this.unitOfWork.Users.Delete(student.ApplicationUser);
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

        public bool IsUserNameUniqueOnEdit(Student student, string username)
        {
            bool isUserNameUnique = ! this.unitOfWork.Students.AllWithDeleted()
                .Any(s => (s.ApplicationUser.UserName == username) &&
                    (s.ApplicationUserId != student.ApplicationUserId));

            return isUserNameUnique;
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}
