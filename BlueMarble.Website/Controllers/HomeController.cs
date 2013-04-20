using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueMarble.Data;
using BlueMarble.Website.Models;

namespace BlueMarble.Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // TODO - Possible random image whenever someone browses to this page.
            return View();
        }

        public ActionResult SearchImages(string Address)
        {
            ImageData temp = new ImageData(){ Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-853.JPG", Latitude = 32, Longitude=-65 };
            ImageData temp2 = new ImageData() { Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-854.JPG", Latitude = 32, Longitude = -65 };
            ImageData temp3 = new ImageData() { Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/STS064/STS064-104-112.JPG", Latitude = 32, Longitude = -65 };

            SearchedImagesModel model = new SearchedImagesModel(new List<ImageData>(){temp, temp2, temp3});

            return View(model);
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
