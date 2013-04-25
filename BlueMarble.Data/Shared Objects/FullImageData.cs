using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueMarble.Data.Shared_Objects
{
    public class FullImageData : ImageData
    {
        public Dataset Dataset { get; set; }
        public Featuredesc Featuredesc { get; set; }
        public Locationdesc Locationdesc { get; set; }

        public FullImageData(ImageData data)
        {
            this.CloudCoveragePercentage = data.CloudCoveragePercentage;
            this.DatasetID = data.DatasetID;
            this.Filesize = data.Filesize;
            this.Framenum = data.Framenum;
            this.Height = data.Height;
            this.Highresurl = data.Highresurl;
            this.ImageDataID = data.ImageDataID;
            this.Latitude = data.Latitude;
            this.Longitude = data.Longitude;
            this.Lowresurl = data.Lowresurl;
            this.Rollnum = data.Rollnum;
            this.Width = data.Width;
        }
    }
}
