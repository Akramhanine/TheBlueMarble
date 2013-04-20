using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlueMarble.Data;

namespace BlueMarble.Website.APIControllers
{
    public class LocationController : ApiController
    {
		MarbleDataBase _database;

		public LocationController()
		{
			_database = new MarbleDataBase();
		}

		public MarbleDataBase Database
		{
			get { return _database; }
		}

		/// <summary>
		/// Returns all locations
		/// </summary>
		/// <returns>IEnumerable list of Locationdesc</returns>
		public IEnumerable<Locationdesc> GetLocations()
		{
			IEnumerable<Locationdesc> locations = from location in Database.Locationdesc
												  select location;

			return locations;
		}

		/// <summary>
		/// Returns all image data for a given location
		/// </summary>
		/// <param name="location">location</param>
		/// <returns>IEnumerable list of ImageData.</returns>
		public IEnumerable<ImageData> GetImagesByLocation(string location)
		{
			//TODO: make this a more efficient and more concise query instead of four queries
			string locationUpper = location.ToUpper();
			//use location name to get location ID
			int locationID = (from loc in Database.Locationdesc
							  where loc.Name == locationUpper
							  select loc.LocationdescID).First();

			//use location ID to get a list of feature IDs
			IEnumerable<int> featureIDs = from feature in Database.Featuredesc
										  where feature.LocationID == locationID
										  select feature.FeaturedescID;

			//use list of feature IDs to get a list of image IDs
			IEnumerable<int> imageIDs = from imagexfeature in Database.Imagexfeature
										where featureIDs.Contains((int)imagexfeature.FeaturedescID)
										select imagexfeature.ImageDataID;
			
			//use list of image IDs to get a list of ImageData
			IEnumerable<ImageData> data = from image in Database.Imagedata
										   where imageIDs.Contains((int)image.ImageDataID)
										   select image;

			return data;
		}
    }
}
