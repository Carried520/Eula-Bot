using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Discord_Bot.Utils
{

    public class GuildPrefix
    {
        public ulong Id { get; set; }
        public string Prefix { get; set; }

    }


    public class UserPrefix
    {
        public ulong Id { get; set; }
        public string Prefix { get; set; }

    }




  public  class GetGuildPrefix
    {
        public async Task<string> GetPrefixAsync (ulong id)
        {
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var collection = database.GetCollection<GuildPrefix>("guildprefixes");
            var filter = Builders<GuildPrefix>.Filter.Eq("_id", id);
            var match = await collection.FindAsync(filter);
            var matched = await match.FirstOrDefaultAsync();
            string prefix;
            if (matched != null)
            {
                prefix = matched.Prefix;
            }
            else
            {
                prefix = null;
            }
            return prefix;
            
        }

        public async Task <string> GetUserPrefix (ulong id)
        {
            var client = new MongoClient(Config.Get("uri"));
            var database = client.GetDatabase("Csharp");
            var UserCollection = database.GetCollection<UserPrefix>("userprefixes");
            var Userfilter = Builders<UserPrefix>.Filter.Eq("_id", id);

            var UserMatch = await UserCollection.FindAsync(Userfilter);
            var UserMatched = await UserMatch.FirstOrDefaultAsync();
            string prefix;
            if (UserMatched != null)
            {
                prefix = UserMatched.Prefix;
            }
            else
            {
                prefix = null;
            }
           


            return prefix;
        }
    }
}
