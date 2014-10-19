namespace Application.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Application.Models;
    using Application.Web.Models;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Application.Services.Interfaces;

    public class StudentsController : Controller
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        // GET: Students
        public ActionResult Index()
        {
            var allStudents = this.studentService.All().Project().To<StudentBasicViewModel>();
            return View(allStudents.AsEnumerable());
        }

        
        public ActionResult Details(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = this.studentService.GetByUserName(username);

            if (student == null)
            {
                return HttpNotFound();
            }

            StudentDetailsEditModel model = Mapper.Map<Student, StudentDetailsEditModel>(student);
            model.AccountDetailsEditModel = Mapper.Map<ApplicationUser, AccountDetailsEditModel>(student.ApplicationUser);

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
            var foundStudents = this.studentService
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

            Student student = this.studentService.GetByUserName(username);
            if (student == null)
            {
                return HttpNotFound();
            }

            StudentDetailsEditModel model = Mapper.Map<Student, StudentDetailsEditModel>(student);
            model.AccountDetailsEditModel = Mapper.Map<ApplicationUser, AccountDetailsEditModel>(student.ApplicationUser);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentDetailsEditModel model)
        {
            Student student = this.studentService.GetById(model.Id);

            bool isUserNameUnique = this.studentService.IsUserNameUnique(student, model.AccountDetailsEditModel.UserName);

            if (!isUserNameUnique)
            {
                this.ModelState.AddModelError("AccountDetailsEditModel.UserName", "Duplicate usernames are not allowed.");
            }

            if (ModelState.IsValid)
            {
                //TODO: Perform validation if the currently logged on user has rights to modify the entry.

                Mapper.Map<StudentDetailsEditModel, Student>(model, student);
                Mapper.Map<AccountDetailsEditModel, ApplicationUser>(model.AccountDetailsEditModel, student.ApplicationUser);
                this.studentService.Update(student);
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
            Student student = this.studentService.GetById(id);
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
            Student student = this.studentService.GetById(id);
            this.studentService.Delete(student);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.studentService.Dispose();
            base.Dispose(disposing);
        }
    }
}
