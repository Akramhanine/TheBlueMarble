using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMarble.Data
{
    class dbimagedata
    {
        int imageID { get; set; }
        int datasetID { get; set; }
        int latitude { get; set; }
        int longitude { get; set; }
        int filesize { get; set; }
        int width { get; set; }
        int height { get; set; }
        int cloudCoveragePercentage { get; set; }
        int rollnum { get; set; }
        int framenum { get; set; }
        string highresurl { get; set; }
        string lowresurl { get; set; }
    }
}
