namespace School.Web.Areas.Teachers.Controllers
{
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
    using School.Web.Areas.Teachers.Models;
    using School.Web.Areas.Teachers.Models.AccountViewModels;

    [Authorize(Roles = GlobalConstants.TeacherRoleName)]
    public class AccountController : Controller
    {
        private readonly ITeacherService teacherService;

        private ApplicationSignInManager signInManager;

        private ApplicationUserManager userManager;
        
        public AccountController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
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
                return RedirectToAction("Index", "Home", new { area = string.Empty });
            }

            TeacherRegisterSubmitModel model = new TeacherRegisterSubmitModel();

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
        public async Task<ActionResult> Register(TeacherRegisterSubmitModel model)
        {
            if (ModelState.IsValid)
            {
                var uploadDirectory = GlobalConstants.TeachersProfileImagesUploadDirectory;

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
                    this.UserManager.AddToRole(user.Id, GlobalConstants.TeacherRoleName);

                    Teacher teacher = new Teacher();
                    teacher.ApplicationUserId = user.Id;
                    Mapper.Map<TeacherRegisterSubmitModel, Teacher>(model, teacher);
                    this.teacherService.Add(teacher);

                     /*For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                     Send an email with this link
                     string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                     var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                     await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");*/

                    //await this.SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Index", "Teachers", new { area = "Administration" });
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