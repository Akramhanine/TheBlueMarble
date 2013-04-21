using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlueMarble.Data;

namespace BlueMarble.Website.APIControllers
{
    public class AnimationController : ApiController
    {
		MarbleDataBase _database;

		public AnimationController()
		{
			_database = new MarbleDataBase();
		}

		public MarbleDataBase Database
		{
			get { return _database; }
		}

		/// <summary>
		/// Returns all image data for a given (film) roll number.  
		/// Ideally this will be called after searching for the missions to give the roll context.
		/// Otherwise typically used internally to bring back a generated animated gif via
		/// Some frame values were non-numeric and those frames will not be returned
		/// </summary>
		/// <param name="rollNum">roll number</param>
		/// <returns>IEnumerable list of ImageData</returns>
		public IEnumerable<ImageData> GetImageSeries(int rollNum)
		{
			IEnumerable<ImageData> data = from image in Database.Imagedata
											where (image.Rollnum == rollNum && image.Framenum >= 0)
											select image;
			return data;
		}

		/// <summary>
		/// Returns an animated gif by combining the low resolution images in the given list of image IDs
		/// </summary>
		/// <param name="rollNum">roll number</param>
		/// <returns>Gif</returns>
		//public GetAnimationLowRes()
		//{
				//call GetImageSeries
		//}

		/// <summary>
		/// Returns an animated gif by combining the high resolution images in the given list of image IDs
		/// </summary>
		/// <param name="rollNum">roll number</param>
		/// <returns>Gif</returns>
		//public GetAnimationHighRes()
		//{
				//call GetImageSeries
		//}
    }
}