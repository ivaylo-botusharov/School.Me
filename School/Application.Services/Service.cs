namespace Application.Services
{
    using System;
    using System.Linq;
    using Application.Data;
    using Application.Services.Interfaces;

    public class Service : IService
    {
        private readonly IStudentService students;
        private readonly ICourseService courses;
        private readonly UnitOfWork unitOfWork;

        public Service()
        {
            this.unitOfWork = new UnitOfWork();
            this.students = new StudentService(unitOfWork);
            this.courses = new CourseService(unitOfWork);
        }

        public IStudentService Students
        {
            get { return this.students; }
        }

        public ICourseService Courses
        {
            get { return this.courses; }
        }
    }
}
