using System.Web.Mvc;

namespace EasyUkr.WebApi.Controllers
{
    [Authorize(Roles = "AppAdmin")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
        [AllowAnonymous]
        public ActionResult RegistrationView()
        {
            ViewBag.Title = "Registration";
            return View();
        }
        [AllowAnonymous]
        public ActionResult LoginView()
        {
            ViewBag.Title = "Log in";
            return View();
        }
        
    }
}
