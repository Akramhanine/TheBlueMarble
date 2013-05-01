using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueMarble.Data;
using System.Data.Entity;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BlueMarble.Geocoding;
using BlueMarble.Geocoding.DataTransferObjects;

namespace BlueMarble.TestHarness
{
	class Program
	{
		static void Main(string[] args)
		{
			// This can be used to force the re-creation of the database
			//Database.SetInitializer<MarbleDataBase>(new DropCreateDatabaseAlways<MarbleDataBase>());

			/*MarbleDataBase db = new MarbleDataBase();
			db.Dataset.Add(new Dataset() { Description = "aljdsfjlaksdf" });
			db.SaveChanges();*/

			/*Dataset set = db.Dataset.Find(1);
			if (set != null)
			{
				Console.Write("Description is: " + set.Description);
			}*/

			// Geocoding test
			//CoordinateRange range = GeocodingUtils.BingGeocodeAddress("cleveland, ohio", 60);
		}
	}
}
