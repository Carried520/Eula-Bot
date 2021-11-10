using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discord_Bot.Utils
{

    public class GuildPrefix
    {
        public ulong Id { get; set; }
        public  List<string> Prefixes { get; set; }

    }


    public class UserPrefix
    {
        public ulong Id { get; set; }
        public List<string> Prefixes { get; set; }

    }




  public  class GetGuildPrefix
    {
        public async Task<List<string>> GetPrefixAsync (ulong id)
        {
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var collection = database.GetCollection<GuildPrefix>("guildprefixes");
            var filter = Builders<GuildPrefix>.Filter.Eq("_id", id);
            var match = await collection.FindAsync(filter);
            var matched = await match.FirstOrDefaultAsync();
            List<string> prefix;
            if (matched != null)
            {
                prefix = matched.Prefixes;
            }
            else
            {
                prefix = null;
            }
            return prefix;
            
        }

        public async Task <List<string>> GetUserPrefix (ulong id)
        {
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var UserCollection = database.GetCollection<UserPrefix>("userprefixes");
            var Userfilter = Builders<UserPrefix>.Filter.Eq("_id", id);

            var UserMatch = await UserCollection.FindAsync(Userfilter);
            var UserMatched = await UserMatch.FirstOrDefaultAsync();
            List<string> prefix;
            if (UserMatched != null)
            {
                prefix = UserMatched.Prefixes;
            }
            else
            {
                prefix = null;
            }
           


            return prefix;
        }
    }
}
