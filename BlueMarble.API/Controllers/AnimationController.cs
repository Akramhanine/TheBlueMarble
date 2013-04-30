using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Windows.Media.Imaging;
using BlueMarble.Data;
using BlueMarble.GifCreator;
using System.IO;
using System.Web;

namespace BlueMarble.Website.APIControllers
{
    public class AnimationController : ApiController
    {
		MarbleDataBase _database;
		int _gifImageRange;

		public AnimationController()
		{
			_database = new MarbleDataBase();
			_gifImageRange = 10; //defaulting to 10
		}

		public MarbleDataBase Database
		{
			get { return _database; }
		}

		/// <summary>
		/// Returns all image data for a given (film) roll number.  
		/// Ideally this will be called after searching for the missions to give the roll context.
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
		/// Generates and returns a link to an animated gif by combining the low resolution images.
		/// The list of images is generated from a single image ID by combining images from the same roll.
		/// Returns an empty string if gif generation fails.
        /// NOTE - this method is incomplete
		/// </summary>
		/// <param name="rollNum">imageID</param>
		/// <returns>url to animated gif hosted on the server</returns>
        //public string GetAnimationLowRes(int imageID)
        //{
        //    string url = "";

        //    //get imagedata for imageID
        //    ImageController imageController = new ImageController();
        //    ImageData image = imageController.GetImageByID(imageID);

        //    //get a series of images based on the start image and range
        //    IEnumerable<ImageData> images = GetImageSeries(image, _gifImageRange);

        //    //pass images into gif animation generator
        //    //url = GenerateGifFromImageSeries(images);

        //    return url;
        //}

		/// <summary>
		/// Returns an animated gif by combining the high resolution images in the given list of image IDs
		/// NOTE: some images do not have high resolution versions.
		/// </summary>
		/// <param name="rollNum">roll number</param>
		/// <returns>Gif</returns>
		//public GetAnimationHighRes()
		//{
		//call GetImageSeries
		//}

		#region private helper methods

		/// <summary>
		/// Returns a list of images before and after the given image based on the passed in range.
		/// Images returned are from the same dataset and roll as the base image.
		/// </summary>
		/// <param name="imageStart">starting image</param>
		/// <param name="range">range of images to create series</param>
		/// <returns></returns>
		private IEnumerable<ImageData> GetImageSeries(ImageData imageStart, int range)
		{
			//use dataset ID and roll to query imagedata for additional images in a series
			IEnumerable<ImageData> data = from image in Database.Imagedata
										  where (image.DatasetID == imageStart.DatasetID &&
												image.Rollnum == imageStart.Rollnum && 
												image.Framenum >= 0 &&
												image.ImageDataID >= (imageStart.ImageDataID - range) &&
												image.ImageDataID <= (imageStart.ImageDataID + range))
										  select image;

			return data;
		}

		//THIS METHOD IS INCOMPLETE
			//needs to save from the GifBitmapEncoder to the server in some way
		///// <summary>
		///// Returns a url path to a gif that is generated and then stored on the server.
		///// </summary>
		///// <param name="images">IEnumerable list of ImageData</param>
		///// <returns>string url</returns>
		//private string GenerateGifFromImageSeries(IEnumerable<ImageData> images)
		//{
		//	string url = HttpContext.Current.Server.MapPath("~/app_data/test");
		//	GifMaker maker = new GifMaker();

		//	GifBitmapEncoder encoder = maker.Create(images);
		//	StreamWriter writer = new StreamWriter(url, true);
			
		//	encoder.Save(writer.BaseStream);
		//	writer.Flush();
		//	writer.Close(); // Close the instance of StreamWriter.
		//	writer.Dispose(); // Dispose from memory.   

		//	return url;
		//}
		#endregion
    }
}