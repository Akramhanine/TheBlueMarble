using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlueMarble.Data;

namespace BlueMarble.Website.APIControllers
{
    public class ImageController : ApiController
    {
		MarbleDataBase _database;

		public ImageController()
		{
			_database = new MarbleDataBase();
		}

		public MarbleDataBase Database
		{
			get { return _database; }
		}

        public ImageData GetImageByID(int id)
        {
			ImageData data = Database.Imagedata.Find(id);
			if (data == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
			return data;
        }
    }
}