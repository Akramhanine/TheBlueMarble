using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BlueMarble.Data;

namespace BlueMarble.ImportUtility
{
    class LocationImporter : ImporterBase
    {
        List<Locationdesc> locations = new List<Locationdesc>();

        public override void ProcessRecord(string[] tokens)
        {
            string locationdesc = tokens[9];

            if (!locations.Exists(l => l.Name == locationdesc))
            {
                Locationdesc location = new Locationdesc
                {
                    Name = locationdesc
                };

                locations.Add(location);
                _database.Locationdesc.Add(location);
            }
        }
    }
}