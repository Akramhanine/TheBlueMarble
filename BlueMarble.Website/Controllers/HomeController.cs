using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlueMarble.Data;
using BlueMarble.Website.Models;
using PagedList;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

namespace BlueMarble.Website.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// The homepage of the website
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            // TODO - Possible random image whenever someone browses to this page.
            return View();
        }

        private IEnumerable<ImageData> _searchImagesData;

        /// <summary>
        /// The search page of the website
        /// </summary>
        /// <param name="Address">The address to search for</param>
        /// <param name="page">Used the pagination controls</param>
        /// <returns></returns>
        public ActionResult SearchImages(string Address, int? page)
        {
            //ImageData temp = new ImageData(){ Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-853.JPG", Latitude = 32, Longitude=-65 };
            //ImageData temp2 = new ImageData() { Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/AS06/AS06-2-854.JPG", Latitude = 32, Longitude = -65 };
            //ImageData temp3 = new ImageData() { Lowresurl = "http://eol.jsc.nasa.gov/sseop/images/ISD/lowres/STS064/STS064-104-112.JPG", Latitude = 32, Longitude = -65 };
            //IList<ImageData> data = new List<ImageData>() { temp, temp2, temp3 };

            // TODO call GET /api/image?address=Address
            //IEnumerable<ImageData> images = new List<ImageData>();

            //var client = new HttpClient(new HttpServer(GlobalConfiguration.Configuration));
            // If page doesn't have a value then we need to query the database
            if (!page.HasValue || _searchImagesData == null)
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:2245");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/image?address=" + Address).Result;  // Blocking call!
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. Blocking!
                    _searchImagesData = response.Content.ReadAsAsync<IEnumerable<ImageData>>().Result;
                    //foreach (var image in images)
                    //{
                    //    Console.WriteLine("{0}\t{1};\t", image.ImageDataID, image.Lowresurl);
                    //}
                } 
            }

            // How many items to display per page.
            int pageSize = 30;
            int pageNumber = (page ?? 1);

            // Send the count of items.
            ViewBag.Count = _searchImagesData.Count();
            ViewBag.Address = Address;

            return View(_searchImagesData.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Default about page.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        /// <summary>
        /// Default contact page
        /// </summary>
        /// <returns></returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
