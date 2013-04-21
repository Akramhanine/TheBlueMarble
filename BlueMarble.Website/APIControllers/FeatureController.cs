using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlueMarble.Data;

namespace BlueMarble.Website.APIControllers
{
    public class FeatureController : ApiController
    {
		MarbleDataBase _database;

		public FeatureController()
		{
			_database = new MarbleDataBase();
		}

		public MarbleDataBase Database
		{
			get { return _database; }
		}

		/// <summary>
		/// Returns all features
		/// </summary>
		/// <returns>IEnumerable list of Featuredesc</returns>
		public IEnumerable<Featuredesc> GetFeatures()
		{
			IEnumerable<Featuredesc> features = (from feature in Database.Featuredesc
												 select feature);

			return features;
		}

		/// <summary>
		/// Returns all image data for a given feature.
		/// Since the feature column of the data given is not delimited properly, this method will do a slower "like" search
		/// </summary>
		/// <param name="location">feature</param>
		/// <returns>IEnumerable list of ImageData.</returns>
		public IEnumerable<ImageData> GetImagesByFeature(string feature)
		{
			//TODO: make this a more efficient and more concise query instead of three queries

			string featureUpper = feature.ToUpper();

			//use feature name to get a list of feature IDs
			//featureID is unique, but multiple feature names may exist as one name in the featuredesc table
			//due to poorly constructed non-delimited data in the input dataset
			IEnumerable<int> featureIDs = from feat in Database.Featuredesc
										  where featureUpper.IndexOf(feat.Name) >= 0
										  select feat.FeaturedescID;

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
