using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BlueMarble.Data;

namespace BlueMarble.ImportUtility
{
    public abstract class ImporterBase
    {
        protected MarbleDataBase _database;

        public virtual void Init(MarbleDataBase database)
        {
            _database = database;
        }

        public virtual void ProcessRecord(string[] tokens)
        {

        }

        public void CloseUp()
        {
            _database.SaveChanges();
        }
    }
}
