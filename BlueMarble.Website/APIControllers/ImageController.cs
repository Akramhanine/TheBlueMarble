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
		double _defaultRange; // default range of miles used when searching for images using a street address

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
		/// Uses the default range to search, with the street address as the center point.
		/// </summary>
		/// <param name="address">street address</param>
		/// <returns>ImageData object</returns>
		public IEnumerable<ImageData> GetImagesByAddress(string address)
		{
			Address addressConverted = GeocodingUtils.MicrosoftGeocodeAddress(address);
			CoordinateRange coords = GeocodingUtils.GetLongLatRangeByDistance(addressConverted.Coordinates, _defaultRange);

			IEnumerable<ImageData> data = (from image in Database.Imagedata
										   where (image.Latitude > coords.LatitudeMin && image.Longitude > coords.LongitudeMin) &&
												 (image.Latitude < coords.LatitudeMax && image.Longitude < coords.LongitudeMax)
										   select image);
			return data;
		}

		/// <summary>
		/// Returns all image data for a given street address
		/// Accepts a range of miles to search, with the street address as the center point.
		/// </summary>
		/// <param name="address">street address</param>
		/// <returns>ImageData object</returns>
		public IEnumerable<ImageData> GetImagesByAddress(string address, double range)
		{
			Address addressConverted = GeocodingUtils.MicrosoftGeocodeAddress(address);
			CoordinateRange coords = GeocodingUtils.GetLongLatRangeByDistance(addressConverted.Coordinates, range);

			IEnumerable<ImageData> data = (from image in Database.Imagedata
										   where (image.Latitude > coords.LatitudeMin && image.Longitude > coords.LongitudeMin) &&
												 (image.Latitude < coords.LatitudeMax && image.Longitude < coords.LongitudeMax)
										   select image);
			return data;
		}


    }
}