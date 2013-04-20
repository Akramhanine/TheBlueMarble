using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMarble.Data
{
    public class ImageData
    {
        public int ImageDataID { get; set; }
        public int DatasetID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Filesize { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int CloudCoveragePercentage { get; set; }
        public int Rollnum { get; set; }
        public int Framenum { get; set; }
        public string Highresurl { get; set; }
        public string Lowresurl { get; set; }
    }
}
