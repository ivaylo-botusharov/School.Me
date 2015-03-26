namespace School.Web.Areas.Administration.Controllers
{
    using School.Common;
    using System.Web.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class HomeController : Controller
    {
        // GET: Administration/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}