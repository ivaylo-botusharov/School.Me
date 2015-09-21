using System;
using System.IO;
using School.Web.Areas.Students.Models.AccountViewModels;

namespace School.Web.Areas.Students.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using School.Common;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.App_Start.IdentityConfig;
    using School.Web.Areas.Students.Models;
                                                                                                                                
    [Authorize(Roles = GlobalConstants.StudentRoleName)]
    public class AccountController : Controller
    {
        private readonly IStudentService studentService;

        private ApplicationSignInManager signInManager;

        private ApplicationUserManager userManager;
        
        public AccountController(IStudentService studentService)
        {
            this.studentService = studentService;
        }
        
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return this.signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

            private set 
            { 
                this.signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (!User.IsInRole(GlobalConstants.AdministratorRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = string.Empty});                
            }

            StudentRegisterSubmitModel model = new StudentRegisterSubmitModel();

            model.RegisterViewModel = new RegisterViewModel
            {
                ImageUrl = GlobalConstants.DefaultProfileImageUrl
            };

            return View(model);
        }

        // POST: /Account/Register
        [HttpPost]
        [OverrideAuthorization]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(StudentRegisterSubmitModel model)
        {
            List<string> validImageTypes = new List<string>()
            {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
            };

            if (ModelState.IsValid)
            {
                if (model.RegisterViewModel.ImageUpload != null && 
                    !validImageTypes.Contains(model.RegisterViewModel.ImageUpload.ContentType))
                {
                    ModelState.AddModelError("", "Please choose either a GIF, JPG or PNG image.");
                    return View(model);
                }

                var uploadDirectory = GlobalConstants.StudentsProfileImagesUploadDirectory;

                model.UploadProfilePhoto(uploadDirectory);

                if (model.RegisterViewModel.ImageUpload == null ||
                model.RegisterViewModel.ImageUpload.ContentLength == 0)
                {
                    model.RegisterViewModel.ImageUrl = GlobalConstants.DefaultProfileImageUrl;
                }

                var user = new ApplicationUser()
                {
                    UserName = model.RegisterViewModel.UserName,
                    Email = model.RegisterViewModel.Email,
                    ImageUrl = model.RegisterViewModel.ImageUrl
                };

                IdentityResult result = await this.UserManager.CreateAsync(user, model.RegisterViewModel.Password);
                
                if (result.Succeeded)
                {
                    this.UserManager.AddToRole(user.Id, GlobalConstants.StudentRoleName);

                    Student student = new Student();
                    student.ApplicationUserId = user.Id;
                    Mapper.Map<StudentRegisterSubmitModel, Student>(model, student);

                    this.studentService.Add(student);

                    //await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Students", new { area = "Administration" });
                }
                else
                {
                    this.AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
 
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.userManager != null)
                {
                    this.userManager.Dispose();
                    this.userManager = null;
                }

                if (this.signInManager != null)
                {
                    this.signInManager.Dispose();
                    this.signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        // Helpers
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }
        }
    }
}