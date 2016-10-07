using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ParichayLite.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public ViewResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public ViewResult Error()
        {
            return View();
        }
    }
}
