using System.Web.Mvc;
using System.Web.UI.WebControls;
using EasyUkr.WebApi.Infrastructure.SessionManager;

namespace EasyUkr.WebApi.Controllers
{
    [Authorize(Roles = "AppAdmin")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return AuthenticationMethods.RedirectToView(RedirectToAction("AdminPage","Admin"),RedirectToAction("LoginView"));
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
