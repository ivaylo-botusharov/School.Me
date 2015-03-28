namespace School.Web.Controllers
{
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About St. Mary School";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "St. Mary School Contact Details";

            return View();
        }
    }
}