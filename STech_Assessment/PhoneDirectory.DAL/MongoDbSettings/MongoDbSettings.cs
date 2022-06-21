using PhoneDirectory.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneDirectory.DAL.MongoDbSettings
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
