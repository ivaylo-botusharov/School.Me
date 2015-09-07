namespace School.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Microsoft.AspNet.Identity;
    using School.Common;
    using School.Models;
    using School.Services.Interfaces;
    using School.Web.Areas.Administration.Models;

    [Authorize(Roles = GlobalConstants.SuperAdministratorRoleName)]
    public class AdministratorsController : Controller
    {
        private readonly IAdministratorService administratorService;

        public AdministratorsController(IAdministratorService administratorService)
        {
            this.administratorService = administratorService;
        }

        public ActionResult Index()
        {
            IQueryable<AdministratorListViewModel> administrators = 
                this.administratorService.All().Project().To<AdministratorListViewModel>();
            return View(administrators);
        }

        public ActionResult Details(string username)
        {
            Administrator administrator = 
                this.administratorService.All().FirstOrDefault(a => a.ApplicationUser.UserName == username);

            if (administrator == null)
            {
                ModelState.AddModelError(string.Empty, "No such user exists.");
                return RedirectToAction("Index");
            }

            AdministratorDetailsEditModel adminModel = 
                Mapper.Map<Administrator, AdministratorDetailsEditModel>(administrator);
            return View(adminModel);
        }

        public ActionResult Edit(string username)
        {
            Administrator administrator = 
                this.administratorService.All().FirstOrDefault(a => a.ApplicationUser.UserName == username);
            
            AdministratorDetailsEditModel adminModel = 
                Mapper.Map<Administrator, AdministratorDetailsEditModel>(administrator);

            return View(adminModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdministratorDetailsEditModel adminModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "User data has not been filled correctly. Please, re-enter");

                return View(adminModel);
            }

            Administrator administrator = this.administratorService.GetById(adminModel.Id);

            if (administrator == null)
            {
                ModelState.AddModelError(string.Empty, "No such user exists.");

                return View();
            }

            bool usernameUnique = this.administratorService.IsUserNameUniqueOnEdit(
               administrator,
               adminModel.AccountDetailsEditModel.UserName);

            if (!usernameUnique)
            {
                ModelState.AddModelError("AccountDetailsEditModel.UserName", "Duplicate usernames are not allowed.");
                return View();
            }

            Mapper.Map<AdministratorDetailsEditModel, Administrator>(adminModel, administrator);

            this.administratorService.Update(administrator);
            
            return RedirectToAction("Index", "Administrators");
        }
        
        public ActionResult Delete(string username)
        {
            Administrator administrator = 
                this.administratorService.All().FirstOrDefault(a => a.ApplicationUser.UserName == username);

            AdministratorDeleteSubmitModel adminModel = 
                Mapper.Map<Administrator, AdministratorDeleteSubmitModel>(administrator);

            adminModel.DeletePermanent = true;

            return View(adminModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(AdministratorDeleteSubmitModel model)
        {
            Administrator administrator = 
                this.administratorService.GetById(model.Id);

            if (administrator.ApplicationUser.Email == "superadmin@superadmin.com")
            {
                ModelState.AddModelError(string.Empty, "Superadmin cannot delete herself / himself.");
                return View();
            }

            if (model.DeletePermanent)
            {
                this.administratorService.HardDelete(administrator);
                return RedirectToAction("Index");
            }

            administrator.DeletedBy = User.Identity.GetUserId();
            this.administratorService.Delete(administrator);

            return RedirectToAction("Index");
        }
    }
}