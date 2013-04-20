using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BlueMarble.Data
{
    class DataBase : DbContext
    {
        public DbSet<dbdataset> dbdataset { get; set; }
        public DbSet<dbfeaturedesc> dbfeaturedesc { get; set; }
        public DbSet<dbimagedata> dbimagedata { get; set; }
        public DbSet<dbimagexfeature> dbimagexfeature { get; set; }
        public DbSet<dblocationdesc> dblocationdesc { get; set; }
    }
}
