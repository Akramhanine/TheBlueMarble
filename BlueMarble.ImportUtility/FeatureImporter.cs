using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BlueMarble.Data;

namespace BlueMarble.ImportUtility
{
    class FeatureImporter : ImporterBase
    {
        List<Featuredesc> features = new List<Featuredesc>();

        List<Locationdesc> _locationSource = new List<Locationdesc>();

        static int commitCount = 200;
        int currentCount = 0;

        public override void Init(MarbleDataBase database)
        {
            base.Init(database);

            var locations = from d in database.Locationdesc
                           select d;
            _locationSource = locations.ToList();
        }

        public override void  ProcessRecord(string[] tokens)
        {
            currentCount++;

            string locationdesc = tokens[9];
            string featuredesc = tokens[10];

            //this should throw if it doesn't find anything - should never happen
            int locationId = GetLocationID(locationdesc);

            if (!features.Exists(f => f.LocationID == locationId && f.Name == featuredesc))
            {
                Featuredesc feature = new Featuredesc
                {
                    LocationID = locationId,
                    Name = featuredesc
                };

                features.Add(feature);
                _database.Featuredesc.Add(feature);

                if (currentCount > commitCount)
                {
                    _database.SaveChanges();

                    _database.Dispose();
                    _database = new MarbleDataBase();
                    _database.Configuration.AutoDetectChangesEnabled = false;

                    var featurecheck = from f in _database.Featuredesc
                                         where f.FeaturedescID == feature.FeaturedescID
                                         select f;

                    foreach (Featuredesc fcheck in featurecheck)
                    {
                        Console.WriteLine("{0} {1} {2}", fcheck.FeaturedescID, fcheck.LocationID, fcheck.Name);
                    }

                    currentCount = 0;
                }
            }
        }

        int GetLocationID(string locationDesc)
        {
            int val = 0;

            Locationdesc data = _locationSource.Find(ls => ls.Name == locationDesc);

            if (data != null)
            {
                val = data.LocationdescID;
            }

            return val;
        }
    }
}

