namespace School.Web.Areas.Administration.Controllers
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
    using School.Web.Areas.Administration.Models;
    
    [Authorize(Roles = GlobalConstants.SuperAdministratorRoleName)]
    public class AccountController : Controller
    {
        private readonly IAdministratorService administratorService;

        private ApplicationUserManager userManager;
        
        public AccountController(IAdministratorService administratorService)
        {
            this.administratorService = administratorService;
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
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(AdministratorRegisterSubmitModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.RegisterViewModel.UserName,
                    Email = model.RegisterViewModel.Email
                };

                IdentityResult result = await this.UserManager.CreateAsync(user, model.RegisterViewModel.Password);
                
                if (result.Succeeded)
                {
                    this.UserManager.AddToRole(user.Id, GlobalConstants.AdministratorRoleName);
                    
                    Administrator administrator = new Administrator();
                    administrator.ApplicationUserId = user.Id;
                    Mapper.Map<AdministratorRegisterSubmitModel, Administrator>(model, administrator);
                    
                    this.administratorService.Add(administrator);

                     /*For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                     Send an email with this link
                     string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                     var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                     await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");*/

                    return RedirectToAction("Index", "Administrators", new { area = "Administration" });
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