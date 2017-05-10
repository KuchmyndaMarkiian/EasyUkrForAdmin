using System.Web.Mvc;

namespace EasyUkr.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }

        public ActionResult RegistrationView()
        {
            ViewBag.Title = "Registration";
            return View();
        }

        public ActionResult LoginView()
        {
            ViewBag.Title = "Log in";
            return View();
        }
        
    }
}
