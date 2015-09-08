using System.Threading.Tasks;

namespace School.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using PagedList;
    using School.Common;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;
    using School.Web.Infrastructure;
    
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class StudentsController : Controller
    {
        private readonly IStudentService studentService;

        public StudentsController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        // GET: Administration/Students
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParam = string.IsNullOrEmpty(sortOrder) ? "username_desc" : string.Empty;
            ViewBag.NameSortParam = sortOrder == "name" ? "name_desc" : "name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IQueryable<Student> students = this.studentService.All();

            if (!string.IsNullOrEmpty(searchString))
            {
                students = students
                    .Where(s => s.ApplicationUser.UserName.Contains(searchString) || s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "username_desc":
                    students = students.OrderByDescending(s => s.ApplicationUser.UserName);
                    break;
                case "name":
                    students = students.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                default:
                    students = students.OrderBy(s => s.ApplicationUser.UserName);
                    break;
            }

            IQueryable<StudentListViewModel> sortedStudents = students.Project().To<StudentListViewModel>();

            int pageSize = 10;
            int pageIndex = page ?? 1;

            RedirectUrl redirectUrl = new RedirectUrl(this.ControllerContext, null);

            Session["redirectUrl"] = redirectUrl;

            return View(sortedStudents.ToPagedList(pageIndex, pageSize));
        }

        public async Task<ActionResult> Details(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = await this.studentService.GetByUserName(username);

            if (student == null)
            {
                return HttpNotFound();
            }

            StudentDetailsEditModel model = Mapper.Map<Student, StudentDetailsEditModel>(student);
            model.AccountDetailsEditModel = Mapper.Map<ApplicationUser, AccountDetailsEditModel>(
                student.ApplicationUser);

            return View(model);
        }

        public async Task<ActionResult> Edit(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError(string.Empty, "No user has been selected");
                return View();
            }

            Student student = await this.studentService.GetByUserName(username);

            if (student == null)
            {
                ModelState.AddModelError(string.Empty, "Such user does not exist");
                return View();
            }

            StudentDetailsEditModel studentModel = Mapper.Map<Student, StudentDetailsEditModel>(student);
            studentModel.AccountDetailsEditModel = Mapper.Map<ApplicationUser, AccountDetailsEditModel>(
                student.ApplicationUser);

            return View(studentModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentDetailsEditModel studentModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "User data has not been filled correctly. Please, re-enter");
                return View(studentModel);
            }

            Student student = this.studentService.GetById(studentModel.Id);

            if (student == null)
            {
                ModelState.AddModelError(string.Empty, "No such user exists");
                return View();
            }

            bool usernameUnique = this.studentService.IsUserNameUniqueOnEdit(
                student, 
                studentModel.AccountDetailsEditModel.UserName);

            bool emailUnique = this.studentService.IsEmailUniqueOnEdit(
                student,
                studentModel.AccountDetailsEditModel.Email);

            if (!usernameUnique)
            {
                ModelState.AddModelError("AccountDetailsEditModel.UserName", "Duplicate usernames are not allowed.");
                return View();
            }

            if (!emailUnique)
            {
                ModelState.AddModelError("AccountDetailsEditModel.Email", "Duplicate emails are not allowed.");
                return View();
            }

            Mapper.Map<StudentDetailsEditModel, Student>(studentModel, student);
            Mapper.Map<AccountDetailsEditModel, ApplicationUser>(
                studentModel.AccountDetailsEditModel, student.ApplicationUser);
            
            this.studentService.Update(student);

            RedirectUrl redirectUrl = Session["redirectUrl"] as RedirectUrl;

            redirectUrl = redirectUrl ?? new RedirectUrl();

            if (!string.IsNullOrEmpty(redirectUrl.RedirectControllerName))
            {
                return RedirectToAction(
                    redirectUrl.RedirectActionName, 
                    redirectUrl.RedirectControllerName, 
                    redirectUrl.RedirectParameters);
            }

            return RedirectToAction("Index", "Students");
        }

        public ActionResult Delete(int id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/

            Student student = this.studentService.GetById(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = this.studentService.GetById(id);

            student.DeletedBy = User.Identity.GetUserId();
            this.studentService.Delete(student);

            RedirectUrl redirectUrl = Session["redirectUrl"] as RedirectUrl;

            redirectUrl = redirectUrl ?? new RedirectUrl();

            if (!string.IsNullOrEmpty(redirectUrl.RedirectControllerName))
            {
                return RedirectToAction(
                    redirectUrl.RedirectActionName, 
                    redirectUrl.RedirectControllerName, 
                    redirectUrl.RedirectParameters);
            }

            return RedirectToAction("Index");
        }
    }
}