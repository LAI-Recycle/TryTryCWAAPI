using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20240300TryCWAAPI.Models
{
    public class MongoDBSettings
    {
        public string ConnectionURL { get; set; } = null;
        public string DatabaseName { get; set; } = null;
        public string CollectionName { get; set; } = null;


    }
}