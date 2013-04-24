using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BlueMarble.Data;

namespace BlueMarble.ImportUtility
{
    class ImageDataImporter : ImporterBase
    {
        List<Dataset> _dataSetList = new List<Dataset>();

        static int commitCount = 200;
        int currentCount = 0;

        public override void Init(MarbleDataBase database)
        {
 	        base.Init(database);

            var missions = from d in database.Dataset
                           select d;
            _dataSetList = missions.ToList();
        }

        public override void ProcessRecord(string[] tokens)
        {
            currentCount++;

            ImageData imageData = null;

            try
            {
                imageData = new ImageData
                {
                    //Rollnum = int.Parse(tokens[1]),
                    //framenum
                    Width = int.Parse(tokens[3]),
                    Height = int.Parse(tokens[4]),
                    Filesize = int.Parse(tokens[5]),
                    //CloudCoveragePercentage = int.Parse(tokens[6]),
                    Latitude = double.Parse(tokens[7]),
                    Longitude = double.Parse(tokens[8]),
                    //tokens[9]
                    //tokens[10]
                    Lowresurl = tokens[11]
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            imageData.DatasetID = GetDataSourceID(tokens[0]);

            int framenum = -1;
            int.TryParse(tokens[2], out framenum);
            imageData.Framenum = framenum;

            int rollnum = 0;
            int.TryParse(tokens[1], out rollnum);
            imageData.Rollnum = rollnum;

            int cloudCoverage = 0;
            int.TryParse(tokens[6], out cloudCoverage);
            imageData.CloudCoveragePercentage = cloudCoverage;
            imageData.Highresurl = imageData.Lowresurl.Replace("lowres", "highres");

            _database.Imagedata.Add(imageData);

            if (currentCount > commitCount)
            {
                _database.SaveChanges();

                //_database.Dispose();
                //_database = new MarbleDataBase();
                _database.Configuration.AutoDetectChangesEnabled = false;

                var imagedatacheck = from d in _database.Imagedata
                    where d.ImageDataID == imageData.ImageDataID
                    select d;

                foreach (ImageData id in imagedatacheck)
                {
                    Console.WriteLine("{0} {1} {2} {3}", id.ImageDataID, id.Latitude, id.Longitude, id.Lowresurl);
                }

                currentCount = 0;
            }
        }

        int GetDataSourceID(string missionDesc)
        {
            int val = 0;

            Dataset data = _dataSetList.Find(ds => ds.Description == missionDesc);

            if (data != null)
            {
                val = data.DatasetID;
            }

            return val;
        }
    }
}
