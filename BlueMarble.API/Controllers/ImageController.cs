using BlueMarble.Data;
using BlueMarble.Data.Shared_Objects;
using BlueMarble.Geocoding;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace BlueMarble.Website.APIControllers
{
	/// <summary>
	/// API Controller for all image operations
	/// </summary>
	public class ImageController : ApiController
	{
		static MarbleDataBase _database;
		double _defaultRange; // default range of miles used when searching for images using a street address

		/// <summary>
		/// Constructor for ImageController
		/// </summary>
		public ImageController()
		{
			_database = new MarbleDataBase();
			_defaultRange = 100; //defaulting to 100 miles
		}

		private MarbleDataBase Database
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
		/// <returns>IEnumerable list of ImageData</returns>
		public IEnumerable<ImageData> GetImagesByAddress(string address)
		{
			CoordinateRange coords = GeocodingUtils.BingGeocodeAddress(address, _defaultRange);

			IEnumerable<ImageData> data = from image in Database.Imagedata
										   where (image.Latitude > coords.LatitudeMin && image.Longitude > coords.LongitudeMin) &&
												 (image.Latitude < coords.LatitudeMax && image.Longitude < coords.LongitudeMax)
										   select image;
			return data;
		}

		/// <summary>
		/// Returns all image data for a given street address
		/// Accepts a range of miles to search, with the street address as the center point.
		/// </summary>
		/// <param name="address">street address</param>
		/// <param name="range">range in miles</param>
		/// <returns>IEnumerable list of ImageData.</returns>
		public IEnumerable<ImageData> GetImagesByAddress(string address, double range)
		{
			CoordinateRange coords = GeocodingUtils.BingGeocodeAddress(address, range);

			IEnumerable<ImageData> data = from image in Database.Imagedata
										   where (image.Latitude > coords.LatitudeMin && image.Longitude > coords.LongitudeMin) &&
												 (image.Latitude < coords.LatitudeMax && image.Longitude < coords.LongitudeMax)
										   select image;
			return data;
		}

		/// <summary>
		/// Returns the full image data for a given image id.
		/// Returns additional information such as mission name, type, features, and region.
		/// </summary>
		/// <param name="imageDataID">Image ID</param>
		/// <returns>IEnumerable list of FullImageData</returns>
		public FullImageData GetFullImageData(int imageDataID)
		{
			//TODO: make this method more efficient

			ImageData imageData = GetImageByID(imageDataID);
			Dataset dataSet = Database.Dataset.Find(imageData.DatasetID);
			Imagexfeature imagexFeature = Database.Imagexfeature.Find(imageData.ImageDataID);
			Featuredesc featureDesc = Database.Featuredesc.Find(imagexFeature.FeaturedescID);
			Locationdesc locationDesc = Database.Locationdesc.Find(featureDesc.LocationID);

			return new FullImageData(imageData)
			{
				Dataset = dataSet,
				Featuredesc = featureDesc,
				Locationdesc = locationDesc
			};
		}
	}
}