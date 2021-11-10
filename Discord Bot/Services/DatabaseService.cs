using MongoDB.Driver;

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
