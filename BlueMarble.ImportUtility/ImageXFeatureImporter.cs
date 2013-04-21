using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BlueMarble.Data;

namespace BlueMarble.ImportUtility
{
    class ImageXFeatureImporter : ImporterBase
    {
        Dictionary<string, int> _imageMap = new Dictionary<string, int>();
        Dictionary<string, int> _locationMap = new Dictionary<string, int>();
        Dictionary<string, int> _featureMap = new Dictionary<string, int>();

        static int commitCount = 200;
        int currentCount = 0;

        public override void Init(MarbleDataBase database)
        {
            base.Init(database);

            var locations = from d in database.Locationdesc
                            select d;
            foreach (Locationdesc location in locations)
            {
                _locationMap.Add(location.Name, location.LocationdescID);
            }

            var images = from d in database.Imagedata
                         select d;
            foreach (ImageData image in images)
            {
                _imageMap.Add(image.Lowresurl, image.ImageDataID);
            }

            var features = from d in database.Featuredesc
                           select d;
            foreach (Featuredesc feature in features)
            {
                _featureMap.Add(feature.LocationID.ToString() + feature.Name, feature.FeaturedescID);
            }
        }

        public override void ProcessRecord(string[] tokens)
        {
            currentCount++;

            string locationdesc = tokens[9];
            string featuredesc = tokens[10];
            string lowresurl = tokens[11];

            //this should throw if it doesn't find anything - should never happen
            int locationId = _locationMap[locationdesc];

            featuredesc = locationId.ToString() + featuredesc;
            int featureId = _featureMap[featuredesc];

            int imageId = _imageMap[lowresurl];
            _imageMap.Remove(lowresurl);

            Imagexfeature imagexfeature = new Imagexfeature
            {
                ImageDataID = imageId,
                FeaturedescID = featureId,
                Priority = 0
            };

            _database.Imagexfeature.Add(imagexfeature);

            if (currentCount > commitCount)
            {
                _database.SaveChanges();

                _database.Dispose();
                _database = new MarbleDataBase();
                _database.Configuration.AutoDetectChangesEnabled = false;

                Console.WriteLine("{0} {1} {2}", imagexfeature.ImagexfeatureID, imagexfeature.ImageDataID, imagexfeature.FeaturedescID);

                currentCount = 0;
            }
        }
    }
}
