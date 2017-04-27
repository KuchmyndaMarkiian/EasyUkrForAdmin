using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyUkr.DataAccessLayer.Contexts;
using EasyUkr.DataAccessLayer.Entities.User;
using EasyUkr.WebApi.MyCode;
using Microsoft.AspNet.Identity;

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
