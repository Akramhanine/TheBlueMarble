using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlueMarble.Data
{
    public class Imagexfeature
    {
        public int ImagexfeatureID { get; set; }
        public int ImageDataID { get; set; }
        public int FeaturedescID { get; set; }
        public int Priority { get; set; }
    }
}
