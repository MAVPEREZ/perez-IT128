using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogDataLibrary.Database;

namespace BlogDataLibrary.Data
{
    internal class SqlData
    {
        private ISqlDataAccess _db;
        private const string connectionStringName = "SqlDb";

        public SqlData(ISqlDataAccess db) 
        {
            _db = db;
        }
    }
}
