namespace Application.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Application.Models;
    using Application.Services.Interfaces;
    using Application.Web.Models;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class StudentsController : Controller
    {
        private readonly IService service;

        public StudentsController(IService service)
        {
            this.service = service;
        }

        // GET: Students
        public ActionResult Index()
        {
            var allStudents = this.service.Students.All().Project().To<StudentBasicViewModel>();
            return View(allStudents.AsEnumerable());
        }

        
        public ActionResult Details(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = this.service.Students.GetByUserName(username);

            if (student == null)
            {
                return HttpNotFound();
            }

            StudentDetailsEditModel model = Mapper.Map<Student, StudentDetailsEditModel>(student);
            return View(model);
        }

        //public ActionResult Details(Guid id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = this.service.Students.GetById(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    StudentDetailsEditModel model = Mapper.Map<Student, StudentDetailsEditModel>(student);
        //    return View(model);
        //}

        [HttpGet]
        public ActionResult Search(string name)
        {
            var foundStudents = this.service.Students
                .SearchByName(name)
                .Project()
                .To<StudentBasicViewModel>();

            return View(foundStudents.AsEnumerable());
        }

        public ActionResult Edit(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = this.service.Students.GetByUserName(username);
            if (student == null)
            {
                return HttpNotFound();
            }

            StudentDetailsEditModel model = Mapper.Map<Student, StudentDetailsEditModel>(student);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentDetailsEditModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Perform validation if the currently logged on user has rights to modify the entry.

                Student student = this.service.Students.GetById(model.Id);
                Mapper.Map<StudentDetailsEditModel, Student>(model, student);
                this.service.Students.Update(student);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        //public ActionResult Edit(Guid id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Student student = this.service.Students.GetById(id);
        //    if (student == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    StudentDetailsEditModel model = Mapper.Map<Student, StudentDetailsEditModel>(student);

        //    return View(model);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(StudentDetailsEditModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //TODO: Perform validation if the currently logged on user has rights to modify the entry
        //        Student student = this.service.Students.GetById(model.Id);
        //        Mapper.Map<StudentDetailsEditModel, Student>(model, student);
        //        this.service.Students.Update(student);
        //        return RedirectToAction("Index");
        //    }
        //    return View(model);
        //}

        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this.service.Students.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Student student = this.service.Students.GetById(id);
            this.service.Students.Delete(student);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.service.Students.Dispose();
            base.Dispose(disposing);
        }
    }
}
