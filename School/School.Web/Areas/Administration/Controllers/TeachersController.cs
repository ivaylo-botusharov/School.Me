namespace School.Web.Areas.Administration.Controllers
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using PagedList;
    using School.Common;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;
    using School.Web.Areas.Teachers.Controllers;
    using System;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class TeachersController : Controller
    {
        private readonly ITeacherService teacherService;

        public TeachersController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        // GET: Administration/Students
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.UserNameSortParam = String.IsNullOrEmpty(sortOrder) ? "username_desc" : "";
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

            IQueryable<Teacher> teachers = this.teacherService.All();

            if (!String.IsNullOrEmpty(searchString))
            {
                teachers = teachers
                    .Where(s => s.ApplicationUser.UserName.Contains(searchString) || s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "username_desc":
                    teachers = teachers.OrderByDescending(t => t.ApplicationUser.UserName);
                    break;
                case "name":
                    teachers = teachers.OrderBy(t => t.Name);
                    break;
                case "name_desc":
                    teachers = teachers.OrderByDescending(t => t.Name);
                    break;
                default:
                    teachers = teachers.OrderBy(t => t.ApplicationUser.UserName);
                    break;
            }

            IQueryable<TeacherListViewModel> sortedTeachers = teachers.Project().To<TeacherListViewModel>();

            int pageSize = 10;
            int pageIndex = (page ?? 1);

            return View(sortedTeachers.ToPagedList(pageIndex, pageSize));
        }

        public ActionResult Details(string username)
        {
            if (username == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Teacher teacher = this.teacherService.GetByUserName(username);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            TeacherDetailsEditModel model = Mapper.Map<Teacher, TeacherDetailsEditModel>(teacher);
            model.AccountDetailsEditModel = Mapper.Map<ApplicationUser, AccountDetailsEditModel>(teacher.ApplicationUser);

            return View(model);
        }

        public ActionResult Edit(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                ModelState.AddModelError("", "No user has been selected");
                return View();
            }

            Teacher teacher = this.teacherService.GetByUserName(username);

            if (teacher == null)
            {
                ModelState.AddModelError("", "Such user does not exist");
                return View();
            }

            TeacherDetailsEditModel teacherModel = Mapper.Map<Teacher, TeacherDetailsEditModel>(teacher);
            teacherModel.AccountDetailsEditModel = Mapper.Map<ApplicationUser, AccountDetailsEditModel>(teacher.ApplicationUser);

            return View(teacherModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TeacherDetailsEditModel teacherModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "User data has not been filled correctly. Please, re-enter");
                return View(teacherModel);
            }

            Teacher teacher = this.teacherService.GetById(teacherModel.Id);

            if (teacher == null)
            {
                ModelState.AddModelError("", "No such user exists");
                return View();
            }

            bool isUserNameUnique = this.teacherService.IsUserNameUniqueOnEdit(
                teacher,
                teacherModel.AccountDetailsEditModel.UserName);

            if (!isUserNameUnique)
            {
                this.ModelState.AddModelError("AccountDetailsEditModel.UserName", "Duplicate usernames are not allowed.");
                return View();
            }

            Mapper.Map<TeacherDetailsEditModel, Teacher>(teacherModel, teacher);
            Mapper.Map<AccountDetailsEditModel, ApplicationUser>(teacherModel.AccountDetailsEditModel, teacher.ApplicationUser);
            this.teacherService.Update(teacher);

            return RedirectToAction("Index", "Teachers");
        }

        public ActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Teacher teacher = this.teacherService.GetById(id);

            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Teacher teacher = this.teacherService.GetById(id);

            if (teacher.ApplicationUserId == User.Identity.GetUserId())
            {
                teacher.DeletedBy = User.Identity.GetUserId();
                this.teacherService.Delete(teacher);

                var accountController = new School.Web.Areas.Teachers.Controllers.AccountController(this.teacherService);
                accountController.ControllerContext = this.ControllerContext;
                accountController.LogOff();

                return RedirectToAction("Index", "Home");
            }

            teacher.DeletedBy = User.Identity.GetUserId();
            this.teacherService.Delete(teacher);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            this.teacherService.Dispose();
            base.Dispose(disposing);
        }
    }
}