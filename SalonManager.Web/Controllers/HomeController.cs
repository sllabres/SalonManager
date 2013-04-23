using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SalonManager.Web.Models;
using SalonManager.Commands;


namespace SalonManager.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLogin userLogin)
        {
            new LoginCommand().Execute();

            return RedirectToAction("Index");
        }
    }
}
