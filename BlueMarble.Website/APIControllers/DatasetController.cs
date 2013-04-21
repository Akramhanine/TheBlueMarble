using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlueMarble.Data;

namespace BlueMarble.Website.APIControllers
{
    public class DatasetController : ApiController
    {
		MarbleDataBase _database;

		public DatasetController()
		{
			_database = new MarbleDataBase();
		}

		public MarbleDataBase Database
		{
			get { return _database; }
		}

		/// <summary>
		/// Returns the dataset for a given mission
		/// </summary>
		/// <param name="mission">mission</param>
		/// <returns>Dataset</returns>
		public Dataset GetDatasetByMission(string mission)
		{
			string missionUpper = mission.ToUpper();

			Dataset dataset = (from data in Database.Dataset
							  where data.Description == missionUpper
							  select data).First();

			return dataset;
		}

		/// <summary>
		/// Returns all images for a given mission
		/// </summary>
		/// <param name="mission">mission</param>
		/// <returns>IEnumerable list of ImageData</returns>
		public IEnumerable<ImageData> GetImagesByMission(string mission)
		{
			Dataset dataset = GetDatasetByMission(mission);

			//use list of image IDs to get a list of ImageData
			IEnumerable<ImageData> data = from image in Database.Imagedata
										  where image.DatasetID == dataset.DatasetID
										  select image;

			return data;
		}

		/// <summary>
		/// Returns a list of all rolls in a given mission
		/// </summary>
		/// <param name="mission">mission</param>
		/// <returns>IEnumerable list of roll IDs</returns>
		public IEnumerable<int> GetRollsByMission(string mission)
		{
			Dataset dataset = GetDatasetByMission(mission);

			IEnumerable<int> rolls =  from image in Database.Imagedata
										where image.DatasetID == dataset.DatasetID
										select image.Rollnum;

			return rolls;
		}


	}
}
