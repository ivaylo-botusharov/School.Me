namespace Application.Services
{
    using Application.Data;
    using Application.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CourseService : ICourseService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public IQueryable<Course> All()
        {
            return unitOfWork.Courses.All();
        }

        public Course GetById(Guid id)
        {
            return unitOfWork.Courses.GetById(id);
        }

        public void Add(Course course)
        {
            unitOfWork.Courses.Add(course);
            unitOfWork.Save();
        }

        public void Update(Course course)
        {
            unitOfWork.Courses.Update(course);
            unitOfWork.Save();
        }

        public void Delete(Course course)
        {
            unitOfWork.Courses.Delete(course);
            unitOfWork.Save();
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}