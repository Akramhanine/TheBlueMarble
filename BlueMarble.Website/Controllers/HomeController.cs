using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueMarble.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // TODO - Possible random image whenever someone browses to this page.

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
