using BlueMarble.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BlueMarble.ImportUtility
{
	public static class UriChecker
	{
		public static void FixInvalidHighResUri()
		{
			MarbleDataBase db = new MarbleDataBase();
			var allImages = from image in db.Imagedata select image;

			foreach (ImageData iData in allImages)
			{
				if (!doesUriExist(iData.Highresurl))
				{
					Console.WriteLine("HighRes url doesn't exist: " + iData.Highresurl);
					iData.Highresurl = iData.Lowresurl;
				}
			}

			db.SaveChanges();
			Console.WriteLine("Saved changes from UriChecker");
		}

		private static bool doesUriExist(string uri)
		{
			bool exists = true;
			WebRequest request = WebRequest.Create(uri);
			request.Timeout = 2000;
			request.Method = "HEAD";
			try
			{
				HttpWebResponse checkResponse = (HttpWebResponse)request.GetResponse();
				checkResponse.Close();
			}
			catch (Exception)
			{
				exists = false;
			}

			return exists;
		}
	}
}
