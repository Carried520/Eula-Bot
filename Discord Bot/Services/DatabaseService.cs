using MongoDB.Driver;
using System;

namespace Discord_Bot.Services
{
    public  class DatabaseService
    {
        public MongoClient Mongo()
        {
            var mongo = new MongoClient(Config.Get("uri"));
            return mongo;

        }
        public IMongoDatabase GetDatabase(string DatabaseName)
        {
            var client = Mongo();
            var database = client.GetDatabase(DatabaseName);
            return database;
        }
        
    }
}
