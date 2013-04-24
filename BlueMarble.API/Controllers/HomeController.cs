using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlueMarble.API.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			// Redirect to the "Index" action in HelpController within the HelpPage "area"
			return RedirectToAction("Index", "Help", new { Area = "HelpPage" });
		}
	}
}
