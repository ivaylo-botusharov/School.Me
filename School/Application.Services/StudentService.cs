namespace Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Application.Data;
    using Application.Models;

    public class StudentService : IStudentService
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public IQueryable<Student> All()
        {
            return unitOfWork.Students.All();
        }

        public Student GetById(int? id)
        {
            return unitOfWork.Students.GetById(id);
        }

        public void Add(Student student)
        {
            unitOfWork.Students.Add(student);
            unitOfWork.Save();
        }

        public void Update(Student student)
        {
            unitOfWork.Students.Update(student);
            unitOfWork.Save();
        }

        public void Delete(Student student)
        {
            unitOfWork.Students.Delete(student);
            unitOfWork.Save();
        }

        public IQueryable<Student> SearchByName(string searchString)
        {
            var query = unitOfWork.Students.All().Where(student => student.Name.Contains(searchString));
            //var query = unitOfWork.Students.Get(filter: student => student.Name.Contains(searchString));
            return query;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
