using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlueMarble.Data;
using GeoCoding;

namespace BlueMarble.Website.APIControllers
{
    public class ImageController : ApiController
    {
		MarbleDataBase _database;
		Double _defaultRange; // default range of miles used when searching for images using a street address

		public ImageController()
		{
			_database = new MarbleDataBase();
			_defaultRange = 100; //defaulting to 100 miles
		}

		public MarbleDataBase Database
		{
			get { return _database; }
		}

		/// <summary>
		/// Returns all image data for given image ID
		/// </summary>
		/// <param name="id">Image ID</param>
		/// <returns>ImageData object</returns>
        public ImageData GetImageByID(int id)
        {
			ImageData data = Database.Imagedata.Find(id);
			if (data == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
			return data;
        }

		/// <summary>
		/// Returns all image data for a given street address
		/// Uses the default range to search from a given latitude and longitude
		/// </summary>
		/// <param name="address">street address</param>
		/// <returns>ImageData object</returns>
		public IEnumerable<ImageData> GetImagesByAddress(string address)
		{
			Address addressConverted = GeocodingUtils.MicrosoftGeocodeAddress(address);
			CoordinateRange range = GeocodingUtils.GetLongLatRangeByDistance(addressConverted.Coordinates, _defaultRange);

			IEnumerable<ImageData> data = (from image in Database.Imagedata
										   where (image.Latitude > range.LatitudeMin && image.Longitude > range.LongitudeMin) &&
												 (image.Latitude < range.LatitudeMax && image.Longitude < range.LongitudeMax)
										   select image);
			return data;
		}

		//create another API method that takes in a range of miles

    }
}