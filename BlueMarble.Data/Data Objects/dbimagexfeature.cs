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
        int ImagexfeatureID { get; set; }
        //[Key, Column(Order = 1, TypeName = "int")]
        // [ForeignKey("ImageData")]
        int ImageDataID { get; set; }
        //[Key, Column(Order = 2, TypeName = "int")]
        //[ForeignKey("Featuredesc")]
        int FeaturedescID { get; set; }
        int Priority { get; set; }
    }
}
