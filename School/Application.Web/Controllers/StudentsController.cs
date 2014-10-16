namespace Application.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Application.Models;
    using Application.Services;
    using Application.Web.Models;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    public class StudentsController : Controller
    {
        private readonly IStudentService service;

        public StudentsController(IStudentService service)
        {
            this.service = service;
        }

        // GET: Students
        public ActionResult Index()
        {
            var allStudents = this.service.All().Project().To<StudentBasicViewModel>();
            return View(allStudents.AsEnumerable());
        }

        // GET: Students/Details/5
        public ActionResult Details(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this.service.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            StudentDetailsEditModel model = Mapper.Map<Student, StudentDetailsEditModel>(student);
            return View(model);
        }

        [HttpGet]
        public ActionResult Search(string search)
        {
            var foundStudents = this.service
                .SearchByName(search)
                .Project()
                .To<StudentBasicViewModel>();

            return View(foundStudents.AsEnumerable());
        }

        // GET: Students/Edit/5
        public ActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this.service.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }

            StudentDetailsEditModel model = Mapper.Map<Student, StudentDetailsEditModel>(student);

            return View(model);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentDetailsEditModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: Perform validation if the currently logged on user has rights to modify the entry
                Student student = this.service.GetById(model.Id);
                Mapper.Map<StudentDetailsEditModel, Student>(model, student);
                this.service.Update(student);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = this.service.GetById(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Student student = this.service.GetById(id);
            this.service.Delete(student);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.service.Dispose();
            base.Dispose(disposing);
        }
    }
}
