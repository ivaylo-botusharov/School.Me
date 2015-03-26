namespace School.Web.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.Models;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

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
            var allStudents = this.studentService.All().Project().To<StudentListViewModel>();
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
                .To<StudentListViewModel>();

            return View(foundStudents.AsEnumerable());
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentDetailsEditModel model)
        {
            Student student = this.studentService.GetById(model.Id);

            bool isUserNameUnique = this.studentService.IsUserNameUniqueOnEdit(student, model.AccountDetailsEditModel.UserName);

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

        protected override void Dispose(bool disposing)
        {
            this.studentService.Dispose();
            base.Dispose(disposing);
        }
    }
}
