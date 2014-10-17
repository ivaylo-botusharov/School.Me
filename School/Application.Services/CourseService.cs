namespace Application.Services
{
    using System;
    using System.Linq;
    using Application.Data;
    using Application.Models;
    using Application.Services.Interfaces;

    public class CourseService : ICourseService
    {
        private readonly UnitOfWork unitOfWork;

        public CourseService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Course> All()
        {
            return this.unitOfWork.Courses.All();
        }

        public Course GetById(Guid id)
        {
            return this.unitOfWork.Courses.GetById(id);
        }

        public void Add(Course course)
        {
            this.unitOfWork.Courses.Add(course);
            this.unitOfWork.Save();
        }

        public void Update(Course course)
        {
            this.unitOfWork.Courses.Update(course);
            this.unitOfWork.Save();
        }

        public void Delete(Course course)
        {
            this.unitOfWork.Courses.Delete(course);
            this.unitOfWork.Save();
        }

        public void Dispose()
        {
            this.unitOfWork.Dispose();
        }
    }
}