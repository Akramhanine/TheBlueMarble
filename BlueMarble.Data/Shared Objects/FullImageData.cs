using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueMarble.Data.Shared_Objects
{
    public class FullImageData
    {
        public Dataset Dataset { get; set; }
        public ImageData ImageData { get; set; }
        public Featuredesc Featuredesc { get; set; }
        public Locationdesc Locationdesc { get; set; }
    }
}
