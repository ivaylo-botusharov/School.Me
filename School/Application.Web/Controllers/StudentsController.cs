namespace Application.Web.Controllers
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Application.Models;
    using Application.Services;
    using Application.Web.Models;
    using AutoMapper.QueryableExtensions;
    using AutoMapper;

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
        public ActionResult Details(int? id)
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

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentDetailsEditModel model)
        {
            if (ModelState.IsValid)
            {
                var student = Mapper.Map<StudentDetailsEditModel, Student>(model);
                this.service.Add(student);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
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
