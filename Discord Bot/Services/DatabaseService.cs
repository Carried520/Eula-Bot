using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Services
{
  public  class DatabaseService
    {
        public MongoClient Mongo()
        {
            var mongo = new MongoClient(Config.Get("uri"));
            return mongo;

        }
    }
}
