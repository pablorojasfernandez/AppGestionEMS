using AppGestionEMS.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppGestionEMS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _application;

        public HomeController()
        {
            _application = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MisDatos()
        {
            string currentUserId = User.Identity.GetUserId();
            return View(_application.Users.Where(u =>  currentUserId == u.Id).ToList());
        }
    }
}