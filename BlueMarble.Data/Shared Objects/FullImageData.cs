using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueMarble.Data.Shared_Objects
{
	// We can't derive from ImageData; if we do, Entity will detect this class and think that the schema changed
	public class FullImageData //: ImageData
	{
		public Dataset Dataset { get; set; }
		public Featuredesc Featuredesc { get; set; }
		public Locationdesc Locationdesc { get; set; }
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

		public FullImageData()
		{

		}
	}
}
