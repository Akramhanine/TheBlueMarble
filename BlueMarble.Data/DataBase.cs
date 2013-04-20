using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BlueMarble.Data
{
    public class MarbleDataBase : DbContext
    {
        public DbSet<Dataset> Dataset { get; set; }
        public DbSet<Featuredesc> Featuredesc { get; set; }
        public DbSet<Imagedata> Imagedata { get; set; }
        public DbSet<Imagexfeature> Imagexfeature { get; set; }
        public DbSet<Locationdesc> Locationdesc { get; set; }
    }
}
