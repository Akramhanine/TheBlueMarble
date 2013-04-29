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
using System.Diagnostics;
using System.Net;
using BlueMarble.Data.Shared_Objects;

namespace BlueMarble.Website.Controllers
{
	public class HomeController : Controller
	{
		private Uri ApiUri = new Uri("http://bigmarbleapi.azurewebsites.net");
		private Uri LittleApiUri = new Uri("http://littlemarbleapi.azurewebsites.net");
		private Uri LocalDebugUri = new Uri("http://localhost:62141");
		private Uri ServerUri;

		public HomeController()
		{
			// Change this to " = LocalDebugUri" if testing locally
			ServerUri = LittleApiUri;
		}

		// This is no longer needed since the API has been moved out of this project.
		//private Uri getServerUri()
		//{
		//	Uri serverUri = new Uri("http://bigmarbleapi.azurewebsites.net/"); // Default to azure site

		//	HttpRequest request = HttpContext.ApplicationInstance.Request;
		//	string uriStr = request.Url.AbsoluteUri.Replace(request.Url.AbsolutePath, String.Empty);
		//	if (Uri.IsWellFormedUriString(uriStr, UriKind.Absolute))
		//	{
		//		serverUri = new Uri(uriStr);
		//	}
		//	return serverUri;
		//}

		private void checkIfImageExists(ImageData image)
		{
			WebRequest request = WebRequest.Create(new Uri(image.Highresurl));
			request.Method = "HEAD";
			try
			{
				HttpWebResponse checkResponse = (HttpWebResponse)request.GetResponse();
			}
			catch (Exception)
			{
				// Doesn't exist
				image.Highresurl = image.Lowresurl;
			}
		}

		/// <summary>
		/// This page allows a user to type in an address
		/// </summary>
		/// <returns></returns>
		public ActionResult Index()
		{
			return View();
		}

		private IEnumerable<ImageData> _searchImagesData = new List<ImageData>();
		private IList<FullImageData> _fullImagesData = new List<FullImageData>();

		/// <summary>
		/// The address search page of the website
		/// </summary>
		/// <param name="Address">The address to search for</param>
		/// <param name="page">Used the pagination controls</param>
		/// <returns></returns>
		public ActionResult SearchImages(string Address, int? page)
		{
			// Call GET /api/image?address=Address

			// If page doesn't have a value then we need to query the database
			if (!page.HasValue || _searchImagesData == null)
			{
				var client = new HttpClient();
				client.BaseAddress = ServerUri;
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = client.GetAsync(string.Format("api/image?address={0}&range={1}", Address, "50")).Result;
				if (response.IsSuccessStatusCode)
				{
					_searchImagesData = response.Content.ReadAsAsync<IEnumerable<ImageData>>().Result;
					
					// TODO Instead of making two calls to the API, change the first call to return all data?
					foreach (ImageData data in _searchImagesData)
					{
						response = client.GetAsync("api/image?imageDataID=" + data.ImageDataID).Result; 
						if (response.IsSuccessStatusCode)
						{
							_fullImagesData.Add(response.Content.ReadAsAsync<FullImageData>().Result);
						}
					}
				}
			}

			// How many items to display per page.
			int pageSize = 20;
			int pageNumber = (page ?? 1);

			// Send the count of items.
			ViewBag.Count = _searchImagesData.Count();
			ViewBag.Address = Address;

			var pagedList = _fullImagesData.ToPagedList(pageNumber, pageSize);
			// TODO This check should be performed when we do the import, not at runtime
			//foreach (var pagedImage in pagedList)
			//{
			//	checkIfImageExists(pagedImage);
			//}
			return View(pagedList);
		}

		/// <summary>
		/// Default about page.
		/// </summary>
		/// <returns></returns>
		public ActionResult About()
		{
			ViewBag.Message = "About The Big Marble";

			return View();
		}

		/// <summary>
		/// Default contact page
		/// </summary>
		/// <returns></returns>
		public ActionResult Location()
		{
			ViewBag.Message = "Location Sprawl";
			IEnumerable<Locationdesc> locations = new List<Locationdesc>(); // Blank list in case the query fails

			// Query for all locations, display as links
			var client = new HttpClient();
			client.BaseAddress = ServerUri;
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			HttpResponseMessage response = client.GetAsync("api/location/").Result;  // Blocking call!
			if (response.IsSuccessStatusCode)
			{

				locations = response.Content.ReadAsAsync<IEnumerable<Locationdesc>>().Result;
				//foreach (var loc in locations)
				//{
				//    Debug.Print("{0}\t{1};\t", loc.LocationdescID, loc.Name);
				//}
			}

			return View(locations);
		}
	}
}
